namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    public interface IDataModifier
    {
        void Insert(object[] objects, DropPointer pointer);
        void Move(RowPointer[] rowPointers, DropPointer pointer);
        void Remove(RowPointer[] rowPointers);
    }
}

