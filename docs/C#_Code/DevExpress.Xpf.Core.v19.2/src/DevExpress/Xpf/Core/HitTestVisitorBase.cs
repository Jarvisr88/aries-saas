namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class HitTestVisitorBase
    {
        private DependencyObject hitElement;

        protected HitTestVisitorBase()
        {
            this.CanContinue = true;
            this.HitTestingInProgressLocker = new Locker();
        }

        internal void SetHitElement(DependencyObject hitElement)
        {
            this.hitElement = hitElement;
        }

        protected void StopHitTesting()
        {
            this.CanContinue = false;
        }

        internal Locker HitTestingInProgressLocker { get; private set; }

        internal bool CanContinue { get; set; }

        protected DependencyObject HitElement =>
            this.hitElement;
    }
}

