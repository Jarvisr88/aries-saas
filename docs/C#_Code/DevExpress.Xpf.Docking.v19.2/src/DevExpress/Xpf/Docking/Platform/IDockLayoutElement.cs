namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public interface IDockLayoutElement : ILayoutElement, IBaseObject, IDisposable, ISupportHierarchy<ILayoutElement>, ISupportVisitor<ILayoutElement>, IDragSource, IDropTarget
    {
        BaseDropInfo CalcDropInfo(BaseLayoutItem item, Point point);
        IDockLayoutElement CheckDragElement();
        ILayoutElementBehavior GetBehavior();
        ILayoutElement GetDragItem();

        LayoutItemType Type { get; }

        BaseLayoutItem Item { get; }

        UIElement View { get; }

        UIElement Element { get; }

        bool IsPageHeader { get; }

        bool AllowActivate { get; }
    }
}

