namespace DevExpress.Printing.Core
{
    using System;

    internal interface IThreadingServices
    {
        int CurrentThreadID { get; }
    }
}

