namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    public abstract class DragStateBase : IDragStateMachine
    {
        private readonly DataModificationController controller;

        public DragStateBase(DataModificationController controller)
        {
            this.controller = controller;
        }

        public virtual void Cancel()
        {
            this.SetIdleState();
        }

        public virtual void Drop(object[] objects, DropPointer pointer)
        {
            this.SetIdleState();
        }

        public virtual void EndDrag()
        {
            this.SetIdleState();
        }

        private void SetIdleState()
        {
            this.SetState(new IdleState(this.Controller));
        }

        protected void SetState(IDragStateMachine state)
        {
            this.Controller.State = state;
        }

        public virtual void StartDrag(RowPointer[] rowPointers)
        {
            this.SetIdleState();
        }

        public DataModificationController Controller =>
            this.controller;

        protected IDataModifier DataModifier =>
            this.Controller.DataModifier;
    }
}

