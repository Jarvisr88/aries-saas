namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Threading;

    public abstract class ScrollInfoBase
    {
        private static TimeSpan defaultUpdateInterval = new TimeSpan(0, 0, 0, 0, 50);
        private IScrollInfoOwner scrollOwner;
        private SizeHelperBase sizeHelper;
        private double extent;
        private double viewport;
        protected double fOffset;
        private bool isValid;
        private DispatcherTimer timer;
        private double prevOffset;

        public ScrollInfoBase(IScrollInfoOwner scrollOwner, SizeHelperBase sizeHelper)
        {
            this.scrollOwner = scrollOwner;
            this.sizeHelper = sizeHelper;
        }

        internal double GetScrollableViewportSize() => 
            this.Viewport;

        public void Invalidate()
        {
            this.isValid = false;
        }

        protected bool IsScrollingPerPage() => 
            this.scrollOwner.WheelScrollLines == -1.0;

        public virtual void LineDown()
        {
            this.LinesDown(1.0);
        }

        protected void LinesDown(double lineSize)
        {
            this.SetOffsetForce(this.Offset + lineSize, false);
        }

        protected void LinesUp(double lineSize)
        {
            this.SetOffsetForce(this.Offset - lineSize, false);
        }

        public virtual void LineUp()
        {
            this.LinesUp(1.0);
        }

        public void MouseWheelDown()
        {
            if (this.IsScrollingPerPage())
            {
                this.PageDown();
            }
            else
            {
                this.LinesDown(this.scrollOwner.WheelScrollLines);
            }
        }

        public void MouseWheelUp()
        {
            if (this.IsScrollingPerPage())
            {
                this.PageUp();
            }
            else
            {
                this.LinesUp(this.scrollOwner.WheelScrollLines);
            }
        }

        protected virtual void NeedMeasure()
        {
            this.ScrollOwner.InvalidateMeasure();
        }

        protected abstract bool OnBeforeChangeOffset();
        protected abstract void OnScrollInfoChanged();
        internal void OnTimerTick()
        {
            if (!this.OnBeforeChangeOffset())
            {
                this.fOffset = this.prevOffset;
            }
            this.OnScrollInfoChanged();
            this.NeedMeasure();
            this.prevOffset = double.NaN;
        }

        public void PageDown()
        {
            this.SetOffsetForce(this.Offset + this.GetScrollableViewportSize(), false);
        }

        public void PageUp()
        {
            this.SetOffsetForce(this.Offset - this.GetScrollableViewportSize(), false);
        }

        private void SetDeferredOffset(double value)
        {
            if (double.IsNaN(this.prevOffset))
            {
                this.prevOffset = this.fOffset;
            }
            this.SetOffsetForce(value, false, false);
            if (this.timer == null)
            {
                this.timer = new DispatcherTimer(DispatcherPriority.Render);
                this.timer.Interval = defaultUpdateInterval;
                this.timer.Tick += new EventHandler(this.timer_Tick);
                this.timer.Start();
            }
        }

        public void SetOffset(double value)
        {
            if (this.ScrollOwner.IsDeferredScrolling)
            {
                this.SetDeferredOffset(value);
            }
            else
            {
                this.SetOffsetForce(value, false);
            }
        }

        public virtual void SetOffsetForce(double value, bool skipValidation = false)
        {
            this.SetOffsetForce(value, true, skipValidation);
            this.NeedMeasure();
        }

        private void SetOffsetForce(double value, bool onChanged, bool skipValidation)
        {
            if (!skipValidation)
            {
                value = this.ValidateOffset(value);
            }
            if ((this.fOffset != value) && this.OnBeforeChangeOffset())
            {
                this.fOffset = value;
                if (onChanged)
                {
                    this.OnScrollInfoChanged();
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.timer = null;
            this.OnTimerTick();
        }

        public void UpdateScrollInfo(double viewport, double extent)
        {
            bool flag = false;
            if (this.Extent != extent)
            {
                flag = true;
                this.extent = extent;
            }
            if (this.Viewport != viewport)
            {
                this.viewport = viewport;
                flag = true;
            }
            if (flag || !this.isValid)
            {
                this.isValid = true;
                this.OnScrollInfoChanged();
            }
        }

        protected virtual double ValidateOffset(double value) => 
            this.ValidateOffsetCore(Math.Ceiling(value));

        protected abstract double ValidateOffsetCore(double value);
        protected virtual void ValidateScrollInfo()
        {
        }

        protected virtual IScrollInfoOwner ScrollOwner =>
            this.scrollOwner;

        protected SizeHelperBase SizeHelper =>
            this.sizeHelper;

        public double Viewport =>
            this.viewport;

        public double Extent =>
            this.extent;

        public virtual double Offset =>
            this.fOffset;
    }
}

