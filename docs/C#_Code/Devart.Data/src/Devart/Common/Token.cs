namespace Devart.Common
{
    using System;

    public class Token
    {
        public readonly Devart.Common.TokenType Type;
        public readonly object Value;
        public readonly int Id;
        public readonly int StartPosition;
        public readonly int EndPosition;
        public readonly int LineBegin;
        public readonly int LineNumber;
        public static Token Begin = new Token(Devart.Common.TokenType.Begin, "", 0, 0, 0, 0, 0);
        public static Token Empty = new Token(Devart.Common.TokenType.End, null, 0, 0, 0, 0, 0);

        public Token(Devart.Common.TokenType type, object value, int id, int startPosition, int endPosition, int lineBegin, int lineNumber)
        {
            this.Type = type;
            this.Value = value;
            this.Id = id;
            this.StartPosition = startPosition;
            this.EndPosition = endPosition;
            this.LineBegin = lineBegin;
            this.LineNumber = lineNumber;
        }

        public override string ToString() => 
            (this.Value != null) ? this.Value.ToString() : string.Empty;

        public int LinePosition =>
            this.StartPosition - this.LineBegin;

        public virtual int EndLineBegin =>
            this.LineBegin;

        public virtual int EndLineNumber =>
            this.LineNumber;

        public virtual int EndLinePosition =>
            this.EndPosition - this.LineBegin;
    }
}

