namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Input;

    public class AutoHideViewUIInteractionListener : LayoutViewUIInteractionListener
    {
        private bool CollapseAutoHideTray(AutoHideTray tray, BaseLayoutItem itemToCollapse = null)
        {
            bool flag = (tray != null) && tray.IsExpanded;
            if (flag)
            {
                tray.DoCollapse(itemToCollapse);
            }
            return flag;
        }

        protected override IFloatingHelper CreateFloatingHelper() => 
            new AutoHideFloatingHelper(base.View);

        private bool GetIsRightButtonPressed() => 
            Mouse.RightButton == MouseButtonState.Pressed;

        public override bool OnActiveItemChanging(ILayoutElement element) => 
            (this.GetIsRightButtonPressed() || !this.TryCollapseAutoHideTray(element)) ? base.OnActiveItemChanging(element) : false;

        public override bool OnMenuAction(LayoutElementHitInfo clickInfo)
        {
            AutoHideTrayElement element = clickInfo.Element as AutoHideTrayElement;
            if (element != null)
            {
                AutoHideTray source = element.Tray;
                if (source != null)
                {
                    this.ShowItemsSelectorMenu(source, source.GetItems());
                    return true;
                }
            }
            AutoHidePaneHeaderItemElement element2 = clickInfo.Element as AutoHidePaneHeaderItemElement;
            if (element2 == null)
            {
                return base.OnMenuAction(clickInfo);
            }
            MenuHelper.ShowMenu(base.View.Container, element2.Item, element2.Element, false);
            return true;
        }

        private bool TryCollapseAutoHideTray(ILayoutElement element)
        {
            BaseLayoutItem objA = ((IDockLayoutElement) element).Item;
            AutoHideTrayElement element2 = element as AutoHideTrayElement;
            if ((objA == null) && (element2 != null))
            {
                this.CollapseAutoHideTray(element2.Tray, null);
            }
            else if (base.View.Container.CanAutoHideOnMouseDown)
            {
                AutoHidePaneHeaderItemElement element3 = element as AutoHidePaneHeaderItemElement;
                return ((element3 != null) && ((element3.Tray != null) && (ReferenceEquals(objA, element3.Tray.HotItem) && this.CollapseAutoHideTray(element3.Tray, objA))));
            }
            return false;
        }
    }
}

