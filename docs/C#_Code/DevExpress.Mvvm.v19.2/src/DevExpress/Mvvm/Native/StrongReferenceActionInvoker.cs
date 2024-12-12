namespace DevExpress.Mvvm.Native
{
    using System;

    public class StrongReferenceActionInvoker : StrongReferenceActionInvokerBase
    {
        public StrongReferenceActionInvoker(object target, Action action) : base(target, action)
        {
        }

        protected override void Execute(object parameter)
        {
            ((Action) base.ActionToInvoke)();
        }
    }
}

