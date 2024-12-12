namespace DevExpress.Xpf.Printing.PreviewControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Rendering;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class PageViewModelBase : IPage, IDocumentPage
    {
        private double scaleMultiplier;
        internal EventHandler PageChanged;
        private readonly Thickness margin = new Thickness(5.0);
        private long pageId;

        protected PageViewModelBase(DevExpress.XtraPrinting.Page page)
        {
            Guard.ArgumentNotNull(page, "page");
            this.PageIndex = page.Index;
            this.PageList = page.Owner;
            this.SyncPageInfo();
        }

        internal void Destroy()
        {
            this.PageList = null;
        }

        private Size GetPageSize(double scale) => 
            new Size(this.Page.PageSize.Width.DocToDip() * scale, this.Page.PageSize.Height.DocToDip() * scale);

        protected virtual double GetScaleMultiplier() => 
            1.0;

        private Size Multiply(Size size, double scale) => 
            new Size(size.Width * scale, size.Height * scale);

        protected void OnPageListChanged()
        {
            this.SyncPageInfo();
        }

        internal virtual void SyncPageInfo()
        {
            this.PageIndex = this.Page.Index;
            this.scaleMultiplier = this.GetScaleMultiplier();
            this.PageSize = this.GetPageSize(this.scaleMultiplier);
            this.VisibleSize = this.GetPageSize(this.scaleMultiplier);
            this.PageChanged.Do<EventHandler>(x => x(this, EventArgs.Empty));
            ((IDocumentPage) this).NeedsInvalidate = true;
            this.pageId = this.Page.ID;
        }

        bool IPage.IsLoading =>
            false;

        Thickness IPage.Margin =>
            this.Margin;

        protected virtual Thickness Margin =>
            this.margin;

        public virtual bool IsSelected { get; protected internal set; }

        public virtual DevExpress.XtraPrinting.Page Page =>
            this.PageList[this.PageIndex];

        public virtual DevExpress.XtraPrinting.PageList PageList { get; internal set; }

        public virtual int PageIndex { get; protected set; }

        public virtual Size PageSize { get; protected set; }

        public virtual Size VisibleSize { get; protected set; }

        double IDocumentPage.ScaleMultiplier =>
            this.scaleMultiplier;

        TextureKey IDocumentPage.TextureKey { get; set; }

        bool IDocumentPage.ForceInvalidate { get; set; }

        bool IDocumentPage.NeedsInvalidate { get; set; }

        internal bool ShouldUpdate =>
            this.pageId != this.Page.ID;
    }
}

