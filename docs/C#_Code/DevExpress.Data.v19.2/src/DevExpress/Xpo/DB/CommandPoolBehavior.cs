namespace DevExpress.Xpo.DB
{
    using System;

    public enum CommandPoolBehavior
    {
        None,
        TransactionNoPrepare,
        Transaction,
        ConnectionSession
    }
}

