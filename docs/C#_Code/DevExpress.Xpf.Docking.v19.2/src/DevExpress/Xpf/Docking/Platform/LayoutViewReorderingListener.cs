namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Dragging;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class LayoutViewReorderingListener : ReorderingListener
    {
        private readonly InsertionHelper insertionHelper = new InsertionHelper();
        private Point? lastDraggingPoint;
        private Point? lastCanDragPoint;
        private bool IsStartedAsReordering;

        public override bool CancelReordering(Point point, ILayoutElement element)
        {
            if (!this.IsStartedAsReordering)
            {
                return false;
            }
            DockLayoutElementDragInfo info = new DockLayoutElementDragInfo(this.View, point, element);
            return !LayoutItemsHelper.CanReorder(info.Item, info.OriginalTarget);
        }

        public override bool CanDrag(Point point, ILayoutElement element)
        {
            if (element == null)
            {
                return false;
            }
            this.lastCanDragPoint = new Point?(point);
            DockLayoutElementDragInfo info = new DockLayoutElementDragInfo(this.View, point, element);
            DockHintHitInfo hitInfo = this.View.AdornerHelper.GetHitInfo(info.Point);
            bool flag = this.IsStartedAsReordering || info.AcceptFill();
            return ((info.AcceptReordering() & flag) && ((hitInfo == null) || !hitInfo.InHint));
        }

        public override bool CanDrop(Point point, ILayoutElement element)
        {
            DockLayoutElementDragInfo info = new DockLayoutElementDragInfo(this.View, point, element);
            DockHintHitInfo hitInfo = this.View.AdornerHelper.GetHitInfo(info.Point);
            return (info.AcceptFill() && ((hitInfo == null) || !hitInfo.InButton));
        }

        protected bool CanInsert(BaseLayoutItem item, Point pt, BaseLayoutItem target, DockType type) => 
            this.insertionHelper.CanInsert() ? (((!this.IsStartedAsReordering || !ReferenceEquals(item.Parent, target)) ? this.View.AdornerHelper.GetTabHeadersAdorner().IsTabHeaderHintEnabled : true) && !this.RaiseItemDocking(DockLayoutManager.DockItemEndDockingEvent, item, pt, target, type, false)) : false;

        private FloatingView GetDragView()
        {
            ILayoutElement element = this.View.Adapter.DragService.DragItem ?? this.View.Container.LinkedDragService.Return<IDragService, ILayoutElement>((<>c.<>9__21_0 ??= x => x.DragItem), (<>c.<>9__21_1 ??= ((Func<ILayoutElement>) (() => null))));
            FloatingView view = this.View.Adapter.GetView(element) as FloatingView;
            if (view == null)
            {
                BaseLayoutItem item;
                IDockLayoutElement element2 = element as IDockLayoutElement;
                if (element2 != null)
                {
                    item = element2.Item;
                }
                else
                {
                    IDockLayoutElement local4 = element2;
                    item = null;
                }
                view = this.View.Container.GetView(item) as FloatingView;
            }
            return view;
        }

        protected void InsertTabPageCore(DockLayoutElementDragInfo dragInfo)
        {
            IDockLayoutContainer dropTarget = dragInfo.DropTarget as IDockLayoutContainer;
            if ((dragInfo.Item != null) && (dropTarget != null))
            {
                FloatingView dragView = this.GetDragView();
                this.insertionHelper.Update(dragInfo, this.IsStartedAsReordering);
                if (this.CanInsert(dragInfo.Item, dragInfo.Point, dropTarget.Item, DockType.Fill))
                {
                    BaseLayoutItem actualSelectedItem = DockControllerHelper.GetActualSelectedItem(dragInfo.Item);
                    if (this.View.Container.DockController.Insert((LayoutGroup) dropTarget.Item, dragInfo.Item, this.insertionHelper.GetIndex(), this.IsStartedAsReordering))
                    {
                        this.View.Container.DockController.Activate(actualSelectedItem);
                    }
                }
                if (dragView != null)
                {
                    dragView.LeaveReordering();
                }
            }
        }

        public override void OnBegin(Point point, ILayoutElement element)
        {
            base.OnBegin(point, element);
            this.IsStartedAsReordering = true;
            IDockLayoutElement dockLayoutElement = (IDockLayoutElement) element;
            BaseLayoutItem item = dockLayoutElement.Item;
            if (item.IsItemWithRestrictedFloating())
            {
                item.DragCursorBounds = FloatingHelper.GetDragCursorBounds(this.View, dockLayoutElement);
            }
        }

        public override void OnCancel()
        {
            this.View.AdornerHelper.ResetTabHeadersHints();
            this.View.AdornerHelper.TryHideAdornerWindow();
            this.IsStartedAsReordering = false;
        }

        public override void OnComplete()
        {
            base.OnComplete();
            this.View.AdornerHelper.ResetTabHeadersHints();
            this.View.AdornerHelper.TryHideAdornerWindow();
            this.IsStartedAsReordering = false;
        }

        public override void OnDragging(Point point, ILayoutElement element)
        {
            if (!this.IsStartedAsReordering)
            {
                this.UpdateHints(new DockLayoutElementDragInfo(this.View, point, element), false);
            }
            else
            {
                DockLayoutElementDragInfo dragInfo = new DockLayoutElementDragInfo(this.View, point, element);
                this.InsertTabPageCore(dragInfo);
            }
            this.lastDraggingPoint = new Point?(point);
        }

        public override void OnDrop(Point point, ILayoutElement element)
        {
            if (this.IsStartedAsReordering)
            {
                this.View.AdornerHelper.TryHideAdornerWindow();
            }
            else
            {
                DockLayoutElementDragInfo dragInfo = new DockLayoutElementDragInfo(this.View, point, element);
                this.InsertTabPageCore(dragInfo);
                this.View.AdornerHelper.ResetDockingHints();
                this.View.AdornerHelper.ResetTabHeadersHints();
                this.View.AdornerHelper.ResetDragVisualization();
                this.View.AdornerHelper.BeginHideAdornerWindow();
            }
            this.IsStartedAsReordering = false;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            ILayoutElement dragElement = this.View.Adapter.DragService.DragItem ?? this.View.Container.LinkedDragService.Return<IDragService, ILayoutElement>((<>c.<>9__4_0 ??= x => x.DragItem), (<>c.<>9__4_1 ??= ((Func<ILayoutElement>) (() => null))));
            FloatingView dragView = this.GetDragView();
            if (dragView != null)
            {
                dragView.EnterReordering();
            }
            Point? lastDraggingPoint = this.lastDraggingPoint;
            Point? nullable = (lastDraggingPoint != null) ? lastDraggingPoint : this.lastCanDragPoint;
            if (nullable != null)
            {
                this.UpdateHints(new DockLayoutElementDragInfo(this.View, nullable.Value, dragElement), true);
            }
            this.IsStartedAsReordering = false;
        }

        public override void OnLeave()
        {
            this.View.AdornerHelper.BeginHideAdornerWindowAndResetTabHeadersHints();
            FloatingView dragView = this.GetDragView();
            if (dragView != null)
            {
                dragView.LeaveReordering();
            }
            this.IsStartedAsReordering = false;
        }

        protected bool RaiseItemDocking(RoutedEvent e, BaseLayoutItem item, Point pt, BaseLayoutItem target, DockType dockType, bool isHiding) => 
            this.View.Container.RaiseItemDockingEvent(e, item, pt, target, dockType, isHiding);

        private void UpdateHints(DockLayoutElementDragInfo dragInfo, bool forceUpdateAdornerBounds = false)
        {
            if (!KeyHelper.IsCtrlPressed)
            {
                this.View.AdornerHelper.UpdateTabHeadersHint(dragInfo, forceUpdateAdornerBounds);
            }
        }

        public LayoutView View =>
            base.ServiceProvider as LayoutView;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutViewReorderingListener.<>c <>9 = new LayoutViewReorderingListener.<>c();
            public static Func<IDragService, ILayoutElement> <>9__4_0;
            public static Func<ILayoutElement> <>9__4_1;
            public static Func<IDragService, ILayoutElement> <>9__21_0;
            public static Func<ILayoutElement> <>9__21_1;

            internal ILayoutElement <GetDragView>b__21_0(IDragService x) => 
                x.DragItem;

            internal ILayoutElement <GetDragView>b__21_1() => 
                null;

            internal ILayoutElement <OnEnter>b__4_0(IDragService x) => 
                x.DragItem;

            internal ILayoutElement <OnEnter>b__4_1() => 
                null;
        }

        private class InsertionHelper
        {
            private int _InsertionIndex;
            private bool _IsStartedAsReordering;
            private System.Windows.Point _Point;
            private int currentIndex;
            private int diffIndex;
            private double difX;
            private double difY;

            public bool CanInsert() => 
                !this._IsStartedAsReordering || ((((Math.Sign(this.diffIndex) == Math.Sign(this.difX)) && (Math.Abs(this.difX) > 0.0)) || ((Math.Sign(this.diffIndex) == Math.Sign(this.difY)) && (Math.Abs(this.difY) > 0.0))) && (this.TabIndex != this.currentIndex));

            public int GetIndex() => 
                this.CanInsert() ? this.Index : -1;

            public void Update(DockLayoutElementDragInfo dragInfo, bool isStartedAsReordering)
            {
                IDockLayoutContainer dropTarget = dragInfo.DropTarget as IDockLayoutContainer;
                BaseLayoutItem item = dragInfo.Item;
                if ((item != null) && (dropTarget != null))
                {
                    TabHeaderInsertHelper helper = new TabHeaderInsertHelper(dropTarget, dragInfo.Point, !ReferenceEquals(item.Parent, dropTarget.Item));
                    this.TabIndex = helper.TabIndex;
                    this.Index = helper.Index;
                    this.Point = dragInfo.Point;
                    this._IsStartedAsReordering = isStartedAsReordering;
                    Func<int> fallback = <>c.<>9__19_1;
                    if (<>c.<>9__19_1 == null)
                    {
                        Func<int> local1 = <>c.<>9__19_1;
                        fallback = <>c.<>9__19_1 = () => -1;
                    }
                    this.currentIndex = (dropTarget.Item as LayoutGroup).Return<LayoutGroup, int>(x => x.TabIndexFromItem(dragInfo.Item), fallback);
                }
            }

            private int Index { get; set; }

            private int TabIndex
            {
                get => 
                    this._InsertionIndex;
                set
                {
                    if (this._InsertionIndex != value)
                    {
                        this.diffIndex = this._InsertionIndex - value;
                        this._InsertionIndex = value;
                    }
                }
            }

            private System.Windows.Point Point
            {
                get => 
                    this._Point;
                set
                {
                    if (this._Point != value)
                    {
                        this.difX = this.Point.X - value.X;
                        this.difY = this.Point.Y - value.Y;
                        this._Point = value;
                    }
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly LayoutViewReorderingListener.InsertionHelper.<>c <>9 = new LayoutViewReorderingListener.InsertionHelper.<>c();
                public static Func<int> <>9__19_1;

                internal int <Update>b__19_1() => 
                    -1;
            }
        }
    }
}

