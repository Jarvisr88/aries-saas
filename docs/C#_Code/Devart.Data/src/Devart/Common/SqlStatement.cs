namespace Devart.Common
{
    using System;
    using System.Data;

    public class SqlStatement
    {
        private readonly DbScript a;
        private readonly int b;
        private readonly int c;
        private readonly int d;
        private readonly int e;
        private readonly uint f;
        private readonly string g;
        private readonly SqlStatementType h;

        protected internal SqlStatement(DbScript script, int offset, int length, int line, int position, string text, SqlStatementType statementType)
        {
            this.a = script;
            this.b = offset;
            this.c = length;
            this.d = line;
            this.e = position;
            this.f = script.e;
            this.g = text;
            this.h = statementType;
        }

        private void a()
        {
            if ((this.a == null) || (this.f != this.a.e))
            {
                throw new InvalidOperationException("The SqlStatement object is no longer valid due to modification of DbScript.ScriptText property.");
            }
        }

        public IDataReader Execute()
        {
            this.a();
            return this.a.a(this, false);
        }

        public void ExecuteNonQuery()
        {
            this.a();
            this.a.a(this, true);
        }

        public string Text
        {
            get
            {
                this.a();
                return ((this.g == null) ? this.a.a(this) : this.g);
            }
        }

        public int Offset
        {
            get
            {
                this.a();
                return this.b;
            }
        }

        public int Length
        {
            get
            {
                this.a();
                return this.c;
            }
        }

        public int LineNumber
        {
            get
            {
                this.a();
                return this.d;
            }
        }

        public int LinePosition
        {
            get
            {
                this.a();
                return this.e;
            }
        }

        public SqlStatementType StatementType =>
            this.h;
    }
}

