namespace DevExpress.Data.Utils
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ListChangedWeakEventHandler<TOwner> : WeakEventHandler<TOwner, ListChangedEventArgs, ListChangedEventHandler> where TOwner: class
    {
        private static Action<WeakEventHandler<TOwner, ListChangedEventArgs, ListChangedEventHandler>, object> action;
        private static Func<WeakEventHandler<TOwner, ListChangedEventArgs, ListChangedEventHandler>, ListChangedEventHandler> create;

        static ListChangedWeakEventHandler();
        public ListChangedWeakEventHandler(TOwner owner, Action<TOwner, object, ListChangedEventArgs> onEventAction);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListChangedWeakEventHandler<TOwner>.<>c <>9;

            static <>c();
            internal void <.cctor>b__3_0(WeakEventHandler<TOwner, ListChangedEventArgs, ListChangedEventHandler> h, object o);
            internal ListChangedEventHandler <.cctor>b__3_1(WeakEventHandler<TOwner, ListChangedEventArgs, ListChangedEventHandler> h);
        }
    }
}

