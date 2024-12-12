namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsIntersectClipCommand : PdfGraphicsCommand
    {
        private readonly RectangleF clip;

        public PdfGraphicsIntersectClipCommand(RectangleF clip)
        {
            this.clip = clip;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            constructor.IntersectClip(this.clip);
        }
    }
}

