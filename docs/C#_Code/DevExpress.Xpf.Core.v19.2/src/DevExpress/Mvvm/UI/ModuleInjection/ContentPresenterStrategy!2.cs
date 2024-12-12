namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public class ContentPresenterStrategy<T, TWrapper> : CommonStrategyBase<T, TWrapper> where T: DependencyObject where TWrapper: class, IContentPresenterWrapper<T>, new()
    {
        private bool isFirstInject;

        public ContentPresenterStrategy()
        {
            this.isFirstInject = true;
        }

        protected override void InitializeCore()
        {
            base.InitializeCore();
            this.isFirstInject = true;
            if (base.Wrapper.ContentTemplate == null)
            {
                base.Wrapper.ContentTemplateSelector ??= base.ViewSelector;
            }
        }

        protected override void OnClear()
        {
            base.OnClear();
            base.SelectedViewModel = null;
        }

        protected override void OnInjected(object viewModel)
        {
            base.OnInjected(viewModel);
            if (this.isFirstInject)
            {
                base.SelectedViewModel ??= viewModel;
            }
            this.isFirstInject = false;
        }

        protected override void OnRemoved(object viewModel)
        {
            base.OnRemoved(viewModel);
            if (viewModel == base.SelectedViewModel)
            {
                base.SelectedViewModel = null;
            }
        }

        protected override void OnSelectedViewModelChanged(object oldValue, object newValue)
        {
            base.OnSelectedViewModelChanged(oldValue, newValue);
            base.Wrapper.Content = newValue;
        }
    }
}

