namespace DevExpress.Utils
{
    using System;
    using System.Drawing;

    public abstract class MouseControllerBase<T> : IDisposable
    {
        private readonly Func<T> getHitInfo;
        private T hitInfo;
        private Point mousePosition;

        public MouseControllerBase(Func<T> getHitInfo)
        {
            Guard.ArgumentNotNull(getHitInfo, "getHitInfo");
            this.getHitInfo = getHitInfo;
            this.mousePosition = Point.Empty;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.hitInfo = default(T);
        }

        public void DoMouseMove(Point mousePosition)
        {
            this.mousePosition = mousePosition;
            this.OnMouseMove();
        }

        protected T GetHitInfo() => 
            this.getHitInfo();

        protected virtual void OnMouseMove()
        {
            this.hitInfo = this.getHitInfo();
        }

        protected T HitInfo =>
            this.hitInfo;

        protected Point MousePosition =>
            this.mousePosition;
    }
}

