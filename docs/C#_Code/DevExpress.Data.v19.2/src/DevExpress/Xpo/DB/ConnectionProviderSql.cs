namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Db;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using DevExpress.Xpo.DB.Exceptions;
    using DevExpress.Xpo.DB.Helpers;
    using DevExpress.Xpo.Helpers;
    using DevExpress.Xpo.Logger;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.ExceptionServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Transactions;

    public abstract class ConnectionProviderSql : DataStoreBase, ISqlGeneratorFormatterEx, ISqlGeneratorFormatter, ISqlGeneratorFormatterSupportSkipTake, ISqlGeneratorFormatterSupportOuterApply, ISqlDataStore, IDataStore, IDataStoreAsync, IDataStoreSchemaExplorer, IDataStoreSchemaExplorerSp, ICommandChannel, ICommandChannelAsync
    {
        private readonly ColumnTypeResolver columnTypeResolver;
        public static bool UseLegacyTimeSpanSupport = false;
        public static bool EnableConnectionStringArgumentEscaping = true;
        private Random randomizer;
        private bool explicitTransaction;
        private IDbConnection connection;
        private string connectString;
        public static int? GlobalDefaultCommandTimeout = 300;
        public int? DefaultCommandTimeout;
        private readonly Dictionary<string, ICustomFunctionOperatorFormattable> customFunctionsByName;
        private readonly Dictionary<string, ICustomAggregateFormattable> customAggregatesByName;
        public const string IdentityColumnMagicName = "XpoIdentityColumn";
        private IDbTransaction transaction;
        protected static TraceSwitch xpoSwitch = new TraceSwitch("XPO", "");
        private LohPooled.OrdinaryDictionary<IDbCommand, PreparedCommandInfo> acquiredCommands;
        private LohPooled.OrdinaryDictionary<Query, PreparedCommandInfo> preparedCommands;
        public static int MaxDeadLockTryCount = 3;
        public static int MaxDeadLockRetryDelayMilliseconds = 500;
        private EventHandler reconnected;

        public event EventHandler Reconnected
        {
            add
            {
                this.reconnected += value;
            }
            remove
            {
                this.reconnected -= value;
            }
        }

        protected ConnectionProviderSql(IDbConnection connection, AutoCreateOption autoCreateOption) : base(autoCreateOption)
        {
            this.customFunctionsByName = new Dictionary<string, ICustomFunctionOperatorFormattable>(StringExtensions.ComparerInvariantCultureIgnoreCase);
            this.customAggregatesByName = new Dictionary<string, ICustomAggregateFormattable>(StringExtensions.ComparerInvariantCultureIgnoreCase);
            IncrementPerformanceCounters();
            this.columnTypeResolver = new ColumnTypeResolver(this);
            this.connection = connection;
            this.connectString = connection.ConnectionString;
            if (this.Connection.State != ConnectionState.Open)
            {
                this.CreateDataBase();
            }
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        [CompilerGenerated, DebuggerHidden]
        private System.Threading.Tasks.Task<ModificationResult> <>n__0(CancellationToken cancellationToken, ModificationStatement[] dmlStatements) => 
            base.ModifyDataAsync(cancellationToken, dmlStatements);

        [CompilerGenerated, DebuggerHidden]
        private System.Threading.Tasks.Task<SelectedData> <>n__1(CancellationToken cancellationToken, SelectStatement[] selects) => 
            base.SelectDataAsync(cancellationToken, selects);

        protected void BeginTransaction()
        {
            if (!this.explicitTransaction)
            {
                this.BeginTransactionCore(null);
            }
        }

        protected virtual void BeginTransactionCore(object il)
        {
            try
            {
                this.OpenConnection();
                if (System.Transactions.Transaction.Current == null)
                {
                    this.transaction = this.ConnectionBeginTransaction(il);
                }
                this.CreateCommandPool();
            }
            catch (Exception exception)
            {
                if (!this.IsConnectionBroken(exception))
                {
                    throw;
                }
                this.DoReconnect();
                this.transaction = this.ConnectionBeginTransaction(il);
                this.CreateCommandPool();
            }
        }

        protected bool CanRetryIfDeadlock(int tryCount, Exception e)
        {
            if (!this.explicitTransaction && ((System.Transactions.Transaction.Current == null) && (tryCount <= MaxDeadLockTryCount)))
            {
                bool flag = false;
                Exception innerException = e;
                while (true)
                {
                    if ((innerException != null) && !flag)
                    {
                        flag = this.IsDeadLock(innerException);
                        if (!flag)
                        {
                            innerException = innerException.InnerException;
                            continue;
                        }
                    }
                    if (!flag)
                    {
                        break;
                    }
                    if (tryCount > 1)
                    {
                        Thread.Sleep(this.Randomizer.Next(MaxDeadLockRetryDelayMilliseconds));
                    }
                    return true;
                }
            }
            return false;
        }

        protected void CloseConnectionInternal()
        {
            this.ReleaseCommandPool();
            this.Connection.Close();
        }

        public abstract ICollection CollectTablesToCreate(ICollection tables);
        protected bool ColumnIsIdentity(DBTable table, string column)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].Name == column)
                {
                    return table.Columns[i].IsIdentity;
                }
            }
            throw new InvalidOperationException("Column not found");
        }

        protected abstract void CommandBuilderDeriveParameters(IDbCommand command);
        protected virtual System.Threading.Tasks.Task<int> CommandExecuteNonQueryAsync(IDbCommand command, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            DbCommand command2 = command as DbCommand;
            if (command2 != null)
            {
                return command2.ExecuteNonQueryAsync(cancellationToken);
            }
            object[] args = new object[] { command.GetType().FullName, "CommandExecuteNonQueryAsync" };
            throw new NotSupportedException(DbRes.GetString("ConnectionProviderSql_DbCommandAsyncOperationsNotSupported", args));
        }

        [AsyncStateMachine(typeof(<CommandExecuteReaderAsync>d__279))]
        protected virtual System.Threading.Tasks.Task<IDataReader> CommandExecuteReaderAsync(IDbCommand command, CommandBehavior commandBehavior, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            <CommandExecuteReaderAsync>d__279 d__;
            d__.command = command;
            d__.commandBehavior = commandBehavior;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<IDataReader>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<CommandExecuteReaderAsync>d__279>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected virtual System.Threading.Tasks.Task<object> CommandExecuteScalarAsync(IDbCommand command, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            DbCommand command2 = command as DbCommand;
            if (command2 != null)
            {
                return command2.ExecuteScalarAsync(cancellationToken);
            }
            object[] args = new object[] { command.GetType().FullName, "CommandExecuteScalarAsync" };
            throw new NotSupportedException(DbRes.GetString("ConnectionProviderSql_DbCommandAsyncOperationsNotSupported", args));
        }

        protected void CommitTransaction()
        {
            if (!this.explicitTransaction)
            {
                this.CommitTransactionCore();
            }
        }

        protected virtual void CommitTransactionCore()
        {
            if (this.CommandPoolBehavior != DevExpress.Xpo.DB.CommandPoolBehavior.ConnectionSession)
            {
                this.ReleaseCommandPool();
            }
            if (this.transaction != null)
            {
                this.transaction.Commit();
            }
            this.transaction = null;
        }

        public virtual string ComposeSafeColumnName(string columnName) => 
            this.GetSafeObjectName(columnName, this.GetSafeNameRoot(columnName), this.GetSafeNameColumnMaxLength());

        public virtual string ComposeSafeConstraintName(string constraintName) => 
            this.GetSafeObjectName(constraintName, this.GetSafeNameRoot(constraintName), this.GetSafeNameConstraintMaxLength());

        public virtual string ComposeSafeSchemaName(string tableName)
        {
            int index = tableName.IndexOf('.');
            if (index <= 0)
            {
                return string.Empty;
            }
            tableName = tableName.Substring(0, index);
            return this.GetSafeObjectName(tableName, this.GetSafeNameRoot(tableName), this.GetSafeNameTableMaxLength());
        }

        public virtual string ComposeSafeTableName(string tableName)
        {
            int index = tableName.IndexOf('.');
            if (index > 0)
            {
                tableName = tableName.Remove(0, index + 1);
            }
            return this.GetSafeObjectName(tableName, this.GetSafeNameRoot(tableName), this.GetSafeNameTableMaxLength());
        }

        private IDbTransaction ConnectionBeginTransaction(object il) => 
            (il != null) ? this.Connection.BeginTransaction((System.Data.IsolationLevel) il) : this.Connection.BeginTransaction();

        protected object ConvertParameter(object parameter)
        {
            if (parameter == null)
            {
                return DBNull.Value;
            }
            TypeCode typeCode = DXTypeExtensions.GetTypeCode(parameter.GetType());
            return this.ConvertToDbParameter(parameter, typeCode);
        }

        protected virtual object ConvertToDbParameter(object clientValue, TypeCode clientValueTypeCode)
        {
            if (clientValueTypeCode != TypeCode.Object)
            {
                if (clientValueTypeCode == TypeCode.Char)
                {
                    return new string((char) clientValue, 1);
                }
            }
            else if (UseLegacyTimeSpanSupport && (clientValue is TimeSpan))
            {
                return ((TimeSpan) clientValue).TotalSeconds;
            }
            return clientValue;
        }

        public virtual void CreateColumn(DBTable table, DBColumn column)
        {
            object[] args = new object[] { this.FormatTableSafe(table), this.FormatColumnSafe(column.Name), this.GetSqlCreateColumnFullAttributes(table, column, false) };
            this.ExecuteSqlSchemaUpdate("Column", column.Name, table.Name, string.Format(CultureInfo.InvariantCulture, "alter table {0} add {1} {2}", args));
        }

        public virtual IDbCommand CreateCommand()
        {
            this.OpenConnection();
            IDbCommand command = this.Connection.CreateCommand();
            command.Connection = this.Connection;
            command.Transaction = this.Transaction;
            if ((this.DefaultCommandTimeout != null) || (GlobalDefaultCommandTimeout != null))
            {
                command.CommandTimeout = (this.DefaultCommandTimeout != null) ? this.DefaultCommandTimeout.Value : GlobalDefaultCommandTimeout.Value;
            }
            return command;
        }

        protected virtual IDbCommand CreateCommand(Query query)
        {
            IDbCommand command = this.CreateCommand();
            this.PrepareParameters(command, query);
            command.CommandText = query.Sql;
            Trace.WriteLineIf(xpoSwitch.TraceInfo, new DbCommandTracer(command));
            return command;
        }

        private void CreateCommandPool()
        {
            if ((this.CommandPoolBehavior != DevExpress.Xpo.DB.CommandPoolBehavior.None) && ((this.preparedCommands == null) || (this.acquiredCommands == null)))
            {
                this.preparedCommands = new LohPooled.OrdinaryDictionary<Query, PreparedCommandInfo>(20, new PreparedCommandQueryComparer());
                this.acquiredCommands = new LohPooled.OrdinaryDictionary<IDbCommand, PreparedCommandInfo>();
            }
        }

        protected abstract IDbConnection CreateConnection();
        protected abstract void CreateDataBase();
        public virtual void CreateForeignKey(DBTable table, DBForeignKey foreignKey)
        {
            StringCollection collection = new StringCollection();
            for (int i = 0; i < foreignKey.Columns.Count; i++)
            {
                collection.Add(this.FormatColumnSafe(foreignKey.Columns[i]));
            }
            StringCollection strings2 = new StringCollection();
            for (int j = 0; j < foreignKey.PrimaryKeyTableKeyColumns.Count; j++)
            {
                strings2.Add(this.FormatColumnSafe(foreignKey.PrimaryKeyTableKeyColumns[j]));
            }
            object[] args = new object[] { this.FormatTableSafe(table), this.FormatConstraintSafe(this.GetForeignKeyName(foreignKey, table)), StringListHelper.DelimitedText(collection, ","), this.FormatTable(this.ComposeSafeSchemaName(foreignKey.PrimaryKeyTable), this.ComposeSafeTableName(foreignKey.PrimaryKeyTable)), StringListHelper.DelimitedText(strings2, ",") };
            this.ExecuteSqlSchemaUpdate("ForeignKey", this.GetForeignKeyName(foreignKey, table), table.Name, string.Format(CultureInfo.InvariantCulture, this.CreateForeignKeyTemplate, args));
        }

        public virtual void CreateIndex(DBTable table, DBIndex index)
        {
            StringCollection collection = new StringCollection();
            for (int i = 0; i < index.Columns.Count; i++)
            {
                collection.Add(this.FormatColumnSafe(index.Columns[i]));
            }
            object[] objArray1 = new object[4];
            object[] objArray2 = new object[4];
            objArray2[0] = index.IsUnique ? "unique" : string.Empty;
            object[] args = objArray2;
            args[1] = this.FormatConstraintSafe(this.GetIndexName(index, table));
            args[2] = this.FormatTableSafe(table);
            args[3] = StringListHelper.DelimitedText(collection, ",");
            this.ExecuteSqlSchemaUpdate("Index", this.GetIndexName(index, table), table.Name, string.Format(CultureInfo.InvariantCulture, this.CreateIndexTemplate, args));
        }

        protected virtual IDataParameter CreateParameter(IDbCommand command) => 
            command.CreateParameter();

        protected virtual IDataParameter CreateParameter(IDbCommand command, object value, string name)
        {
            IDataParameter parameter = this.CreateParameter(command);
            parameter.Value = value;
            parameter.ParameterName = name;
            parameter.DbType ??= DbType.String;
            return parameter;
        }

        public virtual void CreatePrimaryKey(DBTable table)
        {
            StringCollection collection = new StringCollection();
            for (int i = 0; i < table.PrimaryKey.Columns.Count; i++)
            {
                collection.Add(this.FormatColumnSafe(table.PrimaryKey.Columns[i]));
            }
            object[] args = new object[] { this.FormatTableSafe(table), this.FormatConstraintSafe(this.GetPrimaryKeyName(table.PrimaryKey, table)), StringListHelper.DelimitedText(collection, ",") };
            this.ExecuteSqlSchemaUpdate("PrimaryKey", this.GetPrimaryKeyName(table.PrimaryKey, table), table.Name, string.Format(CultureInfo.InvariantCulture, "alter table {0} add constraint {1} primary key ({2})", args));
        }

        public virtual void CreateTable(DBTable table)
        {
            string str = "";
            foreach (DBColumn column in table.Columns)
            {
                if (str.Length > 0)
                {
                    str = str + ", ";
                }
                str = str + this.FormatColumnSafe(column.Name) + " " + this.GetSqlCreateColumnFullAttributes(table, column, true);
            }
            object[] args = new object[] { this.FormatTableSafe(table), str };
            this.ExecuteSqlSchemaUpdate("Table", table.Name, string.Empty, string.Format(CultureInfo.InvariantCulture, "create table {0} ({1})", args));
        }

        private static void DecrementPerformanceCounters()
        {
            DecrementPerformanceCountersCore();
        }

        private static void DecrementPerformanceCountersCore()
        {
            PerformanceCounters.SqlDataStoreCount.Decrement();
            PerformanceCounters.SqlDataStoreFinalized.Increment();
        }

        object ICommandChannel.Do(string command, object args)
        {
            using (this.LockHelper.Lock())
            {
                return this.DoInternal(command, args);
            }
        }

        [AsyncStateMachine(typeof(<DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__257))]
        System.Threading.Tasks.Task<object> ICommandChannelAsync.DoAsync(string command, object args, CancellationToken cancellationToken)
        {
            <DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__257 d__;
            d__.<>4__this = this;
            d__.command = command;
            d__.args = args;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__257>(ref d__);
            return d__.<>t__builder.Task;
        }

        private ParameterValue DoDeleteRecord(DeleteStatement root, TaggedParametersHolder identities)
        {
            int num = this.ExecSql(new DeleteSqlGenerator(this, identities, new Dictionary<OperandValue, string>()).GenerateSql(root));
            if ((root.RecordsAffected == 0) || (root.RecordsAffected == num))
            {
                return null;
            }
            this.RollbackTransaction();
            throw new LockingException();
        }

        [AsyncStateMachine(typeof(<DoDeleteRecordAsync>d__203))]
        private System.Threading.Tasks.Task<ParameterValue> DoDeleteRecordAsync(DeleteStatement root, TaggedParametersHolder identities, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            <DoDeleteRecordAsync>d__203 d__;
            d__.<>4__this = this;
            d__.root = root;
            d__.identities = identities;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<ParameterValue>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<DoDeleteRecordAsync>d__203>(ref d__);
            return d__.<>t__builder.Task;
        }

        private ParameterValue DoInsertRecord(InsertStatement root, TaggedParametersHolder identities)
        {
            if (root.IdentityParameter.ReferenceEqualsNull())
            {
                this.ExecSql(new InsertSqlGenerator(this, identities, new Dictionary<OperandValue, string>()).GenerateSql(root));
                return null;
            }
            identities.ConsolidateIdentity(root.IdentityParameter);
            DBColumnType identityColumnType = root.IdentityColumnType;
            if (identityColumnType == DBColumnType.Int32)
            {
                root.IdentityParameter.Value = (int) this.GetIdentity(root, identities);
            }
            else
            {
                if (identityColumnType != DBColumnType.Int64)
                {
                    throw new NotSupportedException($"The AutoIncremented key with '{root.IdentityColumnType}' type is not supported for '{base.GetType()}'.");
                }
                root.IdentityParameter.Value = this.GetIdentity(root, identities);
            }
            return root.IdentityParameter;
        }

        [AsyncStateMachine(typeof(<DoInsertRecordAsync>d__199))]
        private System.Threading.Tasks.Task<ParameterValue> DoInsertRecordAsync(InsertStatement root, TaggedParametersHolder identities, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            <DoInsertRecordAsync>d__199 d__;
            d__.<>4__this = this;
            d__.root = root;
            d__.identities = identities;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<ParameterValue>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<DoInsertRecordAsync>d__199>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected virtual object DoInternal(string command, object args)
        {
            uint num = <PrivateImplementationDetails>.ComputeStringHash(command);
            if (num <= 0x5b7989eb)
            {
                if (num <= 0x4fd322ac)
                {
                    if (num == 0x41ea026e)
                    {
                        if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithMetadata")
                        {
                            return FixDBNull(new SelectedData(this.SelectDataSimple(new Query((string) args), null, true)));
                        }
                    }
                    else if (num != 0x45b7f1c3)
                    {
                        if ((num == 0x4fd322ac) && (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteNonQuerySQLWithParams"))
                        {
                            return this.ExecSql(this.ProcessSqlQueryCommandArgument(args));
                        }
                    }
                    else if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitCommitTransaction")
                    {
                        this.ExplicitCommitTransaction();
                        return null;
                    }
                }
                else if (num == 0x52efb8eb)
                {
                    if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitBeginTransaction")
                    {
                        if ((args != null) && (args is System.Data.IsolationLevel))
                        {
                            this.ExplicitBeginTransaction((System.Data.IsolationLevel) args);
                        }
                        else
                        {
                            this.ExplicitBeginTransaction();
                        }
                        return null;
                    }
                }
                else if (num != 0x57f6c538)
                {
                    if ((num == 0x5b7989eb) && (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQL"))
                    {
                        return FixDBNull(new SelectedData(this.SelectDataSimple(new Query((string) args), null, false)));
                    }
                }
                else if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithMetadataWithParams")
                {
                    return FixDBNull(new SelectedData(this.SelectDataSimple(this.ProcessSqlQueryCommandArgument(args), null, true)));
                }
            }
            else if (num <= 0x92c458fd)
            {
                if (num == 0x6a460874)
                {
                    if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitRollbackTransaction")
                    {
                        this.ExplicitRollbackTransaction();
                        return null;
                    }
                }
                else if (num == 0x8e14028a)
                {
                    if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteNonQuerySQL")
                    {
                        return this.ExecSql(new Query((string) args));
                    }
                }
                else if ((num == 0x92c458fd) && (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedure"))
                {
                    CommandChannelHelper.SprocQuery query = args as CommandChannelHelper.SprocQuery;
                    if (query == null)
                    {
                        throw new ArgumentException($"Wrong parameter set for command '{"DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedure"}'.");
                    }
                    return FixDBNull(this.ExecuteSproc(query.SprocName, query.Parameters));
                }
            }
            else if (num <= 0xb11c5e39)
            {
                if (num != 0xab79b86b)
                {
                    if ((num == 0xb11c5e39) && (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithParams"))
                    {
                        return FixDBNull(new SelectedData(this.SelectDataSimple(this.ProcessSqlQueryCommandArgument(args), null, false)));
                    }
                }
                else if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteScalarSQLWithParams")
                {
                    return this.FixDBNullScalar(this.GetScalar(this.ProcessSqlQueryCommandArgument(args)));
                }
            }
            else if (num != 0xc3b53055)
            {
                if ((num == 0xfa72d4b9) && (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteScalarSQL"))
                {
                    return this.FixDBNullScalar(this.GetScalar(new Query((string) args)));
                }
            }
            else if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedureParametrized")
            {
                CommandChannelHelper.SprocQuery query2 = args as CommandChannelHelper.SprocQuery;
                if (query2 == null)
                {
                    throw new ArgumentException($"Wrong parameter set for command '{"DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedureParametrized"}'.");
                }
                return FixDBNull(this.ExecuteSprocParametrized(query2.SprocName, query2.Parameters));
            }
            throw new NotSupportedException($"Command '{command}' is not supported.");
        }

        [AsyncStateMachine(typeof(<DoInternalAsync>d__260))]
        protected virtual System.Threading.Tasks.Task<object> DoInternalAsync(string command, object args, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            <DoInternalAsync>d__260 d__;
            d__.<>4__this = this;
            d__.command = command;
            d__.args = args;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<DoInternalAsync>d__260>(ref d__);
            return d__.<>t__builder.Task;
        }

        private void DoReconnect()
        {
            this.ReleaseCommandPool();
            this.OpenConnectionInternal();
            this.OnReconnected();
        }

        private ParameterValue DoUpdateRecord(UpdateStatement root, TaggedParametersHolder identities)
        {
            Query query = new UpdateSqlGenerator(this, identities, new Dictionary<OperandValue, string>()).GenerateSql(root);
            if (query.Sql != null)
            {
                int num = this.ExecSql(query);
                if ((root.RecordsAffected != 0) && (root.RecordsAffected != num))
                {
                    this.RollbackTransaction();
                    throw new LockingException();
                }
            }
            return null;
        }

        [AsyncStateMachine(typeof(<DoUpdateRecordAsync>d__201))]
        private System.Threading.Tasks.Task<ParameterValue> DoUpdateRecordAsync(UpdateStatement root, TaggedParametersHolder identities, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            <DoUpdateRecordAsync>d__201 d__;
            d__.<>4__this = this;
            d__.root = root;
            d__.identities = identities;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<ParameterValue>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<DoUpdateRecordAsync>d__201>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static string EscapeConnectionStringArgument(string argument) => 
            EnableConnectionStringArgumentEscaping ? ConnectionStringParser.EscapeArgument(argument) : argument;

        public int ExecSql(Query query)
        {
            int num;
            using (this.LockHelper.Lock())
            {
                IDbCommand commandFromPool = this.GetCommandFromPool(query);
                try
                {
                    try
                    {
                        num = this.InternalExecSql(commandFromPool);
                    }
                    catch (Exception exception)
                    {
                        if ((this.Transaction != null) || !this.IsConnectionBroken(exception))
                        {
                            throw;
                        }
                        this.DoReconnect();
                        num = this.InternalExecSql(commandFromPool);
                    }
                }
                catch (Exception exception2)
                {
                    throw this.WrapException(exception2, commandFromPool);
                }
                finally
                {
                    this.ReleasePooledCommand(commandFromPool);
                }
            }
            return num;
        }

        protected void ExecSql(QueryCollection queries)
        {
            foreach (Query query in queries)
            {
                this.ExecSql(query);
            }
        }

        [AsyncStateMachine(typeof(<ExecSqlAsync>d__196))]
        public System.Threading.Tasks.Task<int> ExecSqlAsync(Query query, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken = new CancellationToken())
        {
            <ExecSqlAsync>d__196 d__;
            d__.<>4__this = this;
            d__.query = query;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecSqlAsync>d__196>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected virtual SelectedData ExecuteSproc(string sprocName, params OperandValue[] parameters)
        {
            using (IDbCommand command = this.CreateCommand())
            {
                IDataParameter parameter;
                List<IDataParameter> list;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = this.FormatTable(this.ComposeSafeSchemaName(sprocName), this.ComposeSafeTableName(sprocName));
                this.CommandBuilderDeriveParameters(command);
                this.PrepareParametersForExecuteSproc(parameters, command, out list, out parameter);
                return this.ExecuteSprocInternal(command, parameter, list);
            }
        }

        [AsyncStateMachine(typeof(<ExecuteSprocAsync>d__271))]
        protected virtual System.Threading.Tasks.Task<SelectedData> ExecuteSprocAsync(AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken, string sprocName, params OperandValue[] parameters)
        {
            <ExecuteSprocAsync>d__271 d__;
            d__.<>4__this = this;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.sprocName = sprocName;
            d__.parameters = parameters;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteSprocAsync>d__271>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected virtual SelectedData ExecuteSprocInternal(IDbCommand command, IDataParameter returnParameter, List<IDataParameter> outParameters)
        {
            List<SelectStatementResult> selectedStatmentResults = this.GetSelectedStatmentResults(command);
            if (outParameters.Count > 0)
            {
                Func<IDataParameter, SelectStatementResultRow> selector = <>c.<>9__264_0;
                if (<>c.<>9__264_0 == null)
                {
                    Func<IDataParameter, SelectStatementResultRow> local1 = <>c.<>9__264_0;
                    selector = <>c.<>9__264_0 = op => new SelectStatementResultRow(new object[] { op.ParameterName, op.Value });
                }
                selectedStatmentResults.Add(new SelectStatementResult(outParameters.Select<IDataParameter, SelectStatementResultRow>(selector).ToArray<SelectStatementResultRow>()));
            }
            if (returnParameter != null)
            {
                object[] values = new object[] { returnParameter.Value };
                SelectStatementResultRow[] rows = new SelectStatementResultRow[] { new SelectStatementResultRow(values) };
                selectedStatmentResults.Add(new SelectStatementResult(rows));
            }
            return new SelectedData(selectedStatmentResults.ToArray());
        }

        [AsyncStateMachine(typeof(<ExecuteSprocInternalAsync>d__265))]
        protected virtual System.Threading.Tasks.Task<SelectedData> ExecuteSprocInternalAsync(IDbCommand command, IDataParameter returnParameter, List<IDataParameter> outParameters, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            <ExecuteSprocInternalAsync>d__265 d__;
            d__.<>4__this = this;
            d__.command = command;
            d__.returnParameter = returnParameter;
            d__.outParameters = outParameters;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteSprocInternalAsync>d__265>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected virtual SelectedData ExecuteSprocParametrized(string sprocName, params OperandValue[] parameters)
        {
            using (IDbCommand command = this.CreateCommand())
            {
                List<IDataParameter> list;
                IDataParameter parameter;
                this.PrepareCommandForExecuteSprocParametrized(command, sprocName, parameters, out list, out parameter);
                return this.ExecuteSprocInternal(command, parameter, list);
            }
        }

        [AsyncStateMachine(typeof(<ExecuteSprocParametrizedAsync>d__267))]
        protected virtual System.Threading.Tasks.Task<SelectedData> ExecuteSprocParametrizedAsync(AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken, string sprocName, params OperandValue[] parameters)
        {
            <ExecuteSprocParametrizedAsync>d__267 d__;
            d__.<>4__this = this;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.sprocName = sprocName;
            d__.parameters = parameters;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteSprocParametrizedAsync>d__267>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected void ExecuteSqlSchemaUpdate(string objectTypeName, string objectName, string parentObjectName, string textSql)
        {
            if (!this.CanCreateSchema)
            {
                throw new SchemaCorrectionNeededException(textSql);
            }
            try
            {
                this.ExecSql(new Query(textSql));
            }
            catch (Exception exception)
            {
                throw new UnableToCreateDBObjectException(objectTypeName, objectName, parentObjectName, exception);
            }
        }

        public void ExplicitBeginTransaction()
        {
            this.BeginTransactionCore(null);
            this.explicitTransaction = true;
        }

        public void ExplicitBeginTransaction(System.Data.IsolationLevel isolationLevel)
        {
            this.BeginTransactionCore(isolationLevel);
            this.explicitTransaction = true;
        }

        public void ExplicitCommitTransaction()
        {
            this.CommitTransactionCore();
            this.explicitTransaction = false;
        }

        public void ExplicitRollbackTransaction()
        {
            this.RollbackTransactionCore();
            this.explicitTransaction = false;
        }

        private static object[][] FillMetaData(IDataReader reader, Type[] fieldTypes)
        {
            object[][] objArray = new object[reader.FieldCount][];
            bool flag = fieldTypes != null;
            for (int i = reader.FieldCount - 1; i >= 0; i--)
            {
                Type type = flag ? fieldTypes[i] : reader.GetFieldType(i);
                objArray[i] = new object[] { reader.GetName(i), reader.GetDataTypeName(i), DBColumn.GetColumnType(type, true).ToString() };
            }
            return objArray;
        }

        private ReformatReadValueArgs[] FillReformatters(CriteriaOperatorCollection targets)
        {
            ReformatReadValueArgs[] argsArray = new ReformatReadValueArgs[targets.Count];
            for (int i = 0; i < targets.Count; i++)
            {
                Type nullableType = this.columnTypeResolver.ResolveTypeInternal(targets[i]);
                Type underlyingType = Nullable.GetUnderlyingType(nullableType);
                Type targetType = underlyingType;
                if (underlyingType == null)
                {
                    Type local1 = underlyingType;
                    targetType = nullableType;
                }
                argsArray[i] = new ReformatReadValueArgs(targetType);
            }
            return argsArray;
        }

        private static Type[] FillTypes(IDataReader reader)
        {
            Type[] typeArray = new Type[reader.FieldCount];
            for (int i = reader.FieldCount - 1; i >= 0; i--)
            {
                typeArray[i] = reader.GetFieldType(i);
            }
            return typeArray;
        }

        ~ConnectionProviderSql()
        {
            DecrementPerformanceCounters();
        }

        internal static SelectedData FixDBNull(SelectedData data)
        {
            if ((data != null) && ((data.ResultSet != null) && (data.ResultSet.Length != 0)))
            {
                foreach (SelectStatementResult result in data.ResultSet)
                {
                    if ((result != null) && ((result.Rows != null) && (result.Rows.Length != 0)))
                    {
                        foreach (SelectStatementResultRow row in result.Rows)
                        {
                            if ((row != null) && ((row.Values != null) && (row.Values.Length != 0)))
                            {
                                for (int i = 0; i < row.Values.Length; i++)
                                {
                                    if (row.Values[i] is DBNull)
                                    {
                                        row.Values[i] = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return data;
        }

        protected object FixDBNullScalar(object data) => 
            (data is DBNull) ? null : data;

        public virtual string FormatBinary(BinaryOperatorType operatorType, string leftOperand, string rightOperand) => 
            BaseFormatterHelper.DefaultFormatBinary(operatorType, leftOperand, rightOperand);

        public abstract string FormatColumn(string columnName);
        public abstract string FormatColumn(string columnName, string tableAlias);
        public string FormatColumnSafe(string columnName) => 
            this.FormatColumn(this.ComposeSafeColumnName(columnName));

        public abstract string FormatConstraint(string constraintName);
        public string FormatConstraintSafe(string constraintName) => 
            this.FormatConstraint(this.ComposeSafeConstraintName(constraintName));

        private string FormatCustomFunction(params string[] operands)
        {
            string[] strArray;
            string functionName = operands[0];
            if (operands.Length <= 1)
            {
                strArray = new string[0];
            }
            else
            {
                strArray = new string[operands.Length - 1];
                Array.Copy(operands, 1, strArray, 0, operands.Length - 1);
            }
            ICustomFunctionOperatorFormattable customFunctionOperator = this.GetCustomFunctionOperator(functionName);
            if (customFunctionOperator == null)
            {
                throw new NotSupportedException($"DefaultFormatFunction for custom({functionName})");
            }
            return customFunctionOperator.Format(base.GetType(), strArray);
        }

        public abstract string FormatDelete(string tableName, string whereClause);
        public virtual string FormatFunction(FunctionOperatorType operatorType, params string[] operands) => 
            ((operatorType == FunctionOperatorType.Custom) || (operatorType == FunctionOperatorType.CustomNonDeterministic)) ? this.FormatCustomFunction(operands) : BaseFormatterHelper.DefaultFormatFunction(operatorType, operands);

        public virtual string FormatFunction(ProcessParameter processParameter, FunctionOperatorType operatorType, params object[] operands)
        {
            string[] strArray = new string[operands.Length];
            for (int i = 0; i < operands.Length; i++)
            {
                strArray[i] = ((i != 0) || ((operatorType != FunctionOperatorType.Custom) && (operatorType != FunctionOperatorType.CustomNonDeterministic))) ? processParameter(operands[i]) : ((string) operands[i]);
            }
            return this.FormatFunction(operatorType, strArray);
        }

        public abstract string FormatInsert(string tableName, string fields, string values);
        public abstract string FormatInsertDefaultValues(string tableName);
        public virtual string FormatOrder(string sortProperty, SortingDirection direction)
        {
            object[] args = new object[] { sortProperty, (direction == SortingDirection.Ascending) ? "asc" : "desc" };
            return string.Format(CultureInfo.InvariantCulture, "{0} {1}", args);
        }

        public virtual string FormatOuterApply(string sql, string alias)
        {
            object[] args = new object[] { sql, alias };
            return string.Format(CultureInfo.InvariantCulture, "outer apply ({0}) {1}", args);
        }

        public virtual string FormatSelect(string selectedPropertiesSql, string fromSql, string whereSql, string orderBySql, string groupBySql, string havingSql, int topSelectedRecords) => 
            this.FormatSelect(selectedPropertiesSql, fromSql, whereSql, orderBySql, groupBySql, havingSql, 0, topSelectedRecords);

        public virtual string FormatSelect(string selectedPropertiesSql, string fromSql, string whereSql, string orderBySql, string groupBySql, string havingSql, int skipSelectedRecords, int topSelectedRecords)
        {
            if (!this.NativeSkipTakeSupported)
            {
                throw new NotSupportedException();
            }
            if ((skipSelectedRecords != 0) && (orderBySql == null))
            {
                throw new InvalidOperationException("Can not skip records without ORDER BY clause.");
            }
            return null;
        }

        public abstract string FormatTable(string schema, string tableName);
        public abstract string FormatTable(string schema, string tableName, string tableAlias);
        public string FormatTableSafe(DBTable table) => 
            this.FormatTable(this.ComposeSafeSchemaName(table.Name), this.ComposeSafeTableName(table.Name));

        public virtual string FormatUnary(UnaryOperatorType operatorType, string operand) => 
            BaseFormatterHelper.DefaultFormatUnary(operatorType, operand);

        public abstract string FormatUpdate(string tableName, string sets, string whereClause);
        public virtual string GenerateStoredProcedures(DBTable table, out string dropLines)
        {
            throw new NotSupportedException();
        }

        public virtual string GenerateStoredProceduresInfoOnce() => 
            string.Empty;

        public static DBColumnType GetColumnType(DbType type, bool suppressExceptionOnUnknown)
        {
            switch (type)
            {
                case DbType.AnsiString:
                case DbType.String:
                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength:
                    return DBColumnType.String;

                case DbType.Binary:
                    return DBColumnType.ByteArray;

                case DbType.Byte:
                    return DBColumnType.Byte;

                case DbType.Boolean:
                    return DBColumnType.Boolean;

                case DbType.Currency:
                case DbType.Decimal:
                case DbType.VarNumeric:
                    return DBColumnType.Decimal;

                case DbType.Date:
                case DbType.DateTime:
                case DbType.Time:
                case DbType.DateTime2:
                case DbType.DateTimeOffset:
                    return DBColumnType.DateTime;

                case DbType.Double:
                    return DBColumnType.Double;

                case DbType.Guid:
                    return DBColumnType.Guid;

                case DbType.Int16:
                    return DBColumnType.Int16;

                case DbType.Int32:
                    return DBColumnType.Int32;

                case DbType.Int64:
                    return DBColumnType.Int64;

                case DbType.Object:
                    if (!suppressExceptionOnUnknown)
                    {
                        throw new InvalidOperationException(type.ToString());
                    }
                    return DBColumnType.Unknown;

                case DbType.SByte:
                    return DBColumnType.SByte;

                case DbType.Single:
                    return DBColumnType.Single;

                case DbType.UInt16:
                    return DBColumnType.UInt16;

                case DbType.UInt32:
                    return DBColumnType.UInt32;

                case DbType.UInt64:
                    return DBColumnType.UInt64;
            }
            throw new InvalidOperationException(type.ToString());
        }

        private IDbCommand GetCommandFromPool(Query query)
        {
            PreparedCommandInfo info;
            if ((this.preparedCommands == null) || (this.acquiredCommands == null))
            {
                if (this.CommandPoolBehavior != DevExpress.Xpo.DB.CommandPoolBehavior.ConnectionSession)
                {
                    return this.CreateCommand(query);
                }
                this.CreateCommandPool();
            }
            IDbCommand command = null;
            if (this.preparedCommands.TryGetValue(query, out info))
            {
                goto TR_0009;
            }
            else
            {
                IDbCommand command2;
                using (List<OperandValue>.Enumerator enumerator = query.Parameters.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            OperandValue current = enumerator.Current;
                            if (current.Value != null)
                            {
                                continue;
                            }
                            command2 = this.CreateCommand(query);
                        }
                        else
                        {
                            goto TR_000C;
                        }
                        break;
                    }
                }
                return command2;
            }
            goto TR_000C;
        TR_0009:
            if (!info.TryAcquireCommand(out command))
            {
                if (info.IsClosed)
                {
                    this.preparedCommands.Remove(query);
                }
            }
            else
            {
                this.acquiredCommands.Add(command, info);
                if ((this.CommandPoolBehavior != DevExpress.Xpo.DB.CommandPoolBehavior.TransactionNoPrepare) && (!info.IsPrepared && (info.AcquireCount > 1)))
                {
                    info.Prepare(this);
                }
            }
            if (command == null)
            {
                return this.CreateCommand(query);
            }
            this.PreparePoolParameters(command, query);
            Trace.WriteLineIf(xpoSwitch.TraceInfo, new DbCommandTracer(command));
            return command;
        TR_000C:
            info = new PreparedCommandInfo(this.CreateCommand(query));
            if (this.preparedCommands.Count >= 10)
            {
                this.RemoveOldestCommand();
            }
            this.preparedCommands.Add(query, info);
            goto TR_0009;
        }

        public ICustomAggregateFormattable GetCustomAggregate(string aggregateName)
        {
            ICustomAggregateFormattable formattable;
            return (!this.customAggregatesByName.TryGetValue(aggregateName, out formattable) ? ((CriteriaOperator.CustomAggregateCount > 0) ? (CriteriaOperator.GetCustomAggregate(aggregateName) as ICustomAggregateFormattable) : null) : formattable);
        }

        public ICustomFunctionOperatorFormattable GetCustomFunctionOperator(string functionName)
        {
            ICustomFunctionOperatorFormattable formattable;
            return (!this.customFunctionsByName.TryGetValue(functionName, out formattable) ? ((CriteriaOperator.CustomFunctionCount > 0) ? (CriteriaOperator.GetCustomFunction(functionName) as ICustomFunctionOperatorFormattable) : null) : formattable);
        }

        protected DBColumn GetDbColumnByName(DBTable table, string name)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].Name == name)
                {
                    return table.Columns[i];
                }
            }
            throw new InvalidOperationException("Couldn't find primary key column.");
        }

        protected string GetDbNameHashString(string dbName)
        {
            uint num = 0;
            foreach (char ch in dbName)
            {
                uint num3 = num >> 0x19;
                num = ((num << 7) ^ num3) ^ ch;
            }
            return num.ToString("X8", CultureInfo.InvariantCulture);
        }

        public static DbType GetDbType(DBColumnType type)
        {
            switch (type)
            {
                case DBColumnType.Unknown:
                    return DbType.Object;

                case DBColumnType.Boolean:
                    return DbType.Boolean;

                case DBColumnType.Byte:
                    return DbType.Byte;

                case DBColumnType.SByte:
                    return DbType.SByte;

                case DBColumnType.Decimal:
                    return DbType.Decimal;

                case DBColumnType.Double:
                    return DbType.Double;

                case DBColumnType.Single:
                    return DbType.Single;

                case DBColumnType.Int32:
                    return DbType.Int32;

                case DBColumnType.UInt32:
                    return DbType.UInt32;

                case DBColumnType.Int16:
                    return DbType.Int16;

                case DBColumnType.UInt16:
                    return DbType.UInt16;

                case DBColumnType.Int64:
                    return DbType.Int64;

                case DBColumnType.UInt64:
                    return DbType.UInt64;

                case DBColumnType.String:
                    return DbType.String;

                case DBColumnType.DateTime:
                    return DbType.DateTime;

                case DBColumnType.Guid:
                    return DbType.Guid;

                case DBColumnType.ByteArray:
                    return DbType.Binary;
            }
            throw new InvalidOperationException(type.ToString());
        }

        protected virtual string GetForeignKeyName(DBForeignKey cons, DBTable table)
        {
            if (cons.Name != null)
            {
                return cons.Name;
            }
            string str = "FK_" + table.Name + "_";
            foreach (string str2 in cons.Columns)
            {
                str = str + str2;
            }
            return str;
        }

        protected virtual long GetIdentity(Query sql) => 
            -1L;

        protected virtual long GetIdentity(InsertStatement root, TaggedParametersHolder identities) => 
            this.GetIdentity(new InsertSqlGenerator(this, identities, new Dictionary<OperandValue, string>()).GenerateSql(root));

        protected virtual System.Threading.Tasks.Task<long> GetIdentityAsync(Query sql, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken) => 
            Task.FromResult<long>(-1L);

        protected virtual System.Threading.Tasks.Task<long> GetIdentityAsync(InsertStatement root, TaggedParametersHolder identities, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken) => 
            this.GetIdentityAsync(new InsertSqlGenerator(this, identities, new Dictionary<OperandValue, string>()).GenerateSql(root), asyncOperationId, cancellationToken);

        protected virtual string GetIndexName(DBIndex cons, DBTable table)
        {
            if (cons.Name != null)
            {
                return cons.Name;
            }
            string str = "i";
            foreach (string str2 in cons.Columns)
            {
                str = str + str2;
            }
            return (str + "_" + table.Name);
        }

        private static IDisposable GetModifyDataPerformanceCounters(ModificationStatement[] dmlStatements) => 
            GetModifyDataPerformanceCountersCore(dmlStatements);

        private static IDisposable GetModifyDataPerformanceCountersCore(ModificationStatement[] dmlStatements)
        {
            PerformanceCounters.QueueLengthCounter counter = new PerformanceCounters.QueueLengthCounter(PerformanceCounters.SqlDataStoreTotalRequests, PerformanceCounters.SqlDataStoreTotalQueue, PerformanceCounters.SqlDataStoreModifyRequests, PerformanceCounters.SqlDataStoreModifyQueue);
            PerformanceCounters.SqlDataStoreModifyStatements.Increment(dmlStatements.Length);
            return counter;
        }

        protected virtual int GetObjectNameEffectiveLength(string objectName) => 
            string.IsNullOrEmpty(objectName) ? 0 : objectName.Length;

        public abstract string GetParameterName(OperandValue parameter, int index, ref bool createParameter);
        protected static string GetParametersString(IDbCommand query)
        {
            StringBuilder builder = new StringBuilder(query.Parameters.Count * 10);
            int count = query.Parameters.Count;
            for (int i = 0; i < count; i++)
            {
                IDataParameter parameter = (IDataParameter) query.Parameters[i];
                string str = (parameter.Value == null) ? "Null" : parameter.Value.ToString();
                if (str.Length > 0x40)
                {
                    str = str.Substring(0, 0x40) + "...";
                }
                object[] args = new object[] { str };
                builder.AppendFormat(CultureInfo.InvariantCulture, (i == 0) ? "{{{0}}}" : ",{{{0}}}", args);
            }
            return builder.ToString();
        }

        protected virtual string GetPrimaryKeyName(DBPrimaryKey cons, DBTable table) => 
            (cons.Name == null) ? ("PK_" + table.Name) : cons.Name;

        protected static string GetSafeNameAccess(string originalName)
        {
            char[] chArray = originalName.ToCharArray();
            int length = chArray.Length;
            if (length == 0)
            {
                return originalName;
            }
            bool flag = (chArray[0] == ' ') || (chArray[length - 1] == ' ');
            int index = 0;
            goto TR_000C;
        TR_0002:
            index++;
        TR_000C:
            while (true)
            {
                if (index >= length)
                {
                    return new string(chArray);
                }
                char ch = chArray[index];
                if (ch > '[')
                {
                    if ((ch != ']') && (ch != '`'))
                    {
                        break;
                    }
                }
                else
                {
                    switch (ch)
                    {
                        case '\0':
                        case '\x0001':
                        case '\x0002':
                        case '\x0003':
                        case '\x0004':
                        case '\x0005':
                        case '\x0006':
                        case '\a':
                        case '\b':
                        case '\t':
                        case '\n':
                        case '\v':
                        case '\f':
                        case '\r':
                        case '\x000e':
                        case '\x000f':
                        case '\x0010':
                        case '\x0011':
                        case '\x0012':
                        case '\x0013':
                        case '\x0014':
                        case '\x0015':
                        case '\x0016':
                        case '\x0017':
                        case '\x0018':
                        case '\x0019':
                        case '\x001a':
                        case '\x001b':
                        case '\x001c':
                        case '\x001d':
                        case '\x001e':
                        case '\x001f':
                        case '!':
                        case '.':
                            break;

                        case ' ':
                            if (flag)
                            {
                                chArray[index] = '_';
                            }
                            goto TR_0002;

                        case '"':
                        case '#':
                        case '$':
                        case '%':
                        case '&':
                        case '\'':
                        case '(':
                        case ')':
                        case '*':
                        case '+':
                        case ',':
                        case '-':
                            goto TR_0002;

                        default:
                            if (ch == '[')
                            {
                                break;
                            }
                            goto TR_0002;
                    }
                }
                chArray[index] = '_';
                break;
            }
            goto TR_0002;
        }

        protected virtual int GetSafeNameColumnMaxLength() => 
            this.GetSafeNameTableMaxLength();

        protected virtual int GetSafeNameConstraintMaxLength() => 
            this.GetSafeNameTableMaxLength();

        protected static string GetSafeNameDefault(string originalName)
        {
            char[] chArray = originalName.ToCharArray();
            int length = chArray.Length;
            if (length == 0)
            {
                return originalName;
            }
            bool flag = (chArray[0] == ' ') || (chArray[length - 1] == ' ');
            for (int i = 0; i < length; i++)
            {
                UnicodeCategory unicodeCategory = chArray[i].GetUnicodeCategory();
                switch (unicodeCategory)
                {
                    case UnicodeCategory.SpaceSeparator:
                        if (flag)
                        {
                            chArray[i] = '_';
                        }
                        break;

                    case UnicodeCategory.LineSeparator:
                    case UnicodeCategory.ParagraphSeparator:
                    case UnicodeCategory.Control:
                    case UnicodeCategory.ConnectorPunctuation:
                    case UnicodeCategory.DashPunctuation:
                    case UnicodeCategory.OpenPunctuation:
                    case UnicodeCategory.ClosePunctuation:
                    case UnicodeCategory.InitialQuotePunctuation:
                    case UnicodeCategory.FinalQuotePunctuation:
                    case UnicodeCategory.OtherPunctuation:
                        chArray[i] = '_';
                        break;

                    default:
                        break;
                }
            }
            return new string(chArray);
        }

        protected static string GetSafeNameMsSql(string originalName)
        {
            char[] chArray = originalName.ToCharArray();
            int length = chArray.Length;
            int index = 0;
            goto TR_000A;
        TR_0001:
            index++;
        TR_000A:
            while (true)
            {
                if (index >= length)
                {
                    return new string(chArray);
                }
                char ch = chArray[index];
                if (ch <= '.')
                {
                    if ((ch != '\0') && ((ch != '"') && (ch != '.')))
                    {
                        break;
                    }
                }
                else if ((ch != '[') && ((ch != ']') && (ch != 0xffff)))
                {
                    break;
                }
                chArray[index] = '_';
                goto TR_0001;
            }
            if (chArray[index].GetUnicodeCategory() == UnicodeCategory.Surrogate)
            {
                chArray[index] = '_';
            }
            goto TR_0001;
        }

        protected virtual string GetSafeNameRoot(string originalName) => 
            GetSafeNameDefault(originalName);

        protected abstract int GetSafeNameTableMaxLength();
        protected string GetSafeObjectName(string originalName, string patchedName, int maxLength)
        {
            if ((this.GetObjectNameEffectiveLength(originalName) <= maxLength) && (originalName == patchedName))
            {
                return originalName;
            }
            char[] trimChars = new char[] { '_' };
            patchedName = patchedName.TrimEnd(trimChars);
            if (patchedName.IndexOf("__") >= 0)
            {
                StringBuilder builder = new StringBuilder(patchedName.Length - 1);
                char ch = '\0';
                string str3 = patchedName;
                int num3 = 0;
                while (true)
                {
                    if (num3 >= str3.Length)
                    {
                        patchedName = builder.ToString();
                        break;
                    }
                    char ch2 = str3[num3];
                    if ((ch != '_') || (ch2 != '_'))
                    {
                        ch = ch2;
                        builder.Append(ch2);
                    }
                    num3++;
                }
            }
            string objectName = "_" + this.GetDbNameHashString(originalName);
            string str2 = patchedName;
            int objectNameEffectiveLength = this.GetObjectNameEffectiveLength(objectName);
            if ((this.GetObjectNameEffectiveLength(str2) + objectNameEffectiveLength) > maxLength)
            {
                int length = 0;
                int num5 = 0;
                string str4 = str2;
                int num6 = 0;
                while (true)
                {
                    if (num6 < str4.Length)
                    {
                        char c = str4[num6];
                        int num7 = this.GetObjectNameEffectiveLength(new string(c, 1));
                        if (((num5 + num7) + objectNameEffectiveLength) <= maxLength)
                        {
                            length++;
                            num5 += num7;
                            num6++;
                            continue;
                        }
                    }
                    str2 = (length <= 0) ? "T" : str2.Substring(0, length);
                    break;
                }
            }
            return (str2 + objectName);
        }

        protected object GetScalar(Query query)
        {
            object scalar;
            IDbCommand commandFromPool = this.GetCommandFromPool(query);
            try
            {
                try
                {
                    scalar = this.InternalGetScalar(commandFromPool);
                }
                catch (Exception exception)
                {
                    if ((this.Transaction != null) || !this.IsConnectionBroken(exception))
                    {
                        throw;
                    }
                    this.DoReconnect();
                    scalar = this.InternalGetScalar(commandFromPool);
                }
            }
            catch (Exception exception2)
            {
                throw this.WrapException(exception2, commandFromPool);
            }
            finally
            {
                this.ReleasePooledCommand(commandFromPool);
            }
            return scalar;
        }

        [AsyncStateMachine(typeof(<GetScalarAsync>d__194))]
        protected System.Threading.Tasks.Task<object> GetScalarAsync(Query query, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            <GetScalarAsync>d__194 d__;
            d__.<>4__this = this;
            d__.query = query;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<GetScalarAsync>d__194>(ref d__);
            return d__.<>t__builder.Task;
        }

        private static IDisposable GetSelectDataPerformanceCounters(SelectStatement[] selects) => 
            GetSelectDataPerformanceCountersCore(selects);

        private static IDisposable GetSelectDataPerformanceCountersCore(SelectStatement[] selects)
        {
            PerformanceCounters.QueueLengthCounter counter = new PerformanceCounters.QueueLengthCounter(PerformanceCounters.SqlDataStoreTotalRequests, PerformanceCounters.SqlDataStoreTotalQueue, PerformanceCounters.SqlDataStoreSelectRequests, PerformanceCounters.SqlDataStoreSelectQueue);
            PerformanceCounters.SqlDataStoreSelectQueries.Increment(selects.Length);
            return counter;
        }

        protected List<SelectStatementResult> GetSelectedStatmentResults(IDbCommand command)
        {
            List<SelectStatementResult> list2;
            using (IDataReader reader = command.ExecuteReader())
            {
                List<SelectStatementResult> list = new List<SelectStatementResult>();
                if (reader != null)
                {
                    while (true)
                    {
                        List<SelectStatementResultRow> list3 = new List<SelectStatementResultRow>();
                        Type[] fieldTypes = null;
                        while (true)
                        {
                            if (reader.Read())
                            {
                                object[] values = new object[reader.FieldCount];
                                if (this.IsFieldTypesNeeded && (fieldTypes == null))
                                {
                                    fieldTypes = new Type[reader.FieldCount];
                                    for (int i = reader.FieldCount - 1; i >= 0; i--)
                                    {
                                        fieldTypes[i] = reader.GetFieldType(i);
                                    }
                                }
                                this.GetValues(reader, fieldTypes, values);
                                list3.Add(new SelectStatementResultRow(values));
                                continue;
                            }
                            list.Add(new SelectStatementResult(list3.ToArray()));
                            if (reader.NextResult())
                            {
                                break;
                            }
                            return list;
                        }
                    }
                }
                else
                {
                    list.Add(new SelectStatementResult(new SelectStatementResultRow[0]));
                    list2 = list;
                }
            }
            return list2;
        }

        [AsyncStateMachine(typeof(<GetSelectedStatmentResultsAsync>d__273))]
        protected System.Threading.Tasks.Task<List<SelectStatementResult>> GetSelectedStatmentResultsAsync(IDbCommand command, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            <GetSelectedStatmentResultsAsync>d__273 d__;
            d__.<>4__this = this;
            d__.command = command;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<List<SelectStatementResult>>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<GetSelectedStatmentResultsAsync>d__273>(ref d__);
            return d__.<>t__builder.Task;
        }

        public abstract string GetSqlCreateColumnFullAttributes(DBTable table, DBColumn column);
        public virtual string GetSqlCreateColumnFullAttributes(DBTable table, DBColumn column, bool forTableCreate) => 
            this.GetSqlCreateColumnFullAttributes(table, column);

        protected string GetSqlCreateColumnType(DBTable table, DBColumn column)
        {
            if ((column.DBTypeName != null) && (column.DBTypeName.Length > 0))
            {
                return column.DBTypeName;
            }
            switch (column.ColumnType)
            {
                case DBColumnType.Boolean:
                    return this.GetSqlCreateColumnTypeForBoolean(table, column);

                case DBColumnType.Byte:
                    return this.GetSqlCreateColumnTypeForByte(table, column);

                case DBColumnType.SByte:
                    return this.GetSqlCreateColumnTypeForSByte(table, column);

                case DBColumnType.Char:
                    return this.GetSqlCreateColumnTypeForChar(table, column);

                case DBColumnType.Decimal:
                    return this.GetSqlCreateColumnTypeForDecimal(table, column);

                case DBColumnType.Double:
                    return this.GetSqlCreateColumnTypeForDouble(table, column);

                case DBColumnType.Single:
                    return this.GetSqlCreateColumnTypeForSingle(table, column);

                case DBColumnType.Int32:
                    return this.GetSqlCreateColumnTypeForInt32(table, column);

                case DBColumnType.UInt32:
                    return this.GetSqlCreateColumnTypeForUInt32(table, column);

                case DBColumnType.Int16:
                    return this.GetSqlCreateColumnTypeForInt16(table, column);

                case DBColumnType.UInt16:
                    return this.GetSqlCreateColumnTypeForUInt16(table, column);

                case DBColumnType.Int64:
                    return this.GetSqlCreateColumnTypeForInt64(table, column);

                case DBColumnType.UInt64:
                    return this.GetSqlCreateColumnTypeForUInt64(table, column);

                case DBColumnType.String:
                    return this.GetSqlCreateColumnTypeForString(table, column);

                case DBColumnType.DateTime:
                    return this.GetSqlCreateColumnTypeForDateTime(table, column);

                case DBColumnType.Guid:
                    return this.GetSqlCreateColumnTypeForGuid(table, column);

                case DBColumnType.ByteArray:
                    return this.GetSqlCreateColumnTypeForByteArray(table, column);
            }
            if (!UseLegacyTimeSpanSupport || (column.ColumnType != DBColumnType.TimeSpan))
            {
                throw new ArgumentException();
            }
            return this.GetSqlCreateColumnTypeForTimeSpan(table, column);
        }

        protected abstract string GetSqlCreateColumnTypeForBoolean(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForByte(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForByteArray(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForChar(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForDateTime(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForDecimal(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForDouble(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForGuid(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForInt16(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForInt32(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForInt64(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForSByte(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForSingle(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForString(DBTable table, DBColumn column);
        protected virtual string GetSqlCreateColumnTypeForTimeSpan(DBTable table, DBColumn column) => 
            this.GetSqlCreateColumnTypeForDouble(table, column);

        protected abstract string GetSqlCreateColumnTypeForUInt16(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForUInt32(DBTable table, DBColumn column);
        protected abstract string GetSqlCreateColumnTypeForUInt64(DBTable table, DBColumn column);
        public virtual DBTable[] GetStorageTables(params string[] tables)
        {
            if (tables == null)
            {
                tables = this.GetStorageTablesList(false);
                if (tables == null)
                {
                    return new DBTable[0];
                }
            }
            List<DBTable> list = new List<DBTable>(tables.Length);
            foreach (string str in tables)
            {
                DBTable table = new DBTable(str);
                this.GetTableSchema(table, true, true);
                list.Add(table);
            }
            return list.ToArray();
        }

        public abstract string[] GetStorageTablesList(bool includeViews);
        public virtual DBStoredProcedure[] GetStoredProcedures()
        {
            throw new NotImplementedException();
        }

        public abstract void GetTableSchema(DBTable table, bool checkIndexes, bool checkForeignKeys);
        private static IDisposable GetUpdateSchemaPerformanceCounters() => 
            GetUpdateSchemaPerformanceCountersCore();

        private static IDisposable GetUpdateSchemaPerformanceCountersCore() => 
            new PerformanceCounters.QueueLengthCounter(PerformanceCounters.SqlDataStoreTotalRequests, PerformanceCounters.SqlDataStoreTotalQueue, PerformanceCounters.SqlDataStoreSchemaUpdateRequests, PerformanceCounters.SqlDataStoreSchemaUpdateQueue);

        protected virtual void GetValues(IDataReader reader, Type[] fieldTypes, object[] values)
        {
            reader.GetValues(values);
        }

        private static void IncrementPerformanceCounters()
        {
            IncrementPerformanceCountersCore();
        }

        private static void IncrementPerformanceCountersCore()
        {
            PerformanceCounters.SqlDataStoreCount.Increment();
            PerformanceCounters.SqlDataStoreCreated.Increment();
        }

        private int InternalExecSql(IDbCommand command) => 
            LogManager.Log<int>("SQL", () => command.ExecuteNonQuery(), d => LogMessage.CreateMessage(this, command, d));

        private System.Threading.Tasks.Task<int> InternalExecSqlAsync(IDbCommand command, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken) => 
            LogManager.LogAsync<int>("SQL", () => this.CommandExecuteNonQueryAsync(command, asyncOperationId, cancellationToken), d => LogMessage.CreateMessage(this, command, d));

        protected SelectStatementResult[] InternalGetData(IDbCommand command, CriteriaOperatorCollection targets, int skipClause, int topClause, bool includeMetadata) => 
            LogManager.Log<SelectStatementResult[]>("SQL", delegate {
                using (IDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    List<SelectStatementResult> list = new List<SelectStatementResult>();
                    goto TR_001F;
                TR_0005:
                    if ((topClause != 0) || ((skipClause != 0) || !reader.NextResult()))
                    {
                        return list.ToArray();
                    }
                TR_001F:
                    while (true)
                    {
                        if (!this.NativeSkipTakeSupported)
                        {
                            bool flag = false;
                            int num = 0;
                            while (true)
                            {
                                if (num < skipClause)
                                {
                                    if (reader.Read())
                                    {
                                        num++;
                                        continue;
                                    }
                                    if (includeMetadata)
                                    {
                                        list.Add(new SelectStatementResult(FillMetaData(reader, null)));
                                    }
                                    list.Add(new SelectStatementResult());
                                    flag = true;
                                }
                                if (!flag)
                                {
                                    break;
                                }
                                break;
                            }
                        }
                        List<object[]> rows = new List<object[]>();
                        ReformatReadValueArgs[] converters = null;
                        object[][] testData = null;
                        Type[] fieldTypes = null;
                        while (true)
                        {
                            if (((topClause > 0) && (rows.Count >= topClause)) || !reader.Read())
                            {
                                if (includeMetadata)
                                {
                                    testData ??= FillMetaData(reader, fieldTypes);
                                    list.Add(new SelectStatementResult(testData));
                                }
                                list.Add(new SelectStatementResult(rows));
                                break;
                            }
                            if (this.IsFieldTypesNeeded)
                            {
                                fieldTypes ??= FillTypes(reader);
                            }
                            if (includeMetadata)
                            {
                                testData ??= FillMetaData(reader, fieldTypes);
                            }
                            object[] values = new object[reader.FieldCount];
                            this.GetValues(reader, fieldTypes, values);
                            if (targets != null)
                            {
                                converters ??= this.FillReformatters(targets);
                                this.ReformatReadValues(values, converters);
                            }
                            rows.Add(values);
                        }
                        break;
                    }
                    goto TR_0005;
                }
            }, d => LogMessage.CreateMessage(this, command, d));

        protected System.Threading.Tasks.Task<SelectStatementResult[]> InternalGetDataAsync(IDbCommand command, CriteriaOperatorCollection targets, int skipClause, int topClause, bool includeMetadata, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            <>c__DisplayClass127_0 class_;
            return LogManager.LogAsync<SelectStatementResult[]>("SQL", delegate {
                <>c__DisplayClass127_0.<<InternalGetDataAsync>b__0>d local;
                local.<>4__this = class_;
                local.<>t__builder = AsyncTaskMethodBuilder<SelectStatementResult[]>.Create();
                local.<>1__state = -1;
                local.<>t__builder.Start<<>c__DisplayClass127_0.<<InternalGetDataAsync>b__0>d>(ref local);
                return local.<>t__builder.Task;
            }, d => LogMessage.CreateMessage(this, command, d));
        }

        protected virtual object InternalGetScalar(IDbCommand command) => 
            LogManager.Log<object>("SQL", () => command.ExecuteScalar(), d => LogMessage.CreateMessage(this, command, d));

        protected virtual System.Threading.Tasks.Task<object> InternalGetScalarAsync(IDbCommand command, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken) => 
            LogManager.LogAsync<object>("SQL", () => this.CommandExecuteScalarAsync(command, asyncOperationId, cancellationToken), d => LogMessage.CreateMessage(this, command, d));

        private bool IsColumnExists(DBTable table, DBColumn column)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (string.Compare(table.Columns[i].Name, this.ComposeSafeColumnName(column.Name), true) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsColumnsEqual(StringCollection first, StringCollection second)
        {
            if (first.Count != second.Count)
            {
                return false;
            }
            for (int i = 0; i < first.Count; i++)
            {
                if (string.Compare(this.ComposeSafeColumnName(first[i]), this.ComposeSafeColumnName(second[i]), true) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        protected virtual bool IsConnectionBroken(Exception e) => 
            this.Connection.State != ConnectionState.Open;

        protected virtual bool IsDeadLock(Exception e) => 
            false;

        private bool IsForeignKeyExists(DBTable table, DBForeignKey foreignKey)
        {
            bool flag;
            using (List<DBForeignKey>.Enumerator enumerator = table.ForeignKeys.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DBForeignKey current = enumerator.Current;
                        if ((string.Compare(this.ComposeSafeTableName(foreignKey.PrimaryKeyTable), this.ComposeSafeTableName(current.PrimaryKeyTable), true) != 0) || (!this.IsColumnsEqual(current.Columns, foreignKey.Columns) || !this.IsColumnsEqual(current.PrimaryKeyTableKeyColumns, foreignKey.PrimaryKeyTableKeyColumns)))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool IsIndexExists(DBTable table, DBIndex index)
        {
            bool flag;
            using (List<DBIndex>.Enumerator enumerator = table.Indexes.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DBIndex current = enumerator.Current;
                        if (!this.IsColumnsEqual(current.Columns, index.Columns))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        protected bool IsKey(DBTable table, string column)
        {
            for (int i = 0; i < table.PrimaryKey.Columns.Count; i++)
            {
                if (table.PrimaryKey.Columns[i] == column)
                {
                    return true;
                }
            }
            return false;
        }

        protected static bool IsSingleColumnPKColumn(DBTable table, DBColumn column)
        {
            bool flag;
            if (!column.IsKey)
            {
                return false;
            }
            if (table == null)
            {
                return true;
            }
            if (table.PrimaryKey != null)
            {
                return (table.PrimaryKey.Columns.Count == 1);
            }
            using (List<DBColumn>.Enumerator enumerator = table.Columns.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DBColumn current = enumerator.Current;
                        if (!current.IsKey || (current.Name == column.Name))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        public override ModificationResult ModifyData(params ModificationStatement[] dmlStatements)
        {
            ModificationResult result;
            using (GetModifyDataPerformanceCounters(dmlStatements))
            {
                int tryCount = 1;
                while (true)
                {
                    try
                    {
                        result = base.ModifyData(dmlStatements);
                        break;
                    }
                    catch (Exception exception1)
                    {
                        if (!this.CanRetryIfDeadlock(tryCount, exception1))
                        {
                            throw;
                        }
                    }
                    tryCount++;
                }
            }
            return result;
        }

        [AsyncStateMachine(typeof(<ModifyDataAsync>d__17))]
        public override System.Threading.Tasks.Task<ModificationResult> ModifyDataAsync(CancellationToken cancellationToken, params ModificationStatement[] dmlStatements)
        {
            <ModifyDataAsync>d__17 d__;
            d__.<>4__this = this;
            d__.cancellationToken = cancellationToken;
            d__.dmlStatements = dmlStatements;
            d__.<>t__builder = AsyncTaskMethodBuilder<ModificationResult>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ModifyDataAsync>d__17>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected virtual void OnReconnected()
        {
            if (this.reconnected != null)
            {
                this.reconnected(this, EventArgs.Empty);
            }
        }

        protected void OpenConnection()
        {
            ConnectionState closed = this.Connection.State;
            if (closed == ConnectionState.Broken)
            {
                this.CloseConnectionInternal();
                closed = ConnectionState.Closed;
            }
            if (closed == ConnectionState.Closed)
            {
                this.ReleaseCommandPool();
                this.OpenConnectionInternal();
            }
        }

        protected virtual void OpenConnectionInternal()
        {
            this.Connection.Open();
        }

        private void PrepareCommandForExecuteSprocParametrized(IDbCommand command, string sprocName, OperandValue[] parameters, out List<IDataParameter> outParameters, out IDataParameter returnParameter)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = this.FormatTable(this.ComposeSafeSchemaName(sprocName), this.ComposeSafeTableName(sprocName));
            returnParameter = null;
            outParameters = new List<IDataParameter>();
            foreach (SprocParameter parameter in parameters)
            {
                IDbDataParameter item = (IDbDataParameter) this.CreateParameter(command, this.ConvertParameter(parameter.Value), parameter.ParameterName);
                SprocParameterDirection direction = parameter.Direction;
                switch (direction)
                {
                    case SprocParameterDirection.Output:
                    case SprocParameterDirection.InputOutput:
                        outParameters.Add(item);
                        break;

                    case SprocParameterDirection.ReturnValue:
                        if (returnParameter != null)
                        {
                            throw new ArgumentException("Duplicate return parameter.");
                        }
                        returnParameter = item;
                        break;

                    default:
                        break;
                }
                item.Direction = SprocParameter.GetDataParameterDirection(parameter.Direction);
                if (parameter.DbType != null)
                {
                    item.DbType = GetDbType(parameter.DbType.Value);
                }
                if (parameter.Size != null)
                {
                    item.Size = parameter.Size.Value;
                }
                if (parameter.Precision != null)
                {
                    item.Precision = parameter.Precision.Value;
                }
                if (parameter.Scale != null)
                {
                    item.Scale = parameter.Scale.Value;
                }
                command.Parameters.Add(item);
            }
        }

        protected void PrepareParameters(IDbCommand command, Query query)
        {
            for (int i = 0; i < query.Parameters.Count; i++)
            {
                command.Parameters.Add(this.CreateParameter(command, this.ConvertParameter(query.Parameters[i].Value), (string) query.ParametersNames[i]));
            }
        }

        protected void PrepareParametersForExecuteSproc(OperandValue[] parameters, IDbCommand command, out List<IDataParameter> outParameters, out IDataParameter returnParameter)
        {
            int num = 0;
            returnParameter = null;
            outParameters = new List<IDataParameter>();
            foreach (IDataParameter parameter in command.Parameters)
            {
                ParameterDirection direction = parameter.Direction;
                switch (direction)
                {
                    case ParameterDirection.Output:
                    {
                        outParameters.Add(parameter);
                        parameter.Value = DBNull.Value;
                        continue;
                    }
                    case ParameterDirection.InputOutput:
                        outParameters.Add(parameter);
                        parameter.Value = DBNull.Value;
                        break;

                    case ParameterDirection.ReturnValue:
                    {
                        returnParameter = parameter;
                        continue;
                    }
                    default:
                        break;
                }
                if ((parameters != null) && (num < parameters.Length))
                {
                    parameter.Value = this.ConvertParameter(parameters[num++].Value);
                }
            }
        }

        protected virtual void PreparePooledCommand(IDbCommand command)
        {
            command.Prepare();
        }

        private void PreparePoolParameters(IDbCommand command, Query query)
        {
            for (int i = 0; i < query.Parameters.Count; i++)
            {
                ((IDbDataParameter) command.Parameters[i]).Value = this.ConvertParameter(query.Parameters[i].Value);
            }
        }

        protected override ModificationResult ProcessModifyData(params ModificationStatement[] dmlStatements)
        {
            ModificationResult result;
            this.BeginTransaction();
            try
            {
                List<ParameterValue> identities = new List<ParameterValue>();
                TaggedParametersHolder holder = new TaggedParametersHolder();
                ModificationStatement[] statementArray = dmlStatements;
                int index = 0;
                while (true)
                {
                    if (index >= statementArray.Length)
                    {
                        this.CommitTransaction();
                        result = new ModificationResult(identities);
                        break;
                    }
                    ModificationStatement root = statementArray[index];
                    ParameterValue criterion = this.UpdateRecord(root, holder);
                    if (!criterion.ReferenceEqualsNull())
                    {
                        identities.Add(criterion);
                    }
                    index++;
                }
            }
            catch
            {
                this.RollbackTransaction();
                throw;
            }
            return result;
        }

        [AsyncStateMachine(typeof(<ProcessModifyDataAsync>d__207))]
        protected override System.Threading.Tasks.Task<ModificationResult> ProcessModifyDataAsync(AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken, params ModificationStatement[] dmlStatements)
        {
            <ProcessModifyDataAsync>d__207 d__;
            d__.<>4__this = this;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.dmlStatements = dmlStatements;
            d__.<>t__builder = AsyncTaskMethodBuilder<ModificationResult>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ProcessModifyDataAsync>d__207>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected override SelectStatementResult ProcessSelectData(SelectStatement selects)
        {
            Query query = new SelectSqlGenerator(this).GenerateSql(selects);
            return this.SelectData(query, selects.Operands, false);
        }

        protected override System.Threading.Tasks.Task<SelectStatementResult> ProcessSelectDataAsync(SelectStatement selects, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            Query query = new SelectSqlGenerator(this).GenerateSql(selects);
            return this.SelectDataAsync(query, selects.Operands, false, asyncOperationId, cancellationToken);
        }

        private Query ProcessSqlQueryCommandArgument(object args)
        {
            CommandChannelHelper.SqlQuery query = args as CommandChannelHelper.SqlQuery;
            if (query == null)
            {
                throw new ArgumentException($"Wrong parameter set for command '{"DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteNonQuerySQLWithParams"}'.");
            }
            List<string> parametersNames = new List<string>(query.ParametersNames);
            for (int i = query.ParametersNames.Length; i < query.Parameters.Count; i++)
            {
                bool createParameter = true;
                parametersNames.Add(this.GetParameterName(query.Parameters[i], i, ref createParameter));
            }
            return new Query(query.SqlCommand, query.Parameters, parametersNames);
        }

        protected override UpdateSchemaResult ProcessUpdateSchema(bool skipIfFirstTableNotExists, params DBTable[] tables)
        {
            ICollection is2 = this.CollectTablesToCreate(tables);
            if (skipIfFirstTableNotExists && (is2.Count > 0))
            {
                IEnumerator enumerator = tables.GetEnumerator();
                IEnumerator enumerator2 = is2.GetEnumerator();
                enumerator.MoveNext();
                enumerator2.MoveNext();
                if (enumerator.Current == enumerator2.Current)
                {
                    return UpdateSchemaResult.FirstTableNotExists;
                }
            }
            if (this.CanCreateSchema && this.InternalExecuteUpdateSchemaInTransaction)
            {
                this.BeginTransaction();
            }
            try
            {
                if (!this.CanCreateSchema && (is2.Count > 0))
                {
                    IEnumerator enumerator = is2.GetEnumerator();
                    enumerator.MoveNext();
                    throw new SchemaCorrectionNeededException("Table '" + this.ComposeSafeTableName(((DBTable) enumerator.Current).Name) + "' not found");
                }
                foreach (DBTable table in is2)
                {
                    this.CreateTable(table);
                }
                Dictionary<DBTable, DBTable> dictionary = new Dictionary<DBTable, DBTable>();
                DBTable[] tableArray = tables;
                int num = 0;
                while (true)
                {
                    if (num >= tableArray.Length)
                    {
                        if (this.CanCreateSchema)
                        {
                            DBTable[] tableArray2 = tables;
                            int num2 = 0;
                            while (true)
                            {
                                DBTable table5;
                                if (num2 >= tableArray2.Length)
                                {
                                    if (this.InternalExecuteUpdateSchemaInTransaction)
                                    {
                                        this.CommitTransaction();
                                    }
                                    break;
                                }
                                DBTable table4 = tableArray2[num2];
                                dictionary.TryGetValue(table4, out table5);
                                if (table5 != null)
                                {
                                    foreach (DBIndex index in table4.Indexes)
                                    {
                                        if (!this.IsIndexExists(table5, index))
                                        {
                                            this.CreateIndex(table4, index);
                                            table5.AddIndex(index);
                                        }
                                    }
                                    if (this.NeedsIndexForForeignKey)
                                    {
                                        foreach (DBForeignKey key in table4.ForeignKeys)
                                        {
                                            DBIndex index2 = new DBIndex(key.Columns, false);
                                            if (!this.IsIndexExists(table5, index2) && ((table4.PrimaryKey == null) || !this.IsColumnsEqual(table4.PrimaryKey.Columns, index2.Columns)))
                                            {
                                                this.CreateIndex(table4, index2);
                                                table5.AddIndex(index2);
                                            }
                                        }
                                    }
                                    foreach (DBForeignKey key2 in table4.ForeignKeys)
                                    {
                                        if (!this.IsForeignKeyExists(table5, key2))
                                        {
                                            this.CreateForeignKey(table4, key2);
                                        }
                                    }
                                }
                                num2++;
                            }
                        }
                        break;
                    }
                    DBTable table2 = tableArray[num];
                    if (!table2.IsView)
                    {
                        DBTable table3 = new DBTable(table2.Name);
                        bool checkIndexes = false;
                        bool checkForeignKeys = false;
                        if (this.CanCreateSchema)
                        {
                            checkIndexes = table2.Indexes.Count > 0;
                            checkForeignKeys = table2.ForeignKeys.Count > 0;
                            if (this.NeedsIndexForForeignKey)
                            {
                                checkIndexes |= checkForeignKeys;
                            }
                        }
                        this.GetTableSchema(table3, checkIndexes, checkForeignKeys);
                        dictionary[table2] = table3;
                        foreach (DBColumn column in table2.Columns)
                        {
                            if (!this.IsColumnExists(table3, column))
                            {
                                if (!this.CanCreateSchema)
                                {
                                    string[] textArray1 = new string[] { "Column '", this.ComposeSafeColumnName(column.Name), "' not found in table '", this.ComposeSafeTableName(table2.Name), "'" };
                                    throw new SchemaCorrectionNeededException(string.Concat(textArray1));
                                }
                                this.CreateColumn(table2, column);
                            }
                        }
                        if (this.CanCreateSchema && ((table3.PrimaryKey == null) && (table2.PrimaryKey != null)))
                        {
                            this.CreatePrimaryKey(table2);
                        }
                    }
                    num++;
                }
            }
            catch
            {
                if (this.CanCreateSchema && this.InternalExecuteUpdateSchemaInTransaction)
                {
                    this.RollbackTransaction();
                }
                throw;
            }
            return UpdateSchemaResult.SchemaExists;
        }

        protected virtual System.Threading.Tasks.Task<bool> ReaderNextResultAsync(IDataReader reader, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            DbDataReader reader2 = reader as DbDataReader;
            if (reader2 != null)
            {
                return reader2.NextResultAsync(cancellationToken);
            }
            object[] args = new object[] { reader.GetType().FullName, "ReaderNextResultAsync" };
            throw new NotSupportedException(DbRes.GetString("ConnectionProviderSql_DbDataReaderAsyncOperationsNotSupported", args));
        }

        protected virtual System.Threading.Tasks.Task<bool> ReaderReadAsync(IDataReader reader, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            DbDataReader reader2 = reader as DbDataReader;
            if (reader2 != null)
            {
                return reader2.ReadAsync(cancellationToken);
            }
            object[] args = new object[] { reader.GetType().FullName, "ReaderReadAsync" };
            throw new NotSupportedException(DbRes.GetString("ConnectionProviderSql_DbDataReaderAsyncOperationsNotSupported", args));
        }

        protected virtual object ReformatReadValue(object value, ReformatReadValueArgs args)
        {
            if (args.DbType == args.TargetType)
            {
                return value;
            }
            TypeCode targetTypeCode = args.TargetTypeCode;
            if (targetTypeCode == TypeCode.Object)
            {
                if (args.TargetType == typeof(Guid))
                {
                    return ((value is byte[]) ? new Guid((byte[]) value) : new Guid(value.ToString()));
                }
                if (UseLegacyTimeSpanSupport && (args.TargetType == typeof(TimeSpan)))
                {
                    double num = Convert.ToDouble(value);
                    return (((num <= (TimeSpan.MaxValue.TotalSeconds - 0.0005)) || (num >= (TimeSpan.MaxValue.TotalSeconds + 0.0005))) ? (((num >= (TimeSpan.MinValue.TotalSeconds + 0.0005)) || (num <= (TimeSpan.MinValue.TotalSeconds - 0.0005))) ? TimeSpan.FromSeconds(num) : TimeSpan.MinValue) : TimeSpan.MaxValue);
                }
                if (args.TargetType == typeof(object))
                {
                    return value;
                }
            }
            else if (targetTypeCode != TypeCode.Char)
            {
                if ((targetTypeCode == TypeCode.Double) && (args.DbType == typeof(TimeSpan)))
                {
                    return ((TimeSpan) value).TotalSeconds;
                }
            }
            else if (args.DbTypeCode == TypeCode.String)
            {
                string str = (string) value;
                return ((str.Length != 0) ? Convert.ToChar(str) : ' ');
            }
            return Convert.ChangeType(value, args.TargetType, CultureInfo.InvariantCulture);
        }

        private void ReformatReadValues(object[] values, ReformatReadValueArgs[] converters)
        {
            for (int i = 0; i < values.Length; i++)
            {
                object dbData = values[i];
                if (dbData != null)
                {
                    if (dbData == DBNull.Value)
                    {
                        values[i] = null;
                    }
                    else
                    {
                        ReformatReadValueArgs args = converters[i];
                        args.AttachValueReadFromDb(dbData);
                        values[i] = this.ReformatReadValue(dbData, args);
                    }
                }
            }
        }

        public void RegisterCustomAggregate(ICustomAggregateFormattable customAggregate)
        {
            if (customAggregate != null)
            {
                this.customAggregatesByName[customAggregate.Name] = customAggregate;
            }
        }

        public void RegisterCustomAggregates(ICollection<ICustomAggregateFormattable> customAggregates)
        {
            if (customAggregates != null)
            {
                foreach (ICustomAggregateFormattable formattable in customAggregates)
                {
                    this.RegisterCustomAggregate(formattable);
                }
            }
        }

        public void RegisterCustomFunctionOperator(ICustomFunctionOperatorFormattable customFunction)
        {
            if (customFunction != null)
            {
                this.customFunctionsByName[customFunction.Name] = customFunction;
            }
        }

        public void RegisterCustomFunctionOperators(ICollection<ICustomFunctionOperatorFormattable> customFunctions)
        {
            if (customFunctions != null)
            {
                foreach (ICustomFunctionOperatorFormattable formattable in customFunctions)
                {
                    this.RegisterCustomFunctionOperator(formattable);
                }
            }
        }

        private void ReleaseCommandPool()
        {
            if (this.preparedCommands != null)
            {
                foreach (PreparedCommandInfo info in this.preparedCommands.Values)
                {
                    info.CloseCommand();
                }
                this.preparedCommands.Dispose();
                this.preparedCommands = null;
            }
            if (this.acquiredCommands != null)
            {
                this.acquiredCommands.Dispose();
                this.acquiredCommands = null;
            }
        }

        private void ReleasePooledCommand(IDbCommand command)
        {
            PreparedCommandInfo info;
            if ((this.preparedCommands != null) && ((this.acquiredCommands != null) && this.acquiredCommands.TryGetValue(command, out info)))
            {
                this.acquiredCommands.Remove(command);
                if (info.TryReleaseCommand(command))
                {
                    return;
                }
                if (info.IsClosed)
                {
                    this.acquiredCommands.Remove(command);
                }
            }
            command.Dispose();
        }

        private void RemoveOldestCommand()
        {
            if ((this.preparedCommands != null) && (this.acquiredCommands != null))
            {
                Query key = null;
                PreparedCommandInfo info = null;
                DateTime maxValue = DateTime.MaxValue;
                foreach (KeyValuePair<Query, PreparedCommandInfo> pair in this.preparedCommands.Pairs)
                {
                    if ((key == null) || (pair.Value.LastAscuiredAt < maxValue))
                    {
                        key = pair.Key;
                        info = pair.Value;
                        maxValue = pair.Value.LastAscuiredAt;
                    }
                }
                if (key != null)
                {
                    this.preparedCommands.Remove(key);
                    IDbCommand command = info.CloseCommand();
                    if (command != null)
                    {
                        this.acquiredCommands.Remove(command);
                    }
                }
            }
        }

        protected void RollbackTransaction()
        {
            if (!this.explicitTransaction)
            {
                this.RollbackTransactionCore();
            }
        }

        protected virtual void RollbackTransactionCore()
        {
            if (this.CommandPoolBehavior != DevExpress.Xpo.DB.CommandPoolBehavior.ConnectionSession)
            {
                this.ReleaseCommandPool();
            }
            if (this.transaction != null)
            {
                try
                {
                    this.transaction.Rollback();
                }
                catch
                {
                }
                this.transaction = null;
            }
        }

        public override SelectedData SelectData(params SelectStatement[] selects)
        {
            using (GetSelectDataPerformanceCounters(selects))
            {
                return base.SelectData(selects);
            }
        }

        protected SelectStatementResult SelectData(Query query) => 
            this.SelectData(query, false);

        protected SelectStatementResult SelectData(Query query, bool includeMetadata) => 
            this.SelectData(query, null, includeMetadata);

        private SelectStatementResult SelectData(Query query, CriteriaOperatorCollection targets, bool includeMetadata)
        {
            if ((query.ConstantValues == null) || ((query.OperandIndexes == null) || (query.ConstantValues.Count <= 0)))
            {
                return this.SelectDataSimple(query, targets, includeMetadata)[0];
            }
            CriteriaOperatorCollection operators = this.SelectDataPrepareCustomTargets(query, targets);
            SelectStatementResult queryResult = this.SelectDataSimple(query, operators, includeMetadata)[0];
            return this.SelectDataGetSelectStatementResult(query, targets, queryResult);
        }

        [AsyncStateMachine(typeof(<SelectDataAsync>d__19))]
        public override System.Threading.Tasks.Task<SelectedData> SelectDataAsync(CancellationToken cancellationToken, params SelectStatement[] selects)
        {
            <SelectDataAsync>d__19 d__;
            d__.<>4__this = this;
            d__.cancellationToken = cancellationToken;
            d__.selects = selects;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<SelectDataAsync>d__19>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<SelectDataAsync>d__182))]
        private System.Threading.Tasks.Task<SelectStatementResult> SelectDataAsync(Query query, CriteriaOperatorCollection targets, bool includeMetadata, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            <SelectDataAsync>d__182 d__;
            d__.<>4__this = this;
            d__.query = query;
            d__.targets = targets;
            d__.includeMetadata = includeMetadata;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectStatementResult>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<SelectDataAsync>d__182>(ref d__);
            return d__.<>t__builder.Task;
        }

        private SelectStatementResult SelectDataGetSelectStatementResult(Query query, CriteriaOperatorCollection targets, SelectStatementResult queryResult)
        {
            SelectStatementResultRow[] rows = new SelectStatementResultRow[queryResult.Rows.Length];
            int index = 0;
            while (index < rows.Length)
            {
                object[] values = new object[targets.Count];
                int num2 = 0;
                while (true)
                {
                    if (num2 >= targets.Count)
                    {
                        rows[index] = new SelectStatementResultRow(values);
                        index++;
                        break;
                    }
                    values[num2] = !query.OperandIndexes.ContainsKey(num2) ? query.ConstantValues[num2].Value : queryResult.Rows[index].Values[query.OperandIndexes[num2]];
                    num2++;
                }
            }
            return new SelectStatementResult(rows);
        }

        private CriteriaOperatorCollection SelectDataPrepareCustomTargets(Query query, CriteriaOperatorCollection targets)
        {
            CriteriaOperatorCollection operators = new CriteriaOperatorCollection();
            if (query.OperandIndexes.Count == 0)
            {
                operators.Add(new OperandValue(1));
            }
            else
            {
                CriteriaOperator[] collection = new CriteriaOperator[query.OperandIndexes.Count];
                int key = 0;
                while (true)
                {
                    if (key >= targets.Count)
                    {
                        operators.AddRange(collection);
                        break;
                    }
                    if (query.OperandIndexes.ContainsKey(key))
                    {
                        collection[query.OperandIndexes[key]] = targets[key];
                    }
                    key++;
                }
            }
            return operators;
        }

        private SelectStatementResult[] SelectDataSimple(Query query, CriteriaOperatorCollection targets, bool includeMetadata)
        {
            SelectStatementResult[] resultArray2;
            IDbCommand commandFromPool = this.GetCommandFromPool(query);
            try
            {
                SelectStatementResult[] resultArray;
                int tryCount = 1;
                while (true)
                {
                    try
                    {
                        resultArray = this.InternalGetData(commandFromPool, targets, query.SkipSelectedRecords, query.TopSelectedRecords, includeMetadata);
                        break;
                    }
                    catch (Exception exception)
                    {
                        if ((this.Transaction != null) || (!this.IsConnectionBroken(exception) || (tryCount > 1)))
                        {
                            if (!this.CanRetryIfDeadlock(tryCount, exception))
                            {
                                goto TR_0001;
                            }
                        }
                        else
                        {
                            try
                            {
                                this.DoReconnect();
                            }
                            catch
                            {
                                goto TR_0001;
                            }
                        }
                        goto TR_0002;
                    TR_0001:
                        throw this.WrapException(exception, commandFromPool);
                    }
                TR_0002:
                    tryCount++;
                }
                Trace.WriteLineIf(xpoSwitch.TraceInfo, new SelectStatementResultTracer(targets, resultArray));
                resultArray2 = resultArray;
            }
            finally
            {
                this.ReleasePooledCommand(commandFromPool);
            }
            return resultArray2;
        }

        [AsyncStateMachine(typeof(<SelectDataSimpleAsync>d__186))]
        private System.Threading.Tasks.Task<SelectStatementResult[]> SelectDataSimpleAsync(Query query, CriteriaOperatorCollection targets, bool includeMetadata, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            <SelectDataSimpleAsync>d__186 d__;
            d__.<>4__this = this;
            d__.query = query;
            d__.targets = targets;
            d__.includeMetadata = includeMetadata;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectStatementResult[]>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<SelectDataSimpleAsync>d__186>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected void StringBuilderAppendLine(StringBuilder sb)
        {
            this.StringBuilderAppendLine(sb, string.Empty);
        }

        protected void StringBuilderAppendLine(StringBuilder sb, string str)
        {
            sb.AppendLine(str);
        }

        public bool UnregisterCustomAggregate(ICustomAggregateFormattable customAggregate) => 
            (customAggregate != null) ? this.customAggregatesByName.Remove(customAggregate.Name) : false;

        public bool UnregisterCustomAggregate(string functionName) => 
            (functionName != null) ? this.customAggregatesByName.Remove(functionName) : false;

        public bool UnregisterCustomFunctionOperator(ICustomFunctionOperatorFormattable customFunction) => 
            (customFunction != null) ? this.customFunctionsByName.Remove(customFunction.Name) : false;

        public bool UnregisterCustomFunctionOperator(string functionName) => 
            (functionName != null) ? this.customFunctionsByName.Remove(functionName) : false;

        protected ParameterValue UpdateRecord(ModificationStatement root, TaggedParametersHolder identities)
        {
            if (root is InsertStatement)
            {
                return this.DoInsertRecord((InsertStatement) root, identities);
            }
            if (root is UpdateStatement)
            {
                return this.DoUpdateRecord((UpdateStatement) root, identities);
            }
            if (!(root is DeleteStatement))
            {
                throw new InvalidOperationException();
            }
            return this.DoDeleteRecord((DeleteStatement) root, identities);
        }

        protected System.Threading.Tasks.Task<ParameterValue> UpdateRecordAsync(ModificationStatement root, TaggedParametersHolder identities, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            if (root is InsertStatement)
            {
                return this.DoInsertRecordAsync((InsertStatement) root, identities, asyncOperationId, cancellationToken);
            }
            if (root is UpdateStatement)
            {
                return this.DoUpdateRecordAsync((UpdateStatement) root, identities, asyncOperationId, cancellationToken);
            }
            if (!(root is DeleteStatement))
            {
                throw new InvalidOperationException();
            }
            return this.DoDeleteRecordAsync((DeleteStatement) root, identities, asyncOperationId, cancellationToken);
        }

        public override UpdateSchemaResult UpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables)
        {
            using (GetUpdateSchemaPerformanceCounters())
            {
                return base.UpdateSchema(doNotCreateIfFirstTableNotExist, tables);
            }
        }

        protected virtual Exception WrapException(Exception e, IDbCommand query) => 
            new SqlExecutionErrorException(query.CommandText, GetParametersString(query), e);

        protected virtual bool InternalExecuteUpdateSchemaInTransaction =>
            true;

        private Random Randomizer
        {
            get
            {
                this.randomizer ??= new Random(DateTime.UtcNow.GetHashCode());
                return this.randomizer;
            }
        }

        [Description(""), Browsable(false)]
        public bool CanCreateDatabase =>
            this.AutoCreateOption == AutoCreateOption.DatabaseAndSchema;

        [Description(""), Browsable(false)]
        public bool CanCreateSchema =>
            (this.AutoCreateOption == AutoCreateOption.SchemaOnly) || this.CanCreateDatabase;

        [Description(""), Browsable(false)]
        public IDbConnection Connection =>
            this.connection;

        [Description(""), Browsable(false)]
        public string ConnectionString =>
            this.connectString;

        [Obsolete("SyncRoot is obsolette, use LockHelper.Lock() or LockHelper.LockAsync() instead."), Description(""), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override object SyncRoot =>
            this.Connection;

        [Description("")]
        public ICollection<ICustomFunctionOperatorFormattable> CustomFunctionOperators =>
            this.customFunctionsByName.Values;

        [Description("Gets a collection of custom aggregate functions supplied by the current metadata provider.")]
        public ICollection<ICustomAggregateFormattable> CustomAggregates =>
            this.customAggregatesByName.Values;

        protected virtual bool NeedsIndexForForeignKey =>
            true;

        protected virtual string CreateIndexTemplate =>
            "create {0} index {1} on {2}({3})";

        protected virtual string CreateForeignKeyTemplate =>
            "alter table {0} add constraint {1} foreign key ({2}) references {3}({4})";

        protected virtual bool IsFieldTypesNeeded =>
            false;

        [Description(""), Browsable(false)]
        public virtual IDbTransaction Transaction =>
            this.transaction;

        [Obsolete("Use CommandPoolBehavior instead", false)]
        protected virtual bool SupportCommandPrepare =>
            false;

        protected virtual DevExpress.Xpo.DB.CommandPoolBehavior CommandPoolBehavior =>
            DevExpress.Xpo.DB.CommandPoolBehavior.None;

        [Description(""), Browsable(false)]
        public virtual bool NativeSkipTakeSupported =>
            false;

        [Description(""), Browsable(false)]
        public virtual bool BraceJoin =>
            true;

        [Description(""), Browsable(false)]
        public virtual bool SupportNamedParameters =>
            true;

        [Description(""), Browsable(false)]
        public virtual bool NativeOuterApplySupported =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ConnectionProviderSql.<>c <>9 = new ConnectionProviderSql.<>c();
            public static Func<IDataParameter, SelectStatementResultRow> <>9__264_0;
            public static Func<IDataParameter, SelectStatementResultRow> <>9__265_0;

            internal SelectStatementResultRow <ExecuteSprocInternal>b__264_0(IDataParameter op)
            {
                object[] values = new object[] { op.ParameterName, op.Value };
                return new SelectStatementResultRow(values);
            }

            internal SelectStatementResultRow <ExecuteSprocInternalAsync>b__265_0(IDataParameter op)
            {
                object[] values = new object[] { op.ParameterName, op.Value };
                return new SelectStatementResultRow(values);
            }
        }

        [CompilerGenerated]
        private struct <CommandExecuteReaderAsync>d__279 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IDataReader> <>t__builder;
            public IDbCommand command;
            public CommandBehavior commandBehavior;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter awaiter;
                    DbDataReader reader3;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        DbCommand command = this.command as DbCommand;
                        if (command == null)
                        {
                            object[] args = new object[] { this.command.GetType().FullName, "CommandExecuteReaderAsync" };
                            throw new NotSupportedException(DbRes.GetString("ConnectionProviderSql_DbCommandAsyncOperationsNotSupported", args));
                        }
                        awaiter = command.ExecuteReaderAsync(this.commandBehavior, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter, ConnectionProviderSql.<CommandExecuteReaderAsync>d__279>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    reader3 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<DbDataReader>.ConfiguredTaskAwaiter();
                    IDataReader result = reader3;
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(result);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__257 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<object> <>t__builder;
            public ConnectionProviderSql <>4__this;
            public string command;
            public object args;
            private AsyncOperationIdentifier <asyncOperationId>5__1;
            public CancellationToken cancellationToken;
            private IDisposable <>7__wrap1;
            private ConfiguredTaskAwaitable<IDisposable>.ConfiguredTaskAwaiter <>u__1;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<IDisposable>.ConfiguredTaskAwaiter awaiter;
                    IDisposable disposable2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<IDisposable>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            goto TR_000D;
                        }
                        else
                        {
                            this.<asyncOperationId>5__1 = AsyncOperationIdentifier.New();
                            awaiter = this.<>4__this.LockHelper.LockAsync(this.<asyncOperationId>5__1).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_000E;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<IDisposable>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__257>(ref awaiter, ref this);
                            }
                        }
                        return;
                    }
                    goto TR_000E;
                TR_000D:
                    try
                    {
                        ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter2;
                        object obj4;
                        if (num == 1)
                        {
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0008;
                        }
                        else
                        {
                            awaiter2 = this.<>4__this.DoInternalAsync(this.command, this.args, this.<asyncOperationId>5__1, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter2.IsCompleted)
                            {
                                goto TR_0008;
                            }
                            else
                            {
                                this.<>1__state = num = 1;
                                this.<>u__2 = awaiter2;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__257>(ref awaiter2, ref this);
                            }
                        }
                        return;
                    TR_0008:
                        obj4 = awaiter2.GetResult();
                        awaiter2 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                        object result = obj4;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                        return;
                    }
                    finally
                    {
                        if ((num < 0) && (this.<>7__wrap1 != null))
                        {
                            this.<>7__wrap1.Dispose();
                        }
                    }
                    return;
                TR_000E:
                    disposable2 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<IDisposable>.ConfiguredTaskAwaiter();
                    IDisposable disposable = disposable2;
                    this.<>7__wrap1 = disposable;
                    goto TR_000D;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <DoDeleteRecordAsync>d__203 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<ParameterValue> <>t__builder;
            public ConnectionProviderSql <>4__this;
            public TaggedParametersHolder identities;
            public DeleteStatement root;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
                    int num4;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0006;
                    }
                    else
                    {
                        awaiter = this.<>4__this.ExecSqlAsync(new DeleteSqlGenerator(this.<>4__this, this.identities, new Dictionary<OperandValue, string>()).GenerateSql(this.root), this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0006;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoDeleteRecordAsync>d__203>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0006:
                    num4 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                    int num2 = num4;
                    if ((this.root.RecordsAffected != 0) && (this.root.RecordsAffected != num2))
                    {
                        this.<>4__this.RollbackTransaction();
                        throw new LockingException();
                    }
                    ParameterValue result = null;
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(result);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <DoInsertRecordAsync>d__199 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<ParameterValue> <>t__builder;
            public InsertStatement root;
            public ConnectionProviderSql <>4__this;
            public TaggedParametersHolder identities;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
            private ParameterValue <>7__wrap1;
            private ConfiguredTaskAwaitable<long>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                ParameterValue value2;
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
                    ConfiguredTaskAwaitable<long>.ConfiguredTaskAwaiter awaiter2;
                    long num3;
                    ConfiguredTaskAwaitable<long>.ConfiguredTaskAwaiter awaiter3;
                    switch (num)
                    {
                        case 0:
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0004;

                        case 1:
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new ConfiguredTaskAwaitable<long>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_000F;

                        case 2:
                            awaiter3 = this.<>u__2;
                            this.<>u__2 = new ConfiguredTaskAwaitable<long>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_000A;

                        default:
                            if (!this.root.IdentityParameter.ReferenceEqualsNull())
                            {
                                this.identities.ConsolidateIdentity(this.root.IdentityParameter);
                                DBColumnType identityColumnType = this.root.IdentityColumnType;
                                if (identityColumnType == DBColumnType.Int32)
                                {
                                    this.<>7__wrap1 = this.root.IdentityParameter;
                                    awaiter2 = this.<>4__this.GetIdentityAsync(this.root, this.identities, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter2.IsCompleted)
                                    {
                                        goto TR_000F;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 1;
                                        this.<>u__2 = awaiter2;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<long>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoInsertRecordAsync>d__199>(ref awaiter2, ref this);
                                    }
                                }
                                else
                                {
                                    if (identityColumnType != DBColumnType.Int64)
                                    {
                                        throw new NotSupportedException($"The AutoIncremented key with '{this.root.IdentityColumnType}' type is not supported for '{this.<>4__this.GetType()}'.");
                                    }
                                    this.<>7__wrap1 = this.root.IdentityParameter;
                                    awaiter3 = this.<>4__this.GetIdentityAsync(this.root, this.identities, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter3.IsCompleted)
                                    {
                                        goto TR_000A;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 2;
                                        this.<>u__2 = awaiter3;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<long>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoInsertRecordAsync>d__199>(ref awaiter3, ref this);
                                    }
                                }
                            }
                            else
                            {
                                awaiter = this.<>4__this.ExecSqlAsync(new InsertSqlGenerator(this.<>4__this, this.identities, new Dictionary<OperandValue, string>()).GenerateSql(this.root), this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                if (awaiter.IsCompleted)
                                {
                                    goto TR_0004;
                                }
                                else
                                {
                                    this.<>1__state = num = 0;
                                    this.<>u__1 = awaiter;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoInsertRecordAsync>d__199>(ref awaiter, ref this);
                                }
                            }
                            break;
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                    value2 = null;
                    goto TR_0003;
                TR_0009:
                    value2 = this.root.IdentityParameter;
                    goto TR_0003;
                TR_000A:
                    num3 = awaiter3.GetResult();
                    awaiter3 = new ConfiguredTaskAwaitable<long>.ConfiguredTaskAwaiter();
                    object obj3 = num3;
                    this.<>7__wrap1.Value = obj3;
                    this.<>7__wrap1 = null;
                    goto TR_0009;
                TR_000F:
                    num3 = awaiter2.GetResult();
                    awaiter2 = new ConfiguredTaskAwaitable<long>.ConfiguredTaskAwaiter();
                    object obj2 = (int) num3;
                    this.<>7__wrap1.Value = obj2;
                    this.<>7__wrap1 = null;
                    goto TR_0009;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
                return;
            TR_0003:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(value2);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <DoInternalAsync>d__260 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<object> <>t__builder;
            public string command;
            public object args;
            public ConnectionProviderSql <>4__this;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter <>u__1;
            private TaskAwaiter<int> <>u__2;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__3;
            private ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter <>u__4;

            private void MoveNext()
            {
                object obj2;
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter awaiter;
                    ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter awaiter2;
                    TaskAwaiter<int> awaiter3;
                    int num4;
                    TaskAwaiter<int> awaiter4;
                    ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter5;
                    object obj4;
                    ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter6;
                    ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter awaiter7;
                    SelectStatementResult[] resultArray2;
                    ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter awaiter8;
                    ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter awaiter9;
                    ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter awaiter10;
                    switch (num)
                    {
                        case 0:
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0026;

                        case 1:
                            awaiter2 = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            break;

                        case 2:
                            awaiter3 = this.<>u__2;
                            this.<>u__2 = new TaskAwaiter<int>();
                            this.<>1__state = num = -1;
                            goto TR_002D;

                        case 3:
                            awaiter4 = this.<>u__2;
                            this.<>u__2 = new TaskAwaiter<int>();
                            this.<>1__state = num = -1;
                            goto TR_0004;

                        case 4:
                            awaiter5 = this.<>u__3;
                            this.<>u__3 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0041;

                        case 5:
                            awaiter6 = this.<>u__3;
                            this.<>u__3 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_003B;

                        case 6:
                            awaiter7 = this.<>u__4;
                            this.<>u__4 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0013;

                        case 7:
                            awaiter8 = this.<>u__4;
                            this.<>u__4 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0036;

                        case 8:
                            awaiter9 = this.<>u__4;
                            this.<>u__4 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_000D;

                        case 9:
                            awaiter10 = this.<>u__4;
                            this.<>u__4 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0018;

                        default:
                        {
                            string command = this.command;
                            uint num2 = <PrivateImplementationDetails>.ComputeStringHash(command);
                            if (num2 > 0x5b7989eb)
                            {
                                if (num2 > 0x92c458fd)
                                {
                                    if (num2 > 0xb11c5e39)
                                    {
                                        if (num2 == 0xc3b53055)
                                        {
                                            if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedureParametrized")
                                            {
                                                CommandChannelHelper.SprocQuery args = this.args as CommandChannelHelper.SprocQuery;
                                                if (args == null)
                                                {
                                                    throw new ArgumentException($"Wrong parameter set for command '{"DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedureParametrized"}'.");
                                                }
                                                awaiter2 = this.<>4__this.ExecuteSprocParametrizedAsync(this.asyncOperationId, this.cancellationToken, args.SprocName, args.Parameters).ConfigureAwait(false).GetAwaiter();
                                                if (awaiter2.IsCompleted)
                                                {
                                                    break;
                                                }
                                                this.<>1__state = num = 1;
                                                this.<>u__1 = awaiter2;
                                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoInternalAsync>d__260>(ref awaiter2, ref this);
                                                return;
                                            }
                                        }
                                        else if ((num2 == 0xfa72d4b9) && (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteScalarSQL"))
                                        {
                                            awaiter5 = this.<>4__this.GetScalarAsync(new Query((string) this.args), this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                            if (awaiter5.IsCompleted)
                                            {
                                                goto TR_0041;
                                            }
                                            else
                                            {
                                                this.<>1__state = num = 4;
                                                this.<>u__3 = awaiter5;
                                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoInternalAsync>d__260>(ref awaiter5, ref this);
                                            }
                                            return;
                                        }
                                        goto TR_0007;
                                    }
                                    else
                                    {
                                        if (num2 == 0xab79b86b)
                                        {
                                            if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteScalarSQLWithParams")
                                            {
                                                awaiter6 = this.<>4__this.GetScalarAsync(this.<>4__this.ProcessSqlQueryCommandArgument(this.args), this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                                if (awaiter6.IsCompleted)
                                                {
                                                    goto TR_003B;
                                                }
                                                else
                                                {
                                                    this.<>1__state = num = 5;
                                                    this.<>u__3 = awaiter6;
                                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoInternalAsync>d__260>(ref awaiter6, ref this);
                                                }
                                                return;
                                            }
                                        }
                                        else if ((num2 == 0xb11c5e39) && (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithParams"))
                                        {
                                            awaiter8 = this.<>4__this.SelectDataSimpleAsync(this.<>4__this.ProcessSqlQueryCommandArgument(this.args), null, false, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                            if (awaiter8.IsCompleted)
                                            {
                                                goto TR_0036;
                                            }
                                            else
                                            {
                                                this.<>1__state = num = 7;
                                                this.<>u__4 = awaiter8;
                                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoInternalAsync>d__260>(ref awaiter8, ref this);
                                            }
                                            return;
                                        }
                                        goto TR_0007;
                                    }
                                }
                                else if (num2 == 0x6a460874)
                                {
                                    if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitRollbackTransaction")
                                    {
                                        this.<>4__this.ExplicitRollbackTransaction();
                                        obj2 = null;
                                    }
                                    else
                                    {
                                        goto TR_0007;
                                    }
                                    goto TR_0003;
                                }
                                else if (num2 == 0x8e14028a)
                                {
                                    if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteNonQuerySQL")
                                    {
                                        awaiter3 = this.<>4__this.ExecSqlAsync(new Query((string) this.args), this.asyncOperationId, this.cancellationToken).GetAwaiter();
                                        if (awaiter3.IsCompleted)
                                        {
                                            goto TR_002D;
                                        }
                                        else
                                        {
                                            this.<>1__state = num = 2;
                                            this.<>u__2 = awaiter3;
                                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<int>, ConnectionProviderSql.<DoInternalAsync>d__260>(ref awaiter3, ref this);
                                        }
                                    }
                                    else
                                    {
                                        goto TR_0007;
                                    }
                                }
                                else if ((num2 != 0x92c458fd) || (command != "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedure"))
                                {
                                    goto TR_0007;
                                }
                                else
                                {
                                    CommandChannelHelper.SprocQuery args = this.args as CommandChannelHelper.SprocQuery;
                                    if (args == null)
                                    {
                                        throw new ArgumentException($"Wrong parameter set for command '{"DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedure"}'.");
                                    }
                                    awaiter = this.<>4__this.ExecuteSprocAsync(this.asyncOperationId, this.cancellationToken, args.SprocName, args.Parameters).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter.IsCompleted)
                                    {
                                        goto TR_0026;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoInternalAsync>d__260>(ref awaiter, ref this);
                                    }
                                }
                                return;
                            }
                            else
                            {
                                if (num2 > 0x4fd322ac)
                                {
                                    if (num2 == 0x52efb8eb)
                                    {
                                        if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitBeginTransaction")
                                        {
                                            if ((this.args != null) && (this.args is System.Data.IsolationLevel))
                                            {
                                                this.<>4__this.ExplicitBeginTransaction((System.Data.IsolationLevel) this.args);
                                            }
                                            else
                                            {
                                                this.<>4__this.ExplicitBeginTransaction();
                                            }
                                            obj2 = null;
                                        }
                                        else
                                        {
                                            goto TR_0007;
                                        }
                                        goto TR_0003;
                                    }
                                    else
                                    {
                                        if (num2 == 0x57f6c538)
                                        {
                                            if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithMetadataWithParams")
                                            {
                                                awaiter10 = this.<>4__this.SelectDataSimpleAsync(this.<>4__this.ProcessSqlQueryCommandArgument(this.args), null, true, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                                if (awaiter10.IsCompleted)
                                                {
                                                    goto TR_0018;
                                                }
                                                else
                                                {
                                                    this.<>1__state = num = 9;
                                                    this.<>u__4 = awaiter10;
                                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoInternalAsync>d__260>(ref awaiter10, ref this);
                                                }
                                                return;
                                            }
                                        }
                                        else if ((num2 == 0x5b7989eb) && (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQL"))
                                        {
                                            awaiter7 = this.<>4__this.SelectDataSimpleAsync(new Query((string) this.args), null, false, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                            if (awaiter7.IsCompleted)
                                            {
                                                goto TR_0013;
                                            }
                                            else
                                            {
                                                this.<>1__state = num = 6;
                                                this.<>u__4 = awaiter7;
                                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoInternalAsync>d__260>(ref awaiter7, ref this);
                                            }
                                            return;
                                        }
                                        goto TR_0007;
                                    }
                                    return;
                                }
                                else if (num2 == 0x41ea026e)
                                {
                                    if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithMetadata")
                                    {
                                        awaiter9 = this.<>4__this.SelectDataSimpleAsync(new Query((string) this.args), null, true, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                        if (awaiter9.IsCompleted)
                                        {
                                            goto TR_000D;
                                        }
                                        else
                                        {
                                            this.<>1__state = num = 8;
                                            this.<>u__4 = awaiter9;
                                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoInternalAsync>d__260>(ref awaiter9, ref this);
                                        }
                                    }
                                    else
                                    {
                                        goto TR_0007;
                                    }
                                    return;
                                }
                                else if (num2 == 0x45b7f1c3)
                                {
                                    if (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitCommitTransaction")
                                    {
                                        this.<>4__this.ExplicitCommitTransaction();
                                        obj2 = null;
                                    }
                                    else
                                    {
                                        goto TR_0007;
                                    }
                                }
                                else
                                {
                                    if ((num2 != 0x4fd322ac) || (command != "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteNonQuerySQLWithParams"))
                                    {
                                        goto TR_0007;
                                    }
                                    else
                                    {
                                        awaiter4 = this.<>4__this.ExecSqlAsync(this.<>4__this.ProcessSqlQueryCommandArgument(this.args), this.asyncOperationId, this.cancellationToken).GetAwaiter();
                                        if (awaiter4.IsCompleted)
                                        {
                                            goto TR_0004;
                                        }
                                        else
                                        {
                                            this.<>1__state = num = 3;
                                            this.<>u__2 = awaiter4;
                                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<int>, ConnectionProviderSql.<DoInternalAsync>d__260>(ref awaiter4, ref this);
                                        }
                                    }
                                    return;
                                }
                                goto TR_0003;
                            }
                            break;
                        }
                    }
                    SelectedData result = awaiter2.GetResult();
                    awaiter2 = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                    obj2 = ConnectionProviderSql.FixDBNull(result);
                    goto TR_0003;
                TR_0004:
                    num4 = awaiter4.GetResult();
                    awaiter4 = new TaskAwaiter<int>();
                    obj2 = num4;
                    goto TR_0003;
                TR_0007:
                    throw new NotSupportedException($"Command '{this.command}' is not supported.");
                TR_000D:
                    resultArray2 = awaiter9.GetResult();
                    awaiter9 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                    obj2 = ConnectionProviderSql.FixDBNull(new SelectedData(resultArray2));
                    goto TR_0003;
                TR_0013:
                    resultArray2 = awaiter7.GetResult();
                    awaiter7 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                    obj2 = ConnectionProviderSql.FixDBNull(new SelectedData(resultArray2));
                    goto TR_0003;
                TR_0018:
                    resultArray2 = awaiter10.GetResult();
                    awaiter10 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                    obj2 = ConnectionProviderSql.FixDBNull(new SelectedData(resultArray2));
                    goto TR_0003;
                TR_0026:
                    result = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                    obj2 = ConnectionProviderSql.FixDBNull(result);
                    goto TR_0003;
                TR_002D:
                    num4 = awaiter3.GetResult();
                    awaiter3 = new TaskAwaiter<int>();
                    obj2 = num4;
                    goto TR_0003;
                TR_0036:
                    resultArray2 = awaiter8.GetResult();
                    awaiter8 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                    obj2 = ConnectionProviderSql.FixDBNull(new SelectedData(resultArray2));
                    goto TR_0003;
                TR_003B:
                    obj4 = awaiter6.GetResult();
                    awaiter6 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                    object data = obj4;
                    obj2 = this.<>4__this.FixDBNullScalar(data);
                    goto TR_0003;
                TR_0041:
                    obj4 = awaiter5.GetResult();
                    awaiter5 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                    object obj3 = obj4;
                    obj2 = this.<>4__this.FixDBNullScalar(obj3);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            TR_0003:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(obj2);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <DoUpdateRecordAsync>d__201 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<ParameterValue> <>t__builder;
            public ConnectionProviderSql <>4__this;
            public TaggedParametersHolder identities;
            public UpdateStatement root;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ParameterValue value2;
                    ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
                    int num4;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0006;
                    }
                    else
                    {
                        Query query = new UpdateSqlGenerator(this.<>4__this, this.identities, new Dictionary<OperandValue, string>()).GenerateSql(this.root);
                        if (query.Sql == null)
                        {
                            goto TR_0005;
                        }
                        else
                        {
                            awaiter = this.<>4__this.ExecSqlAsync(query, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0006;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ConnectionProviderSql.<DoUpdateRecordAsync>d__201>(ref awaiter, ref this);
                            }
                        }
                    }
                    return;
                TR_0005:
                    value2 = null;
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(value2);
                    return;
                TR_0006:
                    num4 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                    int num2 = num4;
                    if ((this.root.RecordsAffected != 0) && (this.root.RecordsAffected != num2))
                    {
                        this.<>4__this.RollbackTransaction();
                        throw new LockingException();
                    }
                    goto TR_0005;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ExecSqlAsync>d__196 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<int> <>t__builder;
            public ConnectionProviderSql <>4__this;
            public AsyncOperationIdentifier asyncOperationId;
            public Query query;
            public CancellationToken cancellationToken;
            private IDbCommand <command>5__1;
            private IDisposable <>7__wrap1;
            private ConfiguredTaskAwaitable<IDisposable>.ConfiguredTaskAwaiter <>u__1;
            private object <>7__wrap2;
            private int <>7__wrap3;
            private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num2;
                Exception exception3;
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<IDisposable>.ConfiguredTaskAwaiter awaiter;
                    IDisposable disposable2;
                    switch (num)
                    {
                        case 0:
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable<IDisposable>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_002F;

                        case 1:
                        case 2:
                            break;

                        default:
                            awaiter = this.<>4__this.LockHelper.LockAsync(this.asyncOperationId).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_002F;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<IDisposable>.ConfiguredTaskAwaiter, ConnectionProviderSql.<ExecSqlAsync>d__196>(ref awaiter, ref this);
                            }
                            return;
                    }
                    goto TR_002E;
                TR_0012:
                    this.<>7__wrap1 = null;
                    goto TR_000D;
                TR_002E:
                    try
                    {
                        if ((num != 1) && (num != 2))
                        {
                            this.<command>5__1 = this.<>4__this.GetCommandFromPool(this.query);
                        }
                        try
                        {
                            if (num != 1)
                            {
                                int num6 = num;
                            }
                            try
                            {
                                int num4;
                                ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter3;
                                if (num != 1)
                                {
                                    if (num == 2)
                                    {
                                        awaiter3 = this.<>u__2;
                                        this.<>u__2 = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                                        this.<>1__state = num = -1;
                                        goto TR_000E;
                                    }
                                    else
                                    {
                                        this.<>7__wrap3 = 0;
                                    }
                                }
                                try
                                {
                                    ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
                                    if (num == 1)
                                    {
                                        awaiter2 = this.<>u__2;
                                        this.<>u__2 = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                                        this.<>1__state = num = -1;
                                        goto TR_001D;
                                    }
                                    else
                                    {
                                        awaiter2 = this.<>4__this.InternalExecSqlAsync(this.<command>5__1, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                        if (awaiter2.IsCompleted)
                                        {
                                            goto TR_001D;
                                        }
                                        else
                                        {
                                            this.<>1__state = num = 1;
                                            this.<>u__2 = awaiter2;
                                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ConnectionProviderSql.<ExecSqlAsync>d__196>(ref awaiter2, ref this);
                                        }
                                    }
                                    return;
                                TR_001D:
                                    num4 = awaiter2.GetResult();
                                    awaiter2 = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                                    num2 = num4;
                                    goto TR_000D;
                                }
                                catch (OperationCanceledException)
                                {
                                    throw;
                                }
                                catch (Exception exception)
                                {
                                    this.<>7__wrap2 = exception;
                                    this.<>7__wrap3 = 1;
                                }
                                if (this.<>7__wrap3 != 1)
                                {
                                    goto TR_0014;
                                }
                                else
                                {
                                    Exception e = (Exception) this.<>7__wrap2;
                                    if ((this.<>4__this.Transaction != null) || !this.<>4__this.IsConnectionBroken(e))
                                    {
                                        exception3 = this.<>7__wrap2 as Exception;
                                        if (exception3 == null)
                                        {
                                            throw this.<>7__wrap2;
                                        }
                                        ExceptionDispatchInfo.Capture(exception3).Throw();
                                        goto TR_0014;
                                    }
                                    else
                                    {
                                        this.<>4__this.DoReconnect();
                                        awaiter3 = this.<>4__this.InternalExecSqlAsync(this.<command>5__1, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                        if (awaiter3.IsCompleted)
                                        {
                                            goto TR_000E;
                                        }
                                        else
                                        {
                                            this.<>1__state = num = 2;
                                            this.<>u__2 = awaiter3;
                                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ConnectionProviderSql.<ExecSqlAsync>d__196>(ref awaiter3, ref this);
                                        }
                                    }
                                }
                                return;
                            TR_000E:
                                num4 = awaiter3.GetResult();
                                awaiter3 = new ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter();
                                num2 = num4;
                                goto TR_000D;
                            TR_0014:
                                this.<>7__wrap2 = null;
                                goto TR_0013;
                            }
                            catch (OperationCanceledException)
                            {
                                throw;
                            }
                            catch (Exception exception4)
                            {
                                throw this.<>4__this.WrapException(exception4, this.<command>5__1);
                            }
                        }
                        finally
                        {
                            if (num < 0)
                            {
                                this.<>4__this.ReleasePooledCommand(this.<command>5__1);
                            }
                        }
                        return;
                    TR_0013:
                        this.<command>5__1 = null;
                        goto TR_0012;
                    }
                    finally
                    {
                        if ((num < 0) && (this.<>7__wrap1 != null))
                        {
                            this.<>7__wrap1.Dispose();
                        }
                    }
                    return;
                TR_002F:
                    disposable2 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<IDisposable>.ConfiguredTaskAwaiter();
                    IDisposable disposable = disposable2;
                    this.<>7__wrap1 = disposable;
                    goto TR_002E;
                }
                catch (Exception exception6)
                {
                    exception3 = exception6;
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception3);
                }
                return;
            TR_000D:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(num2);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ExecuteSprocAsync>d__271 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public ConnectionProviderSql <>4__this;
            public string sprocName;
            public OperandValue[] parameters;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private IDbCommand <command>5__1;
            private ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<command>5__1 = this.<>4__this.CreateCommand();
                    }
                    try
                    {
                        ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter awaiter;
                        SelectedData data3;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0007;
                        }
                        else
                        {
                            IDataParameter parameter;
                            List<IDataParameter> list;
                            this.<command>5__1.CommandType = CommandType.StoredProcedure;
                            this.<command>5__1.CommandText = this.<>4__this.FormatTable(this.<>4__this.ComposeSafeSchemaName(this.sprocName), this.<>4__this.ComposeSafeTableName(this.sprocName));
                            this.<>4__this.CommandBuilderDeriveParameters(this.<command>5__1);
                            this.<>4__this.PrepareParametersForExecuteSproc(this.parameters, this.<command>5__1, out list, out parameter);
                            awaiter = this.<>4__this.ExecuteSprocInternalAsync(this.<command>5__1, parameter, list, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0007;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter, ConnectionProviderSql.<ExecuteSprocAsync>d__271>(ref awaiter, ref this);
                            }
                        }
                        return;
                    TR_0007:
                        data3 = awaiter.GetResult();
                        awaiter = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                        SelectedData result = data3;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                    }
                    finally
                    {
                        if ((num < 0) && (this.<command>5__1 != null))
                        {
                            this.<command>5__1.Dispose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ExecuteSprocInternalAsync>d__265 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public ConnectionProviderSql <>4__this;
            public IDbCommand command;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            public List<IDataParameter> outParameters;
            public IDataParameter returnParameter;
            private ConfiguredTaskAwaitable<List<SelectStatementResult>>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<List<SelectStatementResult>>.ConfiguredTaskAwaiter awaiter;
                    List<SelectStatementResult> list3;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<List<SelectStatementResult>>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_000B;
                    }
                    else
                    {
                        awaiter = this.<>4__this.GetSelectedStatmentResultsAsync(this.command, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_000B;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<List<SelectStatementResult>>.ConfiguredTaskAwaiter, ConnectionProviderSql.<ExecuteSprocInternalAsync>d__265>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_000B:
                    list3 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<List<SelectStatementResult>>.ConfiguredTaskAwaiter();
                    List<SelectStatementResult> list = list3;
                    this.cancellationToken.ThrowIfCancellationRequested();
                    if (this.outParameters.Count > 0)
                    {
                        Func<IDataParameter, SelectStatementResultRow> selector = ConnectionProviderSql.<>c.<>9__265_0;
                        if (ConnectionProviderSql.<>c.<>9__265_0 == null)
                        {
                            Func<IDataParameter, SelectStatementResultRow> local1 = ConnectionProviderSql.<>c.<>9__265_0;
                            selector = ConnectionProviderSql.<>c.<>9__265_0 = new Func<IDataParameter, SelectStatementResultRow>(this.<ExecuteSprocInternalAsync>b__265_0);
                        }
                        list.Add(new SelectStatementResult(this.outParameters.Select<IDataParameter, SelectStatementResultRow>(selector).ToArray<SelectStatementResultRow>()));
                    }
                    if (this.returnParameter != null)
                    {
                        object[] values = new object[] { this.returnParameter.Value };
                        SelectStatementResultRow[] rows = new SelectStatementResultRow[] { new SelectStatementResultRow(values) };
                        list.Add(new SelectStatementResult(rows));
                    }
                    SelectedData result = new SelectedData(list.ToArray());
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(result);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ExecuteSprocParametrizedAsync>d__267 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public ConnectionProviderSql <>4__this;
            public string sprocName;
            public OperandValue[] parameters;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private IDbCommand <command>5__1;
            private ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<command>5__1 = this.<>4__this.CreateCommand();
                    }
                    try
                    {
                        ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter awaiter;
                        SelectedData data3;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0007;
                        }
                        else
                        {
                            List<IDataParameter> list;
                            IDataParameter parameter;
                            this.<>4__this.PrepareCommandForExecuteSprocParametrized(this.<command>5__1, this.sprocName, this.parameters, out list, out parameter);
                            awaiter = this.<>4__this.ExecuteSprocInternalAsync(this.<command>5__1, parameter, list, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0007;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter, ConnectionProviderSql.<ExecuteSprocParametrizedAsync>d__267>(ref awaiter, ref this);
                            }
                        }
                        return;
                    TR_0007:
                        data3 = awaiter.GetResult();
                        awaiter = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                        SelectedData result = data3;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                    }
                    finally
                    {
                        if ((num < 0) && (this.<command>5__1 != null))
                        {
                            this.<command>5__1.Dispose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <GetScalarAsync>d__194 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<object> <>t__builder;
            public ConnectionProviderSql <>4__this;
            public Query query;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private IDbCommand <command>5__1;
            private object <>7__wrap1;
            private int <>7__wrap2;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                object obj2;
                Exception exception3;
                int num = this.<>1__state;
                try
                {
                    if ((num != 0) && (num != 1))
                    {
                        this.<command>5__1 = this.<>4__this.GetCommandFromPool(this.query);
                    }
                    try
                    {
                        if (num != 0)
                        {
                            int num3 = num;
                        }
                        try
                        {
                            object obj4;
                            ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter2;
                            if (num != 0)
                            {
                                if (num == 1)
                                {
                                    awaiter2 = this.<>u__1;
                                    this.<>u__1 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_000A;
                                }
                                else
                                {
                                    this.<>7__wrap2 = 0;
                                }
                            }
                            try
                            {
                                ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
                                if (num == 0)
                                {
                                    awaiter = this.<>u__1;
                                    this.<>u__1 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_0018;
                                }
                                else
                                {
                                    awaiter = this.<>4__this.InternalGetScalarAsync(this.<command>5__1, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter.IsCompleted)
                                    {
                                        goto TR_0018;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, ConnectionProviderSql.<GetScalarAsync>d__194>(ref awaiter, ref this);
                                    }
                                }
                                return;
                            TR_0018:
                                obj4 = awaiter.GetResult();
                                awaiter = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                                obj2 = obj4;
                                goto TR_0009;
                            }
                            catch (OperationCanceledException)
                            {
                                throw;
                            }
                            catch (Exception exception)
                            {
                                this.<>7__wrap1 = exception;
                                this.<>7__wrap2 = 1;
                            }
                            if (this.<>7__wrap2 != 1)
                            {
                                goto TR_000F;
                            }
                            else
                            {
                                Exception e = (Exception) this.<>7__wrap1;
                                if ((this.<>4__this.Transaction != null) || !this.<>4__this.IsConnectionBroken(e))
                                {
                                    exception3 = this.<>7__wrap1 as Exception;
                                    if (exception3 == null)
                                    {
                                        throw this.<>7__wrap1;
                                    }
                                    ExceptionDispatchInfo.Capture(exception3).Throw();
                                    goto TR_000F;
                                }
                                else
                                {
                                    this.<>4__this.DoReconnect();
                                    awaiter2 = this.<>4__this.InternalGetScalarAsync(this.<command>5__1, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter2.IsCompleted)
                                    {
                                        goto TR_000A;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 1;
                                        this.<>u__1 = awaiter2;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, ConnectionProviderSql.<GetScalarAsync>d__194>(ref awaiter2, ref this);
                                    }
                                }
                            }
                            return;
                        TR_000A:
                            obj4 = awaiter2.GetResult();
                            awaiter2 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                            obj2 = obj4;
                            goto TR_0009;
                        TR_000F:
                            this.<>7__wrap1 = null;
                            goto TR_000E;
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch (Exception exception4)
                        {
                            throw this.<>4__this.WrapException(exception4, this.<command>5__1);
                        }
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            this.<>4__this.ReleasePooledCommand(this.<command>5__1);
                        }
                    }
                    return;
                TR_000E:
                    this.<command>5__1 = null;
                    goto TR_0009;
                }
                catch (Exception exception6)
                {
                    exception3 = exception6;
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception3);
                }
                return;
            TR_0009:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(obj2);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <GetSelectedStatmentResultsAsync>d__273 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<List<SelectStatementResult>> <>t__builder;
            public ConnectionProviderSql <>4__this;
            public IDbCommand command;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private IDataReader <reader>5__1;
            private Type[] <fieldTypes>5__2;
            private List<SelectStatementResultRow> <rows>5__3;
            private List<SelectStatementResult> <selectStatmentResults>5__4;
            private ConfiguredTaskAwaitable<IDataReader>.ConfiguredTaskAwaiter <>u__1;
            private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                List<SelectStatementResult> list;
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<IDataReader>.ConfiguredTaskAwaiter awaiter;
                    IDataReader reader2;
                    switch (num)
                    {
                        case 0:
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable<IDataReader>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0024;

                        case 1:
                        case 2:
                            goto TR_0023;

                        default:
                            awaiter = this.<>4__this.CommandExecuteReaderAsync(this.command, CommandBehavior.Default, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0024;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<IDataReader>.ConfiguredTaskAwaiter, ConnectionProviderSql.<GetSelectedStatmentResultsAsync>d__273>(ref awaiter, ref this);
                            }
                            break;
                    }
                    return;
                TR_0023:
                    try
                    {
                        ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
                        bool flag2;
                        ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
                        if (num == 1)
                        {
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                        }
                        else
                        {
                            if (num == 2)
                            {
                                awaiter3 = this.<>u__2;
                                this.<>u__2 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                                this.<>1__state = num = -1;
                            }
                            else
                            {
                                this.<selectStatmentResults>5__4 = new List<SelectStatementResult>();
                                if (this.<reader>5__1 == null)
                                {
                                    this.<selectStatmentResults>5__4.Add(new SelectStatementResult(new SelectStatementResultRow[0]));
                                    list = this.<selectStatmentResults>5__4;
                                    goto TR_0006;
                                }
                                goto TR_001C;
                            }
                            goto TR_000B;
                        }
                        goto TR_0016;
                    TR_000B:
                        flag2 = awaiter3.GetResult();
                        awaiter3 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                        if (!flag2)
                        {
                            list = this.<selectStatmentResults>5__4;
                            goto TR_0006;
                        }
                        goto TR_001C;
                    TR_0016:
                        flag2 = awaiter2.GetResult();
                        awaiter2 = new ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter();
                        if (flag2)
                        {
                            object[] values = new object[this.<reader>5__1.FieldCount];
                            if (this.<>4__this.IsFieldTypesNeeded && (this.<fieldTypes>5__2 == null))
                            {
                                this.<fieldTypes>5__2 = new Type[this.<reader>5__1.FieldCount];
                                for (int i = this.<reader>5__1.FieldCount - 1; i >= 0; i--)
                                {
                                    this.<fieldTypes>5__2[i] = this.<reader>5__1.GetFieldType(i);
                                }
                            }
                            this.<>4__this.GetValues(this.<reader>5__1, this.<fieldTypes>5__2, values);
                            this.<rows>5__3.Add(new SelectStatementResultRow(values));
                        }
                        else
                        {
                            this.<selectStatmentResults>5__4.Add(new SelectStatementResult(this.<rows>5__3.ToArray()));
                            this.<rows>5__3 = null;
                            this.<fieldTypes>5__2 = null;
                            awaiter3 = this.<>4__this.ReaderNextResultAsync(this.<reader>5__1, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter3.IsCompleted)
                            {
                                goto TR_000B;
                            }
                            else
                            {
                                this.<>1__state = num = 2;
                                this.<>u__2 = awaiter3;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ConnectionProviderSql.<GetSelectedStatmentResultsAsync>d__273>(ref awaiter3, ref this);
                            }
                            return;
                        }
                    TR_001A:
                        while (true)
                        {
                            awaiter2 = this.<>4__this.ReaderReadAsync(this.<reader>5__1, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter2.IsCompleted)
                            {
                                goto TR_0016;
                            }
                            else
                            {
                                this.<>1__state = num = 1;
                                this.<>u__2 = awaiter2;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ConnectionProviderSql.<GetSelectedStatmentResultsAsync>d__273>(ref awaiter2, ref this);
                            }
                            break;
                        }
                        return;
                    TR_001C:
                        while (true)
                        {
                            this.<rows>5__3 = new List<SelectStatementResultRow>();
                            this.<fieldTypes>5__2 = null;
                            break;
                        }
                        goto TR_001A;
                    }
                    finally
                    {
                        if ((num < 0) && (this.<reader>5__1 != null))
                        {
                            this.<reader>5__1.Dispose();
                        }
                    }
                    goto TR_0006;
                TR_0024:
                    reader2 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<IDataReader>.ConfiguredTaskAwaiter();
                    IDataReader reader = reader2;
                    this.<reader>5__1 = reader;
                    goto TR_0023;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
                return;
            TR_0006:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(list);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ModifyDataAsync>d__17 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<ModificationResult> <>t__builder;
            public ModificationStatement[] dmlStatements;
            public ConnectionProviderSql <>4__this;
            public CancellationToken cancellationToken;
            private int <tryCount>5__1;
            private IDisposable <>7__wrap1;
            private ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<>7__wrap1 = ConnectionProviderSql.GetModifyDataPerformanceCounters(this.dmlStatements);
                    }
                    try
                    {
                        if (num != 0)
                        {
                            this.<tryCount>5__1 = 1;
                        }
                        while (true)
                        {
                            try
                            {
                                ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter awaiter;
                                ModificationResult result3;
                                if (num == 0)
                                {
                                    awaiter = this.<>u__1;
                                    this.<>u__1 = new ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_000B;
                                }
                                else
                                {
                                    awaiter = this.<>4__this.<>n__0(this.cancellationToken, this.dmlStatements).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter.IsCompleted)
                                    {
                                        goto TR_000B;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter, ConnectionProviderSql.<ModifyDataAsync>d__17>(ref awaiter, ref this);
                                    }
                                }
                                break;
                            TR_000B:
                                result3 = awaiter.GetResult();
                                awaiter = new ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter();
                                ModificationResult result = result3;
                                this.<>1__state = -2;
                                this.<>t__builder.SetResult(result);
                                return;
                            }
                            catch (Exception exception)
                            {
                                if (!this.<>4__this.CanRetryIfDeadlock(this.<tryCount>5__1, exception))
                                {
                                    throw;
                                }
                            }
                            int num2 = this.<tryCount>5__1;
                            this.<tryCount>5__1 = num2 + 1;
                        }
                    }
                    finally
                    {
                        if ((num < 0) && (this.<>7__wrap1 != null))
                        {
                            this.<>7__wrap1.Dispose();
                        }
                    }
                }
                catch (Exception exception2)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception2);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ProcessModifyDataAsync>d__207 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<ModificationResult> <>t__builder;
            public ConnectionProviderSql <>4__this;
            public ModificationStatement[] dmlStatements;
            private TaggedParametersHolder <identities>5__1;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private List<ParameterValue> <result>5__2;
            private ModificationStatement[] <>7__wrap1;
            private int <>7__wrap2;
            private ConfiguredTaskAwaitable<ParameterValue>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<>4__this.BeginTransaction();
                    }
                    try
                    {
                        ModificationResult result;
                        ConfiguredTaskAwaitable<ParameterValue>.ConfiguredTaskAwaiter awaiter;
                        ParameterValue value4;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable<ParameterValue>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                        }
                        else
                        {
                            this.<result>5__2 = new List<ParameterValue>();
                            this.<identities>5__1 = new TaggedParametersHolder();
                            this.<>7__wrap1 = this.dmlStatements;
                            this.<>7__wrap2 = 0;
                            goto TR_000D;
                        }
                    TR_0008:
                        value4 = awaiter.GetResult();
                        awaiter = new ConfiguredTaskAwaitable<ParameterValue>.ConfiguredTaskAwaiter();
                        ParameterValue criterion = value4;
                        if (!criterion.ReferenceEqualsNull())
                        {
                            this.<result>5__2.Add(criterion);
                        }
                        this.<>7__wrap2++;
                    TR_000D:
                        while (true)
                        {
                            if (this.<>7__wrap2 < this.<>7__wrap1.Length)
                            {
                                ModificationStatement root = this.<>7__wrap1[this.<>7__wrap2];
                                awaiter = this.<>4__this.UpdateRecordAsync(root, this.<identities>5__1, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                if (awaiter.IsCompleted)
                                {
                                    goto TR_0008;
                                }
                                else
                                {
                                    this.<>1__state = num = 0;
                                    this.<>u__1 = awaiter;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<ParameterValue>.ConfiguredTaskAwaiter, ConnectionProviderSql.<ProcessModifyDataAsync>d__207>(ref awaiter, ref this);
                                }
                                return;
                            }
                            else
                            {
                                this.<>7__wrap1 = null;
                                this.<>4__this.CommitTransaction();
                                result = new ModificationResult(this.<result>5__2);
                            }
                            break;
                        }
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                    }
                    catch
                    {
                        this.<>4__this.RollbackTransaction();
                        throw;
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <SelectDataAsync>d__182 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectStatementResult> <>t__builder;
            public Query query;
            public ConnectionProviderSql <>4__this;
            public CriteriaOperatorCollection targets;
            public bool includeMetadata;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                SelectStatementResult result;
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter awaiter;
                    SelectStatementResult[] resultArray2;
                    ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter awaiter2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else if (num == 1)
                    {
                        awaiter2 = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0008;
                    }
                    else if ((this.query.ConstantValues == null) || ((this.query.OperandIndexes == null) || (this.query.ConstantValues.Count <= 0)))
                    {
                        awaiter2 = this.<>4__this.SelectDataSimpleAsync(this.query, this.targets, this.includeMetadata, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter2.IsCompleted)
                        {
                            goto TR_0008;
                        }
                        else
                        {
                            this.<>1__state = num = 1;
                            this.<>u__1 = awaiter2;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter, ConnectionProviderSql.<SelectDataAsync>d__182>(ref awaiter2, ref this);
                        }
                    }
                    else
                    {
                        CriteriaOperatorCollection targets = this.<>4__this.SelectDataPrepareCustomTargets(this.query, this.targets);
                        awaiter = this.<>4__this.SelectDataSimpleAsync(this.query, targets, this.includeMetadata, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter, ConnectionProviderSql.<SelectDataAsync>d__182>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    resultArray2 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                    SelectStatementResult queryResult = resultArray2[0];
                    result = this.<>4__this.SelectDataGetSelectStatementResult(this.query, this.targets, queryResult);
                    goto TR_0003;
                TR_0008:
                    resultArray2 = awaiter2.GetResult();
                    awaiter2 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                    result = resultArray2[0];
                    goto TR_0003;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
                return;
            TR_0003:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(result);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <SelectDataAsync>d__19 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public SelectStatement[] selects;
            public ConnectionProviderSql <>4__this;
            public CancellationToken cancellationToken;
            private IDisposable <>7__wrap1;
            private ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<>7__wrap1 = ConnectionProviderSql.GetSelectDataPerformanceCounters(this.selects);
                    }
                    try
                    {
                        ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter awaiter;
                        SelectedData data3;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0007;
                        }
                        else
                        {
                            awaiter = this.<>4__this.<>n__1(this.cancellationToken, this.selects).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0007;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter, ConnectionProviderSql.<SelectDataAsync>d__19>(ref awaiter, ref this);
                            }
                        }
                        return;
                    TR_0007:
                        data3 = awaiter.GetResult();
                        awaiter = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                        SelectedData result = data3;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                    }
                    finally
                    {
                        if ((num < 0) && (this.<>7__wrap1 != null))
                        {
                            this.<>7__wrap1.Dispose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <SelectDataSimpleAsync>d__186 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectStatementResult[]> <>t__builder;
            public ConnectionProviderSql <>4__this;
            public Query query;
            private IDbCommand <command>5__1;
            public CriteriaOperatorCollection targets;
            public bool includeMetadata;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private int <tryCount>5__2;
            private SelectStatementResult[] <result>5__3;
            private ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<command>5__1 = this.<>4__this.GetCommandFromPool(this.query);
                    }
                    try
                    {
                        if (num != 0)
                        {
                            this.<tryCount>5__2 = 1;
                        }
                        while (true)
                        {
                            int num2;
                            try
                            {
                                ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter awaiter;
                                SelectStatementResult[] resultArray3;
                                if (num == 0)
                                {
                                    awaiter = this.<>u__1;
                                    this.<>u__1 = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                                    this.<>1__state = num = -1;
                                    goto TR_0010;
                                }
                                else
                                {
                                    awaiter = this.<>4__this.InternalGetDataAsync(this.<command>5__1, this.targets, this.query.SkipSelectedRecords, this.query.TopSelectedRecords, this.includeMetadata, this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                                    if (awaiter.IsCompleted)
                                    {
                                        goto TR_0010;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter, ConnectionProviderSql.<SelectDataSimpleAsync>d__186>(ref awaiter, ref this);
                                    }
                                }
                                break;
                            TR_0010:
                                resultArray3 = awaiter.GetResult();
                                awaiter = new ConfiguredTaskAwaitable<SelectStatementResult[]>.ConfiguredTaskAwaiter();
                                SelectStatementResult[] resultArray2 = resultArray3;
                                this.<result>5__3 = resultArray2;
                                goto TR_000F;
                            }
                            catch (OperationCanceledException)
                            {
                                throw;
                            }
                            catch (Exception exception)
                            {
                                if ((this.<>4__this.Transaction != null) || (!this.<>4__this.IsConnectionBroken(exception) || (this.<tryCount>5__2 > 1)))
                                {
                                    if (!this.<>4__this.CanRetryIfDeadlock(this.<tryCount>5__2, exception))
                                    {
                                        goto TR_0006;
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        this.<>4__this.DoReconnect();
                                    }
                                    catch
                                    {
                                        goto TR_0006;
                                    }
                                }
                                goto TR_0007;
                            TR_0006:
                                throw this.<>4__this.WrapException(exception, this.<command>5__1);
                            }
                        TR_0007:
                            num2 = this.<tryCount>5__2 + 1;
                            this.<tryCount>5__2 = num2;
                        }
                        return;
                    TR_000F:
                        Trace.WriteLineIf(ConnectionProviderSql.xpoSwitch.TraceInfo, new ConnectionProviderSql.SelectStatementResultTracer(this.targets, this.<result>5__3));
                        SelectStatementResult[] result = this.<result>5__3;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            this.<>4__this.ReleasePooledCommand(this.<command>5__1);
                        }
                    }
                }
                catch (Exception exception2)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception2);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        protected class DbCommandTracer
        {
            private readonly IDbCommand command;

            public DbCommandTracer(IDbCommand command)
            {
                this.command = command;
            }

            public override string ToString()
            {
                string parametersString = ConnectionProviderSql.GetParametersString(this.command);
                return string.Format(!string.IsNullOrEmpty(parametersString) ? "{0:dd.MM.yy HH:mm:ss.fff} Executing sql '{1}' with parameters {2}" : "{0:dd.MM.yy HH:mm:ss.fff} Executing sql '{1}'", DateTime.Now, this.command.CommandText.Replace('\n', ' '), parametersString);
            }
        }

        private class PreparedCommandInfo
        {
            private readonly IDbCommand command;
            private int acquireCount;
            private bool isPrepared;
            private bool isAcquired;
            private bool isClosed;
            private DateTime lastAscuiredAt;

            public PreparedCommandInfo(IDbCommand command)
            {
                this.command = command;
            }

            public IDbCommand CloseCommand()
            {
                if (this.isClosed)
                {
                    return null;
                }
                this.isClosed = true;
                if (!this.isAcquired)
                {
                    this.command.Dispose();
                }
                return this.command;
            }

            public void Prepare(ConnectionProviderSql connectionProvider)
            {
                if (!this.isPrepared && !this.isClosed)
                {
                    this.isPrepared = true;
                    connectionProvider.PreparePooledCommand(this.command);
                }
            }

            public bool TryAcquireCommand(out IDbCommand command)
            {
                if (this.isAcquired || this.isClosed)
                {
                    command = null;
                    return false;
                }
                this.acquireCount++;
                this.lastAscuiredAt = DateTime.UtcNow;
                this.isAcquired = true;
                command = this.command;
                return true;
            }

            public bool TryReleaseCommand(IDbCommand command)
            {
                if (this.isClosed || !ReferenceEquals(this.command, command))
                {
                    return false;
                }
                this.isAcquired = false;
                return true;
            }

            public bool IsPrepared =>
                this.isPrepared;

            public bool IsAcquired =>
                this.isAcquired;

            public bool IsClosed =>
                this.isClosed;

            public DateTime LastAscuiredAt =>
                this.lastAscuiredAt;

            public int AcquireCount =>
                this.acquireCount;
        }

        private class PreparedCommandQueryComparer : IEqualityComparer<Query>
        {
            private static bool AreEqualsLists(IList xList, IList yList)
            {
                if (!ReferenceEquals(xList, yList))
                {
                    if ((xList == null) || ((yList == null) || (xList.Count != yList.Count)))
                    {
                        return false;
                    }
                    int count = xList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (!Equals(xList[i], yList[i]))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }

            public bool Equals(Query x, Query y) => 
                !ReferenceEquals(x, y) ? ((x != null) && ((y != null) && ((x.Sql == y.Sql) && ((x.TopSelectedRecords == y.TopSelectedRecords) && ((x.SkipSelectedRecords == y.SkipSelectedRecords) && AreEqualsLists(x.ParametersNames, y.ParametersNames)))))) : true;

            public int GetHashCode(Query obj)
            {
                int hashState = HashCodeHelper.StartGeneric<string, int, int>(obj.Sql, obj.TopSelectedRecords, obj.SkipSelectedRecords);
                return ((obj.ParametersNames == null) ? hashState : HashCodeHelper.FinishGenericList<string>(hashState, obj.Parameters.OfType<string>()));
            }
        }

        protected class ReformatReadValueArgs
        {
            public Type DbType = null;
            public TypeCode DbTypeCode = TypeCode.Empty;
            public readonly Type TargetType;
            public readonly TypeCode TargetTypeCode;

            public ReformatReadValueArgs(Type targetType)
            {
                this.TargetType = targetType;
                this.TargetTypeCode = DXTypeExtensions.GetTypeCode(targetType);
            }

            public void AttachValueReadFromDb(object dbData)
            {
                if (this.DbType == null)
                {
                    this.DbType = dbData.GetType();
                    this.DbTypeCode = DXTypeExtensions.GetTypeCode(this.DbType);
                }
            }
        }

        protected class SelectStatementResultTracer
        {
            private SelectStatementResult[] results;
            private CriteriaOperatorCollection targets;

            public SelectStatementResultTracer(CriteriaOperatorCollection targets, SelectStatementResult[] results)
            {
                this.results = results;
                this.targets = targets;
            }

            private int GetValueSize(object value)
            {
                if (value == null)
                {
                    return 4;
                }
                switch (DXTypeExtensions.GetTypeCode(value.GetType()))
                {
                    case TypeCode.Object:
                        return (!(value is byte[]) ? (!(value is Guid) ? 4 : 0x10) : (((byte[]) value).Length + 4));

                    case TypeCode.Boolean:
                        return 1;

                    case TypeCode.Char:
                        return 2;

                    case TypeCode.Byte:
                        return 1;

                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                        return 2;

                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Single:
                        return 4;

                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                        return 8;

                    case TypeCode.Double:
                        return 8;

                    case TypeCode.Decimal:
                        return 10;

                    case TypeCode.DateTime:
                        return 8;

                    case TypeCode.String:
                        return ((((string) value).Length * 2) + 4);
                }
                return 0;
            }

            public override unsafe string ToString()
            {
                SelectStatementResult result = (this.results.Length == 2) ? this.results[1] : this.results[0];
                int length = result.Rows.Length;
                if (length <= 0)
                {
                    return (DateTime.Now.ToString("dd.MM.yy HH:mm:ss.fff ") + "Result: Empty");
                }
                if (this.targets == null)
                {
                    int num7 = 0;
                    int num8 = 0;
                    while (num8 < length)
                    {
                        SelectStatementResultRow row2 = result.Rows[num8];
                        int num9 = 0;
                        while (true)
                        {
                            if (num9 >= row2.Values.Length)
                            {
                                num8++;
                                break;
                            }
                            num7 += this.GetValueSize(row2.Values[num9]);
                            num9++;
                        }
                    }
                    return string.Format("{2}Result: rowcount = {0}, size = {1}", length, num7, DateTime.Now.ToString("dd.MM.yy HH:mm:ss.fff "));
                }
                int num2 = result.Rows[0].Values.Length;
                int[] numArray = new int[num2];
                int index = 0;
                while (index < length)
                {
                    SelectStatementResultRow row = result.Rows[index];
                    int num5 = 0;
                    while (true)
                    {
                        if (num5 >= row.Values.Length)
                        {
                            index++;
                            break;
                        }
                        int* numPtr1 = &(numArray[num5]);
                        numPtr1[0] += this.GetValueSize(row.Values[num5]);
                        num5++;
                    }
                }
                int num3 = 0;
                StringBuilder builder = new StringBuilder(num2 * 6);
                for (int i = 0; i < num2; i++)
                {
                    object[] args = new object[] { this.targets[i], numArray[i] };
                    builder.AppendFormat(CultureInfo.InvariantCulture, (i == 0) ? "{0} = {1}" : ", {0} = {1}", args);
                    num3 += numArray[i];
                }
                return string.Format("{3}Result: rowcount = {0}, total = {1}, {2}", new object[] { length, num3, builder, DateTime.Now.ToString("dd.MM.yy HH:mm:ss.fff ") });
            }
        }
    }
}

