namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Reflection;

    public class WeakReferenceActionInvoker : WeakReferenceActionInvokerBase
    {
        public WeakReferenceActionInvoker(object target, Action action) : base(target, action)
        {
        }

        protected override void Execute(object parameter)
        {
            MethodInfo actionMethod = base.ActionMethod;
            object target = base.ActionTargetReference.Target;
            if ((actionMethod != null) && (target != null))
            {
                actionMethod.Invoke(target, null);
            }
        }
    }
}

