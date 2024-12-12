namespace DevExpress.XtraExport.Helpers
{
    using System;

    public interface IRowBase
    {
        int GetRowLevel();

        int LogicalPosition { get; }

        int DataSourceRowIndex { get; }

        bool IsGroupRow { get; }

        bool IsDataAreaRow { get; }

        DevExpress.XtraExport.Helpers.FormatSettings FormatSettings { get; }
    }
}

