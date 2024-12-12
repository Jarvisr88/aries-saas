namespace DevExpress.Xpf.Editors.ExpressionEditor
{
    using DevExpress.Data;
    using DevExpress.Data.ExpressionEditor;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;

    public class ComboBoxEditWrappper : ISelector
    {
        private readonly ComboBoxEdit comboBox;

        public ComboBoxEditWrappper(ComboBoxEdit comboBox)
        {
            this.comboBox = comboBox;
        }

        void ISelector.SetItemsSource(object[] items, ColumnSortOrder sortOrder)
        {
            this.comboBox.ItemsSource = SortHelper.GetSortedItems(items, sortOrder);
        }

        int ISelector.ItemCount =>
            (this.comboBox.ItemsSource != null) ? ((ICollection) this.comboBox.ItemsSource).Count : 0;

        object ISelector.SelectedItem =>
            this.comboBox.SelectedItem;

        int ISelector.SelectedIndex
        {
            get => 
                this.comboBox.SelectedIndex;
            set => 
                this.comboBox.SelectedIndex = value;
        }
    }
}

