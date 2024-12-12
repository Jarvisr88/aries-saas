namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public class ServerModeSummaryDescriptor : IEquatable<ServerModeSummaryDescriptor>
    {
        public readonly CriteriaOperator SummaryExpression;
        public readonly Aggregate SummaryType;
        public string SummaryCustomAggregateName;
        public readonly CriteriaOperatorCollection SummaryCustomAggregateExpressions;

        public ServerModeSummaryDescriptor(CriteriaOperator expression, Aggregate type);
        public ServerModeSummaryDescriptor(IEnumerable<CriteriaOperator> expressions, string customAggregateName);
        public virtual bool Equals(ServerModeSummaryDescriptor other);
        public override bool Equals(object obj);
        public override int GetHashCode();

        public string SummaryPropertyName { get; }
    }
}

