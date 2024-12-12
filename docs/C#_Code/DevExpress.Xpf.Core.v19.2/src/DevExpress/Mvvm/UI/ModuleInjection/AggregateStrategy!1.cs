namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public abstract class AggregateStrategy<T> : BaseStrategy<T>, IStrategy, IBaseStrategy where T: DependencyObject
    {
        private IStrategy actualStrategy;

        protected AggregateStrategy()
        {
        }

        void IStrategy.Clear()
        {
            this.actualStrategy.Clear();
        }

        object IStrategy.GetParentViewModel() => 
            this.actualStrategy.GetParentViewModel();

        void IStrategy.Inject(object viewModel, Type viewType)
        {
            this.actualStrategy.Inject(viewModel, viewType);
        }

        void IStrategy.Remove(object viewModel)
        {
            this.actualStrategy.Remove(viewModel);
        }

        void IStrategy.Select(object viewModel, bool focus)
        {
            this.actualStrategy.Select(viewModel, focus);
        }

        protected override void InitializeCore()
        {
            base.InitializeCore();
            this.actualStrategy ??= this.SelectStrategy();
            this.actualStrategy.Initialize(base.Owner);
        }

        protected abstract IStrategy SelectStrategy();
        protected override void UninitializeCore()
        {
            this.actualStrategy.Uninitialize();
            base.UninitializeCore();
        }

        object IStrategy.SelectedViewModel =>
            this.actualStrategy.SelectedViewModel;
    }
}

