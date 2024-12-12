namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public abstract class CriteriaToStringBase : IQueryCriteriaVisitor<CriteriaToStringVisitResult>, ICriteriaVisitor<CriteriaToStringVisitResult>, IClientCriteriaVisitor<CriteriaToStringVisitResult>
    {
        public static bool SuppressFuncNormalization;

        protected CriteriaToStringBase();
        private string AggregateToString(Aggregate aggregateType, CriteriaOperator aggregatedExpression);
        private string AggregateToString(string customAggregateName, CriteriaOperatorCollection operands, bool isTopLevel);
        private CriteriaToStringVisitResult CreateIsNotNull(UnaryOperator nullOp);
        private CriteriaToStringVisitResult CreateNotLike(BinaryOperator likeOp);
        protected virtual string GetBetweenText();
        protected virtual string GetCustomFunctionText(string p);
        protected virtual string GetFunctionText(FunctionOperatorType operandType);
        protected virtual string GetInText();
        protected virtual string GetIsNotNullText();
        protected virtual string GetIsNullText();
        protected virtual string GetNotLikeText();
        protected virtual string GetOperatorString(Aggregate operandType);
        public abstract string GetOperatorString(BinaryOperatorType opType);
        public abstract string GetOperatorString(GroupOperatorType opType);
        public abstract string GetOperatorString(UnaryOperatorType opType);
        protected CriteriaToStringVisitResult Process(CriteriaOperator operand);
        protected string ProcessToCommaDelimitedList(ICollection operands);
        public virtual CriteriaToStringVisitResult Visit(AggregateOperand operand);
        public virtual CriteriaToStringVisitResult Visit(BetweenOperator operand);
        public virtual CriteriaToStringVisitResult Visit(BinaryOperator operand);
        public virtual CriteriaToStringVisitResult Visit(FunctionOperator operand);
        public virtual CriteriaToStringVisitResult Visit(GroupOperator operand);
        public virtual CriteriaToStringVisitResult Visit(InOperator operand);
        public virtual CriteriaToStringVisitResult Visit(JoinOperand operand);
        public virtual CriteriaToStringVisitResult Visit(OperandProperty operand);
        public abstract CriteriaToStringVisitResult Visit(OperandValue operand);
        public virtual CriteriaToStringVisitResult Visit(UnaryOperator operand);
        public virtual CriteriaToStringVisitResult Visit(QueryOperand operand);
        public virtual CriteriaToStringVisitResult Visit(QuerySubQueryContainer operand);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CriteriaToStringBase.<>c <>9;
            public static Predicate<UnaryOperator> <>9__12_0;
            public static Predicate<BinaryOperator> <>9__12_1;
            public static Predicate<FunctionOperator> <>9__12_2;
            public static Predicate<FunctionOperator> <>9__21_0;

            static <>c();
            internal bool <Visit>b__12_0(UnaryOperator uop);
            internal bool <Visit>b__12_1(BinaryOperator bop);
            internal bool <Visit>b__12_2(FunctionOperator fop);
            internal bool <Visit>b__21_0(FunctionOperator f);
        }
    }
}

