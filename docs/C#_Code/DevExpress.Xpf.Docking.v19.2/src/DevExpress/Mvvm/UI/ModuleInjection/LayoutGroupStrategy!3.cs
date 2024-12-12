namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public class LayoutGroupStrategy<TTarget, TChild, TWrapper> : SelectorStrategy<TTarget, TWrapper> where TTarget: LayoutGroup where TChild: ContentItem where TWrapper: LayoutGroupWrapperBase<TTarget, TChild>, new()
    {
        protected override void InitializeCore()
        {
            base.InitializeCore();
            base.Wrapper.Owner = base.Owner;
            base.Wrapper.Closing += new EventHandler<LayoutGroupWrapperBase<TTarget, TChild>.ChildClosingEventArgs>(this.OnItemClosing);
        }

        private void OnItemClosing(object sender, LayoutGroupWrapperBase<TTarget, TChild>.ChildClosingEventArgs e)
        {
            if (!base.Owner.CanRemoveViewModel(e.ViewModel))
            {
                e.Cancel = true;
            }
            else
            {
                base.Owner.RemoveViewModel(e.ViewModel);
                this.Remove(e.ViewModel);
            }
        }

        protected override void OnSelectedViewModelPropertyChanged(object oldValue, object newValue, bool focus)
        {
            base.Owner.SelectViewModel(newValue);
            if (base.Wrapper != null)
            {
                base.Wrapper.SelectItem(newValue, focus);
            }
        }

        protected override void UninitializeCore()
        {
            base.Wrapper.Closing -= new EventHandler<LayoutGroupWrapperBase<TTarget, TChild>.ChildClosingEventArgs>(this.OnItemClosing);
            base.Wrapper.Owner = null;
            base.UninitializeCore();
        }
    }
}

