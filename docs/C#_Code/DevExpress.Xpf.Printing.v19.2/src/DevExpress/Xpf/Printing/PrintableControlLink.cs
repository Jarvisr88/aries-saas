namespace DevExpress.Xpf.Printing
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.BrickCollection;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.DataNodes;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Windows;

    public class PrintableControlLink : TemplatedLink
    {
        private readonly IPrintableControl printableControl;
        private bool disposed;

        public PrintableControlLink(IPrintableControl printableControl) : this(printableControl, string.Empty)
        {
        }

        public PrintableControlLink(IPrintableControl printableControl, string documentName) : base(documentName)
        {
            Guard.ArgumentNotNull(printableControl, "printableControl");
            this.printableControl = printableControl;
            base.PrintingSystem.PageInsertComplete += new PageInsertCompleteEventHandler(this.PrintingSystem_PageInsertComplete);
        }

        protected override void Build(bool buildPagesInBackground)
        {
            if ((this.printableControl == null) || (!buildPagesInBackground || !this.printableControl.CanCreateRootNodeAsync))
            {
                base.Build(buildPagesInBackground);
            }
            else
            {
                this.printableControl.CreateRootNodeCompleted += new EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>>(this.printableControl_CreateRootNodeCompleted);
                this.CreateRootNodeAsync();
            }
        }

        protected override void BuildCore()
        {
            if (this.printableControl != null)
            {
                IVisualTreeWalker customVisualTreeWalker = this.printableControl.GetCustomVisualTreeWalker();
                if (customVisualTreeWalker != null)
                {
                    base.BrickCollector.VisualTreeWalker = customVisualTreeWalker;
                }
            }
            base.BuildCore();
        }

        protected override IRootDataNode CreateRootNode()
        {
            if (this.printableControl == null)
            {
                return null;
            }
            DocumentBand root = base.PrintingSystem.PrintingDocument.Root;
            return this.printableControl.CreateRootNode(DrawingConverter.ToSize(base.PrintingSystem.PageSettings.UsablePageSizeInPixels), this.GetBoundsSize(root.GetBand(DocumentBandKind.ReportHeader)), this.GetBoundsSize(root.GetBand(DocumentBandKind.ReportFooter)), this.GetBoundsSize(root.GetBand(DocumentBandKind.PageHeader)), this.GetBoundsSize(root.GetBand(DocumentBandKind.PageFooter)));
        }

        private void CreateRootNodeAsync()
        {
            DocumentBand root = base.PrintingSystem.PrintingDocument.Root;
            this.printableControl.CreateRootNodeAsync(DrawingConverter.ToSize(base.PrintingSystem.PageSettings.UsablePageSizeInPixels), this.GetBoundsSize(root.GetBand(DocumentBandKind.ReportHeader)), this.GetBoundsSize(root.GetBand(DocumentBandKind.ReportFooter)), this.GetBoundsSize(root.GetBand(DocumentBandKind.PageHeader)), this.GetBoundsSize(root.GetBand(DocumentBandKind.PageFooter)));
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if ((this.printableControl != null) && this.printableControl.CanCreateRootNodeAsync)
                    {
                        this.printableControl.CreateRootNodeCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>>(this.printableControl_CreateRootNodeCompleted);
                    }
                    if (base.PrintingSystem != null)
                    {
                        base.PrintingSystem.PageInsertComplete -= new PageInsertCompleteEventHandler(this.PrintingSystem_PageInsertComplete);
                    }
                }
                this.disposed = true;
            }
            base.Dispose(disposing);
        }

        private System.Windows.Size GetBoundsSize(DocumentBand band)
        {
            SizeF ef = new SizeF(band.BrickBounds.Size.Width, band.SelfHeight);
            return DrawingConverter.ToSize(GraphicsUnitConverter2.DocToPixel(ef));
        }

        protected override bool IsDocumentLayoutRightToLeft()
        {
            IRightToLeftSupport printableControl = this.printableControl as IRightToLeftSupport;
            return ((printableControl != null) ? printableControl.RightToLeftLayout : false);
        }

        private void printableControl_CreateRootNodeCompleted(object sender, ScalarOperationCompletedEventArgs<IRootDataNode> e)
        {
            this.printableControl.CreateRootNodeCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>>(this.printableControl_CreateRootNodeCompleted);
            if (!this.disposed)
            {
                base.RootNode = e.Result;
                this.BuildCore();
                base.RaiseDocumentBuildingCompleted();
            }
        }

        private void PrintingSystem_PageInsertComplete(object sender, PageInsertCompleteEventArgs e)
        {
            if (this.printableControl != null)
            {
                BrickEnumerator pageBrickEnumerator = new BrickEnumerator(base.PrintingSystem.Pages[e.PageIndex]);
                this.printableControl.PagePrintedCallback(pageBrickEnumerator, base.BrickCollector.BrickUpdaters);
            }
        }

        internal IPrintableControl PrintableControl =>
            this.printableControl;
    }
}

