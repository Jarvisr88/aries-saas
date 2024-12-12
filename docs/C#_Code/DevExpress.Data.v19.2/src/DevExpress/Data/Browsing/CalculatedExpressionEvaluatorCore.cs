namespace DevExpress.Data.Browsing
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    internal class CalculatedExpressionEvaluatorCore : ExpressionEvaluatorCore, ICriteriaVisitor<object>
    {
        public CalculatedExpressionEvaluatorCore(bool caseSensitive, EvaluateCustomFunctionHandler evaluateCustomFunction);
        public CalculatedExpressionEvaluatorCore(bool caseSensitive, EvaluateCustomFunctionHandler evaluateCustomFunction, CustomAggregateResolveHandler resolveCustomAggregate);
        object ICriteriaVisitor<object>.Visit(InOperator theOperator);
        [IteratorStateMachine(typeof(CalculatedExpressionEvaluatorCore.<GetOperandValues>d__3))]
        private IEnumerable GetOperandValues(CriteriaOperatorCollection operands);
        public override object Visit(OperandValue theOperand);

        [CompilerGenerated]
        private sealed class <GetOperandValues>d__3 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private CriteriaOperatorCollection operands;
            public CriteriaOperatorCollection <>3__operands;
            public CalculatedExpressionEvaluatorCore <>4__this;
            private object <opValue>5__1;
            private List<CriteriaOperator>.Enumerator <>7__wrap1;
            private IEnumerator <>7__wrap2;

            [DebuggerHidden]
            public <GetOperandValues>d__3(int <>1__state);
            private void <>m__Finally1();
            private void <>m__Finally2();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            object IEnumerator<object>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

