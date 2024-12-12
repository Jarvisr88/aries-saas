namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class JBIG2SymbolDictionaryParser<T> where T: IJBIG2SymbolDictionaryDecoder
    {
        private readonly bool sdrefagg;
        private readonly int sdnumexsyms;

        public JBIG2SymbolDictionaryParser(JBIG2StreamHelper streamHelper, IList<JBIG2SymbolDictionary> sdReferred, JBIG2SegmentHeader segmentHeader, int flags)
        {
            this.<Syms>k__BackingField = new List<JBIG2Image>();
            foreach (JBIG2SymbolDictionary dictionary in sdReferred)
            {
                this.Syms.AddRange(dictionary.Glyphs);
            }
            this.<SegmentHeader>k__BackingField = segmentHeader;
            this.<StreamHelper>k__BackingField = streamHelper;
            this.sdrefagg = ((flags >> 1) & 1) != 0;
            this.<Sdtemplate>k__BackingField = (flags >> 10) & 3;
            this.<Sdrtemplate>k__BackingField = (flags >> 12) & 1;
            if ((flags & 1) == 0)
            {
                if ((flags & 0x100) != 0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                int length = (this.Sdtemplate == 0) ? 8 : 2;
                this.<Sdat>k__BackingField = this.StreamHelper.ReadAdaptiveTemplate(length);
            }
            if (this.sdrefagg && (this.Sdrtemplate == 0))
            {
                this.<Sdrat>k__BackingField = this.StreamHelper.ReadAdaptiveTemplate(4);
            }
            this.sdnumexsyms = this.StreamHelper.ReadInt32();
            this.<Sdnumnewsyms>k__BackingField = this.StreamHelper.ReadInt32();
            this.<Limit>k__BackingField = this.Syms.Count + this.Sdnumnewsyms;
            this.<Sdnuminsyms>k__BackingField = this.Syms.Count;
        }

        protected abstract void DecodeNonRefaggGLyph(int symWidth, int hcheight);
        protected abstract void DecodeRefaggNonRefaggninstGlyph(int symWidth, int hcheight, int refaggninst);
        protected abstract void DecodeRefaggRefaggninstGlyph(int symWidth, int hcheight);
        private List<JBIG2Image> GetExportedSymbols()
        {
            List<JBIG2Image> list = new List<JBIG2Image>();
            int num = 0;
            bool flag = false;
            int num2 = 0;
            while (num < this.Limit)
            {
                int num3 = this.Decoder.DecodeEX();
                num2 = (num3 > 0) ? 0 : (num2 + 1);
                T decoder = this.Decoder;
                if (decoder.LastCode || ((num3 > (this.Limit - num)) || ((num3 < 0) || ((num2 > 4) || (flag && (num3 > (this.sdnumexsyms - list.Count)))))))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                int num4 = 0;
                while (true)
                {
                    if (num4 >= num3)
                    {
                        flag = !flag;
                        break;
                    }
                    if (flag)
                    {
                        list.Add(this.Syms[num]);
                    }
                    num++;
                    num4++;
                }
            }
            return list;
        }

        public List<JBIG2Image> Process()
        {
            int num = 0;
            int hcheight = 0;
            while (num < this.Sdnumnewsyms)
            {
                int num3 = this.Decoder.DecodeDH();
                hcheight += num3;
                int symWidth = 0;
                if (hcheight < 0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                int num5 = this.Decoder.DecodeDW();
                while (true)
                {
                    T decoder = this.Decoder;
                    if (decoder.LastCode)
                    {
                        if (!this.sdrefagg)
                        {
                            this.ProcessStripBitmap(hcheight);
                        }
                        break;
                    }
                    if (this.Syms.Count >= this.Limit)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    symWidth += num5;
                    if (symWidth < 0)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    if (!this.sdrefagg)
                    {
                        this.DecodeNonRefaggGLyph(symWidth, hcheight);
                    }
                    else
                    {
                        int refaggninst = this.Decoder.DecodeAI();
                        if (this.Decoder.LastCode || (refaggninst <= 0))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        if (refaggninst > 1)
                        {
                            this.DecodeRefaggNonRefaggninstGlyph(symWidth, hcheight, refaggninst);
                        }
                        else
                        {
                            this.DecodeRefaggRefaggninstGlyph(symWidth, hcheight);
                        }
                    }
                    num++;
                    num5 = this.Decoder.DecodeDW();
                }
            }
            return this.GetExportedSymbols();
        }

        protected abstract void ProcessStripBitmap(int hcheight);

        protected abstract T Decoder { get; }

        protected int Limit { get; }

        protected int Sdtemplate { get; }

        protected int Sdrtemplate { get; }

        protected int Sdnumnewsyms { get; }

        protected List<JBIG2Image> Syms { get; }

        protected int[] Sdat { get; }

        protected int[] Sdrat { get; }

        protected JBIG2SegmentHeader SegmentHeader { get; }

        protected JBIG2StreamHelper StreamHelper { get; }

        protected int Sdnuminsyms { get; }
    }
}

