namespace DevExpress.Xpf.Editors.DataPager
{
    using System;
    using System.Runtime.CompilerServices;

    public class DataPagerPageIndexChangingEventArgs : DataPagerPageIndexChangedEventArgs
    {
        public DataPagerPageIndexChangingEventArgs(int oldValue, int newValue) : base(oldValue, newValue)
        {
        }

        public bool IsCancel { get; set; }
    }
}

