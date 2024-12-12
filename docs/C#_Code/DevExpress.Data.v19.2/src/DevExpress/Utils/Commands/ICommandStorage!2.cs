namespace DevExpress.Utils.Commands
{
    using System;
    using System.Reflection;

    public interface ICommandStorage<TCommand, TCommandID> where TCommand: Command where TCommandID: struct
    {
        void AddCommand(TCommandID commandID, ConstructorInfo constructorInfo);
        void RemoveCommand(TCommandID commandID);
    }
}

