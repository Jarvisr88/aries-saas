namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;

    internal class LazyReadonlyObservableCollection<T>
    {
        private readonly Action initialize;
        private ReadOnlyObservableCollection<T> readonlyCollection;

        public LazyReadonlyObservableCollection(Action initialize)
        {
            this.initialize = initialize;
        }

        public void EnsureCollection()
        {
            if (!this.CollectionCreated)
            {
                this.Collection = new ObservableCollectionCore<T>();
                this.readonlyCollection = new ReadOnlyObservableCollection<T>(this.Collection);
                this.initialize();
            }
        }

        public ObservableCollectionCore<T> Collection { get; private set; }

        public bool CollectionCreated =>
            this.Collection != null;

        public ReadOnlyObservableCollection<T> ReadonlyCollection
        {
            get
            {
                this.EnsureCollection();
                return this.readonlyCollection;
            }
        }
    }
}

