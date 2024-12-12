namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public sealed class MenuItemGroup : NamedMenuItem
    {
        internal MenuItemGroup(string displayName, ImageSource icon, IList<MenuItemBase> children) : base(displayName, icon)
        {
            this.<Children>k__BackingField = children;
        }

        public IList<MenuItemBase> Children { get; }
    }
}

