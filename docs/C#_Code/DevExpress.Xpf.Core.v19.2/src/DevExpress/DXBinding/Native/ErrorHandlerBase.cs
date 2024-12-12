namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class ErrorHandlerBase : IErrorHandler
    {
        public ErrorHandlerBase()
        {
            this.CatchAllExceptions = true;
        }

        public void ClearError()
        {
            this.HasError = false;
        }

        public void Report(string msg, bool critical)
        {
            if (critical)
            {
                this.SetError();
            }
            this.ReportCore(msg);
        }

        protected abstract void ReportCore(string msg);
        public void ReportOrThrow(Exception e)
        {
            if (!this.CatchAllExceptions)
            {
                throw e;
            }
            this.Report(e.Message, true);
        }

        public void SetError()
        {
            this.HasError = true;
        }

        public void Throw(string msg, Exception innerException)
        {
            this.HasError = true;
            this.ThrowCore(msg, innerException);
        }

        protected abstract void ThrowCore(string msg, Exception innerException);

        public bool CatchAllExceptions { get; set; }

        public bool HasError { get; protected set; }
    }
}

