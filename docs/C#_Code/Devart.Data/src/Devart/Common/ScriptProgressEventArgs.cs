namespace Devart.Common
{
    using System;

    public sealed class ScriptProgressEventArgs : EventArgs
    {
        private readonly string a;
        private readonly long b;
        private readonly long c;
        private readonly int d;
        private readonly int e;
        private SqlStatementType f;

        internal ScriptProgressEventArgs(string A_0, long A_1, long A_2, int A_3, int A_4, SqlStatementType A_5)
        {
            this.a = A_0;
            this.c = A_1;
            this.b = A_2;
            this.d = A_3;
            this.e = A_4;
            this.f = A_5;
        }

        public string Text =>
            this.a;

        public long Offset =>
            this.c;

        public long Length =>
            this.b;

        public int LineNumber =>
            this.d;

        public int LinePosition =>
            this.e;

        public SqlStatementType StatementType =>
            this.f;
    }
}

