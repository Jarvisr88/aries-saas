namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class JBIG2TextRegion : JBIG2SegmentData
    {
        private readonly JBIG2RegionSegmentInfo regionInfo;
        private readonly IJBIG2TextRegionParser parser;

        public JBIG2TextRegion(JBIG2StreamHelper streamHelper, JBIG2SegmentHeader header, JBIG2Image image) : base(streamHelper, header, image)
        {
            this.regionInfo = new JBIG2RegionSegmentInfo(base.StreamHelper);
            int flags = base.StreamHelper.ReadInt16();
            if ((flags & 1) != 0)
            {
                this.parser = new JBIG2TextRegionHuffmanDecoderParser(base.StreamHelper, this.regionInfo, flags, base.GetSDReferred(), base.GetUserDefinedHuffmanTables());
            }
            else
            {
                this.parser = new JBIG2TextRegionArithmeticDecoderParser(base.StreamHelper, this.regionInfo, flags, base.GetSDReferred());
            }
        }

        internal JBIG2TextRegion(int refaggninst, int sdrtemplate, int[] at, List<JBIG2Image> glyphs, JBIG2Decoder decoder, JBIG2Image image) : base(image)
        {
            this.regionInfo = new JBIG2RegionSegmentInfo(image.Width, image.Height);
            this.parser = new JBIG2TextRegionArithmeticDecoderParser(refaggninst, sdrtemplate, at, glyphs, decoder, this.regionInfo);
        }

        internal JBIG2TextRegion(JBIG2StreamHelper streamHelper, JBIG2TextRegionHuffmanTables tables, int refaggninst, int sdrtemplate, int[] at, List<JBIG2Image> glyphs, JBIG2Image image, int sdnuminsyms, int sdnumnewsyms) : base(image)
        {
            this.regionInfo = new JBIG2RegionSegmentInfo(image.Width, image.Height);
            this.parser = new JBIG2TextRegionHuffmanDecoderParser(streamHelper, tables, refaggninst, sdrtemplate, at, glyphs, this.regionInfo, sdnuminsyms, sdnumnewsyms);
        }

        public override void Process()
        {
            if ((base.Header == null) || ((base.Header.Flags & 0x3f) != 4))
            {
                JBIG2Image image = this.parser.Process();
                if (image != null)
                {
                    base.Image.Composite(image, this.regionInfo.X, this.regionInfo.Y, this.regionInfo.ComposeOperator);
                }
            }
        }

        protected override bool CacheData =>
            true;
    }
}

