namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class TileLayoutItemDragAndDropController : FlowLayoutItemDragAndDropController
    {
        public TileLayoutItemDragAndDropController(DevExpress.Xpf.Core.Controller controller, Point startDragPoint, FrameworkElement dragControl) : base(controller, startDragPoint, dragControl)
        {
        }

        protected override FrameworkElement CreateDragControlPlaceHolder() => 
            !(base.DragControl is ITile) ? base.CreateDragControlPlaceHolder() : new TilePlaceHolder((ITile) base.DragControl);

        public override void EndDragAndDrop(bool accept)
        {
            if (accept)
            {
                TileLayoutControl.SetGroupHeader(base.DragControl, TileLayoutControl.GetGroupHeader(base.DragControlPlaceHolder));
            }
            else
            {
                this.RestoreGroupHeaderOriginalValues();
            }
            base.EndDragAndDrop(accept);
        }

        protected override Point GetItemPlacePoint(Point p)
        {
            Point point = PointHelper.Subtract(base.StartDragRelativePoint, new Point(0.5, 0.5));
            Point point2 = PointHelper.Multiply(base.DragImageSize.ToPoint(), point);
            return PointHelper.Subtract(p, point2);
        }

        protected override void InitDragControlPlaceHolder()
        {
            this.Controller.ILayoutControl.StopGroupHeaderEditing();
            base.InitDragControlPlaceHolder();
            TileLayoutControl.SetGroupHeader(base.DragControlPlaceHolder, TileLayoutControl.GetGroupHeader(base.DragControl));
        }

        protected void MoveGroupHeaderAndStoreOriginalValues(UIElement from, UIElement to)
        {
            this.GroupHeaderOriginalValues ??= new Dictionary<UIElement, object>();
            object groupHeader = TileLayoutControl.GetGroupHeader(from);
            if (!this.GroupHeaderOriginalValues.ContainsKey(from))
            {
                this.GroupHeaderOriginalValues.Add(from, groupHeader);
            }
            TileLayoutControl.SetGroupHeader(from, DependencyProperty.UnsetValue);
            if (to != null)
            {
                if (!this.GroupHeaderOriginalValues.ContainsKey(to))
                {
                    this.GroupHeaderOriginalValues.Add(to, TileLayoutControl.GetGroupHeader(to));
                }
                TileLayoutControl.SetGroupHeader(to, groupHeader);
            }
        }

        protected override void OnGroupFirstItemChanged(FrameworkElement oldValue, FrameworkElement newValue)
        {
            base.OnGroupFirstItemChanged(oldValue, newValue);
            this.MoveGroupHeaderAndStoreOriginalValues(oldValue, newValue);
        }

        protected void RestoreGroupHeaderOriginalValues()
        {
            if (this.GroupHeaderOriginalValues != null)
            {
                foreach (KeyValuePair<UIElement, object> pair in this.GroupHeaderOriginalValues)
                {
                    TileLayoutControl.SetGroupHeader(pair.Key, pair.Value);
                }
            }
        }

        protected TileLayoutController Controller =>
            (TileLayoutController) base.Controller;

        private Dictionary<UIElement, object> GroupHeaderOriginalValues { get; set; }

        private class TilePlaceHolder : Canvas, ITile
        {
            public TilePlaceHolder(ITile tile)
            {
                this.Size = tile.Size;
            }

            void ITile.Click()
            {
            }

            public TileSize Size { get; private set; }
        }
    }
}

