namespace Devart.Common
{
    using System;

    public sealed class TransactionStateChangedEventArgs : TransactionStateChangeEventArgs
    {
        public TransactionStateChangedEventArgs(TransactionAction action) : base(action)
        {
        }
    }
}

