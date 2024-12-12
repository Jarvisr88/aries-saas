namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfAnnotationBuilder : IPdfAnnotationBuilder
    {
        private readonly PdfRectangle rect;
        private PdfAnnotationFlags flags = PdfAnnotationFlags.Print;
        private string name;
        private PdfRGBColor color;
        private DateTimeOffset? modificationDate;
        private string contents;
        private PdfAnnotationBorder border = new PdfAnnotationBorder();

        public PdfAnnotationBuilder(PdfRectangle rect)
        {
            this.rect = rect;
        }

        public PdfRectangle Rect =>
            this.rect;

        public string Name
        {
            get => 
                this.name;
            set => 
                this.name = value;
        }

        public PdfRGBColor Color
        {
            get => 
                this.color;
            set => 
                this.color = value;
        }

        public DateTimeOffset? ModificationDate
        {
            get => 
                this.modificationDate;
            set => 
                this.modificationDate = value;
        }

        public string Contents
        {
            get => 
                this.contents;
            set => 
                this.contents = value;
        }

        public PdfAnnotationFlags Flags
        {
            get => 
                this.flags;
            set => 
                this.flags = value;
        }

        public PdfAnnotationBorder Border
        {
            get => 
                this.border;
            set => 
                this.border = value;
        }
    }
}

