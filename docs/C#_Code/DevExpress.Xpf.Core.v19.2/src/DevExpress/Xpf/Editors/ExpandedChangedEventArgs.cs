namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExpandedChangedEventArgs : EventArgs
    {
        public ExpandedChangedEventArgs(bool value)
        {
            this.IsExpanded = value;
        }

        public bool IsExpanded { get; set; }
    }
}

