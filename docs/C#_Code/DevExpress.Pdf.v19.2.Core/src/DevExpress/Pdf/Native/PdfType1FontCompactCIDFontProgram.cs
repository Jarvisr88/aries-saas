namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfType1FontCompactCIDFontProgram : PdfType1FontCompactFontProgram
    {
        public const double DefaultCIDFontVersion = 0.0;
        public const int DefaultCIDCount = 0x2210;
        private string registry;
        private string ordering;
        private double supplement;
        private double cidFontVersion;
        private int cidCount;
        private int? uidBase;
        private PdfType1FontCIDGlyphGroupData[] glyphGroupData;
        private PdfType1FontCIDGlyphGroupSelector glyphGroupSelector;

        internal PdfType1FontCompactCIDFontProgram(byte majorVersion, byte minorVersion, string fontName, PdfCompactFontFormatStringIndex stringIndex, IList<byte[]> globalSubrs) : base(majorVersion, minorVersion, fontName, stringIndex, globalSubrs)
        {
            this.cidCount = 0x2210;
        }

        public override IPdfCodePointMapping GetCompositeMapping(short[] cidToGidMap) => 
            new PdfCompositeFontCodePointMapping(cidToGidMap, base.Charset?.SidToGidMapping);

        public override bool Validate()
        {
            bool flag = false;
            PdfTransformationMatrix fontMatrix = base.FontMatrix;
            foreach (PdfType1FontCIDGlyphGroupData data in this.glyphGroupData)
            {
                if (!fontMatrix.Equals(data.FontMatrix))
                {
                    data.FontMatrix = fontMatrix;
                    flag = true;
                }
                PdfType1FontPrivateData @private = data.Private;
                if (@private != null)
                {
                    flag |= @private.Patch();
                }
            }
            return (base.Validate() | flag);
        }

        public string Registry
        {
            get => 
                this.registry;
            internal set => 
                this.registry = value;
        }

        public string Ordering
        {
            get => 
                this.ordering;
            internal set => 
                this.ordering = value;
        }

        public double Supplement
        {
            get => 
                this.supplement;
            internal set => 
                this.supplement = value;
        }

        public double CIDFontVersion
        {
            get => 
                this.cidFontVersion;
            internal set => 
                this.cidFontVersion = value;
        }

        public int CIDCount
        {
            get => 
                this.cidCount;
            internal set => 
                this.cidCount = value;
        }

        public int? UIDBase
        {
            get => 
                this.uidBase;
            internal set => 
                this.uidBase = value;
        }

        public PdfType1FontCIDGlyphGroupData[] GlyphGroupData
        {
            get => 
                this.glyphGroupData;
            internal set => 
                this.glyphGroupData = value;
        }

        public PdfType1FontCIDGlyphGroupSelector GlyphGroupSelector
        {
            get => 
                this.glyphGroupSelector;
            internal set => 
                this.glyphGroupSelector = value;
        }
    }
}

