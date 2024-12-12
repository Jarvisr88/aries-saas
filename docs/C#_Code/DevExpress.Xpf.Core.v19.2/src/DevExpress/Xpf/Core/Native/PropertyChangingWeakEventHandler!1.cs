namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class PropertyChangingWeakEventHandler<TOwner> : WeakEventHandler<TOwner, PropertyChangingEventArgs, PropertyChangingEventHandler> where TOwner: class
    {
        private static Action<WeakEventHandler<TOwner, PropertyChangingEventArgs, PropertyChangingEventHandler>, object> action;
        private static Func<WeakEventHandler<TOwner, PropertyChangingEventArgs, PropertyChangingEventHandler>, PropertyChangingEventHandler> create;

        static PropertyChangingWeakEventHandler();
        public PropertyChangingWeakEventHandler(TOwner owner, Action<TOwner, object, PropertyChangingEventArgs> onEventAction);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PropertyChangingWeakEventHandler<TOwner>.<>c <>9;

            static <>c();
            internal void <.cctor>b__3_0(WeakEventHandler<TOwner, PropertyChangingEventArgs, PropertyChangingEventHandler> h, object o);
            internal PropertyChangingEventHandler <.cctor>b__3_1(WeakEventHandler<TOwner, PropertyChangingEventArgs, PropertyChangingEventHandler> h);
        }
    }
}

