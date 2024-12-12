namespace DevExpress.Utils
{
    using System;

    public class MouseHitTestController<T> : MouseControllerBase<T>
    {
        private readonly Action<T, T> action;

        public MouseHitTestController(Action<T, T> action, Func<T> getHitInfo) : base(getHitInfo)
        {
            Guard.ArgumentNotNull(action, "action");
            this.action = action;
        }

        protected override void OnMouseMove()
        {
            this.action(base.HitInfo, base.GetHitInfo());
            base.OnMouseMove();
        }
    }
}

