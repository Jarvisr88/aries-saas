namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    internal sealed class EmptyDataModifier : IDataModifier
    {
        public void Insert(object[] objects, DropPointer pointer)
        {
        }

        public void Move(RowPointer[] rowPointers, DropPointer pointer)
        {
        }

        public void Remove(RowPointer[] rowPointers)
        {
        }
    }
}

