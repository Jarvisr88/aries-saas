namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    public abstract class UniqueItemsCache<T> : ISupportsSizeOf where T: ICloneable<T>, ISupportsSizeOf
    {
        private readonly List<T> items;
        private Dictionary<T, int> itemDictionary;
        protected IDocumentModel workbook;
        private DXCollectionUniquenessProviderType uniquenessProviderType;

        protected UniqueItemsCache(IDocumentModelUnitConverter unitConverter)
        {
            this.uniquenessProviderType = DXCollectionUniquenessProviderType.MinimizeMemoryUsage;
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.items = new List<T>();
            this.InitItems(unitConverter);
        }

        protected UniqueItemsCache(IDocumentModelUnitConverter unitConverter, IDocumentModel workbook)
        {
            this.uniquenessProviderType = DXCollectionUniquenessProviderType.MinimizeMemoryUsage;
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            Guard.ArgumentNotNull(workbook, "workbook");
            this.items = new List<T>();
            this.workbook = workbook;
            this.InitItems(unitConverter);
        }

        protected UniqueItemsCache(IDocumentModelUnitConverter unitConverter, DXCollectionUniquenessProviderType uniquenessProviderType)
        {
            this.uniquenessProviderType = DXCollectionUniquenessProviderType.MinimizeMemoryUsage;
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.items = new List<T>();
            this.UniquenessProviderType = uniquenessProviderType;
            this.InitItems(unitConverter);
        }

        public int AddItem(T item)
        {
            int num = this.LookupItem(item);
            return ((num >= 0) ? num : this.AddItemCore(item));
        }

        protected virtual int AddItemCore(T item) => 
            (item != null) ? this.AppendItem(item.Clone()) : -1;

        protected int AppendItem(T item)
        {
            int count = this.Count;
            this.items.Add(item);
            if (this.uniquenessProviderType == DXCollectionUniquenessProviderType.MaximizePerformance)
            {
                this.itemDictionary.Add(item, count);
            }
            return count;
        }

        public virtual void CopyFrom(UniqueItemsCache<T> source)
        {
            this.items.Clear();
            if (this.itemDictionary != null)
            {
                this.itemDictionary.Clear();
            }
            int num = 0;
            foreach (T local in source.Items)
            {
                T item = local.Clone();
                this.items.Add(item);
                if (this.uniquenessProviderType == DXCollectionUniquenessProviderType.MaximizePerformance)
                {
                    this.itemDictionary.Add(item, num);
                }
                num++;
            }
        }

        protected abstract T CreateDefaultItem(IDocumentModelUnitConverter unitConverter);
        protected internal virtual Dictionary<T, int> CreateDictionary() => 
            new Dictionary<T, int>();

        public int GetItemIndex(T item)
        {
            lock (cache)
            {
                int num = this.LookupItem(item);
                return ((num >= 0) ? num : this.AddItemCore(item));
            }
        }

        protected virtual void InitItems(IDocumentModelUnitConverter unitConverter)
        {
            T item = this.CreateDefaultItem(unitConverter);
            if (item != null)
            {
                this.AppendItem(item);
            }
        }

        public bool IsIndexValid(int index) => 
            (index >= 0) && (index < this.items.Count);

        protected int LookupItem(T item)
        {
            int num;
            return ((this.uniquenessProviderType != DXCollectionUniquenessProviderType.MaximizePerformance) ? this.items.IndexOf(item) : (!this.itemDictionary.TryGetValue(item, out num) ? -1 : num));
        }

        public int SizeOf()
        {
            int num = ObjectSizeHelper.CalculateApproxObjectSize32(this);
            int count = this.Count;
            for (int i = 0; i < count; i++)
            {
                T local = this[i];
                num += local.SizeOf();
            }
            return num;
        }

        private void SwitchToDictionaryMethod()
        {
            this.itemDictionary = this.CreateDictionary();
            this.uniquenessProviderType = DXCollectionUniquenessProviderType.MaximizePerformance;
            int count = this.Count;
            for (int i = 0; i < count; i++)
            {
                this.itemDictionary.Add(this.items[i], i);
            }
        }

        private void SwitchToIndexOfMethod()
        {
            this.itemDictionary = null;
            this.uniquenessProviderType = DXCollectionUniquenessProviderType.MinimizeMemoryUsage;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int UnsecureAddItem(T item) => 
            this.AddItemCore(item);

        public T this[int index] =>
            this.items[index];

        public T DefaultItem =>
            this.items[0];

        public int Count =>
            this.items.Count;

        protected List<T> Items =>
            this.items;

        protected Dictionary<T, int> ItemDictionary =>
            this.itemDictionary;

        protected DXCollectionUniquenessProviderType UniquenessProviderType
        {
            get => 
                this.uniquenessProviderType;
            set
            {
                value ??= DXCollectionUniquenessProviderType.MinimizeMemoryUsage;
                if (this.uniquenessProviderType != value)
                {
                    if (value == DXCollectionUniquenessProviderType.MaximizePerformance)
                    {
                        this.SwitchToDictionaryMethod();
                    }
                    else
                    {
                        this.SwitchToIndexOfMethod();
                    }
                }
            }
        }
    }
}

