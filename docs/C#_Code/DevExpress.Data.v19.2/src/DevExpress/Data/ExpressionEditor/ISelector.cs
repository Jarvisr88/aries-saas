namespace DevExpress.Data.ExpressionEditor
{
    using DevExpress.Data;
    using System;

    public interface ISelector
    {
        void SetItemsSource(object[] items, ColumnSortOrder sortOrder);

        int ItemCount { get; }

        object SelectedItem { get; }

        int SelectedIndex { get; set; }
    }
}

