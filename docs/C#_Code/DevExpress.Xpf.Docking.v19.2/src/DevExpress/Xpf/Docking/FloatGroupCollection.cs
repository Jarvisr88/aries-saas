namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class FloatGroupCollection : BaseLockableCollection<FloatGroup>, IDisposable
    {
        private bool isDisposing;

        public void AddRange(FloatGroup[] items)
        {
            Array.ForEach<FloatGroup>(items, new Action<FloatGroup>(this.Add));
        }

        protected override void ClearItems()
        {
            FloatGroup[] array = new FloatGroup[base.Count];
            base.Items.CopyTo(array, 0);
            base.ClearItems();
            for (int i = 0; i < array.Length; i++)
            {
                this.OnItemRemoved(array[i]);
            }
        }

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.OnDisposing();
            }
            GC.SuppressFinalize(this);
        }

        protected override void InsertItem(int index, FloatGroup item)
        {
            base.InsertItem(index, item);
            this.OnItemAdded(item);
        }

        protected virtual void OnDisposing()
        {
            for (int i = 0; i < base.Count; i++)
            {
                base.Items[i].IsOpen = false;
            }
        }

        protected virtual void OnItemAdded(FloatGroup item)
        {
            if (item != null)
            {
                if (this.Owner != null)
                {
                    item.Manager = this.Owner;
                }
                item.OnOwnerCollectionChanged();
                item.IsRootGroup = true;
            }
        }

        protected virtual void OnItemRemoved(FloatGroup item)
        {
            if (item != null)
            {
                item.OnOwnerCollectionChanged();
                item.IsRootGroup = false;
            }
        }

        protected override void RemoveItem(int index)
        {
            FloatGroup item = base.Items[index];
            base.RemoveItem(index);
            this.OnItemRemoved(item);
        }

        protected override void SetItem(int index, FloatGroup item)
        {
            base.SetItem(index, item);
            this.OnItemAdded(item);
        }

        public FloatGroup[] ToArray()
        {
            FloatGroup[] array = new FloatGroup[base.Count];
            base.Items.CopyTo(array, 0);
            return array;
        }

        public BaseLayoutItem this[string name] =>
            Array.Find<BaseLayoutItem>(this.GetItems(), item => item.Name == name);

        internal DockLayoutManager Owner { get; set; }
    }
}

