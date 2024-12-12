namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class DeserializeExceptionEventArgs : EventArgs
    {
        public DeserializeExceptionEventArgs(System.Exception exception)
        {
            this.Exception = exception;
        }

        public System.Exception Exception { get; private set; }
    }
}

