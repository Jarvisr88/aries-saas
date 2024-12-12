namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class ApplicationJumpItemCollection : IList<ApplicationJumpItem>, ICollection<ApplicationJumpItem>, IEnumerable<ApplicationJumpItem>, IEnumerable, IList, ICollection
    {
        private IApplicationJumpListImplementation implementation;
        private object syncRoot = new object();

        protected ApplicationJumpItemCollection(IApplicationJumpListImplementation implementation)
        {
            if (implementation == null)
            {
                throw new ArgumentNullException("implementation");
            }
            this.implementation = implementation;
            this.SourceItems = new FreezableCollection<ApplicationJumpItem>();
        }

        public void Add(ApplicationJumpItem item)
        {
            this.implementation.AddItem(ApplicationJumpItem.GetItemInfo(item));
            this.SourceItems.Add(item);
        }

        public void Clear()
        {
            this.implementation.ClearItems();
            this.SourceItems.Clear();
        }

        public bool Contains(ApplicationJumpItem item) => 
            this.implementation.ContainsItem(ApplicationJumpItem.GetItemInfo(item));

        public void CopyTo(ApplicationJumpItem[] array, int arrayIndex)
        {
            foreach (ApplicationJumpItemInfo info in this.implementation.GetItems())
            {
                array[arrayIndex] = ApplicationJumpItem.GetItem(info);
                arrayIndex++;
            }
        }

        public IEnumerator<ApplicationJumpItem> GetEnumerator() => 
            this.implementation.GetItems().Select<ApplicationJumpItemInfo, ApplicationJumpItem>(new Func<ApplicationJumpItemInfo, ApplicationJumpItem>(ApplicationJumpItem.GetItem)).GetEnumerator();

        public int IndexOf(ApplicationJumpItem item) => 
            this.implementation.IndexOfItem(ApplicationJumpItem.GetItemInfo(item));

        public void Insert(int index, ApplicationJumpItem item)
        {
            this.implementation.InsertItem(index, ApplicationJumpItem.GetItemInfo(item));
            this.SourceItems.Insert(index, item);
        }

        public bool Remove(ApplicationJumpItem item)
        {
            this.SourceItems.Remove(item);
            return this.implementation.RemoveItem(ApplicationJumpItem.GetItemInfo(item));
        }

        public void RemoveAt(int index)
        {
            this.implementation.RemoveItemAt(index);
            this.SourceItems.RemoveAt(index);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo((ApplicationJumpItem[]) array, index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        int IList.Add(object value)
        {
            this.Add((ApplicationJumpItem) value);
            return (this.Count - 1);
        }

        bool IList.Contains(object value) => 
            this.Contains((ApplicationJumpItem) value);

        int IList.IndexOf(object value) => 
            this.IndexOf((ApplicationJumpItem) value);

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (ApplicationJumpItem) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((ApplicationJumpItem) value);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public FreezableCollection<ApplicationJumpItem> SourceItems { get; private set; }

        public ApplicationJumpItem this[int index]
        {
            get => 
                ApplicationJumpItem.GetItem(this.implementation.GetItem(index));
            set => 
                this.implementation.SetItem(index, ApplicationJumpItem.GetItemInfo(value));
        }

        public int Count =>
            this.implementation.ItemsCount();

        public bool IsReadOnly =>
            false;

        object IList.this[int index]
        {
            get => 
                this[index];
            set => 
                this[index] = (ApplicationJumpItem) value;
        }

        bool IList.IsFixedSize =>
            false;

        bool IList.IsReadOnly =>
            this.IsReadOnly;

        bool ICollection.IsSynchronized =>
            false;

        object ICollection.SyncRoot =>
            this.syncRoot;
    }
}

