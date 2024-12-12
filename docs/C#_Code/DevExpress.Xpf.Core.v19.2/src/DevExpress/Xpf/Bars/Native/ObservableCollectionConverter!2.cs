namespace DevExpress.Xpf.Bars.Native
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    public class ObservableCollectionConverter<TResult> : ObservableCollection<TResult>
    {
        private Func<TSource, TResult> selector;
        private IEnumerable source;

        protected internal virtual void OnAdd(int p, IList list);
        protected internal virtual void OnRemove(int p, IList list);
        protected internal virtual void OnReset();
        protected internal virtual void OnSelectorChanged(Func<TSource, TResult> oldValue);
        protected internal virtual void OnSourceChanged(IEnumerable oldValue);
        protected internal virtual void OnSourceChanging(IEnumerable newValue);
        private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        private void OnSourceListChanged(object sender, ListChangedEventArgs e);
        protected internal virtual void SubscribeSourceCollectionChanged();
        protected internal virtual void SubscribeSourceCollectionChanged(NotifyCollectionChangedEventHandler handler);
        protected internal virtual void SubscribeSourceListChanged();
        protected internal virtual void SubscribeSourceListChanged(ListChangedEventHandler handler);
        protected internal virtual void UnsubscribeSourceCollectionChanged();
        protected internal virtual void UnsubscribeSourceCollectionChanged(NotifyCollectionChangedEventHandler handler);
        protected internal virtual void UnsubscribeSourceListChanged();
        protected internal virtual void UnsubscribeSourceListChanged(ListChangedEventHandler handler);

        public Func<TSource, TResult> Selector { get; set; }

        public IEnumerable Source { get; set; }
    }
}

