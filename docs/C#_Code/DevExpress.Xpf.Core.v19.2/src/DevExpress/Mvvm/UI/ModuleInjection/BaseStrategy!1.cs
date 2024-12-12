namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class BaseStrategy<T> : IBaseStrategy where T: DependencyObject
    {
        public BaseStrategy()
        {
            this.ViewSelector = new ViewDataTemplateSelector();
        }

        object IBaseStrategy.GetView(object viewModel) => 
            this.GetView(viewModel);

        void IBaseStrategy.Initialize(IStrategyOwner owner)
        {
            if (!this.IsInitialized)
            {
                this.IsInitialized = true;
                this.Owner = owner;
                this.InitializeCore();
            }
        }

        void IBaseStrategy.Uninitialize()
        {
            if (this.IsInitialized)
            {
                this.UninitializeCore();
                this.IsInitialized = false;
            }
        }

        protected virtual object GetView(object viewModel) => 
            this.Target;

        protected virtual void InitializeCore()
        {
        }

        protected virtual void UninitializeCore()
        {
        }

        public bool IsInitialized { get; private set; }

        public IStrategyOwner Owner { get; private set; }

        protected T Target =>
            (T) this.Owner.Target;

        protected ViewDataTemplateSelector ViewSelector { get; private set; }
    }
}

