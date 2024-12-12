namespace DevExpress.Office.Internal
{
    using System;
    using System.Windows.Forms;

    public abstract class OfficeScrollControllerBase
    {
        private IScrollBarAdapter scrollBarAdapter;

        protected OfficeScrollControllerBase()
        {
        }

        public virtual void Activate()
        {
            this.ScrollBarAdapter.Activate();
            this.SubscribeScrollBarAdapterEvents();
        }

        protected abstract DevExpress.Office.Internal.ScrollBarAdapter CreateScrollBarAdapter();
        public virtual void Deactivate()
        {
            this.ScrollBarAdapter.Deactivate();
            this.UnsubscribeScrollBarAdapterEvents();
        }

        protected internal void EmulateScroll(ScrollEventType eventType)
        {
            ScrollEventArgs e = this.ScrollBarAdapter.CreateEmulatedScrollEventArgs(eventType);
            this.OnScroll(this.ScrollBar, e);
        }

        protected internal virtual void Initialize()
        {
            this.scrollBarAdapter = this.CreateScrollBarAdapter();
            this.ScrollBarAdapter.RefreshValuesFromScrollBar();
        }

        public virtual bool IsScrollPossible() => 
            ((this.ScrollBarAdapter.Maximum - this.ScrollBarAdapter.Minimum) > this.ScrollBarAdapter.LargeChange) && ((this.ScrollBarAdapter.Value >= this.ScrollBarAdapter.Minimum) && (this.ScrollBarAdapter.Value <= ((this.ScrollBarAdapter.Maximum - this.ScrollBarAdapter.LargeChange) + 1L)));

        protected abstract void OnScroll(object sender, ScrollEventArgs e);
        protected internal virtual void SubscribeScrollBarAdapterEvents()
        {
            this.ScrollBarAdapter.Scroll += new ScrollEventHandler(this.OnScroll);
        }

        protected internal virtual void SynchronizeScrollbar()
        {
            this.UnsubscribeScrollBarAdapterEvents();
            try
            {
                this.ScrollBarAdapter.ApplyValuesToScrollBar();
            }
            finally
            {
                this.SubscribeScrollBarAdapterEvents();
            }
        }

        protected internal virtual void UnsubscribeScrollBarAdapterEvents()
        {
            this.ScrollBarAdapter.Scroll -= new ScrollEventHandler(this.OnScroll);
        }

        public virtual void UpdateScrollBar()
        {
            if (this.ScrollBar != null)
            {
                this.UpdateScrollBarAdapter();
                this.SynchronizeScrollbar();
            }
        }

        protected abstract void UpdateScrollBarAdapter();

        protected internal virtual bool SupportsDeferredUpdate =>
            false;

        public virtual IScrollBarAdapter ScrollBarAdapter =>
            this.scrollBarAdapter;

        public abstract IOfficeScrollbar ScrollBar { get; }
    }
}

