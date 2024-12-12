namespace DevExpress.Services
{
    using DevExpress.Utils.Commands;
    using System;

    public interface ICommandExecutionListenerService
    {
        void BeginCommandExecution(Command command, ICommandUIState state);
        void EndCommandExecution(Command command, ICommandUIState state);
    }
}

