namespace DevExpress.DataAccess.Wizard.Services
{
    using System;

    public interface IExceptionHandler
    {
        void HandleException(Exception exception);

        bool AnyExceptions { get; }
    }
}

