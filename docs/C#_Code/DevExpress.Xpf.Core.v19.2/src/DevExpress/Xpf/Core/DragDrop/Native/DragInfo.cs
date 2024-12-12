namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    public class DragInfo : DragDropInfoBase
    {
        private readonly object[] data;
        private readonly RowPointer[] rowPointers;

        public DragInfo(object[] data, RowPointer[] rowPointers, DragDropInfoVisualSource source) : base(source)
        {
            object[] objArray1 = data;
            if (data == null)
            {
                object[] local1 = data;
                objArray1 = new object[0];
            }
            this.data = objArray1;
            RowPointer[] pointerArray1 = rowPointers;
            if (rowPointers == null)
            {
                RowPointer[] local2 = rowPointers;
                pointerArray1 = new RowPointer[0];
            }
            this.rowPointers = pointerArray1;
        }

        public object[] Data =>
            this.data;

        public RowPointer[] RowPointers =>
            this.rowPointers;
    }
}

