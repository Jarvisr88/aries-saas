namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Printing.BrickCollection;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class CompositeLink : TemplatedLink
    {
        private readonly IEnumerable<TemplatedLink> links;
        private readonly PageBreakInfoCollection pageBreaks;
        private bool disposed;
        private readonly VisualTreeWalker walker;

        public CompositeLink(IEnumerable<TemplatedLink> links) : this(links, string.Empty)
        {
        }

        public CompositeLink(IEnumerable<TemplatedLink> links, string documentName) : base(documentName)
        {
            this.pageBreaks = new PageBreakInfoCollection();
            this.walker = new VisualTreeWalker();
            this.links = links;
            base.PrintingSystem.PageInsertComplete += new PageInsertCompleteEventHandler(this.OnPageInsertComplete);
        }

        protected override void Build(bool buildPagesInBackground)
        {
            this.Links.ForEach<TemplatedLink>(x => this.SyncPageSettings(x));
            base.Build(buildPagesInBackground);
        }

        public void CreatePageForEachLink()
        {
            base.OnCreateDocumentStarted();
            base.PrintingSystem.ClearContent();
            SinglePrintingDocumentFactory factory = new SinglePrintingDocumentFactory();
            foreach (TemplatedLink link in this.links)
            {
                IDocumentFactory documentFactory = link.PrintingSystem.DocumentFactory;
                link.PrintingSystem.DocumentFactory = factory;
                link.CreateDocument(false);
                link.PrintingSystem.DocumentFactory = documentFactory;
                if (link.PrintingSystem.PageCount > 0)
                {
                    base.PrintingSystem.Pages.Add(link.PrintingSystem.Pages[0]);
                }
            }
            base.OnCreateDocumentFinished();
        }

        protected override IRootDataNode CreateRootNode()
        {
            foreach (TemplatedLink link in this.Links)
            {
                link.PreventDisposing = true;
            }
            return new CompositeRootNode(this);
        }

        internal sealed override VisualDataNodeBandManager CreateVisualDataNodeBandManager() => 
            new CompositeLinkBandManager(base.RootNode, base.BandInitializer, delegate (IDataNode node) {
                IVisualTreeWalker walker = (base.RootNode as CompositeRootNode).TryGetWalker(node) ?? this.walker;
                base.BrickCollector.VisualTreeWalker = walker;
            });

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && (base.PrintingSystem != null))
                {
                    base.PrintingSystem.PageInsertComplete -= new PageInsertCompleteEventHandler(this.OnPageInsertComplete);
                }
                this.disposed = true;
            }
            base.Dispose(disposing);
        }

        internal override void End(bool buildPagesInBackground)
        {
            base.PrintingSystem.End(buildPagesInBackground);
            foreach (TemplatedLink link in this.Links)
            {
                Action<IDisposable> action = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Action<IDisposable> local1 = <>c.<>9__12_0;
                    action = <>c.<>9__12_0 = x => x.Dispose();
                }
                (link.RootNode as IDisposable).Do<IDisposable>(action);
            }
        }

        protected override bool IsDocumentLayoutRightToLeft() => 
            false;

        private void OnPageInsertComplete(object sender, PageInsertCompleteEventArgs e)
        {
            this.links.OfType<PrintableControlLink>().ForEach<PrintableControlLink>(delegate (PrintableControlLink x) {
                if (this.BrickCollector != null)
                {
                    BrickEnumerator pageBrickEnumerator = new BrickEnumerator(this.PrintingSystem.Pages[e.PageIndex]);
                    x.PrintableControl.PagePrintedCallback(pageBrickEnumerator, this.BrickCollector.BrickUpdaters);
                }
            });
        }

        private void SyncPageSettings(TemplatedLink link)
        {
            link.PaperKind = base.PrintingSystem.PageSettings.PaperKind;
            link.CustomPaperSize = base.PrintingSystem.PageSettings.Data.Size;
            link.Landscape = base.PrintingSystem.PageSettings.Landscape;
            link.MinMargins = base.PrintingSystem.PageSettings.MinMargins;
            link.Margins = base.PrintingSystem.PageSettings.Margins;
        }

        public IEnumerable<TemplatedLink> Links =>
            this.links;

        public PageBreakInfoCollection PageBreaks =>
            this.pageBreaks;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CompositeLink.<>c <>9 = new CompositeLink.<>c();
            public static Action<IDisposable> <>9__12_0;

            internal void <End>b__12_0(IDisposable x)
            {
                x.Dispose();
            }
        }
    }
}

