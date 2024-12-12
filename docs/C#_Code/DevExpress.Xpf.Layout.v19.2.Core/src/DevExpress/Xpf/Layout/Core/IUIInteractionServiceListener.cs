namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface IUIInteractionServiceListener : IUIServiceListener
    {
        void OnActivate();
        bool OnActiveItemChanged(ILayoutElement element);
        bool OnActiveItemChanging(ILayoutElement element);
        bool OnClickAction(LayoutElementHitInfo clickInfo);
        bool OnClickPreviewAction(LayoutElementHitInfo clickInfo);
        void OnDeactivate();
        bool OnDoubleClickAction(LayoutElementHitInfo clickInfo);
        bool OnMenuAction(LayoutElementHitInfo clickInfo);
        bool OnMiddleButtonClickAction(LayoutElementHitInfo clickInfo);
        bool OnMouseDown(LayoutElementHitInfo clickInfo);
        bool OnMouseUp(LayoutElementHitInfo clickInfo);
    }
}

