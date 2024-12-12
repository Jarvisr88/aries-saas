namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsFillEllipseCommand : PdfGraphicsNonStrokingCommand
    {
        private readonly RectangleF rect;

        public PdfGraphicsFillEllipseCommand(Brush brush, RectangleF rect) : base(brush)
        {
            this.rect = rect;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            base.Execute(constructor, page);
            constructor.FillEllipse(this.rect);
        }
    }
}

