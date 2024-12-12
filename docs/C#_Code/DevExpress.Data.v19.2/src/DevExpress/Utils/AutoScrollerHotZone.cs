namespace DevExpress.Utils
{
    using System;
    using System.Drawing;

    public abstract class AutoScrollerHotZone
    {
        private Rectangle bounds;

        protected AutoScrollerHotZone()
        {
        }

        protected abstract Rectangle AdjustHotZoneBounds(Rectangle bounds, Point mousePosition);
        protected abstract Rectangle CalculateHotZoneBounds();
        public abstract bool CanActivate(Point mousePosition);
        public virtual bool Initialize(Point mousePosition)
        {
            this.bounds = this.CalculateHotZoneBounds();
            if ((this.bounds.Width <= 0) || (this.bounds.Height <= 0))
            {
                return false;
            }
            this.bounds = this.AdjustHotZoneBounds(this.bounds, mousePosition);
            return true;
        }

        public abstract void PerformAutoScroll();

        public Rectangle Bounds
        {
            get => 
                this.bounds;
            set => 
                this.bounds = value;
        }
    }
}

