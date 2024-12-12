namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class OperatorMenuItemFactory<TItem, TID> where TItem: class where TID: class
    {
        public OperatorMenuItemFactory(Func<AvailableMenuItemIdentities<TID>, AvailableMenuItems<TItem, TID>> createAvailableItems, Func<TID, TItem> createNonAvailableItem)
        {
            Guard.ArgumentNotNull(createAvailableItems, "createAvailableItems");
            Guard.ArgumentNotNull(createNonAvailableItem, "createNonAvailableItem");
            this.<CreateAvailableItems>k__BackingField = createAvailableItems;
            this.<CreateNonAvailableItem>k__BackingField = createNonAvailableItem;
        }

        public Func<AvailableMenuItemIdentities<TID>, AvailableMenuItems<TItem, TID>> CreateAvailableItems { get; }

        public Func<TID, TItem> CreateNonAvailableItem { get; }
    }
}

