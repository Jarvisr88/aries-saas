namespace DevExpress.Xpf.Editors.DataPager
{
    using System;
    using System.Runtime.CompilerServices;

    public class DataPagerPageIndexChangedEventArgs : EventArgs
    {
        public DataPagerPageIndexChangedEventArgs(int oldValue, int newValue)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }

        public int OldValue { get; private set; }

        public int NewValue { get; private set; }
    }
}

