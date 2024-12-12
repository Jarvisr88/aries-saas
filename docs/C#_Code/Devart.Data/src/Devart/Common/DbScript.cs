namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public abstract class DbScript : Component
    {
        protected readonly Lexer lexer;
        private SqlStatementCollection a;
        private bool b;
        private IDbConnection c;
        private int d;
        internal uint e;
        private StreamReader f;
        private int g = 30;
        private const int h = 30;
        protected bool commandTimeoutChanged;
        private IDbCommand i;
        private string j;

        [y("DbScript_Error")]
        public event ScriptErrorEventHandler Error;

        [y("DbScript_Progress")]
        public event ScriptProgressEventHandler Progress;

        protected DbScript(Lexer lexer)
        {
            this.lexer = lexer;
        }

        private void a()
        {
            if (this.f != null)
            {
                this.f.Close();
            }
            if (this.i != null)
            {
                this.i.Dispose();
            }
        }

        internal string a(SqlStatement A_0) => 
            this.GetStatementText(A_0);

        private void a(object A_0)
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(A_0).Find("Name", false);
            if (descriptor != null)
            {
                if (Utils.IsEmpty(this.Name))
                {
                    string fullName = base.GetType().FullName;
                    char[] anyOf = new char[] { '.' };
                    int num = fullName.LastIndexOfAny(anyOf);
                    descriptor.SetValue(A_0, fullName.Substring(num + 1, (fullName.Length - num) - 1) + this.GetHashCode().ToString());
                }
                else
                {
                    descriptor.SetValue(A_0, this.Name);
                }
            }
        }

        internal IDataReader a(SqlStatement A_0, bool A_1) => 
            this.ExecuteSqlStatement(A_0, A_1);

        private bool a(bool A_0, out IDataReader A_1)
        {
            Utils.CheckConnectionOpen(this.c);
            A_1 = null;
            SqlStatement stmt = null;
            bool flag = false;
            try
            {
                if (this.a == null)
                {
                    this.GetNextStatement(out stmt);
                }
                else if (this.d < this.a.Count)
                {
                    stmt = this.a[this.d];
                    this.d++;
                }
                if (stmt != null)
                {
                    if (A_0)
                    {
                        stmt.ExecuteNonQuery();
                    }
                    else
                    {
                        A_1 = stmt.Execute();
                    }
                    flag = true;
                }
            }
            catch (Exception exception1)
            {
                string str;
                long num;
                long num2;
                int num3;
                int num4;
                SqlStatementType type;
                a(stmt, out str, out num, out num2, out num3, out num4, out type);
                ScriptErrorEventArgs e = new ScriptErrorEventArgs(exception1, str, (long) ((int) num), (long) ((int) num2), num3, num4, type);
                this.OnError(e);
                if (!e.Ignore)
                {
                    throw;
                }
                flag = true;
            }
            flag &= !this.b;
            if (flag)
            {
                this.OnProgress(stmt);
            }
            return flag;
        }

        private static void a(SqlStatement A_0, out string A_1, out long A_2, out long A_3, out int A_4, out int A_5, out SqlStatementType A_6)
        {
            A_1 = null;
            A_2 = -1L;
            A_3 = -1L;
            A_4 = -1;
            A_5 = -1;
            A_6 = ~SqlStatementType.Unknown;
            if (A_0 != null)
            {
                A_1 = A_0.Text;
                A_4 = A_0.LineNumber;
                A_5 = A_0.LinePosition;
                A_6 = A_0.StatementType;
                A_2 = (A_0.Offset >= 0) ? ((long) A_0.Offset) : ((long) (((ulong) (-2)) + A_0.Offset));
                A_3 = (A_0.Length >= 0) ? ((long) A_0.Length) : ((long) (((ulong) (-2)) + A_0.Length));
            }
        }

        public virtual void Cancel()
        {
            if (this.i != null)
            {
                this.i.Cancel();
            }
            this.CancelExecute();
        }

        protected void CancelExecute()
        {
            this.b = true;
        }

        protected virtual bool CanExecuteStatement(SqlStatement sqlStatement) => 
            sqlStatement.StatementType != SqlStatementType.Extended;

        protected virtual IDbCommand CreateCommand() => 
            this.Connection.CreateCommand();

        protected SqlStatement CreateSqlStatement(int offset, int length, int line, int position, string text, SqlStatementType statementType) => 
            new SqlStatement(this, offset, length, line, position, text, statementType);

        protected virtual SqlStatementCollection CreateStatementCollection() => 
            new SqlStatementCollection();

        protected override void Dispose(bool disposing)
        {
            this.a();
            base.Dispose(disposing);
        }

        public void Execute()
        {
            IDataReader reader;
            this.b = false;
            Utils.CheckConnectionOpen(this.c);
            this.Reset();
            while (this.a(true, out reader))
            {
            }
        }

        public bool ExecuteNext(out IDataReader reader) => 
            this.a(false, out reader);

        protected virtual IDataReader ExecuteSqlStatement(SqlStatement sqlStatement, bool forceExecute)
        {
            IDataReader reader;
            switch (this.OnSqlStatementExecute(sqlStatement, out reader))
            {
                case SqlStatementStatus.Cancel:
                    this.CancelExecute();
                    break;

                case SqlStatementStatus.Continue:
                    if (this.CanExecuteStatement(sqlStatement))
                    {
                        try
                        {
                            this.i = this.CreateCommand();
                            this.i.CommandTimeout = this.CommandTimeout;
                            this.a(this.i);
                            this.i.CommandText = sqlStatement.Text;
                            if (forceExecute)
                            {
                                this.i.ExecuteNonQuery();
                            }
                            else
                            {
                                reader = this.i.ExecuteReader();
                            }
                        }
                        finally
                        {
                            if (this.i != null)
                            {
                                this.i.Dispose();
                                this.i = null;
                            }
                        }
                    }
                    break;

                default:
                    break;
            }
            return reader;
        }

        protected virtual int GetDefaultCommandTimeout() => 
            this.g;

        protected abstract bool GetNextStatement(out SqlStatement stmt);
        protected virtual string GetStatementText(SqlStatement stmt) => 
            (stmt == null) ? string.Empty : this.ScriptText.Substring(stmt.Offset, stmt.Length);

        protected virtual SqlStatementType GetStatementType(Token token)
        {
            SqlStatementType unknown = SqlStatementType.Unknown;
            switch (token.Id)
            {
                case 0x7e9:
                    unknown = SqlStatementType.Alter;
                    break;

                case 0x7ea:
                    unknown = SqlStatementType.Create;
                    break;

                case 0x7eb:
                    unknown = SqlStatementType.Drop;
                    break;

                case 0x7ec:
                    unknown = SqlStatementType.Select;
                    break;

                case 0x7ed:
                    unknown = SqlStatementType.Insert;
                    break;

                case 0x7ee:
                    unknown = SqlStatementType.Delete;
                    break;

                case 0x7ef:
                    unknown = SqlStatementType.Update;
                    break;

                case 0x7f0:
                    unknown = SqlStatementType.With;
                    break;

                case 0x7f1:
                    unknown = SqlStatementType.Truncate;
                    break;

                case 0x7f2:
                    unknown = SqlStatementType.Commit;
                    break;

                case 0x7f3:
                    unknown = SqlStatementType.Rollback;
                    break;

                default:
                    break;
            }
            return unknown;
        }

        protected virtual void InitializeFromConnection()
        {
        }

        protected virtual void InternalReset()
        {
            this.e++;
            if (this.a != null)
            {
                this.a.Clear();
            }
            this.a = null;
        }

        protected void OnError(ScriptErrorEventArgs e)
        {
            if (this.k != null)
            {
                this.k(this, e);
            }
        }

        protected void OnProgress(SqlStatement stmt)
        {
            if (this.l != null)
            {
                string str;
                long num;
                long num2;
                int num3;
                int num4;
                SqlStatementType type;
                a(stmt, out str, out num, out num2, out num3, out num4, out type);
                this.l(this, new ScriptProgressEventArgs(str, num, num2, num3, num4, type));
            }
        }

        protected virtual SqlStatementStatus OnSqlStatementExecute(SqlStatement stmt, out IDataReader reader)
        {
            reader = null;
            return SqlStatementStatus.Continue;
        }

        public void Open(Stream stream)
        {
            this.lexer.TextReader = new StreamReader(stream, Encoding.Default);
            this.InternalReset();
        }

        public void Open(TextReader reader)
        {
            this.lexer.TextReader = reader;
            this.InternalReset();
        }

        public void Open(string fileName)
        {
            if (this.f != null)
            {
                this.f.Close();
                this.f = null;
            }
            this.f = new StreamReader(fileName, Encoding.Default);
            this.lexer.TextReader = this.f;
            this.InternalReset();
        }

        public virtual void Reset()
        {
            if (this.a == null)
            {
                this.lexer.Reset();
            }
            else
            {
                this.d = 0;
            }
        }

        [MergableProperty(false)]
        public IDbConnection Connection
        {
            get => 
                this.c;
            set
            {
                this.c = value;
                this.InitializeFromConnection();
            }
        }

        [TypeConverter("Devart.Common.Design.DbScriptScriptTextConverter, Devart.Data.Design"), MergableProperty(false)]
        public virtual string ScriptText
        {
            get
            {
                string text = this.lexer.Text;
                return ((text != null) ? text : "");
            }
            set
            {
                if (this.lexer.Text != value)
                {
                    this.lexer.Text = value;
                    this.InternalReset();
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SqlStatementCollection Statements
        {
            get
            {
                if (this.lexer.TextReader != null)
                {
                    throw new InvalidOperationException();
                }
                if (this.a == null)
                {
                    this.a = this.CreateStatementCollection();
                    this.lexer.Reset();
                    while (true)
                    {
                        SqlStatement statement;
                        if (!this.GetNextStatement(out statement))
                        {
                            this.d = 0;
                            break;
                        }
                        this.a.Add(statement);
                    }
                }
                return this.a;
            }
        }

        [Category("Data"), DefaultValue(30), y("DbCommand_CommandTimeout")]
        public int CommandTimeout
        {
            get => 
                (this.commandTimeoutChanged || (this.Connection == null)) ? this.g : this.GetDefaultCommandTimeout();
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(Devart.Common.g.a("InvalidCommandTimeout", value));
                }
                if (this.g != value)
                {
                    this.g = value;
                    this.commandTimeoutChanged = true;
                }
            }
        }

        [DefaultValue(""), Browsable(false)]
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
    }
}

