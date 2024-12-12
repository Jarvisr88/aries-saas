namespace DevExpress.Data
{
    using DevExpress.Data.Details;
    using System;

    public class ServerModeMasterRowInfoCollection : MasterRowInfoCollection
    {
        public ServerModeMasterRowInfoCollection(DataController controller);
        public override MasterRowInfo Find(int listSourceRow);
    }
}

