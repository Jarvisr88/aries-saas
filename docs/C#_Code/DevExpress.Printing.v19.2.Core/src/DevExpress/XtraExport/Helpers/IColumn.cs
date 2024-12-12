namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Export;
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    public interface IColumn
    {
        IEnumerable<IColumn> GetAllColumns();
        int GetColumnGroupLevel();
        string GetGroupColumnHeader();

        string Header { get; }

        string Name { get; }

        string FieldName { get; }

        string HyperlinkEditorCaption { get; }

        string HyperlinkTextFormatString { get; }

        int LogicalPosition { get; }

        int Width { get; }

        int GroupIndex { get; }

        bool ExportModeValue { get; }

        bool IsVisible { get; }

        bool IsFixedLeft { get; }

        ISparklineInfo SparklineInfo { get; }

        ColumnSortOrder SortOrder { get; }

        int SortIndex { get; }

        IUnboundInfo UnboundInfo { get; }

        XlCellFormatting Appearance { get; }

        XlCellFormatting AppearanceHeader { get; }

        ColumnEditTypes ColEditType { get; }

        Type ColumnType { get; }

        DevExpress.XtraExport.Helpers.FormatSettings FormatSettings { get; }

        IDictionary<object, object> DataValidationItems { get; }

        bool IsCollapsed { get; }

        bool IsGroupColumn { get; }

        int VisibleIndex { get; }
    }
}

