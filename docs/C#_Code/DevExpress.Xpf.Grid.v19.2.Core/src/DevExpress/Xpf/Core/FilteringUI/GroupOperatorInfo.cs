namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public sealed class GroupOperatorInfo : ImmutableObject
    {
        internal GroupOperatorInfo(string displayName, Func<IList<CriteriaOperator>, CriteriaOperator> factory)
        {
            if ((displayName == null) || (factory == null))
            {
                throw new ArgumentNullException();
            }
            this.<DisplayName>k__BackingField = displayName;
            this.<Factory>k__BackingField = factory;
        }

        public string DisplayName { get; }

        internal Func<IList<CriteriaOperator>, CriteriaOperator> Factory { get; }
    }
}

