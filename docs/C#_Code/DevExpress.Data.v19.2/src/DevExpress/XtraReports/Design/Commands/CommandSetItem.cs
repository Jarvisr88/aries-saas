namespace DevExpress.XtraReports.Design.Commands
{
    using System;
    using System.ComponentModel.Design;
    using System.Runtime.CompilerServices;

    public class CommandSetItem : MenuCommand
    {
        private MenuCommand menuCommand;
        private EventHandler statusHandler;
        private EventHandler<CommandExecuteEventArgs> execHandler;
        private bool locked;

        public CommandSetItem(IMenuCommandService menuService, EventHandler<CommandExecuteEventArgs> invokeHandler, EventHandler statusHandler, CommandID commandID);
        public void Disable();
        private MenuCommand GetActualCommand();
        public override void Invoke();
        public virtual void Invoke(object[] args);
        public override void Invoke(object arg);
        public void UpdateStatus();

        protected IMenuCommandService MenuService { get; private set; }

        public bool Locked { get; set; }
    }
}

