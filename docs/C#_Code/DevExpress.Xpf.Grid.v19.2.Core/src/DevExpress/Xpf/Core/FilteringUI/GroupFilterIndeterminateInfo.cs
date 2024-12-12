namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;

    internal class GroupFilterIndeterminateInfo
    {
        public readonly object Value;
        public readonly GroupFilterInfo FilterInfo;

        public GroupFilterIndeterminateInfo(object value, GroupFilterInfo filterInfo)
        {
            this.Value = value;
            this.FilterInfo = filterInfo;
        }
    }
}

