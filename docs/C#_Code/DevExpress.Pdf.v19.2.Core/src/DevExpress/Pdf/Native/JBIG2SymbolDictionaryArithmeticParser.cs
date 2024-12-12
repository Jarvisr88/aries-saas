namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JBIG2SymbolDictionaryArithmeticParser : JBIG2SymbolDictionaryParser<JBIG2Decoder>
    {
        public JBIG2SymbolDictionaryArithmeticParser(JBIG2StreamHelper streamHelper, IList<JBIG2SymbolDictionary> sdReferred, JBIG2SegmentHeader segmentHeader, int flags) : base(streamHelper, sdReferred, segmentHeader, flags)
        {
            this.<Decoder>k__BackingField = JBIG2Decoder.Create(base.StreamHelper, base.Limit);
        }

        protected override void DecodeNonRefaggGLyph(int symWidth, int hcheight)
        {
            JBIG2Image image = new JBIG2Image(symWidth, hcheight);
            new JBIG2GenericRegion(base.SegmentHeader, base.Sdat, base.Sdtemplate, this.Decoder).CreateDecoder(image).Decode();
            base.Syms.Add(image);
        }

        protected override void DecodeRefaggNonRefaggninstGlyph(int symWidth, int hcheight, int refaggninst)
        {
            JBIG2Image image = new JBIG2Image(symWidth, hcheight);
            new JBIG2TextRegion(refaggninst, base.Sdrtemplate, base.Sdrat, base.Syms, this.Decoder, image).Process();
            base.Syms.Add(image);
        }

        protected override void DecodeRefaggRefaggninstGlyph(int symWidth, int hcheight)
        {
            JBIG2Image image = new JBIG2Image(symWidth, hcheight);
            int num = this.Decoder.DecodeID();
            int dx = this.Decoder.DecodeRDX();
            int dy = this.Decoder.DecodeRDY();
            if (num >= base.Syms.Count)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            new JBIG2RefinementRegion(base.Syms[num], dx, dy, base.Sdrtemplate, base.Sdrat, this.Decoder, image).Process();
            base.Syms.Add(image);
        }

        protected override void ProcessStripBitmap(int hcheight)
        {
        }

        protected override JBIG2Decoder Decoder { get; }
    }
}

