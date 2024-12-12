namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SubSelectAggregateInfo
    {
        public CriteriaOperator Property;
        public Aggregate AggregateType;
        public string CustomAggregateName;
        public readonly CriteriaOperatorCollection CustomAggregateOperands;
        public string Alias;
        public SubSelectAggregateInfo(CriteriaOperator property, Aggregate aggregateType, string alias)
        {
            if (aggregateType == Aggregate.Custom)
            {
                throw new ArgumentException("aggregateType");
            }
            this.Property = property;
            this.AggregateType = aggregateType;
            this.CustomAggregateName = null;
            this.CustomAggregateOperands = null;
            this.Alias = alias;
        }

        public SubSelectAggregateInfo(IEnumerable<CriteriaOperator> operands, string customAggregateName, string alias)
        {
            if (string.IsNullOrEmpty(customAggregateName))
            {
                throw new ArgumentNullException("customAggregateName");
            }
            this.Property = null;
            this.AggregateType = Aggregate.Custom;
            this.CustomAggregateName = customAggregateName;
            this.CustomAggregateOperands = new CriteriaOperatorCollection();
            this.CustomAggregateOperands.AddRange(operands);
            this.Alias = alias;
        }
    }
}

