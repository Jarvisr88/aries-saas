namespace Devart.Common
{
    using System;

    public abstract class TransactionStateChangeEventArgs : EventArgs
    {
        private TransactionAction a;

        protected TransactionStateChangeEventArgs(TransactionAction action)
        {
            this.a = action;
        }

        public TransactionAction Action =>
            this.a;
    }
}

