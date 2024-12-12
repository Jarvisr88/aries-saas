namespace DevExpress.Xpf.Data.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class UpdateRowAsyncEventArgs : UpdateRowEventArgsBase
    {
        internal UpdateRowAsyncEventArgs(CancellationToken cancellationToken, object row) : base(cancellationToken, row)
        {
        }

        public Task<UpdateRowResult> Result { get; set; }
    }
}

