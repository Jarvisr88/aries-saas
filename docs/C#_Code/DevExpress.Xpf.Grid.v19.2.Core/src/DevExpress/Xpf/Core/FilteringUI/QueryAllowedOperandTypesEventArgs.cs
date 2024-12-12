namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using System;
    using System.Runtime.CompilerServices;

    internal class QueryAllowedOperandTypesEventArgs
    {
        internal QueryAllowedOperandTypesEventArgs(CriteriaOperator filter)
        {
            this.<Filter>k__BackingField = filter;
        }

        public CriteriaOperator Filter { get; }

        public DevExpress.Xpf.Core.FilteringUI.Native.AllowedOperandTypes AllowedOperandTypes { get; set; }
    }
}

