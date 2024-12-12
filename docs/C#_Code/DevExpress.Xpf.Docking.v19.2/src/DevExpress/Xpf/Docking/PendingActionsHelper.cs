namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;

    internal class PendingActionsHelper : Locker
    {
        private readonly List<PendingAction> _actions = new List<PendingAction>();

        public PendingActionsHelper()
        {
            base.Unlocked += new EventHandler(this.PendingActionsHelper_Unlocked);
        }

        public void AddPendingAction(PendingActionCallback callback, object arg)
        {
            this._actions.Add(new PendingAction(callback, arg));
        }

        public void ExecutePendingActions()
        {
            PendingAction[] actionArray = this._actions.ToArray();
            this._actions.Clear();
            foreach (PendingAction action in actionArray)
            {
                action.Execute();
            }
        }

        private void PendingActionsHelper_Unlocked(object sender, EventArgs e)
        {
            this.ExecutePendingActions();
        }

        private class PendingAction
        {
            private PendingActionCallback _callback;
            private object _arg;

            public PendingAction(PendingActionCallback callback, object arg)
            {
                this._callback = callback;
                this._arg = arg;
            }

            public void Execute()
            {
                if (this._callback != null)
                {
                    this._callback(this._arg);
                }
            }
        }
    }
}

