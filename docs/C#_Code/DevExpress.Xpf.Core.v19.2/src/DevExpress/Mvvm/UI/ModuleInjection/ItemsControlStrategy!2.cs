namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public class ItemsControlStrategy<T, TWrapper> : CommonStrategyBase<T, TWrapper> where T: DependencyObject where TWrapper: class, IItemsControlWrapper<T>, new()
    {
        protected override void InitializeCore()
        {
            base.InitializeCore();
            this.InitItemsSource();
            this.InitItemTemplate();
        }

        protected virtual void InitItemsSource()
        {
            base.Wrapper.ItemsSource = base.ViewModels;
        }

        protected virtual void InitItemTemplate()
        {
            if (base.Wrapper.ItemTemplate == null)
            {
                base.Wrapper.ItemTemplateSelector ??= base.ViewSelector;
            }
        }

        protected override void OnClear()
        {
            base.OnClear();
            base.SelectedViewModel = null;
        }

        protected override void OnRemoved(object viewModel)
        {
            base.OnRemoved(viewModel);
            if (viewModel == base.SelectedViewModel)
            {
                base.SelectedViewModel = null;
            }
        }
    }
}

