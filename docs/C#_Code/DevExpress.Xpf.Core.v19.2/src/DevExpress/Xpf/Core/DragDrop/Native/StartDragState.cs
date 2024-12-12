namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class StartDragState : DragStateBase
    {
        public StartDragState(DataModificationController controller, RowPointer[] rowPointers) : base(controller)
        {
            this.RowPointers = rowPointers;
        }

        public override void Drop(object[] objects, DropPointer pointer)
        {
            base.SetState(new DropState(base.Controller, this.RowPointers, pointer));
        }

        public override void EndDrag()
        {
            base.EndDrag();
            base.DataModifier.Remove(this.RowPointers);
        }

        public RowPointer[] RowPointers { get; private set; }
    }
}

