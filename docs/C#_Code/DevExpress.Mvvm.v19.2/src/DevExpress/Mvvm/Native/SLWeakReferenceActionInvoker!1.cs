namespace DevExpress.Mvvm.Native
{
    using System;

    public class SLWeakReferenceActionInvoker<T> : StrongReferenceActionInvoker<T>
    {
        public SLWeakReferenceActionInvoker(object target, Action<T> action) : base(target, action)
        {
        }

        protected override void Execute(object parameter)
        {
            if (base.ActionToInvoke != null)
            {
                base.Execute(parameter);
            }
        }
    }
}

