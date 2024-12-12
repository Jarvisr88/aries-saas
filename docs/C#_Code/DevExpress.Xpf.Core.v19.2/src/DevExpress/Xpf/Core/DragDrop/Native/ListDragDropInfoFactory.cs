namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Popups;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    internal class ListDragDropInfoFactory : IDragDropInfoFactory
    {
        private readonly ListBoxEdit listBox;

        public ListDragDropInfoFactory(ListBoxEdit listBox)
        {
            this.listBox = listBox;
        }

        private void AddMultiSelectionItemsToDrag(ListBoxItem listBoxItemUnderMouse, List<object> selectedData, List<RowPointer> selectedIndices)
        {
            foreach (object obj2 in this.ListBoxCore.SelectedItems)
            {
                if (obj2 != listBoxItemUnderMouse.Content)
                {
                    int index = this.ListBoxCore.Items.IndexOf(obj2);
                    if (index != -1)
                    {
                        selectedData.Add(obj2);
                        selectedIndices.Add(new RowPointer(index));
                    }
                }
            }
        }

        public DragInfo CreateDragInfo(object sender)
        {
            ListBoxItem container = LayoutHelper.FindParentObject<ListBoxItem>(sender as DependencyObject);
            if (container == null)
            {
                return null;
            }
            int handle = this.ListBoxCore.ItemContainerGenerator.IndexFromContainer(container);
            if ((handle == -1) || (container.Content == null))
            {
                return null;
            }
            List<object> selectedData = new List<object>();
            List<RowPointer> selectedIndices = new List<RowPointer>();
            selectedData.Add(container.Content);
            selectedIndices.Add(new RowPointer(handle));
            if (this.ListBox.SelectionMode != SelectionMode.Single)
            {
                this.AddMultiSelectionItemsToDrag(container, selectedData, selectedIndices);
            }
            return new DragInfo(selectedData.ToArray(), selectedIndices.ToArray(), new DragDropInfoVisualSource(this.ListBox, sender, this.ListBox));
        }

        public DropInfo CreateDropInfo(object sender, DragInfo activeDragInfo) => 
            (activeDragInfo == null) ? new DropInfo(new DragDropInfoVisualSource(this.listBox, sender, this.listBox)) : null;

        private ListBoxEdit ListBox =>
            this.listBox;

        private EditorListBox ListBoxCore =>
            this.ListBox.ListBoxCore;
    }
}

