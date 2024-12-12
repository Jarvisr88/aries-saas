namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;

    public class ReraiseAction
    {
        public ReraiseAction(System.Action action, Func<bool> canExecute, ImmediateActionsManager manager)
        {
            this.Action = action;
            this.CanExecute = canExecute;
            this.Manager = manager;
        }

        private System.Action GetPerformAction() => 
            delegate {
                this.Perform();
            };

        public void Perform()
        {
            if (!this.CanExecute())
            {
                this.Manager.EnqueueAction(this.GetPerformAction());
            }
            else
            {
                this.Action();
                this.Action = null;
            }
        }

        private System.Action Action { get; set; }

        private Func<bool> CanExecute { get; set; }

        private ImmediateActionsManager Manager { get; set; }
    }
}

