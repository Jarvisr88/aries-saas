namespace DevExpress.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutAllowEventArgs : LayoutUpgradeEventArgs
    {
        public LayoutAllowEventArgs(string previousVersion) : base(previousVersion)
        {
            this.Allow = true;
        }

        public bool Allow { get; set; }
    }
}

