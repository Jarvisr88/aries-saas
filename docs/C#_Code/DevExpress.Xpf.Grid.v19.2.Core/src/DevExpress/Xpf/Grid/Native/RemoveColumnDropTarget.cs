namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RemoveColumnDropTarget : IDropTarget
    {
        private DataViewBase dataView;

        private bool CanDrop(UIElement source, Point pt) => 
            !ColumnChooserDropTarget.IsForbiddenUngroupGesture(source) && this.IsPositionInDropZone(source, pt);

        public virtual void Drop(UIElement source, Point pt)
        {
            if (this.IsPositionInDropZone(source, pt))
            {
                ColumnChooserDropTarget.DropColumnCore(source);
            }
            DragDropScroller.StopScrolling(this.dataView);
        }

        public virtual DataViewBase GetDataView(UIElement source)
        {
            DataViewBase dataView = this.dataView;
            if (this.dataView == null)
            {
                DataViewBase local1 = this.dataView;
                dataView = this.dataView = DataControlBase.FindCurrentView(source);
            }
            return dataView;
        }

        protected virtual UIElement GetTargetElement(DataViewBase view) => 
            view.HeadersPanel;

        protected virtual bool IsPositionInDropZone(UIElement source, Point pt)
        {
            DataViewBase dataView = this.GetDataView(source);
            return (DesignerHelper.GetValue(dataView, dataView.AllowMoveColumnToDropArea, false) ? (this.IsPositionInsideElement(dataView.RootView.DataControl, pt, new Thickness(0.0)) ? ((this.GetTargetElement(dataView) != null) ? !this.IsPositionInsideElement(this.GetTargetElement(dataView), pt, new Thickness(50.0)) : true) : true) : false);
        }

        protected virtual bool IsPositionInsideElement(UIElement source, Point location, Thickness margin)
        {
            UIElement topLevelVisual = LayoutHelper.GetTopLevelVisual(source);
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(source, topLevelVisual);
            Point position = new Point(location.X - relativeElementRect.X, location.Y - relativeElementRect.Y);
            return LayoutHelper.IsPointInsideElementBounds(position, (FrameworkElement) source, margin);
        }

        public virtual void OnDragLeave()
        {
            if (this.dragElement != null)
            {
                BaseGridColumnHeader.SetIsInDropArea(this.dragElement.DragPreviewElement, false);
                DragDropScroller.StopScrolling(this.dataView);
            }
        }

        public virtual void OnDragOver(UIElement source, Point pt)
        {
            DragDropElementHelper dragDropHelper = ((BaseGridHeader) source).DragDropHelper;
            this.dragElement = (ColumnHeaderDragElement) dragDropHelper.DragElement;
            BaseGridColumnHeader.SetIsInDropArea(this.dragElement.DragPreviewElement, this.CanDrop(source, pt));
            IExtendedColumnChooserView extendedColumnChooserView = BaseGridHeader.GetExtendedColumnChooserView(source);
            if ((extendedColumnChooserView == null) || !extendedColumnChooserView.IsInScrollingMode)
            {
                DragDropScroller.StartScrolling(this.dataView);
            }
        }

        public ColumnHeaderDragElement dragElement { get; protected set; }
    }
}

