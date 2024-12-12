namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Reflection;

    public class WeakReferenceActionInvoker<T> : WeakReferenceActionInvokerBase
    {
        public WeakReferenceActionInvoker(object target, Action<T> action) : base(target, action)
        {
        }

        protected override void Execute(object parameter)
        {
            MethodInfo actionMethod = base.ActionMethod;
            object target = base.ActionTargetReference.Target;
            if ((actionMethod != null) && (target != null))
            {
                object[] parameters = new object[] { (T) parameter };
                actionMethod.Invoke(target, parameters);
            }
        }
    }
}

