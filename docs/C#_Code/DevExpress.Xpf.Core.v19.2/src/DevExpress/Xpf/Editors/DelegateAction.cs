namespace DevExpress.Xpf.Editors
{
    using System;

    public class DelegateAction : IAction
    {
        private readonly Action action;

        public DelegateAction(Action action)
        {
            this.action = action;
        }

        void IAction.Execute()
        {
            this.action();
        }
    }
}

