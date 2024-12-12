namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using System;

    internal class PageCollectionBase<T> : ObservableRangeCollection<T> where T: PageViewModelBase
    {
        protected override void RemoveItem(int index)
        {
            T local = base[index];
            base.RemoveItem(index);
            local.Destroy();
        }
    }
}

