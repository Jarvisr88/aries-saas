namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    public class DropInfo : DragDropInfoBase
    {
        private readonly DevExpress.Xpf.Core.DragDrop.Native.RowPointer rowPointer;
        private readonly object data;
        private readonly DropPosition? staticPosition;

        public DropInfo(DragDropInfoVisualSource source) : this(new DevExpress.Xpf.Core.DragDrop.Native.RowPointer(-2147483648), null, source, 0)
        {
        }

        public DropInfo(DevExpress.Xpf.Core.DragDrop.Native.RowPointer rowPointer, object data, DragDropInfoVisualSource source, DropPosition? staticPosition) : base(source)
        {
            this.rowPointer = rowPointer;
            this.data = data;
            this.staticPosition = staticPosition;
        }

        public DevExpress.Xpf.Core.DragDrop.Native.RowPointer RowPointer =>
            this.rowPointer;

        public object Data =>
            this.data;

        public DropPosition? StaticPosition =>
            this.staticPosition;
    }
}

