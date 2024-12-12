namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class WindowStrategyBase<T> : BaseStrategy<T>, IWindowStrategy, IBaseStrategy where T: DependencyObject
    {
        protected WindowStrategyBase()
        {
        }

        protected abstract void ActivateCore();
        protected abstract void AfterShowDialogCore();
        private void BeforeShow(object viewModel, Type viewType)
        {
            this.ViewModel = viewModel;
            if (viewType != null)
            {
                base.ViewSelector.Add(this.ViewModel, viewType);
            }
        }

        protected abstract void CloseCore();
        void IWindowStrategy.Activate()
        {
            this.ActivateCore();
        }

        void IWindowStrategy.Close()
        {
            if (base.IsInitialized)
            {
                this.CloseCore();
                this.Uninitialize();
            }
        }

        void IWindowStrategy.Show(object viewModel, Type viewType)
        {
            this.BeforeShow(viewModel, viewType);
            this.ShowCore();
        }

        void IWindowStrategy.ShowDialog(object viewModel, Type viewType)
        {
            this.BeforeShow(viewModel, viewType);
            this.Result = new MessageBoxResult?(this.ShowDialogCore());
            this.AfterShowDialogCore();
        }

        protected abstract void ShowCore();
        protected abstract MessageBoxResult ShowDialogCore();
        protected override void UninitializeCore()
        {
            if (this.ViewModel != null)
            {
                base.ViewSelector.Remove(this.ViewModel);
            }
            this.ViewModel = null;
            base.UninitializeCore();
        }

        public object ViewModel { get; private set; }

        public MessageBoxResult? Result { get; private set; }
    }
}

