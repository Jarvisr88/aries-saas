namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;

    public abstract class BandCollectionBase<T> : ObservableCollectionCore<T>, ISupportGetCachedIndex<T>
    {
        private readonly ICollectionOwner owner;

        public BandCollectionBase(ICollectionOwner owner)
        {
            this.owner = owner;
        }

        protected override void ClearItems()
        {
            for (int i = 0; i < base.Count; i++)
            {
                this.owner.OnRemoveItem(base[i]);
                this.ResetItem(base[i]);
            }
            base.ClearItems();
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            this.owner.OnInsertItem(item);
        }

        protected override void RemoveItem(int index)
        {
            T item = base[index];
            this.owner.OnRemoveItem(item);
            this.ResetItem(item);
            base.RemoveItem(index);
        }

        private void ResetItem(T item)
        {
            BandBase base1 = item as BandBase;
            if (base1 == null)
            {
                BandBase local1 = base1;
            }
            else
            {
                base1.ResetBand();
            }
        }
    }
}

