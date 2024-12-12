namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public abstract class NamedItemNotificationCollection<T> : NotificationCollection<T>
    {
        private readonly Dictionary<string, T> nameHash;

        protected NamedItemNotificationCollection()
        {
            this.nameHash = new Dictionary<string, T>();
        }

        protected NamedItemNotificationCollection(DXCollectionUniquenessProviderType uniquenessProviderType) : base(uniquenessProviderType)
        {
            this.nameHash = new Dictionary<string, T>();
        }

        protected abstract string GetItemName(T item);
        protected override void OnClearComplete()
        {
            base.OnClearComplete();
            this.NameHash.Clear();
        }

        protected override void OnInsertComplete(int index, T value)
        {
            base.OnInsertComplete(index, value);
            string itemName = this.GetItemName(value);
            if (this.NameHash.ContainsKey(itemName))
            {
                this.NameHash[itemName] = value;
            }
            else
            {
                this.NameHash.Add(itemName, value);
            }
        }

        protected override void OnRemoveComplete(int index, T value)
        {
            base.OnRemoveComplete(index, value);
            string itemName = this.GetItemName(value);
            if (this.NameHash.ContainsKey(itemName))
            {
                this.NameHash.Remove(itemName);
            }
        }

        protected internal Dictionary<string, T> NameHash =>
            this.nameHash;

        public T this[string name]
        {
            get
            {
                T local;
                if (this.NameHash.TryGetValue(name, out local))
                {
                    return local;
                }
                return default(T);
            }
        }
    }
}

