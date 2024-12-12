namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class TabControlSelectionChangingEventArgs : CancelEventArgs
    {
        public TabControlSelectionChangingEventArgs(int oldSelectedIndex, int newSelectedIndex, object oldSelectedItem, object newSelectedItem)
        {
            this.OldSelectedIndex = oldSelectedIndex;
            this.NewSelectedIndex = newSelectedIndex;
            this.OldSelectedItem = oldSelectedItem;
            this.NewSelectedItem = newSelectedItem;
        }

        public int OldSelectedIndex { get; private set; }

        public int NewSelectedIndex { get; private set; }

        public object OldSelectedItem { get; private set; }

        public object NewSelectedItem { get; private set; }
    }
}

