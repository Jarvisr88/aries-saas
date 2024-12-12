namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Pdf;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    internal abstract class DeferredActionWithForm : DeferredAction
    {
        private PdfForm form;
        private PdfPrintingGraphicsImplementation graphics;

        protected DeferredActionWithForm(RectangleF rect) : base(rect)
        {
        }

        public override void Execute(PrintingSystemBase ps, IGraphics gr)
        {
            if ((this.graphics != null) && (this.Form != null))
            {
                this.graphics.SetDrawingForm(this.Form);
            }
            this.DoAction(ps, gr);
            if ((this.graphics != null) && (this.Form != null))
            {
                this.graphics.FlushFormCommands(this.Form);
            }
        }

        public void InitForm(PdfPrintingGraphicsImplementation graphics)
        {
            RectangleF rect = base.Rect;
            this.graphics = graphics;
            this.Form = graphics.DrawDeferredForm(rect);
            base.Rect = new RectangleF(0f, 0f, rect.Width, rect.Height);
        }

        public PdfForm Form
        {
            get => 
                this.form;
            set => 
                this.form = value;
        }
    }
}

