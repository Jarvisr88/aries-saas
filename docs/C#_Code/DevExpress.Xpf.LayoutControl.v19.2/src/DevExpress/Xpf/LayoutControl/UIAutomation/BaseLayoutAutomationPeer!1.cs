namespace DevExpress.Xpf.LayoutControl.UIAutomation
{
    using System;

    public abstract class BaseLayoutAutomationPeer<T> : BaseLayoutAutomationPeer where T: FrameworkElement
    {
        protected BaseLayoutAutomationPeer(T owner) : base(owner)
        {
        }

        public T Owner =>
            (T) base.Owner;
    }
}

