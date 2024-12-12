namespace DevExpress.XtraReports.Design.Commands
{
    using System;
    using System.ComponentModel.Design;

    public interface ICommandExecutor : IDisposable
    {
        void ExecCommand(CommandID cmdID, object[] parameters);
    }
}

