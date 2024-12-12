namespace DevExpress.Xpf.Bars
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ActionTrigger : EventTrigger
    {
        private ActionGroup owner;
        private bool executed;

        protected internal void AttachToContainer(ActionGroup owner);
        protected internal void DetachFromContainer();
        private void ExecuteOnEvent();
        protected override void OnEvent(object sender, object eventArgs);
        protected virtual void OnExecute(ActionGroup owner);

        public bool ExecuteOnce { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DispatcherPriority? ExecutionPriority { get; set; }
    }
}

