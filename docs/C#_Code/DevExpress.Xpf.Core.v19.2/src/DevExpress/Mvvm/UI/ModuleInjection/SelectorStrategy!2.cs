namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public class SelectorStrategy<T, TWrapper> : ItemsControlStrategy<T, TWrapper> where T: DependencyObject where TWrapper: class, ISelectorWrapper<T>, new()
    {
        protected override void InitializeCore()
        {
            base.InitializeCore();
            base.Wrapper.SelectionChanged += new EventHandler(this.OnTargetControlSelectionChanged);
        }

        protected override void OnSelectedViewModelChanged(object oldValue, object newValue)
        {
            base.OnSelectedViewModelChanged(oldValue, newValue);
            base.Wrapper.SelectedItem = newValue;
        }

        protected virtual void OnTargetControlSelectionChanged(object sender, EventArgs e)
        {
            base.SelectedViewModel = base.Wrapper.SelectedItem;
        }

        protected override void UninitializeCore()
        {
            base.Wrapper.SelectionChanged -= new EventHandler(this.OnTargetControlSelectionChanged);
            base.UninitializeCore();
        }
    }
}

