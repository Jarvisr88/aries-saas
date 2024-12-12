namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class OperatorMenuItemsSubstitutionInfo<T>
    {
        public OperatorMenuItemsSubstitutionInfo(IList<T> list, T defaultItem)
        {
            this.<List>k__BackingField = list;
            this.<DefaultItem>k__BackingField = defaultItem;
        }

        public IList<T> List { get; }

        public T DefaultItem { get; }
    }
}

