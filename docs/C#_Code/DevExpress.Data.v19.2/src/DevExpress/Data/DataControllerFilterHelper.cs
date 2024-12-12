namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;

    public class DataControllerFilterHelper : FilterHelper
    {
        public DataControllerFilterHelper(DataController controller);

        public override VisibleListSourceRowCollection VisibleListSourceRows { get; }

        public DataController Controller { get; }
    }
}

