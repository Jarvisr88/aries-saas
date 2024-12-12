namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Data.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class CollectionViewMoveCurrentEventHandler<TOwner> : WeakEventHandler<TOwner, EventArgs, EventHandler> where TOwner: class
    {
        private static Action<WeakEventHandler<TOwner, EventArgs, EventHandler>, object> action;
        private static Func<WeakEventHandler<TOwner, EventArgs, EventHandler>, EventHandler> create;

        static CollectionViewMoveCurrentEventHandler()
        {
            CollectionViewMoveCurrentEventHandler<TOwner>.action = delegate (WeakEventHandler<TOwner, EventArgs, EventHandler> h, object o) {
                ((ICollectionView) o).CurrentChanged -= h.Handler;
            };
            CollectionViewMoveCurrentEventHandler<TOwner>.create = h => new EventHandler(h.OnEvent);
        }

        public CollectionViewMoveCurrentEventHandler(TOwner owner, Action<TOwner, object, EventArgs> onEventAction) : base(owner, onEventAction, CollectionViewMoveCurrentEventHandler<TOwner>.action, CollectionViewMoveCurrentEventHandler<TOwner>.create)
        {
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CollectionViewMoveCurrentEventHandler<TOwner>.<>c <>9;

            static <>c()
            {
                CollectionViewMoveCurrentEventHandler<TOwner>.<>c.<>9 = new CollectionViewMoveCurrentEventHandler<TOwner>.<>c();
            }

            internal void <.cctor>b__3_0(WeakEventHandler<TOwner, EventArgs, EventHandler> h, object o)
            {
                ((ICollectionView) o).CurrentChanged -= h.Handler;
            }

            internal EventHandler <.cctor>b__3_1(WeakEventHandler<TOwner, EventArgs, EventHandler> h) => 
                new EventHandler(h.OnEvent);
        }
    }
}

