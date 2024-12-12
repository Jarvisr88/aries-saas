namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class GroupNodeModelChildMenuOptions
    {
        public GroupNodeModelChildMenuOptions(bool allowAddCondition, bool allowAddGroup, bool allowAddCustomExpression)
        {
            this.<AllowAddCondition>k__BackingField = allowAddCondition;
            this.<AllowAddGroup>k__BackingField = allowAddGroup;
            this.<AllowAddCustomExpression>k__BackingField = allowAddCustomExpression;
        }

        public bool AllowAddCondition { get; }

        public bool AllowAddGroup { get; }

        public bool AllowAddCustomExpression { get; }
    }
}

