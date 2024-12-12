namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfMarkupAnnotationBuilder : PdfAnnotationBuilder, IPdfMarkupAnnotationBuilder, IPdfAnnotationBuilder
    {
        private double opacity;
        private DateTimeOffset? creationDate;
        private string title;
        private string subject;

        public PdfMarkupAnnotationBuilder(PdfMarkupAnnotation annotation) : base(annotation.Rect)
        {
            this.opacity = 1.0;
            base.Name = annotation.Name;
            base.Color = new PdfRGBColor(annotation.Color);
            base.ModificationDate = annotation.Modified;
            base.Contents = annotation.Contents;
            this.opacity = annotation.Opacity;
            this.creationDate = annotation.CreationDate;
            this.title = annotation.Title;
            this.subject = annotation.Subject;
        }

        public PdfMarkupAnnotationBuilder(PdfRectangle bounds) : base(bounds)
        {
            this.opacity = 1.0;
            this.creationDate = new DateTimeOffset?(DateTimeOffset.Now);
            base.ModificationDate = this.creationDate;
            base.Name = Guid.NewGuid().ToString();
            this.title = Environment.UserName;
        }

        public double Opacity
        {
            get => 
                this.opacity;
            set => 
                this.opacity = value;
        }

        public DateTimeOffset? CreationDate
        {
            get => 
                this.creationDate;
            set => 
                this.creationDate = value;
        }

        public string Title
        {
            get => 
                this.title;
            set => 
                this.title = value;
        }

        public string Subject
        {
            get => 
                this.subject;
            set => 
                this.subject = value;
        }
    }
}

