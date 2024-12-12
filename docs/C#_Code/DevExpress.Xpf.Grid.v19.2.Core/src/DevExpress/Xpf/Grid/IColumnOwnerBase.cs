namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public interface IColumnOwnerBase
    {
        bool AllowFilterColumn(ColumnBase column);
        bool AllowSortColumn(ColumnBase column);
        void ApplyColumnVisibleIndex(BaseColumn column, int oldVisibleIndex);
        void CalcColumnsLayout();
        bool CanClearColumnFilter(ColumnBase column);
        void ChangeColumnSortOrder(ColumnBase column);
        void ClearBindingValues(ColumnBase column);
        void ClearColumnFilter(ColumnBase column);
        BaseEditSettings CreateDefaultEditSettings(IDataColumnInfo column);
        Style GetActualCellStyle(ColumnBase column);
        DataTemplate GetActualCellTemplate();
        ColumnBase GetColumn(string fieldName);
        Type GetColumnType(ColumnBase column, DataProviderBase dataProvider);
        HorizontalAlignment GetDefaultColumnAlignment(ColumnBase column);
        IList<DevExpress.Xpf.Grid.SummaryItemBase> GetTotalSummaryItems(ColumnBase column);
        object GetTotalSummaryValue(DevExpress.Xpf.Grid.SummaryItemBase item);
        void GroupColumn(string fieldName, int index, ColumnSortOrder sortOrder);
        void RebuildColumnChooserColumns();
        void UngroupColumn(string fieldName);
        void UpdateCellDataValues();
        void UpdateContentLayout();
        void UpdateShowEditFilterButton(bool newAllowColumnFilterEditor, bool oldAllowColumnFilterEditor);

        DataTemplateSelector CellTemplateSelector { get; }

        DataTemplate CellDisplayTemplate { get; }

        DataTemplateSelector CellDisplayTemplateSelector { get; }

        DataTemplate CellEditTemplate { get; }

        DataTemplateSelector CellEditTemplateSelector { get; }

        bool AllowColumnsResizing { get; }

        bool AllowSorting { get; }

        bool AllowColumnMoving { get; }

        bool AllowResizing { get; }

        bool UpdateAllowResizingOnWidthChanging { get; }

        bool AllowEditing { get; }

        bool AutoWidth { get; }

        bool AllowColumnFiltering { get; }

        bool ShowAllTableValuesInCheckedFilterPopup { get; }

        bool ShowAllTableValuesInFilterPopup { get; }

        IList<ColumnBase> VisibleColumns { get; }

        Style AutoFilterRowCellStyle { get; }

        Style NewItemRowCellStyle { get; }

        Style ColumnHeaderContentStyle { get; }

        Style TotalSummaryContentStyle { get; }

        DataTemplate ColumnHeaderTemplate { get; }

        DataTemplateSelector ColumnHeaderTemplateSelector { get; }

        DataTemplate ColumnHeaderCustomizationAreaTemplate { get; }

        DataTemplateSelector ColumnHeaderCustomizationAreaTemplateSelector { get; }

        bool LockEditorClose { get; set; }

        bool ShowValidationAttributeErrors { get; }

        ActualTemplateSelectorWrapper ActualGroupValueTemplateSelector { get; }

        bool AllowGrouping { get; }

        Style ColumnHeaderImageStyle { get; }
    }
}

