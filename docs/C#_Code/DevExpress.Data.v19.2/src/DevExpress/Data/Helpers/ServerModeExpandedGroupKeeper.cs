namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class ServerModeExpandedGroupKeeper : ExpandedGroupKeeper
    {
        public ServerModeExpandedGroupKeeper(ServerModeDataControllerBase controller);
        protected override bool IsAllSelected();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServerModeExpandedGroupKeeper.<>c <>9;
            public static Func<GroupRowInfo, bool> <>9__1_0;

            static <>c();
            internal bool <IsAllSelected>b__1_0(GroupRowInfo q);
        }
    }
}

