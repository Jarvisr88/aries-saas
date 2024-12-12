namespace DevExpress.DataAccess.Wizard
{
    using DevExpress.DataAccess.Wizard.Services;
    using System;

    public interface IUIRunnerContext
    {
        IExceptionHandler CreateExceptionHandler(ExceptionHandlerKind kind);
        IExceptionHandler CreateExceptionHandler(ExceptionHandlerKind kind, string caption);
    }
}

