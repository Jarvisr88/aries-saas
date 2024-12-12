namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public abstract class NamedMenuItem : MenuItemBase
    {
        internal NamedMenuItem(string displayName, ImageSource icon)
        {
            this.<DisplayName>k__BackingField = displayName;
            this.<Icon>k__BackingField = icon;
        }

        public string DisplayName { get; }

        public ImageSource Icon { get; }
    }
}

