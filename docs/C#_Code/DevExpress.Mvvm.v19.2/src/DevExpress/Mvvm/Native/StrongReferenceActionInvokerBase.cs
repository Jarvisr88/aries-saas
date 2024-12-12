namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class StrongReferenceActionInvokerBase : ActionInvokerBase
    {
        public StrongReferenceActionInvokerBase(object target, Delegate action) : base(target)
        {
            this.ActionToInvoke = action;
        }

        protected override void ClearCore()
        {
            this.ActionToInvoke = null;
        }

        protected Delegate ActionToInvoke { get; private set; }

        protected override string MethodName =>
            this.ActionToInvoke.Method.Name;
    }
}

