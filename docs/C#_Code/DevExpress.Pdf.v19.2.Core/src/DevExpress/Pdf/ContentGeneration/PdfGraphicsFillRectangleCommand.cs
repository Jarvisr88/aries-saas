namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsFillRectangleCommand : PdfGraphicsNonStrokingCommand
    {
        private readonly RectangleF bounds;

        public PdfGraphicsFillRectangleCommand(Brush brush, RectangleF bounds) : base(brush)
        {
            this.bounds = bounds;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            base.Execute(constructor, page);
            constructor.FillRectangle(this.bounds);
        }
    }
}

