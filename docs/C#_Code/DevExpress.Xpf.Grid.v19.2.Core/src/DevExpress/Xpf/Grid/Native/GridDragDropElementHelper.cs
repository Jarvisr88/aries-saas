namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.InteropServices;

    public class GridDragDropElementHelper : DragDropElementHelper
    {
        public GridDragDropElementHelper(ISupportDragDropColumnHeader supportDragDrop, bool isRelativeMode = true) : base(supportDragDrop, isRelativeMode)
        {
        }

        protected override BaseDragDropStrategy CreateDragDropStrategy() => 
            new GridDragDropStrategy((ISupportDragDropColumnHeader) base.SupportDragDrop, this);
    }
}

