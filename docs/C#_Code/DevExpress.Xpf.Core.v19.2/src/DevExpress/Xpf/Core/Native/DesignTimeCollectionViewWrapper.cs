namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Linq;
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;

    internal class DesignTimeCollectionViewWrapper : IDesignTimeDataSource, ICollectionViewWrapper, ICollectionView, IEnumerable, INotifyCollectionChanged
    {
        private readonly ICollectionView view;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event EventHandler CurrentChanged;

        public event CurrentChangingEventHandler CurrentChanging;

        public DesignTimeCollectionViewWrapper(ICollectionView view);
        public bool Contains(object item);
        public IDisposable DeferRefresh();
        public IEnumerator GetEnumerator();
        public bool MoveCurrentTo(object item);
        public bool MoveCurrentToFirst();
        public bool MoveCurrentToLast();
        public bool MoveCurrentToNext();
        public bool MoveCurrentToPosition(int position);
        public bool MoveCurrentToPrevious();
        public void Refresh();

        public bool CanFilter { get; }

        public bool CanGroup { get; }

        public bool CanSort { get; }

        public CultureInfo Culture { get; set; }

        public object CurrentItem { get; }

        public int CurrentPosition { get; }

        public Predicate<object> Filter { get; set; }

        public ObservableCollection<GroupDescription> GroupDescriptions { get; }

        public ReadOnlyObservableCollection<object> Groups { get; }

        public bool IsCurrentAfterLast { get; }

        public bool IsCurrentBeforeFirst { get; }

        public bool IsEmpty { get; }

        public SortDescriptionCollection SortDescriptions { get; }

        public IEnumerable SourceCollection { get; }

        ICollectionView ICollectionViewWrapper.WrappedView { get; }
    }
}

