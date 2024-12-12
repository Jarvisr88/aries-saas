namespace Devart.Common
{
    using System;

    public sealed class TransactionStateChangingEventArgs : TransactionStateChangeEventArgs
    {
        public TransactionStateChangingEventArgs(TransactionAction action) : base(action)
        {
        }
    }
}

