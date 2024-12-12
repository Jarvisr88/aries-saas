namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    public class DockLayoutManagerItemsCollection : BaseItemsCollection<BaseLayoutItem>
    {
        protected readonly DockLayoutManager Owner;

        public DockLayoutManagerItemsCollection(DockLayoutManager owner)
        {
            this.Owner = owner;
        }

        protected override void InvalidateItemsSource()
        {
            this.Owner.OnResetItemsSource(null);
        }

        protected override void OnAddToItemsSource(IEnumerable newItems, int startingIndex = 0)
        {
            this.Owner.OnAddToItemsSource(newItems, startingIndex);
        }

        protected override void OnCurrentChanged(object sender)
        {
            this.Owner.OnCurrentChanged(sender);
        }

        protected override void OnItemReplacedInItemsSource(IList oldItems, IList newItems, int newStartingIndex)
        {
            this.Owner.OnItemReplacedInItemsSource(oldItems, newItems, newStartingIndex);
        }

        protected override void OnRemoveFromItemsSource(IEnumerable oldItems)
        {
            this.Owner.OnRemoveFromItemsSource(oldItems);
        }

        protected override void OnResetItemsSource()
        {
            this.Owner.OnResetItemsSource(base.ItemsSource);
        }
    }
}

