namespace DevExpress.CodeParser
{
    using System;

    public interface IToken
    {
        int StartPosition { get; }

        int EndPosition { get; }

        int Length { get; }
    }
}

