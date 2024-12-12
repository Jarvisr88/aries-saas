namespace DevExpress.XtraReports.Design.Commands
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Reflection;

    public class MenuCommandHandlerBase : IDisposable
    {
        protected MenuCommandHandlerBase.CommandSetItemCollection commands;
        protected MenuCommandEventHandler onCommandStatusChanged;
        protected Dictionary<CommandID, ICommandExecutor> execHT;
        protected EventHandler<CommandExecuteEventArgs> commandHandler;
        protected IMenuCommandService menuService;

        public event MenuCommandEventHandler CommandStatusChanged;

        public MenuCommandHandlerBase(IServiceProvider serviceProvider);
        public void AddCommand(CommandSetItem cmd);
        protected void AddCommandExecutor(ICommandExecutor executor, EventHandler statusHandler, bool supported, params CommandID[] ids);
        public void Dispose();
        protected virtual void Dispose(bool disposing);
        private void ExecuteCommand(CommandID cmdID, object[] parameters);
        protected override void Finalize();
        public static void InvokeCommandEx(MenuCommand command, object[] args);
        protected void OnCommandChanged(object sender, EventArgs e);
        private void OnCommandStatusChanged(object sender, MenuCommandEventArgs e);
        private void OnMenuCommand(object sender, CommandExecuteEventArgs e);
        public virtual void UpdateCommandStatus();

        protected class CommandSetItemCollection : CollectionBase
        {
            private IMenuCommandService menuService;

            public CommandSetItemCollection(IMenuCommandService menuService);
            public void Add(CommandSetItem cmd);
            public void Clear(EventHandler commandChangedHandler);
            private void RemoveCommand(CommandSetItem cmd, EventHandler commandChangedHandler);
            public void UpdateStatus();

            public CommandSetItem this[int index] { get; }
        }
    }
}

