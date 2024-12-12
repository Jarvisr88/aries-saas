namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class EmptyColumnOwnerBase : IColumnOwnerBase
    {
        public static readonly IColumnOwnerBase Instance = new EmptyColumnOwnerBase();
        private static readonly DevExpress.Xpf.Grid.SummaryItemBase[] EmptySummaryItemArray = new DevExpress.Xpf.Grid.SummaryItemBase[0];

        bool IColumnOwnerBase.AllowFilterColumn(ColumnBase column) => 
            true;

        bool IColumnOwnerBase.AllowSortColumn(ColumnBase column) => 
            true;

        void IColumnOwnerBase.ApplyColumnVisibleIndex(BaseColumn column, int oldVisibleIndex)
        {
        }

        void IColumnOwnerBase.CalcColumnsLayout()
        {
        }

        bool IColumnOwnerBase.CanClearColumnFilter(ColumnBase column) => 
            false;

        void IColumnOwnerBase.ChangeColumnSortOrder(ColumnBase column)
        {
        }

        void IColumnOwnerBase.ClearBindingValues(ColumnBase column)
        {
        }

        void IColumnOwnerBase.ClearColumnFilter(ColumnBase column)
        {
        }

        BaseEditSettings IColumnOwnerBase.CreateDefaultEditSettings(IDataColumnInfo column) => 
            new TextEditSettings();

        Style IColumnOwnerBase.GetActualCellStyle(ColumnBase column) => 
            null;

        DataTemplate IColumnOwnerBase.GetActualCellTemplate() => 
            null;

        ColumnBase IColumnOwnerBase.GetColumn(string fieldName) => 
            null;

        Type IColumnOwnerBase.GetColumnType(ColumnBase column, DataProviderBase dataProvider) => 
            null;

        HorizontalAlignment IColumnOwnerBase.GetDefaultColumnAlignment(ColumnBase column) => 
            HorizontalAlignment.Left;

        IList<DevExpress.Xpf.Grid.SummaryItemBase> IColumnOwnerBase.GetTotalSummaryItems(ColumnBase column) => 
            EmptySummaryItemArray;

        object IColumnOwnerBase.GetTotalSummaryValue(DevExpress.Xpf.Grid.SummaryItemBase item) => 
            null;

        void IColumnOwnerBase.GroupColumn(string fieldName, int index, ColumnSortOrder sortOrder)
        {
        }

        void IColumnOwnerBase.RebuildColumnChooserColumns()
        {
        }

        void IColumnOwnerBase.UngroupColumn(string fieldName)
        {
        }

        void IColumnOwnerBase.UpdateCellDataValues()
        {
        }

        void IColumnOwnerBase.UpdateContentLayout()
        {
        }

        void IColumnOwnerBase.UpdateShowEditFilterButton(bool newAllowColumnFilterEditor, bool oldAllowColumnFilterEditor)
        {
        }

        bool IColumnOwnerBase.AllowColumnsResizing =>
            false;

        DataTemplateSelector IColumnOwnerBase.CellTemplateSelector =>
            null;

        DataTemplate IColumnOwnerBase.CellDisplayTemplate =>
            null;

        DataTemplateSelector IColumnOwnerBase.CellDisplayTemplateSelector =>
            null;

        DataTemplate IColumnOwnerBase.CellEditTemplate =>
            null;

        DataTemplateSelector IColumnOwnerBase.CellEditTemplateSelector =>
            null;

        bool IColumnOwnerBase.AllowSorting =>
            false;

        bool IColumnOwnerBase.AllowColumnMoving =>
            false;

        bool IColumnOwnerBase.AllowResizing =>
            false;

        bool IColumnOwnerBase.UpdateAllowResizingOnWidthChanging =>
            true;

        bool IColumnOwnerBase.AllowEditing =>
            false;

        bool IColumnOwnerBase.AllowColumnFiltering =>
            false;

        bool IColumnOwnerBase.AutoWidth =>
            false;

        IList<ColumnBase> IColumnOwnerBase.VisibleColumns =>
            null;

        Style IColumnOwnerBase.AutoFilterRowCellStyle =>
            null;

        Style IColumnOwnerBase.NewItemRowCellStyle =>
            null;

        Style IColumnOwnerBase.ColumnHeaderContentStyle =>
            null;

        Style IColumnOwnerBase.TotalSummaryContentStyle =>
            null;

        DataTemplate IColumnOwnerBase.ColumnHeaderTemplate =>
            null;

        DataTemplateSelector IColumnOwnerBase.ColumnHeaderTemplateSelector =>
            null;

        DataTemplate IColumnOwnerBase.ColumnHeaderCustomizationAreaTemplate =>
            null;

        DataTemplateSelector IColumnOwnerBase.ColumnHeaderCustomizationAreaTemplateSelector =>
            null;

        bool IColumnOwnerBase.LockEditorClose { get; set; }

        bool IColumnOwnerBase.ShowValidationAttributeErrors =>
            false;

        bool IColumnOwnerBase.AllowGrouping =>
            false;

        ActualTemplateSelectorWrapper IColumnOwnerBase.ActualGroupValueTemplateSelector =>
            null;

        bool IColumnOwnerBase.ShowAllTableValuesInFilterPopup =>
            false;

        bool IColumnOwnerBase.ShowAllTableValuesInCheckedFilterPopup =>
            true;

        Style IColumnOwnerBase.ColumnHeaderImageStyle =>
            null;
    }
}

