namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Customization;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DockLayoutElementDragInfo
    {
        private bool isSplitter;
        private System.Windows.Point splitterItemPoint;

        public DockLayoutElementDragInfo(IView view, System.Windows.Point point, ILayoutElement dragElement)
        {
            this.View = view;
            this.Element = this.CheckDragElement((IDockLayoutElement) dragElement);
            this.Item = this.Element.Item;
            this.HitInfo = this.View.Adapter.CalcHitInfo(view, point);
            this.DragSource = dragElement as IDragSource;
            this.OriginalDropTarget = this.HitInfo.Element as IDropTarget;
            this.DropTarget = this.CheckDropTarget();
            this.Point = this.CheckClientPoint(point);
            if (this.OriginalDropTarget != null)
            {
                this.OriginalTarget = ((IDockLayoutElement) this.OriginalDropTarget).Item;
            }
            if (this.DropTarget != null)
            {
                this.Target = ((IDockLayoutElement) this.DropTarget).Item;
                this.DropInfo = ((IDockLayoutElement) this.DropTarget).CalcDropInfo(this.Item, this.Point);
            }
            if (this.DropInfo != null)
            {
                this.DropType = this.DropInfo.Type;
                this.MoveType = this.DropInfo.MoveType;
                this.TargetRect = this.DropInfo.ItemRect;
                this.InsertIndex = this.DropInfo.InsertIndex;
            }
        }

        public bool AcceptDockCenter() => 
            (this.DragSource != null) && ((this.DropTarget != null) && (this.Item.AllowDock && (this.DragSource.AcceptDockTarget(this, null) && this.DropTarget.AcceptDockSource(this, null))));

        public bool AcceptDocking(DockHintHitInfo hitInfo) => 
            (hitInfo.DockType != DockType.None) && ((this.DragSource != null) && ((this.DropTarget != null) && (((hitInfo.DockType != DockType.Fill) || this.DropTarget.AcceptFill(this)) ? ((hitInfo.IsHideButton || this.Item.AllowDock) ? (!hitInfo.IsHideButton ? (hitInfo.IsCenter ? (this.DragSource.AcceptDockTarget(this, hitInfo) && this.DropTarget.AcceptDockSource(this, hitInfo)) : this.CanDockToSide) : this.CanHide) : false) : false)));

        public bool AcceptDragDrop() => 
            (this.DragSource != null) && ((this.DropTarget != null) && (!ReferenceEquals(this.DragSource, this.DropTarget) && (this.Item.AllowMove && ((!this.Item.IsHidden || this.Item.AllowRestore) && (this.DragSource.AcceptDropTarget(this) && this.DropTarget.AcceptDragSource(this))))));

        public bool AcceptFill() => 
            (this.DragSource != null) && ((this.DropTarget != null) && (this.Item.AllowDock && (((this.Target == null) || this.Target.GetAllowDockToCurrentItem()) && (this.AcceptSelfDock() && this.DropTarget.AcceptFill(this)))));

        public bool AcceptHide() => 
            (this.DragSource != null) && ((this.DropTarget != null) && (this.Item.AllowHide && (!LayoutItemsHelper.IsLayoutItem(this.Item) && (this.AcceptSelfDock() && (this.DropTarget.AcceptDockSource(this, null) && this.CanHide)))));

        public bool AcceptReordering() => 
            (this.DropTarget != null) && this.DropTarget.AcceptReordering(this);

        public bool AcceptSelfDock() => 
            !ReferenceEquals(this.DropTarget, this.DragSource) && ((this.DropTarget != null) && !ReferenceEquals(this.Item, this.Target));

        public static DragInfo CalcDragInfo(IView view, System.Windows.Point point, ILayoutElement element)
        {
            DockLayoutElementDragInfo info = new DockLayoutElementDragInfo(view, point, element);
            if (info.Item == null)
            {
                return null;
            }
            bool flag = info.AcceptDragDrop();
            return new DragInfo(info.Item, info.Target, flag ? info.DropType : DevExpress.Xpf.Layout.Core.DropType.None);
        }

        public System.Windows.Point CheckClientPoint(System.Windows.Point point) => 
            this.isSplitter ? this.splitterItemPoint : point;

        public IDockLayoutElement CheckDragElement(IDockLayoutElement dragElement) => 
            dragElement.CheckDragElement();

        public IDropTarget CheckDropTarget()
        {
            IDockLayoutElement element = this.HitInfo.Element as IDockLayoutElement;
            if (element == null)
            {
                return null;
            }
            if (element.Type == LayoutItemType.Splitter)
            {
                LayoutGroup group = element.Item as LayoutGroup;
                int index = group.ItemsInternal.IndexOf(element.Element);
                BaseLayoutItem objB = group.ItemsInternal[index - 1] as BaseLayoutItem;
                foreach (IDockLayoutElement element3 in element.Container.Items)
                {
                    if (ReferenceEquals(element3.Item, objB))
                    {
                        element = element3;
                        this.isSplitter = true;
                        this.splitterItemPoint = (group.Orientation != Orientation.Horizontal) ? new System.Windows.Point(this.HitInfo.HitPoint.X, (element.Location.Y + element.Size.Height) - 1.0) : new System.Windows.Point((element.Location.X + element.Size.Width) - 1.0, this.HitInfo.HitPoint.Y);
                        break;
                    }
                }
            }
            IDropTarget container = null;
            if (!this.IsControlItemsHost(element) || (LayoutItemsHelper.IsLayoutItem(this.Item) || ((this.Item.ItemType == LayoutItemType.Group) && this.Item.GetRoot().IsLayoutRoot)))
            {
                container = this.CheckNestedItem(element) as IDropTarget;
            }
            container ??= (this.FindNonControlItemHostTarget(element) as IDropTarget);
            IDockLayoutElement element2 = container as IDockLayoutElement;
            if ((element2 != null) && ((element2.Item != null) && (element2.Item.Parent != null)))
            {
                LayoutGroup parent = element2.Item.Parent;
                if (LayoutItemsHelper.IsLayoutItem(element2.Item) && ((parent.GroupBorderStyle == GroupBorderStyle.NoBorder) && (!parent.IsLayoutRoot && !this.isSplitter)))
                {
                    Rect rect = ElementHelper.GetRect(container as ILayoutElement);
                    RectHelper.Inflate(ref rect, (double) MovingHelper.NoBorderMarginHorizontal, (double) MovingHelper.NoBorderMarginVertical);
                    if (!rect.Contains(this.HitInfo.HitPoint))
                    {
                        container = element2.Container as IDropTarget;
                    }
                }
            }
            return container;
        }

        private ILayoutElement CheckNestedItem(ILayoutElement element) => 
            !(element.Container is TabbedLayoutGroupElement) ? (!(element.Container is TabbedPaneElement) ? (!(element.Container is DocumentPaneElement) ? (!(element.Container is AutoHideTrayHeadersGroupElement) ? element : element.Container) : element.Container) : element.Container) : element.Container;

        private ILayoutElement FindNonControlItemHostTarget(ILayoutElement element)
        {
            while ((element.Parent != null) && this.IsControlItemsHost(element.Parent))
            {
                element = element.Parent;
            }
            return this.CheckNestedItem(element);
        }

        private bool IsControlItemsHost(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            return ((item != null) && (item.IsControlItemsHost || LayoutItemsHelper.IsLayoutItem(item)));
        }

        public IView View { get; private set; }

        public System.Windows.Point Point { get; private set; }

        public IDockLayoutElement Element { get; private set; }

        protected LayoutElementHitInfo HitInfo { get; private set; }

        protected BaseDropInfo DropInfo { get; private set; }

        public IDragSource DragSource { get; private set; }

        public IDropTarget DropTarget { get; private set; }

        public IDropTarget OriginalDropTarget { get; private set; }

        public BaseLayoutItem Item { get; private set; }

        public BaseLayoutItem Target { get; private set; }

        public BaseLayoutItem OriginalTarget { get; private set; }

        public DevExpress.Xpf.Layout.Core.DropType DropType { get; private set; }

        public DevExpress.Xpf.Layout.Core.MoveType MoveType { get; private set; }

        public Rect TargetRect { get; private set; }

        public int InsertIndex { get; private set; }

        public bool CanDockToSide
        {
            get
            {
                if (!this.Item.AllowDock)
                {
                    return false;
                }
                DockLayoutManager container = ((LayoutView) this.View).Container;
                bool flag = (this.Item is FloatGroup) && ((FloatGroup) this.Item).IsDocumentHost;
                return (((this.Item.ItemType != LayoutItemType.Document) || (container.DockingStyle == DockingStyle.Default)) ? (!this.Item.GetRoot().GetIsDocumentHost() && (!((container.DockingStyle != DockingStyle.Default) & flag) ? !LayoutItemsHelper.IsEmptyLayoutGroup(this.Target.GetRoot()) : false)) : false);
            }
        }

        public bool CanDockToTab =>
            this.Item.AllowDock && (this.Item.GetAllowDockToDocumentGroup() && ((this.Target is LayoutGroup) && (this.Target.GetIsDocumentHost() && ((LayoutGroup) this.Target).HasNotCollapsedItems)));

        public bool CanDock
        {
            get
            {
                if (!this.Item.AllowDock)
                {
                    return false;
                }
                DockLayoutManager container = ((LayoutView) this.View).Container;
                bool flag = this.Item.ItemType == LayoutItemType.Document;
                if (flag | ((this.Item is FloatGroup) && ((FloatGroup) this.Item).IsDocumentHost))
                {
                    if (container.DockingStyle != DockingStyle.Default)
                    {
                        return false;
                    }
                    DocumentGroup target = this.Target as DocumentGroup;
                    if ((target != null) && !target.HasNotCollapsedItems)
                    {
                        return false;
                    }
                }
                return (!LayoutItemsHelper.IsEmptyLayoutGroup(this.Target) || this.Target.GetIsDocumentHost());
            }
        }

        public bool CanHide
        {
            get
            {
                bool flag = (this.Item is FloatGroup) && ((FloatGroup) this.Item).IsDocumentHost;
                return (this.Item.AllowHide && (!this.Item.GetRoot().GetIsDocumentHost() && !flag));
            }
        }

        public bool CanFill =>
            this.Item.AllowDock;
    }
}

