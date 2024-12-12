namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class AvailableMenuItemIdentities<TID>
    {
        public AvailableMenuItemIdentities(Tree<TID, string>[] identities, TID defaultIdentity)
        {
            this.<Identities>k__BackingField = identities;
            this.<DefaultID>k__BackingField = defaultIdentity;
        }

        public Tree<TID, string>[] Identities { get; }

        public TID DefaultID { get; }
    }
}

