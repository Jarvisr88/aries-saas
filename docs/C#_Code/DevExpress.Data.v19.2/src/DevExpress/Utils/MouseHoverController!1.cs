namespace DevExpress.Utils
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class MouseHoverController<T> : MouseControllerBase<T>
    {
        private Timer timer;
        private readonly Action<T> action;
        private Point prevPoint;
        private Size hoverSize;

        public MouseHoverController(Action<T> action, Func<T> getHitInfo) : base(getHitInfo)
        {
            Guard.ArgumentNotNull(action, "action");
            this.action = action;
            this.prevPoint = Point.Empty;
            this.hoverSize = SystemInformation.MouseHoverSize;
            this.timer = new Timer();
            this.timer.Tick += delegate (object s, EventArgs e) {
                ((MouseHoverController<T>) this).StopTimer();
                if (MouseHoverController<T>.CanRaiseHoverEvent(((MouseHoverController<T>) this).prevPoint, ((MouseHoverController<T>) this).MousePosition, ((MouseHoverController<T>) this).hoverSize))
                {
                    ((MouseHoverController<T>) this).prevPoint = ((MouseHoverController<T>) this).MousePosition;
                    action(((MouseHoverController<T>) this).GetHitInfo());
                }
            };
            this.InitializeTimer(this.timer);
        }

        private static bool CanRaiseHoverEvent(Point prevPoint, Point nextPoint, Size hoverSize)
        {
            int num2 = Math.Abs((int) (nextPoint.Y - prevPoint.Y));
            return ((Math.Abs((int) (nextPoint.X - prevPoint.X)) >= hoverSize.Width) || (num2 >= hoverSize.Height));
        }

        private void DestroyTimer(Timer timer)
        {
            if (timer != null)
            {
                timer.Dispose();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DestroyTimer(this.timer);
            }
            this.timer = null;
            base.Dispose(disposing);
        }

        protected virtual void InitializeTimer(Timer timer)
        {
            timer.Interval = SystemInformation.MouseHoverTime;
        }

        protected override void OnMouseMove()
        {
            this.ResetTimer();
        }

        private void ResetTimer()
        {
            this.StopTimer();
            this.StartTimer();
        }

        private void StartTimer()
        {
            this.timer.Enabled = true;
        }

        private void StopTimer()
        {
            this.timer.Enabled = false;
        }
    }
}

