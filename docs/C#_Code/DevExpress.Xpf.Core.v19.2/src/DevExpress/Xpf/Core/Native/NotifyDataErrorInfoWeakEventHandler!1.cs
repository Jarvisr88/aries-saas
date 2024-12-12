namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class NotifyDataErrorInfoWeakEventHandler<TOwner> : WeakEventHandler<TOwner, DataErrorsChangedEventArgs, EventHandler<DataErrorsChangedEventArgs>> where TOwner: class
    {
        private static Action<WeakEventHandler<TOwner, DataErrorsChangedEventArgs, EventHandler<DataErrorsChangedEventArgs>>, object> action;
        private static Func<WeakEventHandler<TOwner, DataErrorsChangedEventArgs, EventHandler<DataErrorsChangedEventArgs>>, EventHandler<DataErrorsChangedEventArgs>> create;

        static NotifyDataErrorInfoWeakEventHandler();
        public NotifyDataErrorInfoWeakEventHandler(TOwner owner, Action<TOwner, object, DataErrorsChangedEventArgs> onEventAction);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NotifyDataErrorInfoWeakEventHandler<TOwner>.<>c <>9;

            static <>c();
            internal void <.cctor>b__3_0(WeakEventHandler<TOwner, DataErrorsChangedEventArgs, EventHandler<DataErrorsChangedEventArgs>> h, object o);
            internal EventHandler<DataErrorsChangedEventArgs> <.cctor>b__3_1(WeakEventHandler<TOwner, DataErrorsChangedEventArgs, EventHandler<DataErrorsChangedEventArgs>> h);
        }
    }
}

