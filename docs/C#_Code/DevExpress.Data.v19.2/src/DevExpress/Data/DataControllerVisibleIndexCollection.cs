namespace DevExpress.Data
{
    using System;

    public class DataControllerVisibleIndexCollection : VisibleIndexCollection
    {
        public DataControllerVisibleIndexCollection(DataController controller);

        protected DataController Controller { get; }

        protected internal override GroupRowInfoCollection GroupInfo { get; }
    }
}

