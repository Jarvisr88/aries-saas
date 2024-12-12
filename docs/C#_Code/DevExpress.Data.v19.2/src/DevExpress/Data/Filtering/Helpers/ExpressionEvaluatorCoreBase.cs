namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public abstract class ExpressionEvaluatorCoreBase : ICriteriaVisitor<object>
    {
        private EvaluateCustomFunctionHandler evaluateCustomFunction;
        private CustomAggregateResolveHandler resolveCustomAggregate;
        private IComparer customComparer;
        private bool caseSensitive;
        private static readonly object TrueValue;
        private static readonly object FalseValue;
        private static Dictionary<ExpressionEvaluatorCoreBase.FnFuncKey, Func<object[], object>> FnFuncs;

        static ExpressionEvaluatorCoreBase();
        protected ExpressionEvaluatorCoreBase(bool caseSensitive);
        protected ExpressionEvaluatorCoreBase(bool caseSensitive, EvaluateCustomFunctionHandler evaluateCustomFunction);
        protected ExpressionEvaluatorCoreBase(bool caseSensitive, EvaluateCustomFunctionHandler evaluateCustomFunction, CustomAggregateResolveHandler resolveCustomAggregate);
        public static object BoxBool(bool value);
        public static object BoxBool2VL(bool? value);
        public static object BoxBool3VL(bool? value);
        protected abstract void ClearContext();
        protected int Compare(object left, object right);
        protected int Compare(object left, object right, bool isEqualityCompare);
        protected object DoAggregate(Aggregate aggregateType, IEnumerable contextsCollection, CriteriaOperator filterExpression, CriteriaOperator expression);
        private void DoAggregate(ExpressionEvaluatorCoreBase.AggregateProcessingParam param, IEnumerable contextsCollection, CriteriaOperator filterExpression, CriteriaOperator expression);
        private void DoCustomAggregate(ExpressionEvaluatorCoreBase.CustomAggregateProcessingParam param, IEnumerable contextsCollection, CriteriaOperator filterExpression, CriteriaOperatorCollection aggregatedExpressions);
        protected object DoCustomAggregate(string customAggregateName, IEnumerable contextsCollection, CriteriaOperator filterExpression, CriteriaOperatorCollection aggregatedExpressions);
        public object Evaluate(EvaluatorContext evaluationContext, CriteriaOperator evaluatorCriteria);
        public object Evaluate(EvaluatorContext evaluationContext, CriteriaOperator evaluatorCriteria, IComparer customComparer);
        private static object EvaluateLambdableFunction(FunctionOperatorType fnType, bool caseSensitive, bool is3ValuedLogic, object[] args);
        public object[] EvaluateOnObjects(IEnumerable evaluatorContextCollection, CriteriaOperator filterCriteria);
        public object[] EvaluateOnObjects(IEnumerable evaluatorContextCollection, CriteriaOperator filterCriteria, IComparer customComparer);
        public ICollection<EvaluatorContext> Filter(ICollection<EvaluatorContext> evaluatorContextCollection, CriteriaOperator filterCriteria);
        protected bool Fit(CriteriaOperator filterCriteria);
        public bool Fit(EvaluatorContext evaluationContext, CriteriaOperator filterCriteria);
        private object FixValue(object value);
        private object FnCustom(FunctionOperator theOperator);
        private object FnIif(FunctionOperator theOperator);
        private object FnIsNull(FunctionOperator theOperator);
        private object FnIsSameDay(FunctionOperator theOperator);
        protected abstract EvaluatorContext GetContext();
        protected abstract EvaluatorContext GetContext(int upDepth);
        protected bool IsUnknownResult(object value);
        protected bool IsUnknownResult(object value1, object value2);
        protected virtual IEnumerable PopCollectionContext();
        protected object Process(CriteriaOperator operand);
        protected object[] Process(CriteriaOperatorCollection collection);
        protected virtual void PushCollectionContext(IEnumerable context);
        protected abstract void SetContext(EvaluatorContext context);
        private object UnaryNumericPromotions(object operand);
        public virtual object Visit(BetweenOperator theOperator);
        public virtual object Visit(BinaryOperator theOperator);
        public virtual object Visit(FunctionOperator theOperator);
        public virtual object Visit(GroupOperator theOperator);
        public virtual object Visit(InOperator theOperator);
        public virtual object Visit(OperandValue theOperand);
        public virtual object Visit(UnaryOperator theOperator);

        protected IComparer CustomComparer { get; }

        protected internal bool CaseSensitive { get; }

        protected internal virtual bool Is3ValuedLogic { get; }

        protected abstract bool HasContext { get; }

        private abstract class AggregateProcessingParam
        {
            public readonly ExpressionEvaluatorCoreBase Evaluator;

            protected AggregateProcessingParam(ExpressionEvaluatorCoreBase evaluator);
            public abstract object GetResult();
            public abstract bool Process(object operand);
        }

        private class AvgProcessingParam : ExpressionEvaluatorCoreBase.AggregateProcessingParam
        {
            private object result;
            private int count;

            public AvgProcessingParam(ExpressionEvaluatorCoreBase evaluator);
            public override object GetResult();
            public override bool Process(object operand);
        }

        private class CountProcessingParam : ExpressionEvaluatorCoreBase.AggregateProcessingParam
        {
            private int result;

            public CountProcessingParam(ExpressionEvaluatorCoreBase evaluator);
            public override object GetResult();
            public override bool Process(object operand);
        }

        private class CustomAggregateProcessingParam
        {
            private readonly ICustomAggregate customAggregate;
            private readonly object evaluationContext;
            public readonly ExpressionEvaluatorCoreBase Evaluator;

            public CustomAggregateProcessingParam(ExpressionEvaluatorCoreBase evaluator, ICustomAggregate customAggregate);
            public object GetResult();
            public bool Process(object[] operands);
        }

        private class ExistsProcessingParam : ExpressionEvaluatorCoreBase.AggregateProcessingParam
        {
            private bool result;

            public ExistsProcessingParam(ExpressionEvaluatorCoreBase evaluator);
            public override object GetResult();
            public override bool Process(object operand);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FnFuncKey : IEquatable<ExpressionEvaluatorCoreBase.FnFuncKey>
        {
            public readonly bool CaseSensitive;
            public readonly FunctionOperatorType FnType;
            public readonly int ArgsCount;
            public readonly bool Is3ValuedLogic;
            public FnFuncKey(FunctionOperatorType fnType, bool caseSensitive, bool is3ValuedLogic, int argsCount);
            public override int GetHashCode();
            public override bool Equals(object obj);
            public bool Equals(ExpressionEvaluatorCoreBase.FnFuncKey other);
        }

        private class MaxProcessingParam : ExpressionEvaluatorCoreBase.AggregateProcessingParam
        {
            private object result;

            public MaxProcessingParam(ExpressionEvaluatorCoreBase evaluator);
            public override object GetResult();
            public override bool Process(object operand);
        }

        private class MinProcessingParam : ExpressionEvaluatorCoreBase.AggregateProcessingParam
        {
            private object result;

            public MinProcessingParam(ExpressionEvaluatorCoreBase evaluator);
            public override object GetResult();
            public override bool Process(object operand);
        }

        private class SingleProcessingParam : ExpressionEvaluatorCoreBase.AggregateProcessingParam
        {
            private bool processed;
            private object result;

            public SingleProcessingParam(ExpressionEvaluatorCoreBase evaluator);
            public override object GetResult();
            public override bool Process(object operand);
        }

        private class SumProcessingParam : ExpressionEvaluatorCoreBase.AggregateProcessingParam
        {
            private object result;

            public SumProcessingParam(ExpressionEvaluatorCoreBase evaluator);
            public override object GetResult();
            public override bool Process(object operand);
        }
    }
}

