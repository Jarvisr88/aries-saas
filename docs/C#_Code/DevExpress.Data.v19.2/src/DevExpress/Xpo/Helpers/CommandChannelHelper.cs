namespace DevExpress.Xpo.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils;
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    public class CommandChannelHelper
    {
        public const string Message_CommandIsNotSupported = "Command '{0}' is not supported.";
        public const string Message_CommandIsNotSupportedEx = "Command '{0}' is not supported by {1}.";
        public const string Message_CommandWrongParameterSet = "Wrong parameter set for command '{0}'.";
        public const string Command_ExplicitBeginTransaction = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitBeginTransaction";
        public const string Command_ExplicitCommitTransaction = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitCommitTransaction";
        public const string Command_ExplicitRollbackTransaction = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitRollbackTransaction";
        public const string Command_ExecuteStoredProcedure = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedure";
        public const string Command_ExecuteStoredProcedureParametrized = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedureParametrized";
        public const string Command_ExecuteNonQuerySQL = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteNonQuerySQL";
        public const string Command_ExecuteNonQuerySQLWithParams = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteNonQuerySQLWithParams";
        public const string Command_ExecuteScalarSQL = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteScalarSQL";
        public const string Command_ExecuteScalarSQLWithParams = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteScalarSQLWithParams";
        public const string Command_ExecuteQuerySQL = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQL";
        public const string Command_ExecuteQuerySQLWithParams = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithParams";
        public const string Command_ExecuteQuerySQLWithMetadata = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithMetadata";
        public const string Command_ExecuteQuerySQLWithMetadataWithParams = "DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithMetadataWithParams";
        public static bool EnableMonoDeserializationFixForSmallAmountsOfData;

        private static T DeserializeXmlNodes<T>(System.Xml.XmlNode[] nodes)
        {
            Type type = typeof(T);
            XmlSerializer serializer = new XmlSerializer(type);
            XmlDocument document = SafeXml.CreateDocument(null);
            string namespaceURI = null;
            object[] customAttributes = type.GetCustomAttributes(typeof(XmlRootAttribute), true);
            int index = 0;
            while (true)
            {
                if (index < customAttributes.Length)
                {
                    XmlRootAttribute attribute = (XmlRootAttribute) customAttributes[index];
                    if (string.IsNullOrEmpty(attribute.Namespace))
                    {
                        index++;
                        continue;
                    }
                    namespaceURI = attribute.Namespace;
                }
                document.AppendChild(document.CreateElement(type.Name, namespaceURI));
                HashSet<string> set = new HashSet<string>();
                foreach (System.Xml.XmlNode node in nodes)
                {
                    if (node != null)
                    {
                        System.Xml.XmlNode newChild = document.ImportNode(node, true);
                        if (newChild is System.Xml.XmlAttribute)
                        {
                            document.DocumentElement.Attributes.Append((System.Xml.XmlAttribute) newChild);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(newChild.NamespaceURI))
                            {
                                set.Add(newChild.NamespaceURI);
                            }
                            document.DocumentElement.AppendChild(newChild);
                        }
                    }
                }
                string outerXml = document.OuterXml;
                foreach (string str3 in set)
                {
                    outerXml = outerXml.Replace("xmlns=\"" + str3 + "\"", "");
                }
                using (StringReader reader = new StringReader(outerXml))
                {
                    return (T) serializer.Deserialize(reader);
                }
            }
        }

        public static int ExecuteNonQuery(ICommandChannel commandChannel, string sql) => 
            (int) commandChannel.Do("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteNonQuerySQL", sql);

        [AsyncStateMachine(typeof(<ExecuteNonQueryAsync>d__24))]
        public static Task<int> ExecuteNonQueryAsync(ICommandChannelAsync commandChannel, string sql, CancellationToken cancellationToken)
        {
            <ExecuteNonQueryAsync>d__24 d__;
            d__.commandChannel = commandChannel;
            d__.sql = sql;
            d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteNonQueryAsync>d__24>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static int ExecuteNonQueryWithParams(ICommandChannel commandChannel, string sqlCommand, QueryParameterCollection parameters, string[] parametersNames) => 
            (int) commandChannel.Do("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteNonQuerySQLWithParams", new SqlQuery(sqlCommand, parameters, parametersNames));

        [AsyncStateMachine(typeof(<ExecuteNonQueryWithParamsAsync>d__26))]
        public static Task<int> ExecuteNonQueryWithParamsAsync(ICommandChannelAsync commandChannel, string sqlCommand, QueryParameterCollection parameters, string[] parametersNames, CancellationToken cancellationToken)
        {
            <ExecuteNonQueryWithParamsAsync>d__26 d__;
            d__.commandChannel = commandChannel;
            d__.sqlCommand = sqlCommand;
            d__.parameters = parameters;
            d__.parametersNames = parametersNames;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteNonQueryWithParamsAsync>d__26>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static SelectedData ExecuteQuery(ICommandChannel commandChannel, string sql) => 
            ConnectionProviderSql.FixDBNull(TryFixMonoDeserialization<SelectedData>(commandChannel.Do("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQL", sql), "ExecuteQuery"));

        [AsyncStateMachine(typeof(<ExecuteQueryAsync>d__34))]
        public static Task<SelectedData> ExecuteQueryAsync(ICommandChannelAsync commandChannel, string sql, CancellationToken cancellationToken)
        {
            <ExecuteQueryAsync>d__34 d__;
            d__.commandChannel = commandChannel;
            d__.sql = sql;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteQueryAsync>d__34>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static SelectedData ExecuteQueryWithMetadata(ICommandChannel commandChannel, string sql) => 
            ConnectionProviderSql.FixDBNull(TryFixMonoDeserialization<SelectedData>(commandChannel.Do("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithMetadata", sql), "ExecuteQueryWithMetadata"));

        [AsyncStateMachine(typeof(<ExecuteQueryWithMetadataAsync>d__38))]
        public static Task<SelectedData> ExecuteQueryWithMetadataAsync(ICommandChannelAsync commandChannel, string sql, CancellationToken cancellationToken)
        {
            <ExecuteQueryWithMetadataAsync>d__38 d__;
            d__.commandChannel = commandChannel;
            d__.sql = sql;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteQueryWithMetadataAsync>d__38>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static SelectedData ExecuteQueryWithMetadataWithParams(ICommandChannel commandChannel, string sqlCommand, QueryParameterCollection parameters, string[] parametersNames) => 
            ConnectionProviderSql.FixDBNull(TryFixMonoDeserialization<SelectedData>(commandChannel.Do("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithMetadataWithParams", new SqlQuery(sqlCommand, parameters, parametersNames)), "ExecuteQueryWithMetadataWithParams"));

        [AsyncStateMachine(typeof(<ExecuteQueryWithMetadataWithParamsAsync>d__40))]
        public static Task<SelectedData> ExecuteQueryWithMetadataWithParamsAsync(ICommandChannelAsync commandChannel, string sqlCommand, QueryParameterCollection parameters, string[] parametersNames, CancellationToken cancellationToken)
        {
            <ExecuteQueryWithMetadataWithParamsAsync>d__40 d__;
            d__.commandChannel = commandChannel;
            d__.sqlCommand = sqlCommand;
            d__.parameters = parameters;
            d__.parametersNames = parametersNames;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteQueryWithMetadataWithParamsAsync>d__40>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static SelectedData ExecuteQueryWithParams(ICommandChannel commandChannel, string sqlCommand, QueryParameterCollection parameters, string[] parametersNames) => 
            ConnectionProviderSql.FixDBNull(TryFixMonoDeserialization<SelectedData>(commandChannel.Do("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithParams", new SqlQuery(sqlCommand, parameters, parametersNames)), "ExecuteQueryWithParams"));

        [AsyncStateMachine(typeof(<ExecuteQueryWithParamsAsync>d__36))]
        public static Task<SelectedData> ExecuteQueryWithParamsAsync(ICommandChannelAsync commandChannel, string sqlCommand, QueryParameterCollection parameters, string[] parametersNames, CancellationToken cancellationToken)
        {
            <ExecuteQueryWithParamsAsync>d__36 d__;
            d__.commandChannel = commandChannel;
            d__.sqlCommand = sqlCommand;
            d__.parameters = parameters;
            d__.parametersNames = parametersNames;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteQueryWithParamsAsync>d__36>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static object ExecuteScalar(ICommandChannel commandChannel, string sql) => 
            commandChannel.Do("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteScalarSQL", sql);

        public static Task<object> ExecuteScalarAsync(ICommandChannelAsync commandChannel, string sql, CancellationToken cancellationToken) => 
            commandChannel.DoAsync("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteScalarSQL", sql, cancellationToken);

        public static object ExecuteScalarWithParams(ICommandChannel commandChannel, string sqlCommand, QueryParameterCollection parameters, string[] parametersNames) => 
            commandChannel.Do("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteScalarSQLWithParams", new SqlQuery(sqlCommand, parameters, parametersNames));

        public static Task<object> ExecuteScalarWithParamsAsync(ICommandChannelAsync commandChannel, string sqlCommand, QueryParameterCollection parameters, string[] parametersNames, CancellationToken cancellationToken) => 
            commandChannel.DoAsync("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteScalarSQLWithParams", new SqlQuery(sqlCommand, parameters, parametersNames), cancellationToken);

        public static SelectedData ExecuteSproc(ICommandChannel commandChannel, string sprocName, params OperandValue[] parameters) => 
            ConnectionProviderSql.FixDBNull(TryFixMonoDeserialization<SelectedData>(commandChannel.Do("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedure", new SprocQuery(sprocName, parameters)), "ExecuteSproc"));

        [AsyncStateMachine(typeof(<ExecuteSprocAsync>d__19))]
        public static Task<SelectedData> ExecuteSprocAsync(ICommandChannelAsync commandChannel, CancellationToken cancellationToken, string sprocName, params OperandValue[] parameters)
        {
            <ExecuteSprocAsync>d__19 d__;
            d__.commandChannel = commandChannel;
            d__.cancellationToken = cancellationToken;
            d__.sprocName = sprocName;
            d__.parameters = parameters;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteSprocAsync>d__19>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static SelectedData ExecuteSprocParametrized(ICommandChannel commandChannel, string sprocName, params SprocParameter[] parameters) => 
            ConnectionProviderSql.FixDBNull(TryFixMonoDeserialization<SelectedData>(commandChannel.Do("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedureParametrized", new SprocQuery(sprocName, parameters)), "ExecuteSprocParametrized"));

        [AsyncStateMachine(typeof(<ExecuteSprocParametrizedAsync>d__21))]
        public static Task<SelectedData> ExecuteSprocParametrizedAsync(ICommandChannelAsync commandChannel, CancellationToken cancellationToken, string sprocName, params SprocParameter[] parameters)
        {
            <ExecuteSprocParametrizedAsync>d__21 d__;
            d__.commandChannel = commandChannel;
            d__.sprocName = sprocName;
            d__.parameters = parameters;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteSprocParametrizedAsync>d__21>(ref d__);
            return d__.<>t__builder.Task;
        }

        private static T TryFixMonoDeserialization<T>(object res, [CallerMemberName] string operationName = "ICommandChannel.Do")
        {
            System.Xml.XmlNode[] nodes = res as System.Xml.XmlNode[];
            if (nodes == null)
            {
                return (T) res;
            }
            if (EnableMonoDeserializationFixForSmallAmountsOfData)
            {
                return DeserializeXmlNodes<T>(nodes);
            }
            object[] args = new object[] { operationName };
            throw new InvalidOperationException(DbRes.GetString("CommandChannelHelper_CannotDeserializeResponse", args));
        }

        [CompilerGenerated]
        private struct <ExecuteNonQueryAsync>d__24 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<int> <>t__builder;
            public ICommandChannelAsync commandChannel;
            public string sql;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
                    object obj3;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        CancellationToken cancellationToken = new CancellationToken();
                        awaiter = this.commandChannel.DoAsync("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteNonQuerySQL", this.sql, cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, CommandChannelHelper.<ExecuteNonQueryAsync>d__24>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    obj3 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                    int result = (int) obj3;
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
        private struct <ExecuteNonQueryWithParamsAsync>d__26 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<int> <>t__builder;
            public ICommandChannelAsync commandChannel;
            public string sqlCommand;
            public QueryParameterCollection parameters;
            public string[] parametersNames;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
                    object obj3;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = this.commandChannel.DoAsync("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteNonQuerySQLWithParams", new CommandChannelHelper.SqlQuery(this.sqlCommand, this.parameters, this.parametersNames), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, CommandChannelHelper.<ExecuteNonQueryWithParamsAsync>d__26>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    obj3 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                    int result = (int) obj3;
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
        private struct <ExecuteQueryAsync>d__34 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public ICommandChannelAsync commandChannel;
            public string sql;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
                    object obj3;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = this.commandChannel.DoAsync("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQL", this.sql, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, CommandChannelHelper.<ExecuteQueryAsync>d__34>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    obj3 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                    SelectedData result = ConnectionProviderSql.FixDBNull(CommandChannelHelper.TryFixMonoDeserialization<SelectedData>(obj3, "ExecuteQueryAsync"));
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
        private struct <ExecuteQueryWithMetadataAsync>d__38 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public ICommandChannelAsync commandChannel;
            public string sql;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
                    object obj3;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = this.commandChannel.DoAsync("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithMetadata", this.sql, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, CommandChannelHelper.<ExecuteQueryWithMetadataAsync>d__38>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    obj3 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                    SelectedData result = ConnectionProviderSql.FixDBNull(CommandChannelHelper.TryFixMonoDeserialization<SelectedData>(obj3, "ExecuteQueryWithMetadataAsync"));
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
        private struct <ExecuteQueryWithMetadataWithParamsAsync>d__40 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public ICommandChannelAsync commandChannel;
            public string sqlCommand;
            public QueryParameterCollection parameters;
            public string[] parametersNames;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
                    object obj3;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = this.commandChannel.DoAsync("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithMetadataWithParams", new CommandChannelHelper.SqlQuery(this.sqlCommand, this.parameters, this.parametersNames), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, CommandChannelHelper.<ExecuteQueryWithMetadataWithParamsAsync>d__40>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    obj3 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                    SelectedData result = ConnectionProviderSql.FixDBNull(CommandChannelHelper.TryFixMonoDeserialization<SelectedData>(obj3, "ExecuteQueryWithMetadataWithParamsAsync"));
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
        private struct <ExecuteQueryWithParamsAsync>d__36 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public ICommandChannelAsync commandChannel;
            public string sqlCommand;
            public QueryParameterCollection parameters;
            public string[] parametersNames;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
                    object obj3;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = this.commandChannel.DoAsync("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteQuerySQLWithParams", new CommandChannelHelper.SqlQuery(this.sqlCommand, this.parameters, this.parametersNames), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, CommandChannelHelper.<ExecuteQueryWithParamsAsync>d__36>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    obj3 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                    SelectedData result = ConnectionProviderSql.FixDBNull(CommandChannelHelper.TryFixMonoDeserialization<SelectedData>(obj3, "ExecuteQueryWithParamsAsync"));
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
        private struct <ExecuteSprocAsync>d__19 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public ICommandChannelAsync commandChannel;
            public string sprocName;
            public OperandValue[] parameters;
            public CancellationToken cancellationToken;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
                    object obj3;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = this.commandChannel.DoAsync("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedure", new CommandChannelHelper.SprocQuery(this.sprocName, this.parameters), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, CommandChannelHelper.<ExecuteSprocAsync>d__19>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    obj3 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                    SelectedData result = ConnectionProviderSql.FixDBNull(CommandChannelHelper.TryFixMonoDeserialization<SelectedData>(obj3, "ExecuteSprocAsync"));
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
        private struct <ExecuteSprocParametrizedAsync>d__21 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public ICommandChannelAsync commandChannel;
            public string sprocName;
            public SprocParameter[] parameters;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
                    object obj3;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        CancellationToken cancellationToken = new CancellationToken();
                        awaiter = this.commandChannel.DoAsync("DevExpress.Xpo.Helpers.CommandChannelHelper.ExecuteStoredProcedureParametrized", new CommandChannelHelper.SprocQuery(this.sprocName, this.parameters), cancellationToken).ConfigureAwait(false).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, CommandChannelHelper.<ExecuteSprocParametrizedAsync>d__21>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    obj3 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                    SelectedData result = ConnectionProviderSql.FixDBNull(CommandChannelHelper.TryFixMonoDeserialization<SelectedData>(obj3, "ExecuteSprocParametrizedAsync"));
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

        [Serializable]
        public class SprocQuery
        {
            [XmlAttribute]
            public string SprocName;
            [XmlElement(typeof(OperandValue)), XmlElement(typeof(ConstantValue)), XmlElement(typeof(ParameterValue)), XmlElement(typeof(OperandParameter)), XmlElement(typeof(SprocParameter))]
            public OperandValue[] Parameters;

            public SprocQuery()
            {
            }

            public SprocQuery(string sprocName, OperandValue[] parameters)
            {
                this.SprocName = sprocName;
                this.Parameters = parameters;
            }
        }

        [Serializable]
        public class SqlQuery
        {
            [XmlAttribute]
            public string SqlCommand;
            public QueryParameterCollection Parameters;
            public string[] ParametersNames;

            public SqlQuery()
            {
            }

            public SqlQuery(string sqlCommand, QueryParameterCollection parameters, string[] parametersNames)
            {
                this.SqlCommand = sqlCommand;
                this.Parameters = parameters;
                this.ParametersNames = parametersNames;
            }
        }
    }
}

