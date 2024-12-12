namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Linq;

    internal sealed class OperatorMenuItemFactoryImplementation<TItem, TID> where TItem: class where TID: class
    {
        private readonly Func<TID, TItem> createItem;
        private OperatorMenuItemFactory<TItem, TID> factoryCore;

        public OperatorMenuItemFactoryImplementation(Func<TID, TItem> createItem)
        {
            Guard.ArgumentNotNull(createItem, "createItem");
            this.createItem = createItem;
        }

        private AvailableMenuItems<TItem, TID> CreateAvailableItems(AvailableMenuItemIdentities<TID> ids) => 
            new AvailableMenuItems<TItem, TID>(ids.Identities.Transform<TID, string, IdentifiedOperatorMenuItem<TID, TItem>>(id => new IdentifiedOperatorMenuItem<TID, TItem>(id, base.createItem(id))).ToArray<Tree<IdentifiedOperatorMenuItem<TID, TItem>, string>>(), ids.DefaultID);

        public OperatorMenuItemFactory<TItem, TID> Factory
        {
            get
            {
                this.factoryCore ??= new OperatorMenuItemFactory<TItem, TID>(new Func<AvailableMenuItemIdentities<TID>, AvailableMenuItems<TItem, TID>>(this.CreateAvailableItems), this.createItem);
                return this.factoryCore;
            }
        }
    }
}

