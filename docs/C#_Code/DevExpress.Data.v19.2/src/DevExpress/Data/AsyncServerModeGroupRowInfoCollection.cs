namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;

    public class AsyncServerModeGroupRowInfoCollection : ServerDataControllerGroupRowInfoCollection
    {
        private List<GroupRowInfo> rootGroups;

        public AsyncServerModeGroupRowInfoCollection(AsyncServerModeDataController controller);
        protected override void ClearItems();
        internal ServerModeGroupRowInfo FindGroup(ListSourceGroupInfo sourceGroupInfo);
        internal void UpdateRootGroups();

        public override int RootGroupCount { get; }

        internal List<GroupRowInfo> RootGroups { get; }
    }
}

