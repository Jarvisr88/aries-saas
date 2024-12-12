namespace DevExpress.Mvvm.Native
{
    using System;

    public class SLWeakReferenceActionInvoker : StrongReferenceActionInvoker
    {
        public SLWeakReferenceActionInvoker(object target, Action action) : base(target, action)
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

