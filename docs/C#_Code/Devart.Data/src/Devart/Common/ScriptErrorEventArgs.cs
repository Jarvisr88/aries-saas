namespace Devart.Common
{
    using System;

    public sealed class ScriptErrorEventArgs : EventArgs
    {
        private readonly System.Exception a;
        private bool b;
        private readonly long c;
        private readonly int d;
        private readonly int e;
        private readonly long f;
        private readonly string g;
        private SqlStatementType h;

        public ScriptErrorEventArgs(System.Exception e, string text, long offset, long length, int lineNumber, int linePosition, SqlStatementType statementType)
        {
            this.a = e;
            this.g = text;
            this.f = offset;
            this.c = length;
            this.d = lineNumber;
            this.e = linePosition;
            this.h = statementType;
        }

        public System.Exception Exception =>
            this.a;

        public bool Ignore
        {
            get => 
                this.b;
            set => 
                this.b = value;
        }

        public string Text =>
            this.g;

        public long Offset =>
            this.f;

        public long Length =>
            this.c;

        public int LineNumber =>
            this.d;

        public int LinePosition =>
            this.e;

        public SqlStatementType StatementType =>
            this.h;
    }
}

