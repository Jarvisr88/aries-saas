namespace Devart.Common
{
    using System;

    public enum TransactionAction
    {
        BeginTransaction = 1,
        Commit = 2,
        Rollback = 3,
        Savepoint = 4,
        ReleaseSavepoint = 5,
        RollbackToSavepoint = 6
    }
}

