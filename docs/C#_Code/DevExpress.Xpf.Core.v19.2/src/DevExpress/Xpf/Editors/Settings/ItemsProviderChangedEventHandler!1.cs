namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Data.Utils;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    public class ItemsProviderChangedEventHandler<TOwner> : WeakEventHandler<TOwner, ItemsProviderChangedEventArgs, ItemsProviderChangedEventHandler> where TOwner: class
    {
        private static Action<WeakEventHandler<TOwner, ItemsProviderChangedEventArgs, ItemsProviderChangedEventHandler>, object> action;
        private static Func<WeakEventHandler<TOwner, ItemsProviderChangedEventArgs, ItemsProviderChangedEventHandler>, ItemsProviderChangedEventHandler> create;

        static ItemsProviderChangedEventHandler()
        {
            ItemsProviderChangedEventHandler<TOwner>.action = delegate (WeakEventHandler<TOwner, ItemsProviderChangedEventArgs, ItemsProviderChangedEventHandler> h, object o) {
                ((IItemsProvider2) o).ItemsProviderChanged -= h.Handler;
            };
            ItemsProviderChangedEventHandler<TOwner>.create = h => new ItemsProviderChangedEventHandler(h.OnEvent);
        }

        public ItemsProviderChangedEventHandler(TOwner owner, Action<TOwner, object, ItemsProviderChangedEventArgs> onEventAction) : base(owner, onEventAction, ItemsProviderChangedEventHandler<TOwner>.action, ItemsProviderChangedEventHandler<TOwner>.create)
        {
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemsProviderChangedEventHandler<TOwner>.<>c <>9;

            static <>c()
            {
                ItemsProviderChangedEventHandler<TOwner>.<>c.<>9 = new ItemsProviderChangedEventHandler<TOwner>.<>c();
            }

            internal void <.cctor>b__3_0(WeakEventHandler<TOwner, ItemsProviderChangedEventArgs, ItemsProviderChangedEventHandler> h, object o)
            {
                ((IItemsProvider2) o).ItemsProviderChanged -= h.Handler;
            }

            internal ItemsProviderChangedEventHandler <.cctor>b__3_1(WeakEventHandler<TOwner, ItemsProviderChangedEventArgs, ItemsProviderChangedEventHandler> h) => 
                new ItemsProviderChangedEventHandler(h.OnEvent);
        }
    }
}

