namespace DevExpress.Xpf.Data.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public sealed class UpdateRowEventArgs : UpdateRowEventArgsBase
    {
        internal UpdateRowEventArgs(CancellationToken cancellationToken, object source, object row) : base(cancellationToken, row)
        {
            this.Source = source;
        }

        public UpdateRowResult Result { get; set; }

        public object Source { get; private set; }
    }
}

