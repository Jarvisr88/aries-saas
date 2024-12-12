namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutUpgradeEventArgs : RoutedEventArgs
    {
        public LayoutUpgradeEventArgs(object source, string restoredVersion) : base(DXSerializer.LayoutUpgradeEvent, source)
        {
            this.RestoredVersion = restoredVersion;
        }

        public string RestoredVersion { get; private set; }
    }
}

