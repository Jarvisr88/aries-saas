namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfGraphicsRotateTransformCommand : PdfGraphicsCommand
    {
        private readonly float degree;

        public PdfGraphicsRotateTransformCommand(float degree)
        {
            this.degree = degree;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            constructor.RotateTransform(this.degree);
        }
    }
}

