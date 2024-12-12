namespace DevExpress.DXBinding.Native
{
    using System;

    public interface IErrorHandler
    {
        void ClearError();
        void Report(string msg, bool critical);
        void ReportOrThrow(Exception e);
        void SetError();
        void Throw(string msg, Exception innerException);

        bool HasError { get; }

        bool CatchAllExceptions { get; set; }
    }
}

