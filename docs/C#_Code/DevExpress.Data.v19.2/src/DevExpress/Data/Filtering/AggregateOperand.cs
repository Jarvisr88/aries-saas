namespace DevExpress.Data.Filtering
{
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml.Serialization;

    [Serializable, XmlInclude(typeof(ContainsOperator))]
    public class AggregateOperand : CriteriaOperator, IAggregateOperand, ICustomAggregateOperand
    {
        private CriteriaOperator condition;
        private OperandProperty property;
        private CriteriaOperator aggregatedExpression;
        private Aggregate type;
        private string customAggregateName;
        private readonly CriteriaOperatorCollection customAggregateOperands;

        public AggregateOperand();
        public AggregateOperand(string collectionProperty, Aggregate type);
        public AggregateOperand(string collectionProperty, string customAggregateName);
        public AggregateOperand(string collectionProperty, Aggregate type, CriteriaOperator condition);
        public AggregateOperand(string collectionProperty, string aggregatedExpression, Aggregate type);
        public AggregateOperand(string collectionProperty, string customAggregateName, CriteriaOperator condition);
        public AggregateOperand(OperandProperty collectionProperty, CriteriaOperator aggregatedExpression, Aggregate type, CriteriaOperator condition);
        public AggregateOperand(OperandProperty collectionProperty, IEnumerable<CriteriaOperator> aggregatedExpressions, string customAggregateName, CriteriaOperator condition);
        public AggregateOperand(string collectionProperty, string aggregatedExpression, Aggregate type, CriteriaOperator condition);
        public override void Accept(ICriteriaVisitor visitor);
        public override T Accept<T>(ICriteriaVisitor<T> visitor);
        public AggregateOperand Avg(CriteriaOperator aggregatedExpression);
        public AggregateOperand Clone();
        protected override CriteriaOperator CloneCommon();
        public AggregateOperand Count();
        public AggregateOperand Count(CriteriaOperator aggregatedExpression);
        public AggregateOperand Custom(string customAggregateName, IEnumerable<CriteriaOperator> aggregatedExpressions);
        public override bool Equals(object obj);
        public AggregateOperand Exists();
        public AggregateOperand Exists(CriteriaOperator aggregatedExpression);
        public override int GetHashCode();
        private static OperandProperty GetPropertyByName(string propertyName);
        public AggregateOperand Max(CriteriaOperator aggregatedExpression);
        public AggregateOperand Min(CriteriaOperator aggregatedExpression);
        public AggregateOperand Single(CriteriaOperator aggregatedExpression);
        public AggregateOperand Sum(CriteriaOperator aggregatedExpression);
        public static AggregateOperand TopLevel(Aggregate type, CriteriaOperator aggregatedExpression = null);
        public static AggregateOperand TopLevel(string customAggregateName, IEnumerable<CriteriaOperator> aggregatedExpressions = null);

        public CriteriaOperator Condition { get; set; }

        public OperandProperty CollectionProperty { get; set; }

        public CriteriaOperator AggregatedExpression { get; set; }

        public Aggregate AggregateType { get; set; }

        public string CustomAggregateName { get; set; }

        public CriteriaOperatorCollection CustomAggregateOperands { get; }

        public bool IsTopLevel { get; }

        CriteriaOperator IAggregateOperand.Condition { get; set; }

        object IAggregateOperand.AggregationObject { get; set; }

        CriteriaOperator IAggregateOperand.AggregatedExpression { get; set; }

        Aggregate IAggregateOperand.AggregateType { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AggregateOperand.<>c <>9;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__64_0;

            static <>c();
            internal CriteriaOperator <Clone>b__64_0(CriteriaOperator t);
        }
    }
}

