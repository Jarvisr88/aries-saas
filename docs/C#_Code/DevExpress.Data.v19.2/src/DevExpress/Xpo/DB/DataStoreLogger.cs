namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpo.DB.Helpers;
    using DevExpress.Xpo.Helpers;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class DataStoreLogger : DataStoreSerialized, ICommandChannel, ICommandChannelAsync
    {
        private readonly TextWriter _logWriter;

        public DataStoreLogger(IDataStore nestedProvider, TextWriter logWriter) : base(nestedProvider)
        {
            this._logWriter = logWriter;
        }

        object ICommandChannel.Do(string command, object args) => 
            this.DoInternal(command, args);

        Task<object> ICommandChannelAsync.DoAsync(string command, object args, CancellationToken cancellationToken) => 
            this.DoInternalAsync(command, args, cancellationToken);

        protected virtual object DoInternal(string command, object args)
        {
            ICommandChannel nestedDataStore = base.NestedDataStore as ICommandChannel;
            if (nestedDataStore != null)
            {
                return nestedDataStore.Do(command, args);
            }
            if (base.NestedDataStore == null)
            {
                throw new NotSupportedException($"Command '{command}' is not supported.");
            }
            throw new NotSupportedException($"Command '{command}' is not supported by {base.NestedDataStore.GetType().FullName}.");
        }

        protected virtual Task<object> DoInternalAsync(string command, object args, CancellationToken cancellationToken)
        {
            ICommandChannelAsync nestedDataStore = base.NestedDataStore as ICommandChannelAsync;
            if (nestedDataStore != null)
            {
                return nestedDataStore.DoAsync(command, args, cancellationToken);
            }
            if (base.NestedDataStore == null)
            {
                throw new NotSupportedException($"Command '{command}' is not supported.");
            }
            object[] objArray1 = new object[] { nestedDataStore.GetType().FullName };
            throw new InvalidOperationException(DbRes.GetString("Async_CommandChannelDoesNotImplementICommandChannelAsync", objArray1));
        }

        protected virtual string GetDisplayValue(object obj)
        {
            if (obj == null)
            {
                return "null";
            }
            string str = obj as string;
            if (str != null)
            {
                str = str.Replace(@"\", @"\\").Replace("\n", @"\n").Replace("\r", @"\r").Replace("\t", @"\t");
                return ((str.Length >= 0x20) ? ("\"" + str.Substring(0, 0x18) + "\"...") : ("\"" + str + "\""));
            }
            if (!(obj is char))
            {
                return (!(obj is IConvertible) ? obj.ToString() : Convert.ToString(obj, CultureInfo.InvariantCulture));
            }
            char ch = (char) obj;
            return ((ch >= ' ') ? ("'" + ch.ToString() + "'") : ((ch != '\n') ? ((ch != '\r') ? ((ch != '\t') ? (@"'\" + ((int) ch).ToString() + "'") : @"'\t'") : @"'\r'") : @"'\n'"));
        }

        protected override ModificationResult ProcessModifyData(params ModificationStatement[] dmlStatements)
        {
            ModificationResult result2;
            this.LogWriter.WriteLine("{0:u} ModifyData:", DateTime.Now.ToUniversalTime());
            TaggedParametersHolder identities = new TaggedParametersHolder();
            int num = 0;
            foreach (ModificationStatement statement in dmlStatements)
            {
                this.LogWriter.WriteLine(" {0}", QueryStatementToStringFormatter.GetString(statement, identities));
                InsertStatement statement2 = statement as InsertStatement;
                if ((statement2 != null) && !statement2.IdentityParameter.ReferenceEqualsNull())
                {
                    statement2.IdentityParameter.Value = "identity " + num.ToString();
                    num++;
                }
            }
            this.LogWriter.Write(" result: ");
            try
            {
                ModificationResult result = base.ProcessModifyData(dmlStatements);
                if (result.Identities.Length == 0)
                {
                    this.LogWriter.WriteLine("Ok.");
                }
                else
                {
                    this.LogWriter.Write("Ok, identities returned: ");
                    int index = 0;
                    while (true)
                    {
                        if (index >= result.Identities.Length)
                        {
                            this.LogWriter.WriteLine();
                            break;
                        }
                        if (index > 0)
                        {
                            this.LogWriter.Write(", ");
                        }
                        this.LogWriter.Write(this.GetDisplayValue(result.Identities[index]));
                        index++;
                    }
                }
                result2 = result;
            }
            catch (Exception exception)
            {
                this.LogWriter.WriteLine("Exception:");
                this.LogWriter.WriteLine("{0}", exception);
                throw;
            }
            return result2;
        }

        protected override SelectedData ProcessSelectData(params SelectStatement[] selects)
        {
            SelectedData data2;
            this.LogWriter.Write("{0:u} SelectData request with {1} queries:", DateTime.Now.ToUniversalTime(), selects.Length);
            foreach (SelectStatement statement in selects)
            {
                this.LogWriter.WriteLine("{0} ;", QueryStatementToStringFormatter.GetString(statement));
            }
            try
            {
                SelectedData data = base.ProcessSelectData(selects);
                int index = 0;
                while (true)
                {
                    if (index >= data.ResultSet.Length)
                    {
                        data2 = data;
                        break;
                    }
                    SelectStatementResult result = data.ResultSet[index];
                    this.LogWriter.WriteLine("result[{0}] {1} rows:", index, result.Rows.Length);
                    int num3 = Math.Min(5, result.Rows.Length);
                    int num4 = 0;
                    while (true)
                    {
                        if (num4 >= num3)
                        {
                            if (num3 < result.Rows.Length)
                            {
                                this.LogWriter.WriteLine("  ...");
                            }
                            index++;
                            break;
                        }
                        SelectStatementResultRow row = result.Rows[num4];
                        int num5 = 0;
                        while (true)
                        {
                            if (num5 >= row.Values.Length)
                            {
                                this.LogWriter.WriteLine();
                                num4++;
                                break;
                            }
                            if (num5 > 0)
                            {
                                this.LogWriter.Write("\t");
                            }
                            else
                            {
                                this.LogWriter.Write(" ");
                            }
                            this.LogWriter.Write(this.GetDisplayValue(row.Values[num5]));
                            num5++;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.LogWriter.WriteLine("Exception:");
                this.LogWriter.WriteLine("{0}", exception);
                throw;
            }
            return data2;
        }

        protected override UpdateSchemaResult ProcessUpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables)
        {
            UpdateSchemaResult result2;
            string str = string.Empty;
            foreach (DBTable table in tables)
            {
                if (str.Length > 0)
                {
                    str = str + ", ";
                }
                str = str + table.Name;
            }
            this.LogWriter.Write("{0:u} UpdateSchema for tables {1}, opt: {2}, result: ", DateTime.Now.ToUniversalTime(), str, doNotCreateIfFirstTableNotExist);
            try
            {
                UpdateSchemaResult result = base.ProcessUpdateSchema(doNotCreateIfFirstTableNotExist, tables);
                this.LogWriter.WriteLine("{0}", result);
                result2 = result;
            }
            catch (Exception exception)
            {
                this.LogWriter.WriteLine("Exception:");
                this.LogWriter.WriteLine("{0}", exception);
                throw;
            }
            return result2;
        }

        public TextWriter LogWriter =>
            this._logWriter;
    }
}

