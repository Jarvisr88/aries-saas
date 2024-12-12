namespace Devart.Common
{
    using System;

    public class ConnectionLostEventArgs : EventArgs
    {
        private readonly object a;
        private readonly ConnectionLostCause b;
        private readonly int c;
        private Devart.Common.RetryMode d;
        private ConnectionLostContext e;

        public ConnectionLostEventArgs(object component, ConnectionLostCause cause, ConnectionLostContext context, Devart.Common.RetryMode retryMode, int attemptNumber)
        {
            this.a = component;
            this.d = retryMode;
            this.c = attemptNumber;
            this.b = cause;
            this.e = context;
        }

        public object Component =>
            this.a;

        public ConnectionLostCause Cause =>
            this.b;

        public int AttemptNumber =>
            this.c;

        public Devart.Common.RetryMode RetryMode
        {
            get => 
                this.d;
            set => 
                this.d = value;
        }

        public ConnectionLostContext Context
        {
            get => 
                this.e;
            set => 
                this.e = value;
        }
    }
}

