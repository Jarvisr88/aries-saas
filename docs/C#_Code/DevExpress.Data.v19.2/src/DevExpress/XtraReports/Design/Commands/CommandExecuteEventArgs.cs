namespace DevExpress.XtraReports.Design.Commands
{
    using System;

    public class CommandExecuteEventArgs : EventArgs
    {
        private object[] args;

        public CommandExecuteEventArgs(object[] args);

        public object[] Args { get; }
    }
}

