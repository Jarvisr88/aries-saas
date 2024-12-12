namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CriteriaOperatorValue
    {
        public readonly CriteriaOperator Criteria;
        public CriteriaOperatorValue(CriteriaOperator criteria)
        {
            this.Criteria = criteria;
        }
    }
}

