namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class MovingHelper
    {
        public static readonly int NoBorderMarginHorizontal = -3;
        public static readonly int NoBorderMarginVertical = -3;

        public MovingHelper(LayoutView view)
        {
            this.View = view;
        }

        public bool CanMove(Point point, ILayoutElement element)
        {
            DockLayoutElementDragInfo info = new DockLayoutElementDragInfo(this.View, point, element);
            return ((info.Item != null) && info.AcceptDragDrop());
        }

        public void Move(Point point, ILayoutElement element)
        {
            this.View.Adapter.SelectionService.ClearSelection(this.View);
            DockLayoutElementDragInfo dragInfo = new DockLayoutElementDragInfo(this.View, point, element);
            this.MoveItemCore(dragInfo);
        }

        protected bool MoveItemCore(DockLayoutElementDragInfo dragInfo)
        {
            bool flag = (dragInfo.Element is HiddenItemElement) && (dragInfo.Item is FixedItem);
            return this.View.Container.LayoutController.Move(flag ? FixedItemFactory.CreateFixedItem(dragInfo.Item as FixedItem) : dragInfo.Item, dragInfo.Target, dragInfo.MoveType, dragInfo.InsertIndex);
        }

        public LayoutView View { get; private set; }
    }
}

