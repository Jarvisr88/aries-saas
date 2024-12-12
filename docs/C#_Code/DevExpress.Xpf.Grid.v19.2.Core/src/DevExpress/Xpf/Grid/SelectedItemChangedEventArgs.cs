namespace DevExpress.Xpf.Grid
{
    using System;

    public class SelectedItemChangedEventArgs : CurrentItemChangedEventArgs
    {
        public SelectedItemChangedEventArgs(DataControlBase source, object oldItem, object newItem) : base(source, oldItem, newItem)
        {
        }
    }
}

