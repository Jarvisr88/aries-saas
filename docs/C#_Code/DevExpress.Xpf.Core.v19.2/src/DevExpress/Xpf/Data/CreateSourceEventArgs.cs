namespace DevExpress.Xpf.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public sealed class CreateSourceEventArgs : BaseAsyncArgs
    {
        internal CreateSourceEventArgs(CancellationToken cancellationToken) : base(cancellationToken)
        {
        }

        public object Source { get; set; }
    }
}

