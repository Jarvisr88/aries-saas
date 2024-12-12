namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.ObjectModel;

    public class GridRuntimeStringCollection : ObservableCollection<RuntimeStringIdInfo>
    {
        protected override void InsertItem(int index, RuntimeStringIdInfo item)
        {
            if (base.Contains(item))
            {
                throw new ArgumentException("Element with such stringId already exists in collection");
            }
            base.InsertItem(index, item);
        }
    }
}

