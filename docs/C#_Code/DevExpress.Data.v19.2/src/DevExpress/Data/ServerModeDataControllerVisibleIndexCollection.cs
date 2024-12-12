namespace DevExpress.Data
{
    using System;

    public class ServerModeDataControllerVisibleIndexCollection : DataControllerVisibleIndexCollection
    {
        public ServerModeDataControllerVisibleIndexCollection(DataController controller);
        protected override int GetMaxCount();
    }
}

