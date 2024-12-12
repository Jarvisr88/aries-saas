namespace DevExpress.Xpf.Docking
{
    using System;

    public interface ISupportBatchUpdate
    {
        void BeginUpdate();
        void EndUpdate();

        bool IsUpdatedLocked { get; }
    }
}

