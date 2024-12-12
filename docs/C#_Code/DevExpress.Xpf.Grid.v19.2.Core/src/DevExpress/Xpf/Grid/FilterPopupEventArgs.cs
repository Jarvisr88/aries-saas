namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FilterPopupEventArgs : RoutedEventArgs
    {
        private readonly Lazy<DevExpress.Xpf.Grid.ExcelColumnFilterSettings> excelColumnFilterSettingsLazy;

        public FilterPopupEventArgs(ColumnBase column, DevExpress.Xpf.Editors.PopupBaseEdit popupBaseEdit, Lazy<DevExpress.Xpf.Grid.ExcelColumnFilterSettings> excelColumnFilterSettingsLazy)
        {
            this.Column = column;
            this.PopupBaseEdit = popupBaseEdit;
            this.excelColumnFilterSettingsLazy = excelColumnFilterSettingsLazy;
        }

        public ColumnBase Column { get; private set; }

        public DevExpress.Xpf.Editors.ComboBoxEdit ComboBoxEdit =>
            this.PopupBaseEdit as DevExpress.Xpf.Editors.ComboBoxEdit;

        public DevExpress.Xpf.Editors.PopupBaseEdit PopupBaseEdit { get; private set; }

        public DevExpress.Xpf.Grid.ExcelColumnFilterSettings ExcelColumnFilterSettings =>
            this.excelColumnFilterSettingsLazy.Value;

        public DataViewBase Source =>
            this.Column.View;
    }
}

