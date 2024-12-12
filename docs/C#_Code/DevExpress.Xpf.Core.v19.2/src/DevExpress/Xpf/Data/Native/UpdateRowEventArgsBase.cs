namespace DevExpress.Xpf.Data.Native
{
    using DevExpress.Xpf.Data;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class UpdateRowEventArgsBase : BaseAsyncArgs
    {
        internal UpdateRowEventArgsBase(CancellationToken cancellationToken, object row) : base(cancellationToken)
        {
            this.Row = row;
        }

        public object Row { get; private set; }
    }
}

