namespace DevExpress.Xpf.Layout.Core.Base
{
    using System;

    public abstract class BaseException : Exception
    {
        public static readonly string ElementIsNull = "Element Is Null";

        public BaseException(string message) : base(message)
        {
        }
    }
}

