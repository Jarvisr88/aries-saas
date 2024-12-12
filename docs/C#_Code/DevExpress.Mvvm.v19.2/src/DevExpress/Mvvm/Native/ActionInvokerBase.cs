namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class ActionInvokerBase : IActionInvoker
    {
        private WeakReference targetReference;

        public ActionInvokerBase(object target)
        {
            this.targetReference = (target == null) ? null : new WeakReference(target);
        }

        protected abstract void ClearCore();
        void IActionInvoker.ClearIfMatched(Delegate action, object recipient)
        {
            object target = this.Target;
            if ((recipient == target) && ((action == null) || (action.Method.Name == this.MethodName)))
            {
                this.targetReference = null;
                this.ClearCore();
            }
        }

        void IActionInvoker.ExecuteIfMatched(Type messageTargetType, object parameter)
        {
            object target = this.Target;
            if ((target != null) && ((messageTargetType == null) || messageTargetType.IsAssignableFrom(target.GetType())))
            {
                this.Execute(parameter);
            }
        }

        protected abstract void Execute(object parameter);

        public object Target
        {
            get
            {
                Func<WeakReference, object> evaluator = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<WeakReference, object> local1 = <>c.<>9__3_0;
                    evaluator = <>c.<>9__3_0 = x => x.Target;
                }
                return this.targetReference.With<WeakReference, object>(evaluator);
            }
        }

        protected abstract string MethodName { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ActionInvokerBase.<>c <>9 = new ActionInvokerBase.<>c();
            public static Func<WeakReference, object> <>9__3_0;

            internal object <get_Target>b__3_0(WeakReference x) => 
                x.Target;
        }
    }
}

