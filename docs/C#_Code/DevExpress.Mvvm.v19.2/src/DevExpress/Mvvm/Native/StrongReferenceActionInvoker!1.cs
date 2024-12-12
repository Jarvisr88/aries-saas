namespace DevExpress.Mvvm.Native
{
    using System;

    public class StrongReferenceActionInvoker<T> : StrongReferenceActionInvokerBase
    {
        public StrongReferenceActionInvoker(object target, Action<T> action) : base(target, action)
        {
        }

        protected override void Execute(object parameter)
        {
            ((Action<T>) base.ActionToInvoke)((T) parameter);
        }
    }
}

