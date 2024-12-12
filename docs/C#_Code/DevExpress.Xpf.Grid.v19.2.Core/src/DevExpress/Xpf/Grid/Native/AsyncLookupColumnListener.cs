namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.FilteringUI;
    using DevExpress.Xpf.Editors.Helpers;
    using System;

    internal class AsyncLookupColumnListener
    {
        private readonly Action<object> itemUpdatedAction;

        private AsyncLookupColumnListener(Action<object> itemUpdatedAction)
        {
            this.itemUpdatedAction = itemUpdatedAction;
        }

        private void itemsProvider_ItemsProviderChanged(object sender, ItemsProviderChangedEventArgs e)
        {
            ItemsProviderRowLoadedEventArgs args = e as ItemsProviderRowLoadedEventArgs;
            if (args != null)
            {
                this.itemUpdatedAction(args.Value);
            }
        }

        private void Subscribe(IItemsProvider2 itemsProvider)
        {
            itemsProvider.ItemsProviderChanged += new ItemsProviderChangedEventHandler(this.itemsProvider_ItemsProviderChanged);
        }

        public static UnsubscribeAction Subscribe(IItemsProvider2 itemsProvider, Action<object> itemUpdatedAction)
        {
            AsyncLookupColumnListener listener = new AsyncLookupColumnListener(itemUpdatedAction);
            listener.Subscribe(itemsProvider);
            return delegate {
                listener.Unsubscribe(itemsProvider);
            };
        }

        private void Unsubscribe(IItemsProvider2 itemsProvider)
        {
            itemsProvider.ItemsProviderChanged -= new ItemsProviderChangedEventHandler(this.itemsProvider_ItemsProviderChanged);
        }
    }
}

