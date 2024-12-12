namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class CanExecuteChangedEventHandler<TOwner> : WeakEventHandler<TOwner, EventArgs, EventHandler> where TOwner: class
    {
        private static Action<WeakEventHandler<TOwner, EventArgs, EventHandler>, object> action;
        private static Func<WeakEventHandler<TOwner, EventArgs, EventHandler>, EventHandler> create;

        static CanExecuteChangedEventHandler();
        public CanExecuteChangedEventHandler(TOwner owner, Action<TOwner, object, EventArgs> onEventAction);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CanExecuteChangedEventHandler<TOwner>.<>c <>9;

            static <>c();
            internal void <.cctor>b__3_0(WeakEventHandler<TOwner, EventArgs, EventHandler> h, object o);
            internal EventHandler <.cctor>b__3_1(WeakEventHandler<TOwner, EventArgs, EventHandler> h);
        }
    }
}

