namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    [Serializable]
    public enum GroupType
    {
        public const GroupType And = GroupType.And;,
        public const GroupType Or = GroupType.Or;,
        public const GroupType NotAnd = GroupType.NotAnd;,
        public const GroupType NotOr = GroupType.NotOr;
    }
}

