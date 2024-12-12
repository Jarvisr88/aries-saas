namespace DevExpress.Data
{
    using System;

    public class ControllerRowExceptionEventArgs : ControllerRowEventArgs
    {
        private System.Exception exception;
        private ExceptionAction action;

        public ControllerRowExceptionEventArgs(int controllerRow, object row, System.Exception exception);

        public ExceptionAction Action { get; set; }

        public System.Exception Exception { get; }
    }
}

