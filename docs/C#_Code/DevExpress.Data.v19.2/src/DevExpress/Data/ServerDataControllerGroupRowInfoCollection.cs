namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;

    public class ServerDataControllerGroupRowInfoCollection : DataControllerGroupRowInfoCollection
    {
        public ServerDataControllerGroupRowInfoCollection(DataController controller);
        public override GroupRowInfo Add(byte level, int ChildControllerRow, GroupRowInfo parentGroup);
        protected override GroupRowInfo CreateGroupRowInfo(byte level, int childControllerRow, GroupRowInfo parentGroupRow);
        internal void UpdateChildren(ServerModeGroupRowInfo sgroup, List<GroupRowInfo> insertList);
    }
}

