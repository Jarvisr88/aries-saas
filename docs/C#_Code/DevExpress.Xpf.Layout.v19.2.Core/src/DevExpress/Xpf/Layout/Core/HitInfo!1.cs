namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public abstract class HitInfo<HitType>
    {
        private Point hitPointCore;
        private HitType hitTestCore;
        private object hitResultCore;
        protected static HitType[] EmptyTests;

        static HitInfo()
        {
            HitInfo<HitType>.EmptyTests = new HitType[0];
        }

        public HitInfo(Point pt)
        {
            this.hitPointCore = pt;
            this.hitTestCore = default(HitType);
        }

        public bool CheckAndSetHitTest(object expected, object hitResult, HitType hitTest)
        {
            bool flag = Equals(hitResult, expected);
            if (flag)
            {
                this.hitTestCore = hitTest;
                this.hitResultCore = hitResult;
            }
            return flag;
        }

        public bool CheckAndSetHitTest(Rect bounds, Point pt, HitType hitTest)
        {
            bool flag = !bounds.IsEmpty && bounds.Contains(pt);
            if (flag)
            {
                this.hitTestCore = hitTest;
            }
            return flag;
        }

        protected virtual bool CheckAndSetHitTest(object element, Rect bounds, Point pt, HitType hitTest)
        {
            bool flag = this.HitTestBounds(element, pt, bounds);
            if (flag)
            {
                this.hitTestCore = hitTest;
            }
            return flag;
        }

        protected virtual HitType[] GetValidHotTests() => 
            HitInfo<HitType>.EmptyTests;

        protected virtual HitType[] GetValidPressedTests() => 
            HitInfo<HitType>.EmptyTests;

        protected virtual bool HitTestBounds(object element, Point pt, Rect bounds) => 
            !bounds.IsEmpty && bounds.Contains(pt);

        public bool IsEqual(HitInfo<HitType> hi) => 
            (hi != null) && Equals(hi.hitTestCore, this.hitTestCore);

        public Point HitPoint =>
            this.hitPointCore;

        public HitType HitTest =>
            this.hitTestCore;

        public virtual object HitResult =>
            this.hitResultCore;

        public abstract bool IsEmpty { get; }

        public virtual bool IsHot =>
            Array.IndexOf<HitType>(this.GetValidHotTests(), this.hitTestCore) != -1;

        public virtual bool IsPressed =>
            Array.IndexOf<HitType>(this.GetValidPressedTests(), this.hitTestCore) != -1;
    }
}

