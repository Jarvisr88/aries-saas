namespace DevExpress.Xpf.Core.Native
{
    using System;

    [Flags]
    public enum ItemPropertyNotificationMode
    {
        public const ItemPropertyNotificationMode None = ItemPropertyNotificationMode.None;,
        public const ItemPropertyNotificationMode PropertyChanged = ItemPropertyNotificationMode.PropertyChanged;,
        public const ItemPropertyNotificationMode DependencyProperties = ItemPropertyNotificationMode.DependencyProperties;,
        public const ItemPropertyNotificationMode PropertyChanging = ItemPropertyNotificationMode.PropertyChanging;,
        public const ItemPropertyNotificationMode ErrorsChanged = ItemPropertyNotificationMode.ErrorsChanged;,
        public const ItemPropertyNotificationMode All = ItemPropertyNotificationMode.All;
    }
}

