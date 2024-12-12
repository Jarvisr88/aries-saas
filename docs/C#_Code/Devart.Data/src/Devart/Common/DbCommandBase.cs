namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;

    public abstract class DbCommandBase : DbCommand
    {
        private string a;
        private int b;
        private System.Data.CommandType c;
        private UpdateRowSource d;
        private bool e;
        private bool f;
        private IDisposable g;
        private string h;
        protected WeakReference weakDataReader;
        public const int DefaultCommandTimeout = 30;
        private Devart.Common.DbCommandBase.a i;
        private string j;
        private object k;

        protected DbCommandBase()
        {
            this.b = 30;
            this.d = UpdateRowSource.Both;
        }

        protected DbCommandBase(DbCommandBase from)
        {
            this.a = from.a;
            this.c = from.c;
            this.b = from.b;
            this.d = from.d;
            this.e = from.e;
        }

        internal string a() => 
            this.CreateSql();

        private object a(CommandBehavior A_0) => 
            this.ExecuteNonQuery();

        protected abstract void AddCommand();
        protected abstract void AddDataReader(DbDataReader reader);
        private object AsyncExecuteReader(CommandBehavior behavior) => 
            base.ExecuteReader(behavior);

        internal void b()
        {
            this.Unprepare();
        }

        public IAsyncResult BeginExecuteNonQuery() => 
            this.BeginExecuteNonQuery(null, null);

        public IAsyncResult BeginExecuteNonQuery(AsyncCallback callback, object stateObject)
        {
            if (this.i != null)
            {
                throw new InvalidOperationException();
            }
            this.i = new Devart.Common.DbCommandBase.a(this.a);
            return this.i.BeginInvoke(CommandBehavior.Default, callback, stateObject);
        }

        public IAsyncResult BeginExecuteReader() => 
            this.BeginExecuteReader(null, null, CommandBehavior.Default);

        public IAsyncResult BeginExecuteReader(CommandBehavior behavior) => 
            this.BeginExecuteReader(null, null, behavior);

        public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject) => 
            this.BeginExecuteReader(callback, stateObject, CommandBehavior.Default);

        public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject, CommandBehavior behavior)
        {
            if (this.i != null)
            {
                throw new InvalidOperationException(Devart.Common.g.a("ExecutionInProgress"));
            }
            this.i = new Devart.Common.DbCommandBase.a(this.AsyncExecuteReader);
            return this.i.BeginInvoke(behavior, callback, stateObject);
        }

        public override void Cancel()
        {
        }

        protected abstract void ClearParameters();
        protected internal void CreateParameters()
        {
            System.Data.CommandType commandType = this.CommandType;
            if (commandType == System.Data.CommandType.Text)
            {
                this.ParseSqlParameters(this.CommandText);
            }
            else if (commandType == System.Data.CommandType.StoredProcedure)
            {
                if (Utils.IsEmpty(this.CommandText))
                {
                    throw new InvalidOperationException(Devart.Common.g.a("ProcNameNotDef"));
                }
                this.DescribeProcedure(this.CommandText);
            }
            else if (commandType == System.Data.CommandType.TableDirect)
            {
                if (Utils.IsEmpty(this.CommandText))
                {
                    throw new InvalidOperationException(Devart.Common.g.a("TableNameNotDef"));
                }
                this.ClearParameters();
            }
        }

        protected virtual string CreateSql()
        {
            System.Data.CommandType commandType = this.CommandType;
            if (commandType == System.Data.CommandType.Text)
            {
                return this.CommandText;
            }
            if (commandType == System.Data.CommandType.StoredProcedure)
            {
                return this.CreateStoredProcSql(this.CommandText);
            }
            if (commandType != System.Data.CommandType.TableDirect)
            {
                throw new InvalidOperationException(Devart.Common.g.a("InvalidCommandType"));
            }
            return ("SELECT * FROM " + this.CommandText);
        }

        protected abstract string CreateStoredProcSql(string name);
        protected abstract void DescribeProcedure(string name);
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Unprepare();
            }
            base.Dispose(disposing);
            if (!base.DesignMode && (this.Owner != null))
            {
                GlobalComponentsCache.RemoveFromGlobalList(this);
            }
        }

        public int EndExecuteNonQuery(IAsyncResult result)
        {
            int num;
            if (this.i == null)
            {
                throw new InvalidOperationException();
            }
            try
            {
                num = (int) this.i.EndInvoke(result);
            }
            finally
            {
                this.i = null;
            }
            return num;
        }

        public DbDataReader EndExecuteReader(IAsyncResult result)
        {
            DbDataReader reader;
            Utils.CheckArgumentNull(result, "result");
            try
            {
                reader = (DbDataReader) this.i.EndInvoke(result);
            }
            finally
            {
                this.i = null;
            }
            return reader;
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior) => 
            this.ExecuteDbDataReader(behavior, false);

        protected DbDataReader ExecuteDbDataReader(CommandBehavior behavior, bool nonQuery)
        {
            Utils.CheckConnectionOpen(base.Connection);
            if (this.HasOpenReader)
            {
                throw new InvalidOperationException(Devart.Common.g.a("ReaderNotClosed"));
            }
            IDisposable g = this.g;
            bool flag = true;
            ILocalFailoverManager manager = this.LocalFailoverManager.StartUse(true);
            while (true)
            {
                try
                {
                    while (true)
                    {
                        bool flag2 = false;
                        try
                        {
                            DbDataReader reader;
                            DbDataReader reader2;
                            if (g != null)
                            {
                                if (flag)
                                {
                                    flag = false;
                                    this.UseLoadBalancing(true);
                                }
                                reader = this.InternalExecute(behavior, g, 0, 0, nonQuery);
                                flag2 = this.IsReadOnlyOperation(g);
                                Utils.SetWeakTarget(ref this.weakDataReader, reader);
                                this.AddDataReader(reader);
                                reader2 = reader;
                            }
                            else
                            {
                                if (flag)
                                {
                                    flag = false;
                                    this.UseLoadBalancing(false);
                                }
                                g = this.InternalPrepare(true, 0, 0);
                                flag2 = this.IsReadOnlyOperation(g);
                                try
                                {
                                    reader = this.InternalExecute(behavior, g, 0, 0, nonQuery);
                                    this.AddDataReader(reader);
                                    reader2 = reader;
                                }
                                catch
                                {
                                    try
                                    {
                                        g.Dispose();
                                    }
                                    catch
                                    {
                                    }
                                    g = null;
                                    throw;
                                }
                                finally
                                {
                                    if (g != null)
                                    {
                                        g.Dispose();
                                    }
                                }
                            }
                            return reader2;
                        }
                        catch (Exception exception)
                        {
                            if (this.LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.Execute, flag2 ? RetryMode.Reexecute : RetryMode.Raise, exception) == RetryMode.Raise)
                            {
                                throw;
                            }
                        }
                        break;
                    }
                }
                finally
                {
                    if (manager != null)
                    {
                        manager.Dispose();
                    }
                }
            }
        }

        public override int ExecuteNonQuery()
        {
            using (IDataReader reader = base.ExecuteReader())
            {
                reader.Close();
                return reader.RecordsAffected;
            }
        }

        public DbDataReader ExecutePageReader(CommandBehavior behavior, int startRecord, int maxRecords) => 
            this.ExecutePageReaderInternal(behavior, startRecord, maxRecords);

        protected virtual DbDataReader ExecutePageReaderInternal(CommandBehavior behavior, int startRecord, int maxRecords)
        {
            Utils.CheckConnectionOpen(base.Connection);
            ILocalFailoverManager manager = this.LocalFailoverManager.StartUse(true);
            while (true)
            {
                try
                {
                    while (true)
                    {
                        try
                        {
                            using (IDisposable disposable = this.InternalPrepare(true, startRecord, maxRecords))
                            {
                                DbDataReader reader = this.InternalExecute(behavior, disposable, startRecord, maxRecords);
                                this.AddDataReader(reader);
                                return reader;
                            }
                        }
                        catch (Exception exception)
                        {
                            if (this.LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.Execute, RetryMode.Reexecute, exception) == RetryMode.Raise)
                            {
                                throw;
                            }
                        }
                        break;
                    }
                }
                finally
                {
                    if (manager != null)
                    {
                        manager.Dispose();
                    }
                }
            }
        }

        public override object ExecuteScalar()
        {
            using (IDataReader reader = base.ExecuteReader())
            {
                bool flag = false;
                while (true)
                {
                    if (reader.FieldCount > 0)
                    {
                        flag = true;
                    }
                    else if (reader.NextResult())
                    {
                        continue;
                    }
                    if (!flag || !reader.Read())
                    {
                        break;
                    }
                    return reader.GetValue(0);
                }
            }
            return null;
        }

        public int GetRecordCount()
        {
            int num2;
            try
            {
                using (DbCommand command = base.Connection.CreateCommand())
                {
                    command.CommandTimeout = this.CommandTimeout;
                    command.CommandText = this.GetRecordCountSql(this.a());
                    command.Parameters.Clear();
                    int num = 0;
                    while (true)
                    {
                        DbParameterBase base2;
                        if (num >= base.Parameters.Count)
                        {
                            num2 = Convert.ToInt32(command.ExecuteScalar());
                            break;
                        }
                        ICloneable cloneable = base.Parameters[num] as ICloneable;
                        if (cloneable != null)
                        {
                            base2 = (DbParameterBase) cloneable.Clone();
                        }
                        else
                        {
                            base2 = (DbParameterBase) command.CreateParameter();
                            ((DbParameterBase) base.Parameters[num]).CopyTo(base2);
                        }
                        command.Parameters.Add(base2);
                        num++;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new QueryRecordCountException(Devart.Common.g.a("ErrorRetrievingRecordCount"), exception);
            }
            return num2;
        }

        protected virtual string GetRecordCountSql(string commandText) => 
            "select count(*) as record_count from (" + commandText + "\r\n) record_count_table";

        protected abstract DbDataReader InternalExecute(CommandBehavior behavior, IDisposable stmt, int startRecord, int maxRecords);
        protected virtual DbDataReader InternalExecute(CommandBehavior behavior, IDisposable stmt, int startRecord, int maxRecords, bool nonQuery) => 
            this.InternalExecute(behavior, stmt, startRecord, maxRecords);

        protected abstract IDisposable InternalPrepare(bool implicitPrepare, int startRecord, int maxRecords);
        protected virtual bool IsReadOnlyOperation(IDisposable stmt) => 
            false;

        protected abstract void ParseSqlParameters(string sql);
        public override void Prepare()
        {
            Utils.CheckConnectionOpen(base.Connection);
            ILocalFailoverManager manager = this.LocalFailoverManager.StartUse(true);
            while (true)
            {
                try
                {
                    while (true)
                    {
                        try
                        {
                            if (!this.IsPrepared)
                            {
                                this.g = this.InternalPrepare(false, 0, 0);
                                this.AddCommand();
                            }
                            return;
                        }
                        catch (Exception exception)
                        {
                            if (this.LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.Prepare, RetryMode.Reexecute, exception) == RetryMode.Raise)
                            {
                                throw;
                            }
                        }
                        break;
                    }
                }
                finally
                {
                    if (manager != null)
                    {
                        manager.Dispose();
                    }
                }
            }
        }

        protected virtual void PropertyChanging()
        {
            this.Unprepare();
        }

        protected abstract void RemoveCommand();
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual void ResetCommandTimeout()
        {
            this.b = 30;
        }

        protected void SaveParameter(DbParameter result)
        {
            if (Devart.Common.DbCommandBuilder.b != null)
            {
                Devart.Common.DbCommandBuilder.b.Add(this);
                Devart.Common.DbCommandBuilder.b.Add(result);
            }
        }

        protected internal void SetParameterCheck(bool parameterCheck)
        {
            this.f = parameterCheck;
        }

        protected virtual bool ShouldSerializeCommandTimeout() => 
            this.b != 30;

        protected virtual void Unprepare()
        {
            if (this.g != null)
            {
                if (base.Connection != null)
                {
                    this.RemoveCommand();
                }
                this.g.Dispose();
                this.g = null;
            }
            this.weakDataReader = null;
            this.h = null;
        }

        protected internal void UpdateParameters()
        {
            if (this.f)
            {
                System.Data.CommandType commandType = this.CommandType;
                if (commandType == System.Data.CommandType.Text)
                {
                    this.ParseSqlParameters(this.CommandText);
                }
                else if (commandType == System.Data.CommandType.TableDirect)
                {
                    this.ClearParameters();
                }
            }
        }

        protected virtual void UseLoadBalancing(bool ignoreBalancing)
        {
        }

        [DefaultValue(""), y("DbCommand_CommandText"), MergableProperty(false), RefreshProperties(RefreshProperties.All), Category("Data")]
        public override string CommandText
        {
            get
            {
                string a = this.a;
                return ((a != null) ? a : string.Empty);
            }
            set
            {
                if (this.a != value)
                {
                    this.PropertyChanging();
                    this.a = value;
                    this.UpdateParameters();
                }
            }
        }

        [Category("Data"), y("DbCommand_CommandTimeout")]
        public override int CommandTimeout
        {
            get => 
                this.b;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(Devart.Common.g.a("InvalidCommandTimeout", value));
                }
                this.b = value;
            }
        }

        [y("DbCommand_CommandType"), RefreshProperties(RefreshProperties.All), Category("Data"), DefaultValue(1)]
        public override System.Data.CommandType CommandType
        {
            get
            {
                System.Data.CommandType c = this.c;
                return ((c != ((System.Data.CommandType) 0)) ? c : System.Data.CommandType.Text);
            }
            set
            {
                if (this.c != value)
                {
                    if ((value != System.Data.CommandType.Text) && ((value != System.Data.CommandType.StoredProcedure) && (value != System.Data.CommandType.TableDirect)))
                    {
                        throw new ArgumentException(Devart.Common.g.a("InvalidCommandType", value));
                    }
                    this.PropertyChanging();
                    this.c = value;
                    this.UpdateParameters();
                }
            }
        }

        [Browsable(false), DesignOnly(true), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(true)]
        public override bool DesignTimeVisible
        {
            get => 
                !this.e;
            set
            {
                this.e = !value;
                TypeDescriptor.Refresh(this);
            }
        }

        [DefaultValue(3), y("DbCommand_UpdatedRowSource"), Category("Update")]
        public override UpdateRowSource UpdatedRowSource
        {
            get => 
                this.d;
            set
            {
                if (this.d != value)
                {
                    if ((value != UpdateRowSource.None) && ((value != UpdateRowSource.OutputParameters) && ((value != UpdateRowSource.FirstReturnedRecord) && (value != UpdateRowSource.Both))))
                    {
                        throw new ArgumentException(Devart.Common.g.a("InvalidUpdateRowSource"));
                    }
                    this.d = value;
                }
            }
        }

        protected abstract ILocalFailoverManager LocalFailoverManager { get; }

        [Category("Behavior"), DefaultValue(false), RefreshProperties(RefreshProperties.Repaint), y("DbCommand_ParameterCheck")]
        public bool ParameterCheck
        {
            get => 
                this.f;
            set
            {
                if (value != this.f)
                {
                    this.f = value;
                    this.UpdateParameters();
                }
            }
        }

        protected internal string Sql
        {
            get
            {
                if (this.CommandText.Length == 0)
                {
                    throw new InvalidOperationException(Devart.Common.g.a("CommandTextRequired"));
                }
                this.h ??= this.CreateSql();
                return this.h;
            }
        }

        protected internal IDisposable Stmt =>
            this.g;

        protected internal bool IsPrepared =>
            this.g != null;

        protected internal bool HasOpenReader
        {
            get
            {
                DbDataReader dataReader = this.DataReader;
                return ((dataReader != null) && !dataReader.IsClosed);
            }
        }

        protected internal DbDataReader DataReader =>
            (DbDataReader) Utils.GetWeakTarget(this.weakDataReader);

        [DefaultValue(""), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string Name
        {
            get => 
                (this.Site != null) ? this.Site.Name : ((this.j == null) ? string.Empty : this.j);
            set
            {
                if (this.Site == null)
                {
                    this.j = (value == null) ? string.Empty : value;
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Owner
        {
            get => 
                this.k;
            set
            {
                if ((this.k != null) && !base.DesignMode)
                {
                    GlobalComponentsCache.RemoveFromGlobalList(this);
                }
                this.k = value;
                if ((this.k != null) && !base.DesignMode)
                {
                    GlobalComponentsCache.AddToGlobalList(this);
                }
            }
        }

        private delegate object a(CommandBehavior A_0);
    }
}

