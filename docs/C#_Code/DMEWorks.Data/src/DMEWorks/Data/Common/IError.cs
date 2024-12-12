namespace DMEWorks.Data.Common
{
    using System;

    public interface IError
    {
        bool IsError { get; }

        string Message { get; }
    }
}

