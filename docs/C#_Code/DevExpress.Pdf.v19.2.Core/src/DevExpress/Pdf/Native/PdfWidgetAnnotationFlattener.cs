namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfWidgetAnnotationFlattener : PdfWidgetAnnotationRemover
    {
        private readonly PdfObjectCollection objects;
        private readonly PdfPage page;
        private PdfCommandConstructor constructor;
        private bool replaceCommands;
        private PdfDocumentStateBase documentState;

        public PdfWidgetAnnotationFlattener(PdfPage page, PdfDocumentStateBase documentState) : base(page)
        {
            this.page = page;
            this.documentState = documentState;
            this.objects = page.DocumentCatalog.Objects;
            this.constructor = new PdfCommandConstructor(page.Resources);
            this.constructor.SaveGraphicsState();
            this.constructor.AddCommands(page.GetCommandsData());
            this.constructor.RestoreGraphicsState();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.constructor != null))
            {
                if (this.replaceCommands)
                {
                    this.page.ReplaceCommands(this.constructor.Commands);
                }
                this.constructor.Dispose();
                this.constructor = null;
            }
            base.Dispose(disposing);
        }

        protected override bool RemoveWidget(PdfWidgetAnnotation widget)
        {
            if (!base.RemoveWidget(widget))
            {
                return false;
            }
            PdfRectangle rect = widget.Rect;
            if (!widget.Flags.HasFlag(PdfAnnotationFlags.Hidden) && ((rect.Width > 0.0) && (rect.Height > 0.0)))
            {
                PdfForm form = widget.EnsureAppearance(PdfAnnotationAppearanceState.Normal, this.documentState);
                if (form != null)
                {
                    this.constructor.DrawForm(this.objects.AddResolvedObject(form), form.GetTrasformationMatrix(rect));
                    this.replaceCommands = true;
                }
            }
            return true;
        }
    }
}

