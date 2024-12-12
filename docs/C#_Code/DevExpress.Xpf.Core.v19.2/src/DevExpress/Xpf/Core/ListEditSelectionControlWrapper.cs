namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class ListEditSelectionControlWrapper : SelectionControlWrapper
    {
        public ListEditSelectionControlWrapper(ListBoxEdit view)
        {
            this.ListBox = view;
        }

        public override void ClearSelection()
        {
            this.ListBox.SelectedItems.Clear();
        }

        public override IList GetSelectedItems() => 
            this.ListBox.SelectedItems;

        private void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Action(e.NewItems, e.OldItems);
        }

        public override void SelectItem(object item)
        {
            this.ListBox.SelectedItems.Add(item);
        }

        public override void SubscribeSelectionChanged(Action<IList, IList> a)
        {
            this.Action = a;
            this.ListBox.SelectedItems.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SelectedItems_CollectionChanged);
        }

        public override void UnselectItem(object item)
        {
            this.ListBox.SelectedItems.Remove(item);
        }

        public override void UnsubscribeSelectionChanged()
        {
            this.ListBox.SelectedItems.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.SelectedItems_CollectionChanged);
        }

        private Action<IList, IList> Action { get; set; }

        private ListBoxEdit ListBox { get; set; }
    }
}

