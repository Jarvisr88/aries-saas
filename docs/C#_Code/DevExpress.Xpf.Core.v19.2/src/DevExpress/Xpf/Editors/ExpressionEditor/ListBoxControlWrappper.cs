namespace DevExpress.Xpf.Editors.ExpressionEditor
{
    using DevExpress.Data;
    using DevExpress.Data.ExpressionEditor;
    using System;
    using System.Windows.Controls;

    public class ListBoxControlWrappper : ISelector
    {
        private readonly ListBox listBox;

        public ListBoxControlWrappper(ListBox listBox)
        {
            this.listBox = listBox;
        }

        void ISelector.SetItemsSource(object[] items, ColumnSortOrder sortOrder)
        {
            this.listBox.ItemsSource = SortHelper.GetSortedItems(items, sortOrder);
        }

        int ISelector.ItemCount =>
            this.listBox.Items.Count;

        object ISelector.SelectedItem =>
            this.listBox.SelectedItem;

        int ISelector.SelectedIndex
        {
            get => 
                this.listBox.SelectedIndex;
            set => 
                this.listBox.SelectedIndex = value;
        }
    }
}

