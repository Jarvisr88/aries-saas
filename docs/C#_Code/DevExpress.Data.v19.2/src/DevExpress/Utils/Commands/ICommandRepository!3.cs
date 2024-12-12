namespace DevExpress.Utils.Commands
{
    using System;

    public interface ICommandRepository<TCommand, TCommandID, TOwner> : ICommandStorage<TCommand, TCommandID> where TCommand: Command where TCommandID: struct where TOwner: class
    {
        TCommand GetCommand(TCommandID commandID, TOwner owner);
        TCommand GetCommand(TCommandID commandID, TOwner owner, Func<TCommand> fallback);
        TCommand GetCommand(TCommandID commandID, TOwner owner, Func<ConstructorInfo> getConstructorInfo);
    }
}

