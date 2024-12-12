namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    public class IdleState : DragStateBase, IDragStateMachine
    {
        public IdleState(DataModificationController controller) : base(controller)
        {
        }

        public override void Drop(object[] objects, DropPointer pointer)
        {
            base.DataModifier.Insert(objects, pointer);
        }

        public override void StartDrag(RowPointer[] rowPointers)
        {
            base.SetState(new StartDragState(base.Controller, rowPointers));
        }
    }
}

