namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JBIG2SymbolDictionary : JBIG2SegmentData
    {
        public JBIG2SymbolDictionary(JBIG2Image image) : base(image)
        {
        }

        public JBIG2SymbolDictionary(JBIG2StreamHelper streamHelper, JBIG2SegmentHeader header, JBIG2Image image) : base(streamHelper, header, image)
        {
            int flags = base.StreamHelper.ReadInt16();
            if ((flags & 1) != 0)
            {
                this.<Glyphs>k__BackingField = new JBIG2SymbolDictionaryHuffmanParser(base.StreamHelper, base.GetSDReferred(), base.Header, flags, base.GetUserDefinedHuffmanTables()).Process();
            }
            else
            {
                this.<Glyphs>k__BackingField = new JBIG2SymbolDictionaryArithmeticParser(base.StreamHelper, base.GetSDReferred(), base.Header, flags).Process();
            }
        }

        public List<JBIG2Image> Glyphs { get; }
    }
}

