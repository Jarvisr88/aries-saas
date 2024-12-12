namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfType1FontProgram
    {
        public const int DefaultUniqueID = 0;
        public const double DefaultStrokeWidth = 0.0;
        internal static readonly byte[] EmptyCharstring = new byte[] { 14 };
        private string fontName;
        private PdfType1FontInfo fontInfo;
        private PdfType1FontPaintType paintType = PdfType1FontPaintType.Invalid;
        private PdfTransformationMatrix fontMatrix;
        private PdfRectangle fontBBox;
        private int uniqueID;
        private double strokeWidth;
        private PdfType1FontPrivateData privateData;

        protected PdfType1FontProgram()
        {
        }

        public virtual IPdfCodePointMapping GetCompositeMapping(short[] cidToGidMap) => 
            new PdfCompositeFontCodePointMapping(cidToGidMap, null);

        public abstract IPdfCodePointMapping GetSimpleMapping(PdfSimpleFontEncoding fontEncoding);

        public string FontName
        {
            get => 
                this.fontName;
            protected set => 
                this.fontName = value;
        }

        public PdfType1FontInfo FontInfo
        {
            get => 
                this.fontInfo;
            set => 
                this.fontInfo = value;
        }

        public PdfType1FontPaintType PaintType
        {
            get => 
                this.paintType;
            internal set => 
                this.paintType = value;
        }

        public PdfTransformationMatrix FontMatrix
        {
            get => 
                this.fontMatrix;
            internal set => 
                this.fontMatrix = value;
        }

        public PdfRectangle FontBBox
        {
            get => 
                this.fontBBox;
            internal set => 
                this.fontBBox = value;
        }

        public int UniqueID
        {
            get => 
                this.uniqueID;
            internal set => 
                this.uniqueID = value;
        }

        public double StrokeWidth
        {
            get => 
                this.strokeWidth;
            internal set => 
                this.strokeWidth = value;
        }

        public PdfType1FontPrivateData Private
        {
            get => 
                this.privateData;
            internal set => 
                this.privateData = value;
        }

        public abstract PdfType1FontType FontType { get; }
    }
}

