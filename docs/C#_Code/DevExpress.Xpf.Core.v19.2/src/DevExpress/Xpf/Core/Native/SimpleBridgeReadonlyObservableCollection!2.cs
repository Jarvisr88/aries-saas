namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    public class SimpleBridgeReadonlyObservableCollection<T, Key> : ReadOnlyObservableCollection<T>, IWeakEventListener where T: class
    {
        private readonly ObservableCollection<T> coreCollection;
        private readonly Func<Key, T> cast;
        private readonly IList<Key> keys;

        private SimpleBridgeReadonlyObservableCollection(ObservableCollection<T> coreCollection, IList<Key> keys, Func<Key, T> cast);
        public static SimpleBridgeReadonlyObservableCollection<T, Key> Create(IList<Key> keys, Func<Key, T> cast);
        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e);
    }
}

