namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class DropState : DragStateBase
    {
        public DropState(DataModificationController controller, RowPointer[] rowPointers, DropPointer pointer) : base(controller)
        {
            this.RowPointers = rowPointers;
            this.Pointer = pointer;
        }

        public override void EndDrag()
        {
            base.EndDrag();
            base.DataModifier.Move(this.RowPointers, this.Pointer);
        }

        public RowPointer[] RowPointers { get; private set; }

        public DropPointer Pointer { get; private set; }
    }
}

