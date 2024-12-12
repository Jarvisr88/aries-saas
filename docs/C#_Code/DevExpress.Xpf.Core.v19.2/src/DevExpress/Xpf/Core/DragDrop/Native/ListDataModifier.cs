namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections;
    using System.Linq;

    internal class ListDataModifier : IDataModifier
    {
        private ListBoxEdit listBox;

        public ListDataModifier(ListBoxEdit listBox)
        {
            this.listBox = listBox;
        }

        private int ConvertHandle(int handle)
        {
            if ((handle < 0) || (this.listBox.ListBoxCore == null))
            {
                return handle;
            }
            EditorsCompositeCollection itemsSource = this.listBox.ListBoxCore.ItemsSource as EditorsCompositeCollection;
            return ((itemsSource != null) ? (handle - itemsSource.GetCustomCollectionCount()) : handle);
        }

        private int GetInsertIndex(int rowHandle, DropPosition position) => 
            (position != DropPosition.Append) ? ((position != DropPosition.After) ? rowHandle : (rowHandle + 1)) : this.GetItemsSource().Count;

        private IList GetItemsSource() => 
            (IList) this.listBox.ItemsSource;

        public void Insert(object[] objects, DropPointer pointer)
        {
            int insertIndex = this.GetInsertIndex(this.ConvertHandle(pointer.RowPointer.Handle), pointer.Position);
            this.GetItemsSource().InsertMultiple(objects, insertIndex);
        }

        public void Move(RowPointer[] rowPointers, DropPointer pointer)
        {
        }

        public void Remove(RowPointer[] rowPointers)
        {
            this.GetItemsSource().RemoveMultiple((from x in rowPointers select this.ConvertHandle(x.Handle)).ToArray<int>());
        }
    }
}

