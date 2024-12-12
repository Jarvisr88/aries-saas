namespace DevExpress.Xpf.Grid
{
    using System;

    public interface IInvalidRowExceptionEventArgs
    {
        string ErrorText { get; }

        string WindowCaption { get; }

        DevExpress.Xpf.Grid.ExceptionMode ExceptionMode { get; set; }

        System.Exception Exception { get; }
    }
}

