namespace DevExpress.Data.Filtering
{
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class JoinOperand : CriteriaOperator, IAggregateOperand, ICustomAggregateOperand
    {
        private string joinTypeName;
        private CriteriaOperator condition;
        private CriteriaOperator aggregatedExpression;
        private Aggregate type;
        private string customAggregateName;
        private readonly CriteriaOperatorCollection customAggregateOperands;

        public JoinOperand();
        public JoinOperand(string joinTypeName, CriteriaOperator condition);
        public JoinOperand(string joinTypeName, CriteriaOperator condition, Aggregate type, CriteriaOperator aggregatedExpression);
        public JoinOperand(string joinTypeName, CriteriaOperator condition, string customAggregateName, IEnumerable<CriteriaOperator> aggregatedExpressions);
        public override void Accept(ICriteriaVisitor visitor);
        public override T Accept<T>(ICriteriaVisitor<T> visitor);
        public JoinOperand Avg(CriteriaOperator aggregatedExpression);
        public JoinOperand Clone();
        protected override CriteriaOperator CloneCommon();
        public JoinOperand Count();
        public JoinOperand Count(CriteriaOperator aggregatedExpression);
        public JoinOperand Custom(string customAggregateName, IEnumerable<CriteriaOperator> aggregatedExpressions);
        public override bool Equals(object obj);
        public JoinOperand Exists();
        public JoinOperand Exists(CriteriaOperator aggregatedExpression);
        public override int GetHashCode();
        internal static CriteriaOperator JoinOrAggreagate(OperandProperty collectionProperty, CriteriaOperator condition, Aggregate type, CriteriaOperator aggregated);
        internal static CriteriaOperator JoinOrAggreagate(OperandProperty collectionProperty, CriteriaOperator condition, string customAggregateName, IEnumerable<CriteriaOperator> aggregated);
        public JoinOperand Max(CriteriaOperator aggregatedExpression);
        public JoinOperand Min(CriteriaOperator aggregatedExpression);
        public JoinOperand Sum(CriteriaOperator aggregatedExpression);

        public CriteriaOperator Condition { get; set; }

        public string JoinTypeName { get; set; }

        public CriteriaOperator AggregatedExpression { get; set; }

        public Aggregate AggregateType { get; set; }

        public string CustomAggregateName { get; set; }

        public CriteriaOperatorCollection CustomAggregateOperands { get; }

        CriteriaOperator IAggregateOperand.Condition { get; set; }

        object IAggregateOperand.AggregationObject { get; set; }

        CriteriaOperator IAggregateOperand.AggregatedExpression { get; set; }

        Aggregate IAggregateOperand.AggregateType { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly JoinOperand.<>c <>9;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__53_0;

            static <>c();
            internal CriteriaOperator <Clone>b__53_0(CriteriaOperator t);
        }
    }
}

