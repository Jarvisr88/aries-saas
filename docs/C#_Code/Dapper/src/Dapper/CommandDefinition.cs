namespace Dapper
{
    using System;
    using System.Data;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    [StructLayout(LayoutKind.Sequential)]
    public struct CommandDefinition
    {
        private static SqlMapper.Link<Type, Action<IDbCommand>> commandInitCache;
        internal static CommandDefinition ForCallback(object parameters)
        {
            if (parameters is DynamicParameters)
            {
                return new CommandDefinition(parameters);
            }
            return new CommandDefinition();
        }

        internal void OnCompleted()
        {
            SqlMapper.IParameterCallbacks parameters = this.Parameters as SqlMapper.IParameterCallbacks;
            if (parameters == null)
            {
                SqlMapper.IParameterCallbacks local1 = parameters;
            }
            else
            {
                parameters.OnCompleted();
            }
        }

        public string CommandText { get; }
        public object Parameters { get; }
        public IDbTransaction Transaction { get; }
        public int? CommandTimeout { get; }
        public System.Data.CommandType? CommandType { get; }
        public bool Buffered =>
            (this.Flags & CommandFlags.Buffered) != CommandFlags.None;
        internal bool AddToCache =>
            (this.Flags & CommandFlags.NoCache) == CommandFlags.None;
        public CommandFlags Flags { get; }
        public bool Pipelined =>
            (this.Flags & CommandFlags.Pipelined) != CommandFlags.None;
        public CommandDefinition(string commandText, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = new int?(), System.Data.CommandType? commandType = new System.Data.CommandType?(), CommandFlags flags = 1, System.Threading.CancellationToken cancellationToken = new System.Threading.CancellationToken())
        {
            this.<CommandText>k__BackingField = commandText;
            this.<Parameters>k__BackingField = parameters;
            this.<Transaction>k__BackingField = transaction;
            this.<CommandTimeout>k__BackingField = commandTimeout;
            this.<CommandType>k__BackingField = commandType;
            this.<Flags>k__BackingField = flags;
            this.<CancellationToken>k__BackingField = cancellationToken;
        }

        private CommandDefinition(object parameters)
        {
            this = new CommandDefinition();
            this.<Parameters>k__BackingField = parameters;
        }

        public System.Threading.CancellationToken CancellationToken { get; }
        internal IDbCommand SetupCommand(IDbConnection cnn, Action<IDbCommand, object> paramReader)
        {
            IDbCommand command = cnn.CreateCommand();
            Action<IDbCommand> init = GetInit(command.GetType());
            if (init != null)
            {
                init(command);
            }
            if (this.Transaction != null)
            {
                command.Transaction = this.Transaction;
            }
            command.CommandText = this.CommandText;
            if (this.CommandTimeout != null)
            {
                command.CommandTimeout = this.CommandTimeout.Value;
            }
            else if (SqlMapper.Settings.CommandTimeout != null)
            {
                command.CommandTimeout = SqlMapper.Settings.CommandTimeout.Value;
            }
            if (this.CommandType != null)
            {
                command.CommandType = this.CommandType.Value;
            }
            if (paramReader != null)
            {
                paramReader(command, this.Parameters);
            }
            return command;
        }

        private static Action<IDbCommand> GetInit(Type commandType)
        {
            Action<IDbCommand> action;
            if (commandType == null)
            {
                return null;
            }
            if (!SqlMapper.Link<Type, Action<IDbCommand>>.TryGet(commandInitCache, commandType, out action))
            {
                MethodInfo methodInfo = GetBasicPropertySetter(commandType, "BindByName", typeof(bool));
                MethodInfo info2 = GetBasicPropertySetter(commandType, "InitialLONGFetchSize", typeof(int));
                action = null;
                if ((methodInfo != null) || (info2 != null))
                {
                    Type[] parameterTypes = new Type[] { typeof(IDbCommand) };
                    DynamicMethod method = new DynamicMethod(commandType.Name + "_init", null, parameterTypes);
                    ILGenerator iLGenerator = method.GetILGenerator();
                    if (methodInfo != null)
                    {
                        iLGenerator.Emit(OpCodes.Ldarg_0);
                        iLGenerator.Emit(OpCodes.Castclass, commandType);
                        iLGenerator.Emit(OpCodes.Ldc_I4_1);
                        iLGenerator.EmitCall(OpCodes.Callvirt, methodInfo, null);
                    }
                    if (info2 != null)
                    {
                        iLGenerator.Emit(OpCodes.Ldarg_0);
                        iLGenerator.Emit(OpCodes.Castclass, commandType);
                        iLGenerator.Emit(OpCodes.Ldc_I4_M1);
                        iLGenerator.EmitCall(OpCodes.Callvirt, info2, null);
                    }
                    iLGenerator.Emit(OpCodes.Ret);
                    action = (Action<IDbCommand>) method.CreateDelegate(typeof(Action<IDbCommand>));
                }
                SqlMapper.Link<Type, Action<IDbCommand>>.TryAdd(ref commandInitCache, commandType, ref action);
            }
            return action;
        }

        private static MethodInfo GetBasicPropertySetter(Type declaringType, string name, Type expectedType)
        {
            PropertyInfo property = declaringType.GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            return (((property == null) || (!property.CanWrite || (!(property.PropertyType == expectedType) || (property.GetIndexParameters().Length != 0)))) ? null : property.GetSetMethod());
        }
    }
}

