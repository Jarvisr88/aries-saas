namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;

    public class DataControllerGroupRowInfoCollection : GroupRowInfoCollection
    {
        public DataControllerGroupRowInfoCollection(DataController controller);

        protected override DataColumnSortInfoCollection SortInfo { get; }

        public override VisibleListSourceRowCollection VisibleListSourceRows { get; }

        protected DataController Controller { get; }
    }
}

