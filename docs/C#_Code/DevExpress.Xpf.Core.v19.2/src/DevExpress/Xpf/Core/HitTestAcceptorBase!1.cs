namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public abstract class HitTestAcceptorBase<T> where T: HitTestVisitorBase
    {
        protected HitTestAcceptorBase()
        {
        }

        public abstract void Accept(FrameworkElement element, T visitor);
    }
}

