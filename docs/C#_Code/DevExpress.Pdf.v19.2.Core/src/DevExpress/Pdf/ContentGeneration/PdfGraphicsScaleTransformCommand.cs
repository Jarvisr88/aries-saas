namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfGraphicsScaleTransformCommand : PdfGraphicsCommand
    {
        private readonly float sx;
        private readonly float sy;

        public PdfGraphicsScaleTransformCommand(float sx, float sy)
        {
            this.sx = sx;
            this.sy = sy;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            constructor.ScaleTransform((double) this.sx, (double) this.sy);
        }
    }
}

