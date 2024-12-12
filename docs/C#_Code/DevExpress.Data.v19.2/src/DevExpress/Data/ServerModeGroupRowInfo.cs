namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;

    public class ServerModeGroupRowInfo : GroupRowInfo
    {
        private ListSourceGroupInfo listGroupInfo;
        public bool ChildrenReady;
        public bool IsSummaryReady;

        public ServerModeGroupRowInfo();
        public ServerModeGroupRowInfo(byte level, int childControllerRow, GroupRowInfo parentGroup, ListSourceGroupInfo listGroupInfo);
        public override void ClearSummary();
        internal void SetSummary(SummaryItemCollection groupSummary, List<object> summaryValues);

        public ListSourceGroupInfo ListGroupInfo { get; set; }

        public override object GroupValue { get; }
    }
}

