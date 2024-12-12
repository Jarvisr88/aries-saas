namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    public interface IDragStateMachine
    {
        void Cancel();
        void Drop(object[] objects, DropPointer pointer);
        void EndDrag();
        void StartDrag(RowPointer[] rowPointers);
    }
}

