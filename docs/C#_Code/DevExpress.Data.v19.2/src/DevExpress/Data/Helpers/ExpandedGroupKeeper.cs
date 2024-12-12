namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ExpandedGroupKeeper : GroupKeeperBase<IEnumerable<GroupRowInfo>>
    {
        public ExpandedGroupKeeper(DataController controller);
        protected override IEnumerable<GroupRowInfo> GetSelectedGroups();
        protected override bool IsAllSelected();
        protected override void OnRestoreAllSelected();
        protected override void OnRestoreGroup(GroupRowInfo group, object selection);
        internal void RemoveExpanded();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExpandedGroupKeeper.<>c <>9;
            public static Func<GroupRowInfo, bool> <>9__1_0;

            static <>c();
            internal bool <GetSelectedGroups>b__1_0(GroupRowInfo q);
        }
    }
}

