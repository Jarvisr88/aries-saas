namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;

    public interface IAdditionalSheetInfo
    {
        string Name { get; }

        XlSheetVisibleState VisibleState { get; }
    }
}

