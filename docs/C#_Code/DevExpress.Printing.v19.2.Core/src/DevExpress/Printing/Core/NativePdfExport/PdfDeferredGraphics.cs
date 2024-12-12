namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class PdfDeferredGraphics : PdfGraphics
    {
        private bool isDisposed;
        private readonly List<DeferredAction> deferredActions;

        public PdfDeferredGraphics(Stream stream, PdfExportOptions options, PrintingSystemBase ps, PageRangeIndexMapper pageIndexMapper) : base(stream, options, ps, pageIndexMapper)
        {
            this.deferredActions = new List<DeferredAction>();
        }

        public override void AddDrawingAction(DeferredAction action)
        {
            DeferredActionWithForm form = action as DeferredActionWithForm;
            if (form != null)
            {
                form.InitForm(base.Graphics);
            }
            this.deferredActions.Add(action);
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.isDisposed & disposing)
            {
                this.isDisposed = true;
                base.Graphics.ClosePage();
                base.Graphics.ExportDocument.FlushPage();
                foreach (DeferredAction action in this.deferredActions)
                {
                    action.Execute(base.PrintingSystem, this);
                }
            }
            base.Dispose(disposing);
        }
    }
}

