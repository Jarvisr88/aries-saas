namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public class ColumnFilterComboBoxEditHelper
    {
        private readonly ColumnBase Column;

        public ColumnFilterComboBoxEditHelper(ColumnBase column)
        {
            this.Column = column;
            this.ComboBoxEditSettings = !this.Column.IsAsyncLookup ? (this.Column.EditSettings as DevExpress.Xpf.Editors.Settings.ComboBoxEditSettings) : null;
        }

        public void CreateComboBoxEdit(Action<DevExpress.Xpf.Editors.ComboBoxEdit> setUpAction)
        {
            this.ComboBoxEdit = (this.ComboBoxEditSettings != null) ? ((DevExpress.Xpf.Editors.ComboBoxEdit) this.ComboBoxEditSettings.CreateEditor(false, EmptyDefaultEditorViewInfo.Instance, EditorOptimizationMode.Disabled)) : new DevExpress.Xpf.Editors.ComboBoxEdit();
            this.ComboBoxEdit.SelectItemWithNullValue = true;
            if (this.Column.IsAsyncLookup)
            {
                this.ComboBoxEdit.AllowLiveDataShaping = false;
                this.ComboBoxEdit.ValueMember = "EditValue";
                this.ComboBoxEdit.DisplayMember = "DisplayValue";
            }
            if (setUpAction != null)
            {
                setUpAction(this.ComboBoxEdit);
            }
        }

        public void UpdateItems(DataViewBase view, bool isItemsLoading, IList items)
        {
            this.IsItemsLoading = isItemsLoading;
            if (view.HasValidationError)
            {
                this.ComboBoxEdit.ItemsSource = null;
            }
            else
            {
                bool? nullable1;
                this.ComboBoxEdit.ItemsPanel = FilterPopupVirtualizingStackPanel.GetItemsPanelTemplate((this.IsItemsLoading || this.Column.IsAsyncLookup) ? 0x7fffffff : items.Count);
                this.ComboBoxEdit.ItemsSource = items;
                if (this.IsItemsLoading)
                {
                    nullable1 = false;
                }
                else
                {
                    nullable1 = null;
                }
                this.ComboBoxEdit.ShowCustomItems = nullable1;
            }
        }

        public DevExpress.Xpf.Editors.ComboBoxEdit ComboBoxEdit { get; set; }

        public DevExpress.Xpf.Editors.Settings.ComboBoxEditSettings ComboBoxEditSettings { get; private set; }

        public bool IsItemsLoading { get; set; }
    }
}

