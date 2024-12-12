namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.ObjectModel;

    public class DetailDescriptorCollection : ObservableCollection<DetailDescriptorBase>
    {
        private readonly MultiDetailDescriptorBase owner;

        public DetailDescriptorCollection(MultiDetailDescriptorBase owner)
        {
            this.owner = owner;
        }

        protected override void ClearItems()
        {
            foreach (DetailDescriptorBase base2 in this)
            {
                this.owner.OnDescriptorRemoved(base2);
            }
            base.ClearItems();
        }

        protected override void InsertItem(int index, DetailDescriptorBase item)
        {
            base.InsertItem(index, item);
            this.owner.OnDescriptorAdded(item);
        }

        protected override void RemoveItem(int index)
        {
            this.owner.OnDescriptorRemoved(base[index]);
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, DetailDescriptorBase item)
        {
            this.owner.OnDescriptorRemoved(base[index]);
            base.SetItem(index, item);
            this.owner.OnDescriptorAdded(item);
        }
    }
}

