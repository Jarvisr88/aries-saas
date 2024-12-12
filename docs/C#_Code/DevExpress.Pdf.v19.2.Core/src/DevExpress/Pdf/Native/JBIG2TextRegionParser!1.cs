namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class JBIG2TextRegionParser<T> : IJBIG2TextRegionParser where T: IJBIG2TextRegionDecoder
    {
        private readonly bool sbrefine;
        private readonly int logsbstrips;
        private readonly JBIG2Corner refcorner;
        private readonly bool transposed;
        private readonly int sbcombop;
        private readonly bool sbdefpixel;
        private readonly int sbdsoffset;
        private readonly int sbnuminstances;
        private readonly JBIG2RegionSegmentInfo regionInfo;

        protected JBIG2TextRegionParser(JBIG2StreamHelper streamHelper, JBIG2RegionSegmentInfo regionInfo, int flags, IList<JBIG2SymbolDictionary> sdReferred)
        {
            this.<Glyphs>k__BackingField = new List<JBIG2Image>();
            this.<StreamHelper>k__BackingField = streamHelper;
            this.regionInfo = regionInfo;
            if ((flags & 1) != 0)
            {
                this.<HuffmanFlags>k__BackingField = streamHelper.ReadInt16();
            }
            this.sbrefine = (flags & 2) != 0;
            this.logsbstrips = (flags & 12) >> 2;
            this.<Sbstrips>k__BackingField = 1 << (this.logsbstrips & 0x1f);
            this.refcorner = (JBIG2Corner) ((flags & 0x30) >> 4);
            this.transposed = (flags & 0x40) != 0;
            this.sbcombop = (flags & 0x180) >> 7;
            this.sbdefpixel = (flags & 0x200) != 0;
            this.sbdsoffset = (flags & 0x7c00) >> 10;
            if (this.sbdsoffset > 15)
            {
                this.sbdsoffset -= 0x20;
            }
            this.<Sbrtemplate>k__BackingField = (flags & 0x8000) >> 15;
            if (this.sbrefine && (this.Sbrtemplate == 0))
            {
                this.<Sbrat>k__BackingField = streamHelper.ReadAdaptiveTemplate(4);
            }
            this.sbnuminstances = streamHelper.ReadInt32();
            foreach (JBIG2SymbolDictionary dictionary in sdReferred)
            {
                this.Glyphs.AddRange(dictionary.Glyphs);
            }
        }

        protected JBIG2TextRegionParser(int refaggninst, int sdrtemplate, int[] at, List<JBIG2Image> glyphs, JBIG2RegionSegmentInfo regionInfo)
        {
            this.<Glyphs>k__BackingField = new List<JBIG2Image>();
            this.sbnuminstances = refaggninst;
            this.<Sbrtemplate>k__BackingField = sdrtemplate;
            this.<Sbrat>k__BackingField = at;
            this.<Glyphs>k__BackingField = glyphs;
            this.sbrefine = true;
            this.<Sbstrips>k__BackingField = 1;
            this.sbdefpixel = false;
            this.sbcombop = 0;
            this.transposed = false;
            this.refcorner = JBIG2Corner.TopLeft;
            this.sbdsoffset = 0;
            this.regionInfo = regionInfo;
        }

        public JBIG2Image Process()
        {
            if (this.Glyphs.Count == 0)
            {
                return null;
            }
            JBIG2Image image = new JBIG2Image(this.regionInfo.Width, this.regionInfo.Height);
            int num = 0;
            image.Clear(this.sbdefpixel);
            int num2 = this.Decoder.DecodeDT() * -this.Sbstrips;
            int num3 = 0;
            int num4 = 0;
            while (num4 < this.sbnuminstances)
            {
                int num5 = this.Decoder.DecodeDT() * this.Sbstrips;
                num2 += num5;
                num3 += this.Decoder.DecodeFS();
                num = num3;
                while (true)
                {
                    T decoder = this.Decoder;
                    if (!decoder.LastCode)
                    {
                        int num7 = num2 + ((this.Sbstrips == 1) ? 0 : this.Decoder.DecodeIT());
                        int num8 = this.Decoder.DecodeID();
                        JBIG2Image ib = this.Glyphs[num8];
                        if (this.sbrefine && (this.Decoder.DecodeRI() > 0))
                        {
                            ib = this.RefinementDecode(ib);
                        }
                        if (!this.transposed && (this.refcorner > JBIG2Corner.TopLeft))
                        {
                            num += ib.Width - 1;
                        }
                        else if (this.transposed && ((this.refcorner & JBIG2Corner.TopLeft) == JBIG2Corner.BottomLeft))
                        {
                            num += ib.Height - 1;
                        }
                        int x = 0;
                        int y = 0;
                        int num11 = this.transposed ? num7 : num;
                        int num12 = this.transposed ? num : num7;
                        switch (this.refcorner)
                        {
                            case JBIG2Corner.BottomLeft:
                                x = num11;
                                y = (num12 - ib.Height) + 1;
                                break;

                            case JBIG2Corner.TopLeft:
                                x = num11;
                                y = num12;
                                break;

                            case JBIG2Corner.BottomRight:
                                x = (num11 - ib.Width) + 1;
                                y = (num12 - ib.Height) + 1;
                                break;

                            case JBIG2Corner.TopRight:
                                x = (num11 - ib.Width) + 1;
                                y = num12;
                                break;

                            default:
                                break;
                        }
                        image.Composite(ib, x, y, this.sbcombop);
                        if (!this.transposed && (this.refcorner < JBIG2Corner.BottomRight))
                        {
                            num += ib.Width - 1;
                        }
                        else if (this.transposed && ((this.refcorner & JBIG2Corner.TopLeft) != JBIG2Corner.BottomLeft))
                        {
                            num += ib.Height - 1;
                        }
                        num4++;
                        if (num4 <= this.sbnuminstances)
                        {
                            num += this.Decoder.DecodeDS() + this.sbdsoffset;
                            continue;
                        }
                    }
                    break;
                }
            }
            return image;
        }

        private JBIG2Image RefinementDecode(JBIG2Image ib)
        {
            int num = this.Decoder.DecodeRDW();
            int num2 = this.Decoder.DecodeRDH();
            int num3 = this.Decoder.DecodeRDX();
            int num4 = this.Decoder.DecodeRDY();
            JBIG2Image refImage = new JBIG2Image(ib.Width + num, ib.Height + num2);
            int dx = (num >> 1) + num3;
            this.RefinementDecode(ib, dx, (num2 >> 1) + num4, refImage);
            return refImage;
        }

        protected abstract void RefinementDecode(JBIG2Image ib, int dx, int dy, JBIG2Image refImage);

        protected int HuffmanFlags { get; }

        protected List<JBIG2Image> Glyphs { get; }

        protected int[] Sbrat { get; }

        protected int Sbrtemplate { get; }

        protected int Sbstrips { get; }

        protected abstract T Decoder { get; }

        protected JBIG2StreamHelper StreamHelper { get; }
    }
}

