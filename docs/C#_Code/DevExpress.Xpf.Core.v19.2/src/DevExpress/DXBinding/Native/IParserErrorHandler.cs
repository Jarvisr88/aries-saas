namespace DevExpress.DXBinding.Native
{
    using System;

    internal interface IParserErrorHandler
    {
        void Error(int pos, string msg);

        bool HasError { get; }
    }
}

