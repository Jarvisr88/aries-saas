namespace DevExpress.Utils.Commands
{
    using System;

    public abstract class CommandRepositoryBase<TCommand, TCommandID, TOwner, TArgument> : CommandStorageBase<TCommand, TCommandID, CommandFactory<TCommand, TOwner>>, ICommandRepository<TCommand, TCommandID, TArgument>, ICommandStorage<TCommand, TCommandID> where TCommand: Command where TCommandID: struct where TOwner: class where TArgument: class
    {
        public CommandRepositoryBase();
        public CommandRepositoryBase(int capacity);
        public TCommand GetCommand(TCommandID commandID, TArgument argument);
        public TCommand GetCommand(TCommandID commandID, TArgument argument, Func<TCommand> fallback);
        public TCommand GetCommand(TCommandID commandID, TArgument argument, Func<ConstructorInfo> getConstructorInfo);
    }
}

