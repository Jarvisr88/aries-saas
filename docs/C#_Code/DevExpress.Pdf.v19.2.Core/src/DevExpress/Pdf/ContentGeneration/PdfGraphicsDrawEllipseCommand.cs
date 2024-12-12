namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsDrawEllipseCommand : PdfGraphicsStrokingCommand
    {
        private readonly RectangleF rect;

        public PdfGraphicsDrawEllipseCommand(Pen pen, RectangleF rect) : base(pen)
        {
            this.rect = rect;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            base.Execute(constructor, page);
            constructor.DrawEllipse(this.rect);
        }
    }
}

