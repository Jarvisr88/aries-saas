namespace DevExpress.Data.Utils
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class PropertyChangedWeakEventHandler<TOwner> : WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler> where TOwner: class
    {
        private static Action<WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler>, object> action;
        private static Func<WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler>, PropertyChangedEventHandler> create;

        static PropertyChangedWeakEventHandler();
        public PropertyChangedWeakEventHandler(TOwner owner, Action<TOwner, object, PropertyChangedEventArgs> onEventAction);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PropertyChangedWeakEventHandler<TOwner>.<>c <>9;

            static <>c();
            internal void <.cctor>b__3_0(WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler> h, object o);
            internal PropertyChangedEventHandler <.cctor>b__3_1(WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler> h);
        }
    }
}

