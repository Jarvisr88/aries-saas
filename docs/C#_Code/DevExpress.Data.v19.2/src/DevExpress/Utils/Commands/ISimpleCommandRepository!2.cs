namespace DevExpress.Utils.Commands
{
    using System;

    public interface ISimpleCommandRepository<TCommand, TCommandID> : ICommandStorage<TCommand, TCommandID> where TCommand: Command where TCommandID: struct
    {
        TCommand GetCommand(TCommandID commandID);
        TCommand GetCommand(TCommandID commandID, Func<TCommand> fallback);
        TCommand GetCommand(TCommandID commandID, Func<ConstructorInfo> getConstructorInfo);
    }
}

