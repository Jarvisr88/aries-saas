namespace DevExpress.Data.Browsing
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;

    internal class CalculatedExpressionEvaluator : ExpressionEvaluator
    {
        private readonly ExpressionEvaluatorCoreBase evaluatorCore;

        public CalculatedExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria, bool caseSensitive);
        public CalculatedExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria, bool caseSensitive, ICollection<ICustomFunctionOperator> customFunctions);
        public CalculatedExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria, bool caseSensitive, ICollection<ICustomFunctionOperator> customFunctions, ICollection<ICustomAggregate> customAggregates);

        protected override ExpressionEvaluatorCoreBase EvaluatorCore { get; }
    }
}

