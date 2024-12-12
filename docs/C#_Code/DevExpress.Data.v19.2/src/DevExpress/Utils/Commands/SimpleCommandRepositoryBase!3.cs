namespace DevExpress.Utils.Commands
{
    using System;

    public abstract class SimpleCommandRepositoryBase<TCommand, TCommandID, TFactory> : CommandStorageBase<TCommand, TCommandID, TFactory>, ISimpleCommandRepository<TCommand, TCommandID>, ICommandStorage<TCommand, TCommandID> where TCommand: Command where TCommandID: struct where TFactory: class
    {
        private readonly Func<TFactory, TCommand> getCommand;

        public SimpleCommandRepositoryBase(Func<TFactory, TCommand> getCommand);
        public SimpleCommandRepositoryBase(Func<TFactory, TCommand> getCommand, int capacity);
        public TCommand GetCommand(TCommandID commandID);
        public TCommand GetCommand(TCommandID commandID, Func<TCommand> fallback);
        public TCommand GetCommand(TCommandID commandID, Func<ConstructorInfo> getConstructorInfo);
    }
}

