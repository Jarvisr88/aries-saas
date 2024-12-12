namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class FloatingViewUIInteractionListener : LayoutViewUIInteractionListener
    {
        protected override bool CanMaximizeOrRestore(ILayoutElement element) => 
            ((IDockLayoutElement) element).Item.IsMaximizable;

        protected override bool DockElementOnDoubleClick(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            DockSituation lastDockSituation = item.GetLastDockSituation();
            return (((lastDockSituation == null) || !(lastDockSituation.DockTarget is AutoHideGroup)) ? base.View.Container.DockController.Dock(item) : base.View.Container.DockController.Hide(item, lastDockSituation.DockTarget as AutoHideGroup));
        }

        public override void OnActivate()
        {
            base.OnActivate();
            FloatPanePresenter rootKey = base.View.RootKey as FloatPanePresenter;
            if (rootKey != null)
            {
                rootKey.Activate(base.View.Container);
            }
        }
    }
}

