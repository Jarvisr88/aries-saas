namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.ObjectModel;
    using System.Reflection;

    public class ReadOnlyGridSortInfoCollection : ReadOnlyObservableCollection<GridSortInfo>
    {
        public ReadOnlyGridSortInfoCollection(ObservableCollection<GridSortInfo> list) : base(list)
        {
        }

        public GridSortInfo this[string name] =>
            GridSortInfo.GetSortInfoByFieldName(this, name);
    }
}

