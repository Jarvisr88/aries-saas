namespace DevExpress.CodeParser
{
    using System;
    using System.Reflection;

    public interface ITokenCollection
    {
        IToken this[int index] { get; }

        int Count { get; }
    }
}

