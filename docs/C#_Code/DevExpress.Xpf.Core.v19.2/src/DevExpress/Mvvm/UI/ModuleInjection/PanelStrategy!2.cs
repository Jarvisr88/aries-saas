namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Linq;
    using System.Windows.Controls;

    public class PanelStrategy<T, TWrapper> : CommonStrategyBase<T, TWrapper> where T: DependencyObject where TWrapper: class, IPanelWrapper<T>, new()
    {
        private ContentPresenter FindChild(object viewModel) => 
            base.Wrapper.Children.OfType<ContentPresenter>().FirstOrDefault<ContentPresenter>(x => x.Content == viewModel);

        protected override void OnClear()
        {
            base.OnClear();
            base.SelectedViewModel = null;
        }

        protected override void OnClearing()
        {
            base.OnClearing();
            foreach (object obj2 in base.ViewModels)
            {
                ContentPresenter child = this.FindChild(obj2);
                base.Wrapper.RemoveChild(child);
            }
        }

        protected override void OnInjected(object viewModel)
        {
            base.OnInjected(viewModel);
            ContentPresenter child = new ContentPresenter();
            child.Content = viewModel;
            child.ContentTemplateSelector = base.ViewSelector;
            base.Wrapper.AddChild(child);
        }

        protected override void OnRemoved(object viewModel)
        {
            base.OnRemoved(viewModel);
            ContentPresenter child = this.FindChild(viewModel);
            base.Wrapper.RemoveChild(child);
            if (viewModel == base.SelectedViewModel)
            {
                base.SelectedViewModel = null;
            }
        }
    }
}

