namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class AvailableMenuItems<TItem, TID>
    {
        public AvailableMenuItems(Tree<IdentifiedOperatorMenuItem<TID, TItem>, string>[] items, TID defaultIdentity)
        {
            this.<Items>k__BackingField = items;
            this.<DefaultID>k__BackingField = defaultIdentity;
        }

        public Tree<IdentifiedOperatorMenuItem<TID, TItem>, string>[] Items { get; }

        public TID DefaultID { get; }
    }
}

