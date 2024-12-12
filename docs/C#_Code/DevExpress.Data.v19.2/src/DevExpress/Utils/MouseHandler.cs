namespace DevExpress.Utils
{
    using DevExpress.Utils.KeyboardHandler;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public abstract class MouseHandler : IDisposable
    {
        public static readonly MouseEventArgs EmptyMouseEventArgs = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);
        private bool isDisposed;
        private bool suspended;
        private int clickCount;
        private Point clickScreenPoint;
        private MouseHandlerState state;
        private Timer clickTimer;
        private IOfficeScroller officeScroller;
        private DevExpress.Utils.AutoScroller autoScroller;

        protected MouseHandler()
        {
        }

        protected abstract void CalculateAndSaveHitInfo(MouseEventArgs e);
        protected virtual MouseEventArgs ConvertMouseEventArgs(MouseEventArgs screenMouseEventArgs) => 
            screenMouseEventArgs;

        protected abstract DevExpress.Utils.AutoScroller CreateAutoScroller();
        protected abstract IOfficeScroller CreateOfficeScroller();
        protected internal virtual Timer CreateTimer() => 
            new Timer();

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.autoScroller != null)
                {
                    this.autoScroller.Dispose();
                    this.autoScroller = null;
                }
                if (this.officeScroller != null)
                {
                    this.officeScroller.Dispose();
                    this.officeScroller = null;
                }
                if (this.clickTimer != null)
                {
                    this.clickTimer.Dispose();
                    this.clickTimer = null;
                }
                if (this.state != null)
                {
                    this.state.Finish();
                    this.state = null;
                }
            }
            this.isDisposed = true;
        }

        protected internal Size GetDoubleClickSize() => 
            SystemInformation.DoubleClickSize;

        public Size GetDragSize() => 
            SystemInformation.DragSize;

        protected abstract void HandleClickTimerTick();
        protected virtual void HandleMouseDoubleClick(MouseEventArgs e)
        {
            this.State.OnMouseDoubleClick(e);
        }

        protected virtual void HandleMouseDown(MouseEventArgs e)
        {
            this.State.OnMouseDown(e);
        }

        protected virtual void HandleMouseMove(MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            this.CalculateAndSaveHitInfo(e);
            this.State.OnMouseMove(e);
            this.AutoScroller.OnMouseMove(pt);
        }

        protected virtual void HandleMouseTripleClick(MouseEventArgs e)
        {
            this.State.OnMouseTripleClick(e);
        }

        protected virtual void HandleMouseUp(MouseEventArgs e)
        {
            this.CalculateAndSaveHitInfo(e);
            this.State.OnMouseUp(e);
        }

        protected abstract void HandleMouseWheel(MouseEventArgs e);
        protected virtual bool HandlePopupMenu(MouseEventArgs e) => 
            this.State.OnPopupMenu(e);

        public virtual void Initialize()
        {
            this.officeScroller = this.CreateOfficeScroller();
            this.clickTimer = this.CreateTimer();
            this.autoScroller = this.CreateAutoScroller();
            this.SwitchToDefaultState();
        }

        protected virtual bool IsDoubleClick(MouseEventArgs e) => 
            this.IsMultipleClickCore(e) && (this.ClickCount == 2);

        protected internal virtual bool IsMultipleClickCore(MouseEventArgs e) => 
            (this.IsClickTimerActive && (e.Button == MouseButtons.Left)) && ((Math.Abs((int) (e.X - this.clickScreenPoint.X)) <= this.GetDoubleClickSize().Width) && (Math.Abs((int) (e.Y - this.clickScreenPoint.Y)) <= this.GetDoubleClickSize().Height));

        protected internal virtual bool IsTripleClick(MouseEventArgs e) => 
            this.IsMultipleClickCore(e) && (this.SupportsTripleClick && (this.ClickCount == 3));

        public virtual void OnClickTimerTick(object sender, EventArgs e)
        {
            this.ClickCount = 0;
            if (this.IsClickTimerActive)
            {
                this.HandleClickTimerTick();
            }
            this.StopClickTimer();
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Middle) != MouseButtons.None)
            {
                this.StartOfficeScroller(new Point(e.X, e.Y));
            }
            else
            {
                this.CalculateAndSaveHitInfo(e);
                int clickCount = this.ClickCount;
                this.ClickCount = clickCount + 1;
                if (this.IsTripleClick(e))
                {
                    this.StopClickTimer();
                    this.HandleMouseTripleClick(this.ConvertMouseEventArgs(e));
                }
                else if (!this.IsDoubleClick(e))
                {
                    this.StopClickTimer();
                    this.RunClickTimer();
                    this.ClickCount = 1;
                    this.clickScreenPoint = new Point(e.X, e.Y);
                    this.HandleMouseDown(this.ConvertMouseEventArgs(e));
                }
                else
                {
                    this.StopClickTimer();
                    if (this.SupportsTripleClick)
                    {
                        this.RunClickTimer();
                        this.ClickCount = 2;
                    }
                    this.HandleMouseDoubleClick(this.ConvertMouseEventArgs(e));
                }
            }
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
            this.HandleMouseMove(this.ConvertMouseEventArgs(e));
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
            this.HandleMouseUp(this.ConvertMouseEventArgs(e));
        }

        public virtual void OnMouseWheel(MouseEventArgs e)
        {
            this.HandleMouseWheel(this.ConvertMouseEventArgs(e));
        }

        public virtual bool OnPopupMenu(MouseEventArgs e) => 
            this.HandlePopupMenu(this.ConvertMouseEventArgs(e));

        public void Resume()
        {
            this.suspended = false;
        }

        public virtual void RunClickTimer()
        {
            this.ClickTimer.Interval = SystemInformation.DoubleClickTime;
            this.clickTimer.Tick += new EventHandler(this.OnClickTimerTick);
            this.ClickTimer.Start();
        }

        protected abstract void StartOfficeScroller(Point clientPoint);
        public virtual void StopClickTimer()
        {
            if (this.ClickTimer != null)
            {
                this.ClickTimer.Stop();
                this.ClickTimer.Tick -= new EventHandler(this.OnClickTimerTick);
            }
        }

        public void Suspend()
        {
            this.suspended = true;
        }

        public virtual void SwitchStateCore(MouseHandlerState newState, Point mousePosition)
        {
            if (newState != null)
            {
                this.AutoScroller.Deactivate();
                if (this.state != null)
                {
                    this.state.Finish();
                }
                this.state = newState;
                if (this.state.AutoScrollEnabled)
                {
                    this.AutoScroller.Activate(mousePosition);
                }
                this.state.Start();
            }
        }

        public abstract void SwitchToDefaultState();

        public bool IsDisposed =>
            this.isDisposed;

        protected virtual bool SupportsTripleClick =>
            false;

        protected internal bool Suspended =>
            this.suspended;

        protected internal int ClickCount
        {
            get => 
                this.clickCount;
            set => 
                this.clickCount = value;
        }

        public virtual bool IsControlPressed =>
            DevExpress.Utils.KeyboardHandler.KeyboardHandler.IsControlPressed;

        public MouseHandlerState State =>
            this.state;

        public Timer ClickTimer =>
            this.clickTimer;

        public bool IsClickTimerActive =>
            (this.clickTimer != null) ? this.clickTimer.Enabled : false;

        public IOfficeScroller OfficeScroller =>
            this.officeScroller;

        public DevExpress.Utils.AutoScroller AutoScroller =>
            this.autoScroller;
    }
}

