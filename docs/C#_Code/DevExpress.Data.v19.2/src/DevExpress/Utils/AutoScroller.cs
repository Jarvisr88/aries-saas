namespace DevExpress.Utils
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public abstract class AutoScroller : IDisposable
    {
        protected internal static readonly int AutoScrollTimerInterval = 100;
        private bool isDisposed;
        private System.Windows.Forms.Timer timer;
        private bool isActive;
        private DevExpress.Utils.MouseHandler mouseHandler;
        private AutoScrollerHotZoneCollection hotZones;
        private AutoScrollerHotZone activeHotZone;

        protected AutoScroller(DevExpress.Utils.MouseHandler mouseHandler)
        {
            if (mouseHandler == null)
            {
                throw new ArgumentNullException();
            }
            this.mouseHandler = mouseHandler;
            this.hotZones = new AutoScrollerHotZoneCollection();
            this.timer = this.CreateTimer();
            this.timer.Interval = AutoScrollTimerInterval;
        }

        public virtual void Activate(Point mousePosition)
        {
            this.isActive = false;
            this.hotZones.Clear();
            this.PopulateHotZones();
            int count = this.hotZones.Count;
            for (int i = 0; i < count; i++)
            {
                this.isActive |= this.hotZones[i].Initialize(mousePosition);
            }
        }

        protected internal virtual AutoScrollerHotZone CalculateActiveHotZone(Point pt)
        {
            int count = this.hotZones.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.hotZones[i].CanActivate(pt))
                {
                    return this.hotZones[i];
                }
            }
            return null;
        }

        protected internal virtual System.Windows.Forms.Timer CreateTimer() => 
            new System.Windows.Forms.Timer();

        public virtual void Deactivate()
        {
            this.isActive = false;
            this.hotZones.Clear();
            this.activeHotZone = null;
        }

        public void Dispose()
        {
            if (!this.isDisposed)
            {
                if (this.timer != null)
                {
                    this.Deactivate();
                    this.StopTimer();
                    this.timer.Dispose();
                    this.timer = null;
                }
                this.hotZones.Clear();
                this.activeHotZone = null;
                this.isDisposed = true;
            }
        }

        public virtual void OnMouseMove(Point pt)
        {
            if (this.isActive)
            {
                AutoScrollerHotZone objA = this.CalculateActiveHotZone(pt);
                if (!ReferenceEquals(objA, this.activeHotZone))
                {
                    this.activeHotZone = objA;
                    if (this.activeHotZone != null)
                    {
                        this.StartTimer();
                    }
                    else
                    {
                        this.StopTimer();
                    }
                }
            }
        }

        protected internal virtual void OnTimerTick(object sender, EventArgs e)
        {
            if (this.activeHotZone != null)
            {
                this.activeHotZone.PerformAutoScroll();
            }
        }

        protected abstract void PopulateHotZones();
        public void Resume()
        {
            this.StartTimer();
        }

        protected internal virtual void StartTimer()
        {
            this.StopTimer();
            this.SubscribeTimerEvents();
            this.timer.Start();
        }

        protected internal virtual void StopTimer()
        {
            this.timer.Stop();
            this.UnsubscribeTimerEvents();
        }

        protected internal virtual void SubscribeTimerEvents()
        {
            this.timer.Tick += new EventHandler(this.OnTimerTick);
        }

        public void Suspend()
        {
            this.StopTimer();
        }

        protected internal virtual void UnsubscribeTimerEvents()
        {
            this.timer.Tick -= new EventHandler(this.OnTimerTick);
        }

        public bool IsDisposed =>
            this.isDisposed;

        public DevExpress.Utils.MouseHandler MouseHandler =>
            this.mouseHandler;

        protected internal System.Windows.Forms.Timer Timer =>
            this.timer;

        protected internal bool IsActive
        {
            get => 
                this.isActive;
            set => 
                this.isActive = value;
        }

        protected internal AutoScrollerHotZoneCollection HotZones =>
            this.hotZones;
    }
}

