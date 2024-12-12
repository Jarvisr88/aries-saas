namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfType1FontCIDGlyphGroupData
    {
        private double underlinePosition = -100.0;
        private double underlineThickness = 50.0;
        private PdfRectangle fontBBox = PdfType1FontCompactFontProgram.DefaultFontBBox;
        private PdfType1FontType fontType = PdfType1FontType.Type2;
        private PdfTransformationMatrix fontMatrix = PdfType1FontCompactFontProgram.DefaultFontMatrix;
        private int uniqueID;
        private PdfType1FontPrivateData privateData;
        private int cidCount = 0x2210;
        private string fontName;

        public double UnderlinePosition
        {
            get => 
                this.underlinePosition;
            internal set => 
                this.underlinePosition = value;
        }

        public double UnderlineThickness
        {
            get => 
                this.underlineThickness;
            internal set => 
                this.underlineThickness = value;
        }

        public PdfRectangle FontBBox
        {
            get => 
                this.fontBBox;
            internal set => 
                this.fontBBox = value;
        }

        public PdfType1FontType FontType
        {
            get => 
                this.fontType;
            internal set => 
                this.fontType = value;
        }

        public PdfTransformationMatrix FontMatrix
        {
            get => 
                this.fontMatrix;
            internal set => 
                this.fontMatrix = value;
        }

        public int UniqueID
        {
            get => 
                this.uniqueID;
            internal set => 
                this.uniqueID = value;
        }

        public PdfType1FontPrivateData Private
        {
            get => 
                this.privateData;
            internal set => 
                this.privateData = value;
        }

        public int CIDCount
        {
            get => 
                this.cidCount;
            internal set => 
                this.cidCount = value;
        }

        public string FontName
        {
            get => 
                this.fontName;
            internal set => 
                this.fontName = value;
        }
    }
}

