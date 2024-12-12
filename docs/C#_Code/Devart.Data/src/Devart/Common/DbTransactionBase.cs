namespace Devart.Common
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class DbTransactionBase : DbTransaction
    {
        protected System.Data.IsolationLevel isolationLevel;
        protected bool isDisposed;
        private string a;

        public event TransactionStateChangedEventHandler StateChanged;

        public event TransactionStateChangingEventHandler StateChanging;

        protected DbTransactionBase()
        {
        }

        protected void CheckDisposed()
        {
            if (this.isDisposed)
            {
                throw new InvalidOperationException(this.AlreadyDisposedErrorMessage);
            }
        }

        protected void OnStateChanged(TransactionAction action, DbConnection connection)
        {
            if (this.c != null)
            {
                TransactionStateChangedEventArgs e = new TransactionStateChangedEventArgs(action);
                this.c(this, e);
            }
        }

        protected void OnStateChanging(TransactionAction action, DbConnection connection)
        {
            if (this.b != null)
            {
                TransactionStateChangingEventArgs e = new TransactionStateChangingEventArgs(action);
                this.b(this, e);
            }
        }

        private string AlreadyDisposedErrorMessage
        {
            get
            {
                this.a ??= $"This {base.GetType().Name} has completed; it is no longer usable.";
                return this.a;
            }
        }

        public override System.Data.IsolationLevel IsolationLevel =>
            this.isolationLevel;
    }
}

