namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    public class ExpressionEvaluatorCore : ExpressionEvaluatorCoreBase, IClientCriteriaVisitor<object>, ICriteriaVisitor<object>
    {
        private EvaluatorContext[] contexts;
        private readonly JoinEvaluationContextCache JoinCache;
        protected readonly EvaluatorPropertyCache PropertyCache;

        public ExpressionEvaluatorCore(bool caseSensitive);
        public ExpressionEvaluatorCore(bool caseSensitive, EvaluateCustomFunctionHandler evaluateCustomFunction);
        public ExpressionEvaluatorCore(bool caseSensitive, EvaluateCustomFunctionHandler evaluateCustomFunction, CustomAggregateResolveHandler resolveCustomAggregate);
        protected sealed override void ClearContext();
        private IEnumerable CreateNestedContext(EvaluatorProperty collectionProperty);
        private IEnumerable CreateNestedJoinContext(string joinTypeName, CriteriaOperator condition, int top, out bool filtered);
        protected sealed override EvaluatorContext GetContext();
        protected sealed override EvaluatorContext GetContext(int upDepth);
        protected sealed override IEnumerable PopCollectionContext();
        protected sealed override void PushCollectionContext(IEnumerable context);
        protected sealed override void SetContext(EvaluatorContext context);
        public virtual object Visit(AggregateOperand theOperand);
        public virtual object Visit(JoinOperand theOperand);
        public virtual object Visit(OperandProperty theOperand);

        protected sealed override bool HasContext { get; }
    }
}

