namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class FilterCriteriaMapper
    {
        public static T MapGeneral<T>(this CriteriaOperator criteria, Func<BinaryOperator, Option<T>> binary = null, UnaryOperatorMapper<T> unary = null, Func<InOperator, Option<T>> @in = null, Func<BetweenOperator, Option<T>> between = null, Func<FunctionOperator, Option<T>> function = null, GroupOperatorMapper<T> and = null, GroupOperatorMapper<T> or = null, NotOperatorMapper<T> not = null, FallbackMapper<T> fallback = null, NullMapper<T> @null = null);
        internal static string ToPropertyName(this CriteriaOperator operandProperty);
        internal static T TryFallback<T>(this CriteriaOperator theOperator, FallbackMapper<T> fallback, string message);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterCriteriaMapper.<>c <>9;
            public static Func<string> <>9__3_0;
            public static Func<string, string> <>9__3_2;

            static <>c();
            internal string <ToPropertyName>b__3_0();
            internal string <ToPropertyName>b__3_2(string x);
        }

        private class MapVisitor<T> : IClientCriteriaVisitor<T>, ICriteriaVisitor<T>
        {
            private readonly Func<BinaryOperator, Option<T>> binary;
            private readonly UnaryOperatorMapper<T> unary;
            private readonly Func<InOperator, Option<T>> @in;
            private readonly Func<BetweenOperator, Option<T>> between;
            private readonly Func<FunctionOperator, Option<T>> function;
            private readonly GroupOperatorMapper<T> and;
            private readonly GroupOperatorMapper<T> or;
            private readonly NotOperatorMapper<T> not;
            private readonly FallbackMapper<T> fallback;

            public MapVisitor(Func<BinaryOperator, Option<T>> binary, UnaryOperatorMapper<T> unary, Func<InOperator, Option<T>> @in, Func<BetweenOperator, Option<T>> between, Func<FunctionOperator, Option<T>> function, GroupOperatorMapper<T> and, GroupOperatorMapper<T> or, NotOperatorMapper<T> not, FallbackMapper<T> fallback);
            [CompilerGenerated]
            private T <DevExpress.Data.Filtering.ICriteriaVisitor<T>.Visit>b__17_1(CriteriaOperator x);
            T IClientCriteriaVisitor<T>.Visit(AggregateOperand theOperand);
            T IClientCriteriaVisitor<T>.Visit(JoinOperand theOperand);
            T IClientCriteriaVisitor<T>.Visit(OperandProperty theOperand);
            T ICriteriaVisitor<T>.Visit(BetweenOperator theOperator);
            T ICriteriaVisitor<T>.Visit(BinaryOperator theOperator);
            T ICriteriaVisitor<T>.Visit(FunctionOperator theOperator);
            T ICriteriaVisitor<T>.Visit(GroupOperator theOperator);
            T ICriteriaVisitor<T>.Visit(InOperator theOperator);
            T ICriteriaVisitor<T>.Visit(OperandValue theOperand);
            T ICriteriaVisitor<T>.Visit(UnaryOperator theOperator);
            private T FallbackToResult(CriteriaOperator theOperator, Option<T> result);
            private T ProcessOperator<TCriteria>(TCriteria theOperator, Func<TCriteria, Option<T>> map, string error) where TCriteria: CriteriaOperator;
            private T TryFallback(CriteriaOperator theOperator, string message);
        }
    }
}

