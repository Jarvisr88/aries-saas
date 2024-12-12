namespace DevExpress.Data
{
    using System;

    public class ServerModeExceptionThrownEventArgs : EventArgs
    {
        private System.Exception _Exception;

        public ServerModeExceptionThrownEventArgs(System.Exception exception);

        public System.Exception Exception { get; }
    }
}

