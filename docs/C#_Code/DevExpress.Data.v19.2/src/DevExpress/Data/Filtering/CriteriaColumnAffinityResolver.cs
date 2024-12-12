namespace DevExpress.Data.Filtering
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class CriteriaColumnAffinityResolver
    {
        public static bool UseLegacyResolver;

        [IteratorStateMachine(typeof(CriteriaColumnAffinityResolver.<Chop>d__10))]
        private static IEnumerable<CriteriaOperator> Chop(CriteriaOperator choppable);
        [IteratorStateMachine(typeof(CriteriaColumnAffinityResolver.<Chop>d__9))]
        private static IEnumerable<CriteriaOperator> Chop(IEnumerable<CriteriaOperator> andGroupCriteria);
        private static OperandProperty GetAffinityColumn(CriteriaOperator op, IEqualityComparer<string> _PropEqualityComparer = null);
        public static OperandProperty GetAffinityColumnLegacy(CriteriaOperator op);
        public static string GetAffinityColumnName(CriteriaOperator op, IEqualityComparer<string> _PropEqualityComparer = null);
        public static OperandProperty GetAffinityColumnWithOptionalLegacyFallback(CriteriaOperator op);
        public static Tuple<CriteriaOperator, IDictionary<string, CriteriaOperator>> SplitByColumnNames(CriteriaOperator op, IEqualityComparer<string> _PropEqualityComparer = null);
        public static IDictionary<OperandProperty, CriteriaOperator> SplitByColumns(CriteriaOperator op);
        public static IDictionary<OperandProperty, CriteriaOperator> SplitByColumnsLegacy(CriteriaOperator op);
        private static IDictionary<OperandProperty, CriteriaOperator> SplitByColumnsNew(CriteriaOperator op, IEqualityComparer<string> _PropEqualityComparer = null);
        public static IDictionary<OperandProperty, CriteriaOperator> SplitByColumnsWithOptionalLegacyFallback(CriteriaOperator op);
        internal static bool TryGetPropertyNameSimple(CriteriaOperator criterion, out string propertyName);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CriteriaColumnAffinityResolver.<>c <>9;
            public static Predicate<GroupOperator> <>9__9_0;
            public static Predicate<GroupOperator> <>9__10_0;

            static <>c();
            internal bool <Chop>b__10_0(GroupOperator g);
            internal bool <Chop>b__9_0(GroupOperator g);
        }


        [CompilerGenerated]
        private sealed class <Chop>d__9 : IEnumerable<CriteriaOperator>, IEnumerable, IEnumerator<CriteriaOperator>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private CriteriaOperator <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<CriteriaOperator> andGroupCriteria;
            public IEnumerable<CriteriaOperator> <>3__andGroupCriteria;
            private GroupOperator <nestedGroup>5__1;
            private IEnumerator<CriteriaOperator> <>7__wrap1;
            private IEnumerator<CriteriaOperator> <>7__wrap2;

            [DebuggerHidden]
            public <Chop>d__9(int <>1__state);
            private void <>m__Finally1();
            private void <>m__Finally2();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<CriteriaOperator> IEnumerable<CriteriaOperator>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            CriteriaOperator IEnumerator<CriteriaOperator>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Affinity
        {
            public string PropertyName;
            public DefaultBoolean IsAffine;
            private Affinity(DefaultBoolean _IsAffine);
            private Affinity(string _PropertyName);
            public static CriteriaColumnAffinityResolver.Affinity Affine(string _PropertyName);
            public static CriteriaColumnAffinityResolver.Affinity NotAffine();
            public static CriteriaColumnAffinityResolver.Affinity Ignorable();
        }

        private sealed class CriteriaColumnAffinityResolverLegacy : IClientCriteriaVisitor<OperandProperty>, ICriteriaVisitor<OperandProperty>
        {
            private static CriteriaColumnAffinityResolver.CriteriaColumnAffinityResolverLegacy Instance;

            static CriteriaColumnAffinityResolverLegacy();
            private CriteriaColumnAffinityResolverLegacy();
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
            public static OperandProperty GetAffinityColumn(CriteriaOperator op);
            private OperandProperty Process(CriteriaOperator op);
            public static IDictionary<OperandProperty, CriteriaOperator> SplitByColumns(CriteriaOperator op);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly CriteriaColumnAffinityResolver.CriteriaColumnAffinityResolverLegacy.<>c <>9;
                public static Predicate<GroupOperator> <>9__14_0;

                static <>c();
                internal bool <SplitByColumns>b__14_0(GroupOperator g);
            }
        }

        private sealed class TrivialCore : IClientCriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>, ICriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>
        {
            public readonly IEqualityComparer<string> PropEqualityComparer;

            public TrivialCore(IEqualityComparer<string> _PropEqualityComparer);
            CriteriaColumnAffinityResolver.Affinity IClientCriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>.Visit(AggregateOperand theOperand);
            CriteriaColumnAffinityResolver.Affinity IClientCriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>.Visit(JoinOperand theOperand);
            CriteriaColumnAffinityResolver.Affinity IClientCriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>.Visit(OperandProperty theOperand);
            CriteriaColumnAffinityResolver.Affinity ICriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>.Visit(BetweenOperator theOperator);
            CriteriaColumnAffinityResolver.Affinity ICriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>.Visit(BinaryOperator theOperator);
            CriteriaColumnAffinityResolver.Affinity ICriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>.Visit(FunctionOperator theOperator);
            CriteriaColumnAffinityResolver.Affinity ICriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>.Visit(GroupOperator theOperator);
            CriteriaColumnAffinityResolver.Affinity ICriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>.Visit(InOperator theOperator);
            CriteriaColumnAffinityResolver.Affinity ICriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>.Visit(OperandValue theOperand);
            CriteriaColumnAffinityResolver.Affinity ICriteriaVisitor<CriteriaColumnAffinityResolver.Affinity>.Visit(UnaryOperator theOperator);
            public CriteriaColumnAffinityResolver.Affinity Process(CriteriaOperator op);
            private CriteriaColumnAffinityResolver.Affinity Process(IEnumerable<CriteriaOperator> criteria);
        }
    }
}

