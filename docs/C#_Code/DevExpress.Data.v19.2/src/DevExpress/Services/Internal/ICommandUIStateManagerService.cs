namespace DevExpress.Services.Internal
{
    using DevExpress.Utils.Commands;
    using System;

    public interface ICommandUIStateManagerService
    {
        void UpdateCommandUIState(Command command, ICommandUIState state);
    }
}

