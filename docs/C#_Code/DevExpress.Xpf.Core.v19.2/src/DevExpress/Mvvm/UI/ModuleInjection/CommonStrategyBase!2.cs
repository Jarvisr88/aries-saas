namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class CommonStrategyBase<T, TWrapper> : StrategyBase<T> where T: DependencyObject where TWrapper: class, ITargetWrapper<T>, new()
    {
        protected CommonStrategyBase()
        {
        }

        protected override void InitializeCore()
        {
            base.InitializeCore();
            TWrapper local1 = Activator.CreateInstance<TWrapper>();
            local1.Target = base.Target;
            this.Wrapper = local1;
        }

        protected override void UninitializeCore()
        {
            T local = default(T);
            this.Wrapper.Target = local;
            TWrapper local2 = default(TWrapper);
            this.Wrapper = local2;
            base.UninitializeCore();
        }

        protected TWrapper Wrapper { get; private set; }
    }
}

