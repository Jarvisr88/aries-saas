namespace DevExpress.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutUpgradeEventArgs : EventArgs
    {
        public LayoutUpgradeEventArgs(string previousVersion)
        {
            this.PreviousVersion = previousVersion;
        }

        public LayoutUpgradeEventArgs(string previousVersion, object[] newHiddenItems)
        {
            this.PreviousVersion = previousVersion;
            this.NewHiddenItems = newHiddenItems;
        }

        public string PreviousVersion { get; private set; }

        public object[] NewHiddenItems { get; private set; }
    }
}

