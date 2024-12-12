namespace DevExpress.Xpf.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class BaseAsyncArgs : EventArgs
    {
        internal BaseAsyncArgs(System.Threading.CancellationToken cancellationToken)
        {
            this.CancellationToken = cancellationToken;
        }

        public System.Threading.CancellationToken CancellationToken { get; private set; }
    }
}

