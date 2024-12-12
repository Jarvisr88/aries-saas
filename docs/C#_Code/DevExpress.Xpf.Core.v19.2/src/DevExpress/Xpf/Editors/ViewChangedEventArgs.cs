namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ViewChangedEventArgs : RoutedEventArgs
    {
        public ViewChangedEventArgs(bool isIntermediate)
        {
            this.IsIntermediate = isIntermediate;
        }

        public bool IsIntermediate { get; private set; }
    }
}

