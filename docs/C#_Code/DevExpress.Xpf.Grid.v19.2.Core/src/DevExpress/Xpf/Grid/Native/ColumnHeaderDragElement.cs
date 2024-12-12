namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;

    public class ColumnHeaderDragElement : HeaderDragElementBase
    {
        public ColumnHeaderDragElement(BaseGridHeader columnHeaderElement, Point offset) : base(columnHeaderElement, columnHeaderElement.CreateDragElementDataContext(), offset)
        {
            DataControlBase.SetCurrentViewInternal(base.container, columnHeaderElement.GridView);
        }

        protected override void AddGridChild(object child)
        {
            this.OwnerView.AddChild(child);
        }

        protected override void RemoveGridChild(object child)
        {
            this.OwnerView.RemoveChild(child);
        }

        protected override void SetDragElementAllowTransparency(FrameworkElement elem, bool allowTransparency)
        {
            BaseGridColumnHeader.SetDragElementAllowTransparency(elem, allowTransparency);
        }

        protected override void SetDragElementSize(FrameworkElement elem, Size size)
        {
            BaseGridColumnHeader.SetDragElementSize(elem, size);
        }

        protected BaseGridHeader ColumnHeaderElement =>
            (BaseGridHeader) base.HeaderElement;

        protected virtual DataViewBase OwnerView =>
            this.ColumnHeaderElement.GridView.RootView;

        protected override FrameworkElement HeaderButton =>
            this.ColumnHeaderElement.HeaderContent;

        protected override string DragElementTemplatePropertyName =>
            "DragElementTemplate";
    }
}

