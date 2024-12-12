namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.UIInteraction;
    using System;

    public class CustomizationViewUIInteractionListener : UIInteractionServiceListener
    {
        public override void OnActivate()
        {
            this.View.Container.CustomizationController.CloseMenu();
        }

        public override bool OnActiveItemChanged(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            this.View.Container.Activate(item);
            LayoutGroup root = item.GetRoot();
            if (root != null)
            {
                this.View.Container.CustomizationController.CustomizationRoot = root;
            }
            return ((item != null) && item.IsActive);
        }

        public override bool OnActiveItemChanging(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            return ((item != null) && (item.AllowActivate && !item.IsHidden));
        }

        public override bool OnClickPreviewAction(LayoutElementHitInfo clickInfo)
        {
            this.View.Container.RenameHelper.CancelRenamingAndResetClickedState();
            return base.OnClickPreviewAction(clickInfo);
        }

        public override bool OnMenuAction(LayoutElementHitInfo clickInfo)
        {
            this.View.Container.RenameHelper.CancelRenamingAndResetClickedState();
            IDockLayoutElement element = clickInfo.Element as IDockLayoutElement;
            if (!(element is HiddenItemElement))
            {
                return ((element is TreeItemElement) && MenuHelper.ShowMenu(this.View.Container, element.Item, element.Element, false));
            }
            if (element.Item is FixedItem)
            {
                return false;
            }
            this.View.Container.CustomizationController.MenuSource = element.Element;
            this.View.Container.CustomizationController.ShowHiddenItemMenu(element.Item);
            return true;
        }

        public CustomizationView View =>
            base.ServiceProvider as CustomizationView;
    }
}

