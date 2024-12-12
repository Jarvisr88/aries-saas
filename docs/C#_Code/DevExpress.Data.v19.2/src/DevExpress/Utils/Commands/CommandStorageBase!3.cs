namespace DevExpress.Utils.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class CommandStorageBase<TCommand, TCommandID, TFactory> : ICommandStorage<TCommand, TCommandID> where TCommand: Command where TCommandID: struct where TFactory: class
    {
        private readonly CommandStorageBase<TCommand, TCommandID, TFactory>.CommandCache commandCache;

        public CommandStorageBase();
        public CommandStorageBase(int capacity);
        public void AddCommand(TCommandID commandID, ConstructorInfo constructorInfo);
        public void AddCommand(TCommandID commandID, TFactory factory);
        private static TFactory CreateCommandFactory(ConstructorInfo constructorInfo);
        protected TFactory GetFactoryCore(TCommandID commandID);
        private static ParameterExpression[] GetParameterExpressionList(Type[] parameterTypeList);
        public void RemoveCommand(TCommandID commandID);

        protected CommandStorageBase<TCommand, TCommandID, TFactory>.CommandCache Cache { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CommandStorageBase<TCommand, TCommandID, TFactory>.<>c <>9;
            public static Func<ParameterInfo, Type> <>9__7_0;

            static <>c();
            internal Type <CreateCommandFactory>b__7_0(ParameterInfo x);
        }

        private protected class CommandCache : Dictionary<TCommandID, TFactory>
        {
            public CommandCache();
            public CommandCache(int capacity);
        }
    }
}

