namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class TabControlTabMovedEventArgs : EventArgs
    {
        public TabControlTabMovedEventArgs(object item, int oldIndex, int newIndex)
        {
            this.Item = item;
            this.OldTabIndex = oldIndex;
            this.NewTabIndex = newIndex;
        }

        public object Item { get; private set; }

        public int OldTabIndex { get; private set; }

        public int NewTabIndex { get; private set; }
    }
}

