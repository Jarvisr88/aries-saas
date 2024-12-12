namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class FloatingHelper : IFloatingHelper
    {
        public FloatingHelper(LayoutView view)
        {
            this.View = view;
        }

        internal virtual Rect Check(Rect screenRect, Point startPoint)
        {
            if (!screenRect.Contains(startPoint))
            {
                RectHelper.Offset(ref screenRect, (startPoint.X - 15.0) - screenRect.X, (startPoint.Y - 15.0) - screenRect.Y);
            }
            return screenRect;
        }

        public unsafe Rect GetDragCursorBounds(IDockLayoutElement element)
        {
            Point dragOrigin = this.View.Adapter.DragService.DragOrigin;
            BaseLayoutItem item = element.Item;
            Thickness floatingBorderMargin = item.GetFloatingBorderMargin();
            Rect itemScreenRect = this.GetItemScreenRect(element);
            Rect itemContainerScreenRect = this.GetItemContainerScreenRect(element);
            Rect rect3 = this.GetFloatingBounds(element, itemScreenRect, itemContainerScreenRect, false);
            Func<FloatPanePresenter, UIElement> evaluator = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<FloatPanePresenter, UIElement> local1 = <>c.<>9__6_0;
                evaluator = <>c.<>9__6_0 = x => x.Element;
            }
            Point location = item.GetDockLayoutManager().TranslatePoint(dragOrigin, (element.Element as FloatPanePresenter).Return<FloatPanePresenter, UIElement>(evaluator, () => element.Element));
            if (!rect3.Contains(dragOrigin))
            {
                location = new Point(15.0, 15.0);
            }
            Size size = new Size();
            if (!item.IsFloatingRootItem && !(item is FloatGroup))
            {
                size = rect3.Size;
                Point* pointPtr5 = &location;
                pointPtr5.X += floatingBorderMargin.Left;
                Point* pointPtr6 = &location;
                pointPtr6.Y += floatingBorderMargin.Top;
            }
            else
            {
                Rect rect = element.IsPageHeader ? itemContainerScreenRect : (itemContainerScreenRect.IsEmpty ? itemScreenRect : itemContainerScreenRect);
                RectHelper.Deflate(ref rect, floatingBorderMargin);
                size = rect.Size;
                FloatGroup root = item.GetRoot() as FloatGroup;
                location = item.GetDockLayoutManager().TranslatePoint(dragOrigin, root.UIElement);
                Point* pointPtr1 = &location;
                pointPtr1.X -= floatingBorderMargin.Left;
                Point* pointPtr2 = &location;
                pointPtr2.Y -= floatingBorderMargin.Top;
                if (root.FloatState == FloatState.Maximized)
                {
                    Rect restoreBounds = DocumentPanel.GetRestoreBounds(root);
                    RectHelper.Deflate(ref restoreBounds, floatingBorderMargin);
                    restoreBounds.Location = rect3.Location;
                    size = restoreBounds.Size;
                    Point* pointPtr3 = &location;
                    pointPtr3.X += floatingBorderMargin.Left;
                    Point* pointPtr4 = &location;
                    pointPtr4.Y += floatingBorderMargin.Top;
                    if (!restoreBounds.Contains(dragOrigin))
                    {
                        location = new Point(15.0, 15.0);
                    }
                }
            }
            if (element is AutoHidePaneHeaderItemElement)
            {
                location = new Point(15.0, 15.0);
            }
            return new Rect(location, size);
        }

        public static Rect GetDragCursorBounds(LayoutView view, IDockLayoutElement dockLayoutElement) => 
            (dockLayoutElement.Item.IsAutoHidden ? new AutoHideFloatingHelper(view) : new FloatingHelper(view)).GetDragCursorBounds(dockLayoutElement);

        private Rect GetFloatingBounds(IDockLayoutElement dockLayoutElement, Rect itemScreenRect, Rect itemContainerScreenRect, bool needCheck = true)
        {
            Point dragOrigin;
            IDockLayoutElement element = dockLayoutElement;
            BaseLayoutItem item = element.Item;
            Size layoutSize = element.IsPageHeader ? itemContainerScreenRect.Size() : itemScreenRect.Size();
            if (needCheck || !item.IsFloatingRootItem)
            {
                layoutSize = item.CheckSize(layoutSize);
            }
            if (!MathHelper.IsEmpty(this.View.Adapter.DragService.DragOrigin))
            {
                dragOrigin = this.View.Adapter.DragService.DragOrigin;
            }
            else
            {
                dragOrigin = new Point();
            }
            Point startPoint = dragOrigin;
            Rect screenRect = new Rect(itemScreenRect.Location(), layoutSize);
            if (needCheck)
            {
                screenRect = this.Check(screenRect, startPoint);
                screenRect = new MarginHelper(item.GetFloatingBorderMargin()).Correct(screenRect);
            }
            return screenRect;
        }

        public virtual IView GetFloatingView(ILayoutElement element)
        {
            IView view2;
            IDockLayoutElement element2 = (IDockLayoutElement) element;
            BaseLayoutItem item = element2.Item;
            DockLayoutManager container = this.View.Container;
            try
            {
                container.BeginFloating();
                Rect itemScreenRect = this.GetItemScreenRect(element2);
                Rect itemContainerScreenRect = this.GetItemContainerScreenRect(element2);
                FloatingView view = container.GetView(((LayoutGroup) container.DockController.Float(item, this.GetFloatingBounds(element2, itemScreenRect, itemContainerScreenRect, true)))) as FloatingView;
                if (view != null)
                {
                    view.SetFloatingBounds(this.GetFloatingBounds(element2, itemScreenRect, itemContainerScreenRect, true));
                    NotificationBatch.Action(view.Container, view.FloatGroup, null);
                }
                view2 = view;
            }
            finally
            {
                container.EndFloating();
            }
            return view2;
        }

        protected virtual Rect GetItemContainerScreenRect(ILayoutElement element) => 
            (element.Container == null) ? Rect.Empty : ElementHelper.GetScreenRect(this.View, element.Container);

        protected virtual Rect GetItemScreenRect(ILayoutElement element) => 
            ElementHelper.GetScreenRect(this.View, element);

        public LayoutView View { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatingHelper.<>c <>9 = new FloatingHelper.<>c();
            public static Func<FloatPanePresenter, UIElement> <>9__6_0;

            internal UIElement <GetDragCursorBounds>b__6_0(FloatPanePresenter x) => 
                x.Element;
        }
    }
}

