namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;

    public class AsyncServerModeDataControllerVisibleIndexCollection : DataControllerVisibleIndexCollection
    {
        public AsyncServerModeDataControllerVisibleIndexCollection(DataController controller);
        protected override int GetMaxCount();
        protected override List<GroupRowInfo> GetRootGroups();
    }
}

