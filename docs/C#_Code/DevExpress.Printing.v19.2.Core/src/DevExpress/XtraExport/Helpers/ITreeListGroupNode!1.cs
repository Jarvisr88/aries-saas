namespace DevExpress.XtraExport.Helpers
{
    using System;

    public interface ITreeListGroupNode<out TRow> : IGroupRow<TRow>, IRowBase where TRow: IRowBase
    {
        bool ExportNodeSummary { get; }
    }
}

