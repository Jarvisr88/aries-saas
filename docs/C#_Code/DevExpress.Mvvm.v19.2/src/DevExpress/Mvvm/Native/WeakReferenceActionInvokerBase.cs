namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class WeakReferenceActionInvokerBase : ActionInvokerBase
    {
        public WeakReferenceActionInvokerBase(object target, Delegate action) : base(target)
        {
            this.ActionMethod = action.Method;
            this.ActionTargetReference = new WeakReference(action.Target);
        }

        protected override void ClearCore()
        {
            this.ActionTargetReference = null;
            this.ActionMethod = null;
        }

        protected MethodInfo ActionMethod { get; private set; }

        protected WeakReference ActionTargetReference { get; private set; }

        protected override string MethodName =>
            this.ActionMethod.Name;
    }
}

