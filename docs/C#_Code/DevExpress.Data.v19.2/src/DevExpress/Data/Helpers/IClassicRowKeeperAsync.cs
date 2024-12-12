namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public interface IClassicRowKeeperAsync
    {
        bool IsRestoredAsExpanded(GroupRowInfo group);
        void OnTotalsReceived();

        bool IsRestoreAllExpanded { get; }
    }
}

