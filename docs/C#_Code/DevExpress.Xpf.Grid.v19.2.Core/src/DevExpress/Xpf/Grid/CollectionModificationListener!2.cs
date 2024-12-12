namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class CollectionModificationListener<TInsert, TItem>
    {
        private HashSet<TItem> removedSet;
        private Dictionary<TItem, TInsert> insertions;
        private readonly Func<TInsert, TItem> mapInsertion;
        private bool enableMinimizationCore;

        public CollectionModificationListener(Func<TInsert, TItem> mapInsertion)
        {
            this.removedSet = new HashSet<TItem>();
            this.insertions = new Dictionary<TItem, TInsert>();
            this.enableMinimizationCore = true;
            if (mapInsertion == null)
            {
                throw new ArgumentNullException();
            }
            this.mapInsertion = mapInsertion;
        }

        public void Add(TInsert v)
        {
            TItem newItem = this.mapInsertion(v);
            if (!this.TryMinimizeModification(() => ((CollectionModificationListener<TInsert, TItem>) this).removedSet.Remove(newItem)))
            {
                this.insertions.Add(newItem, v);
            }
        }

        private ReadOnlyCollection<T> CreateReadOnlyCollection<T>(IEnumerable<T> set) => 
            new ReadOnlyCollection<T>(this.IsDirty ? Enumerable.Empty<T>().ToList<T>() : set.ToList<T>());

        public void Remove(TItem v)
        {
            if (!this.TryMinimizeModification(() => ((CollectionModificationListener<TInsert, TItem>) this).insertions.Remove(v)))
            {
                this.removedSet.Add(v);
            }
        }

        public void Reset()
        {
            this.removedSet.Clear();
            this.insertions.Clear();
            this.IsDirty = false;
        }

        private bool TryMinimizeModification(Func<bool> minimizationAction) => 
            !this.EnableMinimization ? false : minimizationAction();

        public ReadOnlyCollection<TItem> RemovedItems =>
            this.CreateReadOnlyCollection<TItem>(this.removedSet);

        public ReadOnlyCollection<TInsert> AddedItems =>
            this.CreateReadOnlyCollection<TInsert>(this.insertions.Values);

        public bool IsDirty { get; set; }

        public bool EnableMinimization
        {
            get => 
                this.enableMinimizationCore;
            set => 
                this.enableMinimizationCore = value;
        }
    }
}

