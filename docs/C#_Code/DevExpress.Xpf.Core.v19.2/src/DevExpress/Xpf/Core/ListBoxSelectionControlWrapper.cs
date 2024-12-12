namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class ListBoxSelectionControlWrapper : SelectionControlWrapper
    {
        public ListBoxSelectionControlWrapper(System.Windows.Controls.ListBox view)
        {
            this.ListBox = view;
        }

        public override void ClearSelection()
        {
            this.ListBox.SelectedItems.Clear();
        }

        public override IList GetSelectedItems() => 
            this.ListBox.SelectedItems;

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Action(e.AddedItems, e.RemovedItems);
        }

        public override void SelectItem(object item)
        {
            this.ListBox.SelectedItems.Add(item);
        }

        public override void SubscribeSelectionChanged(Action<IList, IList> a)
        {
            this.Action = a;
            this.ListBox.SelectionChanged += new SelectionChangedEventHandler(this.ListBox_SelectionChanged);
        }

        public override void UnselectItem(object item)
        {
            this.ListBox.SelectedItems.Remove(item);
        }

        public override void UnsubscribeSelectionChanged()
        {
            this.ListBox.SelectionChanged -= new SelectionChangedEventHandler(this.ListBox_SelectionChanged);
        }

        private System.Windows.Controls.ListBox ListBox { get; set; }

        private Action<IList, IList> Action { get; set; }
    }
}

