namespace DevExpress.Data.Utils
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class CollectionChangedWeakEventHandler<TOwner> : WeakEventHandler<TOwner, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler> where TOwner: class
    {
        private static Action<WeakEventHandler<TOwner, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler>, object> action;
        private static Func<WeakEventHandler<TOwner, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler>, NotifyCollectionChangedEventHandler> create;

        static CollectionChangedWeakEventHandler();
        public CollectionChangedWeakEventHandler(TOwner owner, Action<TOwner, object, NotifyCollectionChangedEventArgs> onEventAction);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CollectionChangedWeakEventHandler<TOwner>.<>c <>9;

            static <>c();
            internal void <.cctor>b__3_0(WeakEventHandler<TOwner, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler> h, object o);
            internal NotifyCollectionChangedEventHandler <.cctor>b__3_1(WeakEventHandler<TOwner, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler> h);
        }
    }
}

