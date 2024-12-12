namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class DataModificationController : IDragStateMachine
    {
        public DataModificationController(IDataModifier dataModifier)
        {
            this.DataModifier = dataModifier;
            this.State = new IdleState(this);
        }

        void IDragStateMachine.Cancel()
        {
            this.State.Cancel();
        }

        void IDragStateMachine.Drop(object[] objects, DropPointer pointer)
        {
            this.State.Drop(objects, pointer);
        }

        void IDragStateMachine.EndDrag()
        {
            this.State.EndDrag();
        }

        void IDragStateMachine.StartDrag(RowPointer[] rowPointers)
        {
            this.State.StartDrag(rowPointers);
        }

        public IDataModifier DataModifier { get; private set; }

        public IDragStateMachine State { get; set; }
    }
}

