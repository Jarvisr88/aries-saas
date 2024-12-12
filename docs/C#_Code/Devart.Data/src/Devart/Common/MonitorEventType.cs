namespace Devart.Common
{
    using System;

    public enum MonitorEventType
    {
        Connect,
        Disconnect,
        Prepare,
        Execute,
        BeginTransaction,
        Commit,
        Rollback,
        Error,
        ActivateInPool,
        ReturnToPool,
        Custom
    }
}

