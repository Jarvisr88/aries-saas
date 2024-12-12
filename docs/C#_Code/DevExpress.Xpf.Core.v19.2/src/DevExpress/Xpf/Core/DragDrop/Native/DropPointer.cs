namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using System;

    public class DropPointer
    {
        private readonly DevExpress.Xpf.Core.DragDrop.Native.RowPointer rowPointer;
        private readonly DropPosition position;

        public DropPointer(DevExpress.Xpf.Core.DragDrop.Native.RowPointer rowPointer, DropPosition position)
        {
            this.rowPointer = rowPointer;
            this.position = position;
        }

        public DevExpress.Xpf.Core.DragDrop.Native.RowPointer RowPointer =>
            this.rowPointer;

        public DropPosition Position =>
            this.position;
    }
}

