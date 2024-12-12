namespace Devart.Common
{
    using System;
    using System.Data;

    public class DbStatementExecuteEventArgs : EventArgs
    {
        private readonly string a;
        private readonly int b;
        private readonly int c;
        private readonly int d;
        private readonly int e;
        private readonly SqlStatementType f;
        protected IDataReader g;
        private SqlStatementStatus h = SqlStatementStatus.Continue;

        internal DbStatementExecuteEventArgs(string A_0, int A_1, int A_2, int A_3, int A_4, SqlStatementType A_5)
        {
            this.a = A_0;
            this.e = A_1;
            this.b = A_2;
            this.c = A_3;
            this.d = A_4;
            this.f = A_5;
        }

        public string Text =>
            this.a;

        public int Offset =>
            this.e;

        public int Length =>
            this.b;

        public int LineNumber =>
            this.c;

        public int LinePosition =>
            this.d;

        public IDataReader Reader
        {
            get => 
                this.g;
            set => 
                this.g = value;
        }

        public SqlStatementType StatementType =>
            this.f;

        public SqlStatementStatus StatementStatus
        {
            get => 
                this.h;
            set => 
                this.h = value;
        }
    }
}

