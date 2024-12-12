namespace DevExpress.Xpf.GridData
{
    using DevExpress.Data;
    using System;

    public interface IColumnInfo
    {
        string FieldName { get; }

        ColumnSortOrder SortOrder { get; }

        UnboundColumnType UnboundType { get; }

        string UnboundExpression { get; }

        bool ReadOnly { get; }
    }
}

