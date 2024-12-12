namespace DevExpress.XtraExport.Helpers
{
    using System;

    public interface IGridOptionsBehavior
    {
        bool ReadOnly { get; }

        bool AlignGroupSummaryInGroupRow { get; }
    }
}

