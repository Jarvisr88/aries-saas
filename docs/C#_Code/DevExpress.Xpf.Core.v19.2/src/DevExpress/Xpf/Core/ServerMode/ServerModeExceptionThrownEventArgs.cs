namespace DevExpress.Xpf.Core.ServerMode
{
    using System;
    using System.Windows;

    public class ServerModeExceptionThrownEventArgs : RoutedEventArgs
    {
        private System.Exception exception;

        public ServerModeExceptionThrownEventArgs(System.Exception exception)
        {
            this.exception = exception;
        }

        public System.Exception Exception =>
            this.exception;
    }
}

