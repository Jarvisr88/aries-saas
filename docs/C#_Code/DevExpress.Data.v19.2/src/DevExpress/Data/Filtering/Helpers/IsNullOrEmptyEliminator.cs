namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class IsNullOrEmptyEliminator : IClientCriteriaVisitor<CriteriaOperator>, ICriteriaVisitor<CriteriaOperator>
    {
        private bool found;
        private readonly string FieldName;

        private IsNullOrEmptyEliminator(string fieldName);
        public static bool Eliminate(CriteriaOperator op, string fieldName, out CriteriaOperator patched);
        protected CriteriaOperator Process(CriteriaOperator op);
        protected IEnumerable<CriteriaOperator> Process(IEnumerable<CriteriaOperator> ops);
        public CriteriaOperator Visit(AggregateOperand theOperand);
        public CriteriaOperator Visit(BetweenOperator theOperator);
        public CriteriaOperator Visit(BinaryOperator theOperator);
        public CriteriaOperator Visit(FunctionOperator theOperator);
        public CriteriaOperator Visit(GroupOperator theOperator);
        public CriteriaOperator Visit(InOperator theOperator);
        public CriteriaOperator Visit(JoinOperand theOperand);
        public CriteriaOperator Visit(OperandProperty theOperand);
        public CriteriaOperator Visit(OperandValue theOperand);
        public CriteriaOperator Visit(UnaryOperator theOperator);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly IsNullOrEmptyEliminator.<>c <>9;
            public static Func<CriteriaOperator, bool> <>9__15_1;

            static <>c();
            internal bool <Process>b__15_1(CriteriaOperator p);
        }
    }
}

