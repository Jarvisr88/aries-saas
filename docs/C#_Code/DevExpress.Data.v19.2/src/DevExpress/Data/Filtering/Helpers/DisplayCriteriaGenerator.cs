namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class DisplayCriteriaGenerator : IClientCriteriaVisitor<CriteriaOperator>, ICriteriaVisitor<CriteriaOperator>
    {
        public readonly IDisplayCriteriaGeneratorNamesSource NamesSource;

        protected DisplayCriteriaGenerator(IDisplayCriteriaGeneratorNamesSource namesSource);
        protected virtual OperandProperty Convert(OperandProperty theOperand);
        protected virtual CriteriaOperator Process(CriteriaOperator inputValue);
        public static CriteriaOperator Process(IDisplayCriteriaGeneratorNamesSource namesSource, CriteriaOperator op);
        protected virtual CriteriaOperator ProcessPossibleValue(CriteriaOperator possibleProperty, CriteriaOperator possibleValue);
        protected virtual object ProcessValue(OperandProperty originalProperty, object originalValue);
        public virtual CriteriaOperator Visit(AggregateOperand theOperand);
        public virtual CriteriaOperator Visit(BetweenOperator theOperator);
        public virtual CriteriaOperator Visit(BinaryOperator theOperator);
        public virtual CriteriaOperator Visit(FunctionOperator theOperator);
        public virtual CriteriaOperator Visit(GroupOperator theOperator);
        public virtual CriteriaOperator Visit(InOperator theOperator);
        public virtual CriteriaOperator Visit(JoinOperand theOperand);
        public virtual CriteriaOperator Visit(OperandProperty theOperand);
        public virtual CriteriaOperator Visit(OperandValue theOperand);
        public virtual CriteriaOperator Visit(UnaryOperator theOperator);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DisplayCriteriaGenerator.<>c <>9;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__7_0;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__8_0;

            static <>c();
            internal CriteriaOperator <Visit>b__7_0(CriteriaOperator t);
            internal CriteriaOperator <Visit>b__8_0(CriteriaOperator t);
        }

        private class OperandPropertyValueFinder : IClientCriteriaVisitor<OperandProperty>, ICriteriaVisitor<OperandProperty>
        {
            private static readonly DisplayCriteriaGenerator.OperandPropertyValueFinder Instance;

            static OperandPropertyValueFinder();
            private OperandPropertyValueFinder();
            OperandProperty IClientCriteriaVisitor<OperandProperty>.Visit(AggregateOperand theOperand);
            OperandProperty IClientCriteriaVisitor<OperandProperty>.Visit(JoinOperand theOperand);
            OperandProperty IClientCriteriaVisitor<OperandProperty>.Visit(OperandProperty theOperand);
            OperandProperty ICriteriaVisitor<OperandProperty>.Visit(BetweenOperator theOperator);
            OperandProperty ICriteriaVisitor<OperandProperty>.Visit(BinaryOperator theOperator);
            OperandProperty ICriteriaVisitor<OperandProperty>.Visit(FunctionOperator theOperator);
            OperandProperty ICriteriaVisitor<OperandProperty>.Visit(GroupOperator theOperator);
            OperandProperty ICriteriaVisitor<OperandProperty>.Visit(InOperator theOperator);
            OperandProperty ICriteriaVisitor<OperandProperty>.Visit(OperandValue theOperand);
            OperandProperty ICriteriaVisitor<OperandProperty>.Visit(UnaryOperator theOperator);
            public static OperandProperty Find(CriteriaOperator criteria);
            private OperandProperty Process(CriteriaOperator criteria);
        }
    }
}

