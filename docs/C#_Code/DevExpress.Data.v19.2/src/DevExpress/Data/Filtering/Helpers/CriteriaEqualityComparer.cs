namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class CriteriaEqualityComparer
    {
        private static readonly Func<CriteriaOperator, CriteriaOperator, bool> DefEq;
        public static readonly IEqualityComparer<CriteriaOperator> Default;
        public static readonly IEqualityComparer<CriteriaOperator> OperandValueInvariant;

        static CriteriaEqualityComparer();
        private static bool AreEquals(IList<CriteriaOperator> a, IList<CriteriaOperator> b, Func<CriteriaOperator, CriteriaOperator, bool> nestedEquals);
        public static IEqualityComparer<CriteriaOperator> BuildEqualityComparerWithOverrides(Func<CriteriaOperator, CriteriaOperator, bool?> comparerOverride, Func<CriteriaOperator, int?> hashCodeOverride);
        public static Func<CriteriaOperator, CriteriaOperator, bool> BuildEqualityComparisonWithOverrides(Func<CriteriaOperator, CriteriaOperator, bool?> overrides);
        public static Func<CriteriaOperator, int> BuildHashCodeCalculatorWithOverrides(Func<CriteriaOperator, int?> overrides);
        public static bool CompareEqualsOperandValueInvariant(CriteriaOperator x, CriteriaOperator y);
        internal static bool DefaultEquals(AggregateOperand a, AggregateOperand b, Func<CriteriaOperator, CriteriaOperator, bool> nestedEquals = null);
        internal static bool DefaultEquals(BetweenOperator a, BetweenOperator b, Func<CriteriaOperator, CriteriaOperator, bool> nestedEquals = null);
        internal static bool DefaultEquals(BinaryOperator a, BinaryOperator b, Func<CriteriaOperator, CriteriaOperator, bool> nestedEquals = null);
        internal static bool DefaultEquals(FunctionOperator a, FunctionOperator b, Func<CriteriaOperator, CriteriaOperator, bool> nestedEquals = null);
        internal static bool DefaultEquals(GroupOperator a, GroupOperator b, Func<CriteriaOperator, CriteriaOperator, bool> nestedEquals = null);
        internal static bool DefaultEquals(InOperator a, InOperator b, Func<CriteriaOperator, CriteriaOperator, bool> nestedEquals = null);
        internal static bool DefaultEquals(JoinOperand a, JoinOperand b, Func<CriteriaOperator, CriteriaOperator, bool> nestedEquals = null);
        internal static bool DefaultEquals(UnaryOperator a, UnaryOperator b, Func<CriteriaOperator, CriteriaOperator, bool> nestedEquals = null);
        internal static bool DefaultEqualsOperandParameter(OperandParameter a, OperandParameter b);
        internal static bool DefaultEqualsOperandProperty(OperandProperty a, OperandProperty b);
        internal static bool DefaultEqualsOperandValue(OperandValue a, OperandValue b);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CriteriaEqualityComparer.<>c <>9;
            public static Func<CriteriaOperator, int?> <>9__22_0;

            static <>c();
            internal bool? <.cctor>b__23_0(CriteriaOperator x, CriteriaOperator y);
            internal int? <.cctor>b__23_1(CriteriaOperator x);
            internal int? <BuildHashCodeCalculatorWithOverrides>b__22_0(CriteriaOperator x);
        }

        private class DefaultEqualsHandmadePatternMatcher : IClientCriteriaVisitor<Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool>>, ICriteriaVisitor<Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool>>
        {
            private static readonly CriteriaEqualityComparer.DefaultEqualsHandmadePatternMatcher Instance;

            static DefaultEqualsHandmadePatternMatcher();
            private DefaultEqualsHandmadePatternMatcher();
            public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> Match(CriteriaOperator op);
            public Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> Visit(AggregateOperand theOperand);
            public Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> Visit(BetweenOperator theOperator);
            public Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> Visit(BinaryOperator theOperator);
            public Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> Visit(FunctionOperator theOperator);
            public Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> Visit(GroupOperator theOperator);
            public Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> Visit(InOperator theOperator);
            public Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> Visit(JoinOperand theOperand);
            public Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> Visit(OperandProperty theOperand);
            public Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> Visit(OperandValue theOperand);
            public Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> Visit(UnaryOperator theOperator);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly CriteriaEqualityComparer.DefaultEqualsHandmadePatternMatcher.<>c <>9;
                public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> <>9__3_0;
                public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> <>9__4_0;
                public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> <>9__5_0;
                public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> <>9__6_0;
                public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> <>9__7_0;
                public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> <>9__8_0;
                public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> <>9__9_0;
                public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> <>9__10_0;
                public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> <>9__11_0;
                public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> <>9__11_1;
                public static Func<CriteriaOperator, CriteriaOperator, Func<CriteriaOperator, CriteriaOperator, bool>, bool> <>9__12_0;

                static <>c();
                internal bool <Visit>b__10_0(CriteriaOperator a, CriteriaOperator b, Func<CriteriaOperator, CriteriaOperator, bool> eq);
                internal bool <Visit>b__11_0(CriteriaOperator a, CriteriaOperator b, Func<CriteriaOperator, CriteriaOperator, bool> eq);
                internal bool <Visit>b__11_1(CriteriaOperator a, CriteriaOperator b, Func<CriteriaOperator, CriteriaOperator, bool> eq);
                internal bool <Visit>b__12_0(CriteriaOperator a, CriteriaOperator b, Func<CriteriaOperator, CriteriaOperator, bool> eq);
                internal bool <Visit>b__3_0(CriteriaOperator a, CriteriaOperator b, Func<CriteriaOperator, CriteriaOperator, bool> eq);
                internal bool <Visit>b__4_0(CriteriaOperator a, CriteriaOperator b, Func<CriteriaOperator, CriteriaOperator, bool> eq);
                internal bool <Visit>b__5_0(CriteriaOperator a, CriteriaOperator b, Func<CriteriaOperator, CriteriaOperator, bool> eq);
                internal bool <Visit>b__6_0(CriteriaOperator a, CriteriaOperator b, Func<CriteriaOperator, CriteriaOperator, bool> eq);
                internal bool <Visit>b__7_0(CriteriaOperator a, CriteriaOperator b, Func<CriteriaOperator, CriteriaOperator, bool> eq);
                internal bool <Visit>b__8_0(CriteriaOperator a, CriteriaOperator b, Func<CriteriaOperator, CriteriaOperator, bool> eq);
                internal bool <Visit>b__9_0(CriteriaOperator a, CriteriaOperator b, Func<CriteriaOperator, CriteriaOperator, bool> eq);
            }
        }

        public sealed class FuncEqualityComparer<T> : IEqualityComparer<T>
        {
            private readonly Func<T, T, bool> _eq;
            private readonly Func<T, int> _hash;

            public FuncEqualityComparer(Func<T, T, bool> eq, Func<T, int> hash);
            public bool Equals(T x, T y);
            public int GetHashCode(T obj);
        }

        private class HashCodeCalculatorWithOverrides : IClientCriteriaVisitor<int>, ICriteriaVisitor<int>
        {
            private readonly Func<CriteriaOperator, int?> Override;

            public HashCodeCalculatorWithOverrides(Func<CriteriaOperator, int?> _override);
            public int Process(CriteriaOperator op);
            public IEnumerable<int> Process(IEnumerable<CriteriaOperator> ops);
            public int Visit(AggregateOperand o);
            public int Visit(BetweenOperator o);
            public int Visit(BinaryOperator o);
            public int Visit(FunctionOperator theOperator);
            public int Visit(GroupOperator theOperator);
            public int Visit(InOperator theOperator);
            public int Visit(JoinOperand o);
            public int Visit(OperandProperty theOperand);
            public int Visit(OperandValue theOperand);
            public int Visit(UnaryOperator theOperator);
        }

        internal static class HashSeed
        {
            public const int AggregateOperand = 0x7ce064a;
            public const int OperandProperty = 0x13d1d1ac;
            public const int JoinOperand = -1835721830;
            public const int BetweenOperator = 0x3dc54487;
            public const int BinaryOperator = -2044361752;
            public const int UnaryOperator = -521218148;
            public const int InOperator = -859476546;
            public const int GroupOperator = 0x2c74d9f9;
            public const int OperandValue = -846290000;
            public const int ConstantValue = -1215107661;
            public const int OperandParameter = -847796681;
            public const int FunctionOperator = 0x1f98300e;
        }
    }
}

