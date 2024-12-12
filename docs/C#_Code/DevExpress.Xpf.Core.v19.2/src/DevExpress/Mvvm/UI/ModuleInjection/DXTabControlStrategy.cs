namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Xpf.Core;
    using System;

    public class DXTabControlStrategy : SelectorStrategy<DXTabControl, DXTabControlWrapper>
    {
        protected override void InitializeCore()
        {
            base.InitializeCore();
            base.Target.TabRemoving += new TabControlTabRemovingEventHandler(this.OnTargetTabRemoving);
        }

        protected override void OnSelectedViewModelChanged(object oldValue, object newValue)
        {
            base.OnSelectedViewModelChanged(oldValue, newValue);
            DXTabItem tabItem = base.Target.GetTabItem(newValue);
            if ((tabItem != null) && !tabItem.IsVisible)
            {
                base.Target.ShowTabItem(tabItem, true);
            }
        }

        private void OnTargetTabRemoving(object sender, TabControlTabRemovingEventArgs e)
        {
            e.Cancel = true;
            if (base.Owner.CanRemoveViewModel(e.Item))
            {
                base.Owner.RemoveViewModel(e.Item);
                this.Remove(e.Item);
            }
        }

        protected override void UninitializeCore()
        {
            base.Target.TabRemoving -= new TabControlTabRemovingEventHandler(this.OnTargetTabRemoving);
            base.UninitializeCore();
        }
    }
}

