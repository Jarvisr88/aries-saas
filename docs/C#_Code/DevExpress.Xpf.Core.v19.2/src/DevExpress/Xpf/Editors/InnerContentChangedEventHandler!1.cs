namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    public class InnerContentChangedEventHandler<TOwner> : WeakEventHandler<TOwner, EventArgs, EventHandler> where TOwner: class
    {
        private static Action<WeakEventHandler<TOwner, EventArgs, EventHandler>, object> unsibscribe;
        private static Func<WeakEventHandler<TOwner, EventArgs, EventHandler>, EventHandler> action;

        static InnerContentChangedEventHandler()
        {
            InnerContentChangedEventHandler<TOwner>.unsibscribe = delegate (WeakEventHandler<TOwner, EventArgs, EventHandler> h, object o) {
                ((INotifyContentChanged) o).ContentChanged -= h.Handler;
            };
            InnerContentChangedEventHandler<TOwner>.action = h => new EventHandler(h.OnEvent);
        }

        public InnerContentChangedEventHandler(TOwner owner, Action<TOwner, object, EventArgs> onEventAction) : base(owner, onEventAction, InnerContentChangedEventHandler<TOwner>.unsibscribe, InnerContentChangedEventHandler<TOwner>.action)
        {
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InnerContentChangedEventHandler<TOwner>.<>c <>9;

            static <>c()
            {
                InnerContentChangedEventHandler<TOwner>.<>c.<>9 = new InnerContentChangedEventHandler<TOwner>.<>c();
            }

            internal void <.cctor>b__3_0(WeakEventHandler<TOwner, EventArgs, EventHandler> h, object o)
            {
                ((INotifyContentChanged) o).ContentChanged -= h.Handler;
            }

            internal EventHandler <.cctor>b__3_1(WeakEventHandler<TOwner, EventArgs, EventHandler> h) => 
                new EventHandler(h.OnEvent);
        }
    }
}

