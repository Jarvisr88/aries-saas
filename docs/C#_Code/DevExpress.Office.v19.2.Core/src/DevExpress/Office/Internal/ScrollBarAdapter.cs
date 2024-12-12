namespace DevExpress.Office.Internal
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Windows.Forms;

    public abstract class ScrollBarAdapter : IScrollBarAdapter, IBatchUpdateable, IBatchUpdateHandler
    {
        private readonly IOfficeScrollbar scrollBar;
        private readonly BatchUpdateHelper batchUpdateHelper;
        private readonly IPlatformSpecificScrollBarAdapter adapter;
        private double factor = 1.0;
        private long minimum;
        private long maximum;
        private long val;
        private long largeChange;
        private bool enabled;
        private ScrollEventHandler onScroll;

        public event ScrollEventHandler Scroll
        {
            add
            {
                this.onScroll += value;
            }
            remove
            {
                this.onScroll -= value;
            }
        }

        protected ScrollBarAdapter(IOfficeScrollbar scrollBar, IPlatformSpecificScrollBarAdapter adapter)
        {
            Guard.ArgumentNotNull(scrollBar, "scrollBar");
            Guard.ArgumentNotNull(adapter, "adapter");
            this.scrollBar = scrollBar;
            this.adapter = adapter;
            this.batchUpdateHelper = new BatchUpdateHelper(this);
        }

        public virtual void Activate()
        {
            this.RefreshValuesFromScrollBar();
            this.SubscribeScrollbarEvents();
        }

        public virtual void ApplyValuesToScrollBar()
        {
            if (this.DeferredScrollBarUpdate)
            {
                this.Synchronized = false;
            }
            else
            {
                this.ApplyValuesToScrollBarCore();
            }
        }

        protected internal virtual void ApplyValuesToScrollBarCore()
        {
            this.Adapter.ApplyValuesToScrollBarCore(this);
            this.Synchronized = true;
        }

        public void BeginUpdate()
        {
            this.batchUpdateHelper.BeginUpdate();
        }

        public void CancelUpdate()
        {
            this.batchUpdateHelper.CancelUpdate();
        }

        public virtual ScrollEventArgs CreateEmulatedScrollEventArgs(ScrollEventType eventType)
        {
            this.EnsureSynchronized();
            if (eventType <= ScrollEventType.SmallIncrement)
            {
                if (eventType == ScrollEventType.SmallDecrement)
                {
                    return new ScrollEventArgs(eventType, this.ScrollBar.Value - this.ScrollBar.SmallChange);
                }
                if (eventType == ScrollEventType.SmallIncrement)
                {
                    return new ScrollEventArgs(eventType, this.ScrollBar.Value + this.ScrollBar.SmallChange);
                }
            }
            else
            {
                if (eventType == ScrollEventType.First)
                {
                    return new ScrollEventArgs(eventType, this.ScrollBar.Minimum);
                }
                if (eventType == ScrollEventType.Last)
                {
                    return this.adapter.CreateLastScrollEventArgs(this);
                }
            }
            Exceptions.ThrowInternalException();
            return null;
        }

        public virtual void Deactivate()
        {
            this.UnsubscribeScrollbarEvents();
        }

        void IBatchUpdateHandler.OnBeginUpdate()
        {
        }

        void IBatchUpdateHandler.OnCancelUpdate()
        {
        }

        void IBatchUpdateHandler.OnEndUpdate()
        {
        }

        void IBatchUpdateHandler.OnFirstBeginUpdate()
        {
        }

        void IBatchUpdateHandler.OnLastCancelUpdate()
        {
            this.OnLastEndUpdateCore();
        }

        void IBatchUpdateHandler.OnLastEndUpdate()
        {
            this.OnLastEndUpdateCore();
        }

        public void EndUpdate()
        {
            this.batchUpdateHelper.EndUpdate();
        }

        public virtual void EnsureSynchronized()
        {
            this.EnsureSynchronizedCore();
        }

        public bool EnsureSynchronizedCore()
        {
            if (!this.ShouldSynchronize())
            {
                return false;
            }
            this.ApplyValuesToScrollBarCore();
            return true;
        }

        public virtual int GetPageDownRawScrollBarValue()
        {
            this.EnsureSynchronized();
            return this.Adapter.GetPageDownRawScrollBarValue(this);
        }

        public virtual int GetPageUpRawScrollBarValue()
        {
            this.EnsureSynchronized();
            return this.Adapter.GetPageUpRawScrollBarValue(this);
        }

        public int GetRawScrollBarValue()
        {
            this.EnsureSynchronized();
            return this.Adapter.GetRawScrollBarValue(this);
        }

        protected internal virtual void OnLastEndUpdateCore()
        {
            this.ValidateValues();
        }

        protected internal virtual void OnScroll(object sender, ScrollEventArgs e)
        {
            this.Adapter.OnScroll(this, sender, e);
        }

        public virtual void RaiseScroll(ScrollEventArgs args)
        {
            if (this.onScroll != null)
            {
                this.onScroll(this.ScrollBar, args);
            }
        }

        public virtual void RefreshValuesFromScrollBar()
        {
            this.BeginUpdate();
            try
            {
                this.Minimum = (long) Math.Round((double) (((double) this.ScrollBar.Minimum) / this.Factor));
                this.Maximum = (long) Math.Round((double) (((double) this.ScrollBar.Maximum) / this.Factor));
                this.LargeChange = (long) Math.Round((double) (((double) this.ScrollBar.LargeChange) / this.Factor));
                this.Value = (long) Math.Round((double) (((double) this.ScrollBar.Value) / this.Factor));
                this.Enabled = this.ScrollBar.Enabled;
            }
            finally
            {
                this.EndUpdate();
            }
            this.Synchronized = true;
        }

        public bool SetRawScrollBarValue(int value) => 
            this.Adapter.SetRawScrollBarValue(this, value);

        protected internal virtual bool ShouldSynchronize() => 
            this.DeferredScrollBarUpdate && !this.Synchronized;

        protected internal virtual void SubscribeScrollbarEvents()
        {
            this.ScrollBar.Scroll += new ScrollEventHandler(this.OnScroll);
        }

        public virtual bool SynchronizeScrollBarAvoidJump()
        {
            if (!this.ShouldSynchronize() || ((((float) this.Value) / Math.Max(1f, (float) (((this.Maximum - this.LargeChange) + 1L) - this.Minimum))) > (((double) this.ScrollBar.Value) / Math.Max(1.0, (double) (((this.ScrollBar.Maximum - this.ScrollBar.LargeChange) + 1) - this.ScrollBar.Minimum)))))
            {
                return false;
            }
            this.ApplyValuesToScrollBarCore();
            return true;
        }

        protected internal virtual void UnsubscribeScrollbarEvents()
        {
            this.ScrollBar.Scroll -= new ScrollEventHandler(this.OnScroll);
        }

        protected internal virtual void ValidateValues()
        {
            if (!this.IsUpdateLocked)
            {
                this.ValidateValuesCore();
            }
        }

        protected internal virtual void ValidateValuesCore()
        {
            if (this.LargeChange < 0L)
            {
                Exceptions.ThrowArgumentException("LargeChange", this.LargeChange);
            }
            if (this.Minimum > this.Maximum)
            {
                this.maximum = this.Minimum;
            }
            this.largeChange = Math.Min(this.LargeChange, (this.Maximum - this.Minimum) + 1L);
            this.val = Math.Max(this.Value, this.Minimum);
            this.val = Math.Min(this.Value, (this.Maximum - this.LargeChange) + 1L);
        }

        public double Factor
        {
            get => 
                this.factor;
            set => 
                this.factor = value;
        }

        protected internal abstract bool DeferredScrollBarUpdate { get; }

        protected internal abstract bool Synchronized { get; set; }

        public long Minimum
        {
            get => 
                this.minimum;
            set
            {
                if (!this.minimum.Equals(value))
                {
                    this.minimum = value;
                    this.ValidateValues();
                }
            }
        }

        public long Maximum
        {
            get => 
                this.maximum;
            set
            {
                if (!this.maximum.Equals(value))
                {
                    this.maximum = value;
                    this.ValidateValues();
                }
            }
        }

        public long Value
        {
            get => 
                this.val;
            set
            {
                if (!this.val.Equals(value))
                {
                    this.val = value;
                    this.ValidateValues();
                }
            }
        }

        public long LargeChange
        {
            get => 
                this.largeChange;
            set
            {
                if (!this.largeChange.Equals(value))
                {
                    this.largeChange = value;
                    this.ValidateValues();
                }
            }
        }

        public bool Enabled
        {
            get => 
                this.enabled;
            set => 
                this.enabled = value;
        }

        public IOfficeScrollbar ScrollBar =>
            this.scrollBar;

        protected internal IPlatformSpecificScrollBarAdapter Adapter =>
            this.adapter;

        BatchUpdateHelper IBatchUpdateable.BatchUpdateHelper =>
            this.batchUpdateHelper;

        public bool IsUpdateLocked =>
            this.batchUpdateHelper.IsUpdateLocked;
    }
}

