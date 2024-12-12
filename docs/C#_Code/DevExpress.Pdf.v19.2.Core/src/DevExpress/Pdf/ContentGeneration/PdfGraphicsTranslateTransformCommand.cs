namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfGraphicsTranslateTransformCommand : PdfGraphicsCommand
    {
        private readonly float x;
        private readonly float y;

        public PdfGraphicsTranslateTransformCommand(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            constructor.TranslateTransform((double) this.x, (double) this.y);
        }
    }
}

