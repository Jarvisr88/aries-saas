namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class ColumnContentChangedEventHandler<TOwner> : WeakEventHandler<TOwner, ColumnContentChangedEventArgs, ColumnContentChangedEventHandler> where TOwner: class
    {
        private static Func<WeakEventHandler<TOwner, ColumnContentChangedEventArgs, ColumnContentChangedEventHandler>, ColumnContentChangedEventHandler> action;

        static ColumnContentChangedEventHandler()
        {
            ColumnContentChangedEventHandler<TOwner>.action = h => new ColumnContentChangedEventHandler(h.OnEvent);
        }

        public ColumnContentChangedEventHandler(TOwner owner, Action<TOwner, object, ColumnContentChangedEventArgs> onEventAction, Action<WeakEventHandler<TOwner, ColumnContentChangedEventArgs, ColumnContentChangedEventHandler>, object> unsubscribe) : base(owner, onEventAction, unsubscribe, ColumnContentChangedEventHandler<TOwner>.action)
        {
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColumnContentChangedEventHandler<TOwner>.<>c <>9;

            static <>c()
            {
                ColumnContentChangedEventHandler<TOwner>.<>c.<>9 = new ColumnContentChangedEventHandler<TOwner>.<>c();
            }

            internal ColumnContentChangedEventHandler <.cctor>b__2_0(WeakEventHandler<TOwner, ColumnContentChangedEventArgs, ColumnContentChangedEventHandler> h) => 
                new ColumnContentChangedEventHandler(h.OnEvent);
        }
    }
}

