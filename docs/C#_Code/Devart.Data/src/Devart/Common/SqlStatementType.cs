namespace Devart.Common
{
    using System;

    public enum SqlStatementType
    {
        Unknown,
        Select,
        Insert,
        Update,
        Delete,
        Truncate,
        Batch,
        Alter,
        Create,
        Drop,
        Execute,
        Commit,
        Rollback,
        With,
        Extended
    }
}

