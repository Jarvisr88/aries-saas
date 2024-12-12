namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ExpressionEvaluator
    {
        protected internal readonly EvaluatorContextDescriptor DefaultDescriptor;
        protected readonly CriteriaOperator evaluatorCriteria;
        private readonly ExpressionEvaluatorCoreBase evaluatorCore;
        private bool throwExceptionIfNotFoundCustomFunction;
        private CustomFunctionCollection customFunctionCollection;
        private CustomAggregateCollection customAggregateCollection;

        public ExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria);
        public ExpressionEvaluator(PropertyDescriptorCollection properties, CriteriaOperator criteria);
        public ExpressionEvaluator(PropertyDescriptorCollection properties, string criteria);
        public ExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria, bool caseSensitive);
        public ExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria, ICollection<ICustomFunctionOperator> customFunctions);
        public ExpressionEvaluator(PropertyDescriptorCollection properties, CriteriaOperator criteria, bool caseSensitive);
        public ExpressionEvaluator(PropertyDescriptorCollection properties, CriteriaOperator criteria, ICollection<ICustomFunctionOperator> customFunctions);
        public ExpressionEvaluator(PropertyDescriptorCollection properties, string criteria, bool caseSensitive);
        public ExpressionEvaluator(PropertyDescriptorCollection properties, string criteria, ICollection<ICustomFunctionOperator> customFunctions);
        protected ExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria, bool caseSensitive, bool doCreateEvaluatorCore);
        public ExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria, bool caseSensitive, ICollection<ICustomFunctionOperator> customFunctions);
        public ExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria, ICollection<ICustomFunctionOperator> customFunctions, ICollection<ICustomAggregate> customAggregates);
        protected ExpressionEvaluator(PropertyDescriptorCollection properties, CriteriaOperator criteria, bool caseSensitive, bool doCreateEvaluatorCore);
        public ExpressionEvaluator(PropertyDescriptorCollection properties, CriteriaOperator criteria, bool caseSensitive, ICollection<ICustomFunctionOperator> customFunctions);
        public ExpressionEvaluator(PropertyDescriptorCollection properties, string criteria, bool caseSensitive, ICollection<ICustomFunctionOperator> customFunctions);
        public ExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria, bool caseSensitive, bool doCreateEvaluatorCore, ICollection<ICustomFunctionOperator> customFunctions);
        public ExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria, bool caseSensitive, ICollection<ICustomFunctionOperator> customFunctions, ICollection<ICustomAggregate> customAggregates);
        public ExpressionEvaluator(PropertyDescriptorCollection properties, CriteriaOperator criteria, bool caseSensitive, bool doCreateEvaluatorCore, ICollection<ICustomFunctionOperator> customFunctions);
        public ExpressionEvaluator(EvaluatorContextDescriptor descriptor, CriteriaOperator criteria, bool caseSensitive, bool doCreateEvaluatorCore, ICollection<ICustomFunctionOperator> customFunctions, ICollection<ICustomAggregate> customAggregates);
        public ExpressionEvaluator(PropertyDescriptorCollection properties, CriteriaOperator criteria, bool caseSensitive, bool doCreateEvaluatorCore, ICollection<ICustomFunctionOperator> customFunctions, ICollection<ICustomAggregate> customAggregates);
        public object Evaluate(object theObject);
        public object Evaluate(object theObject, IComparer customComparer);
        protected virtual object EvaluateCustomFunction(string functionName, params object[] operands);
        public object[] EvaluateOnObjects(IEnumerable objects);
        public object[] EvaluateOnObjects(IEnumerable objects, IComparer customComparer);
        public ICollection Filter(IEnumerable objects);
        public bool Fit(object theObject);
        protected virtual EvaluatorContext PrepareContext(object valuesSource);
        private void RegisterCustomAggregates(ICollection<ICustomAggregate> customAggregates);
        private void RegisterCustomFunctions(ICollection<ICustomFunctionOperator> customFunctions);
        protected virtual ICustomAggregate ResolveCustomAggregate(string customAggregateName);

        protected virtual ExpressionEvaluatorCoreBase EvaluatorCore { get; }

        public IEvaluatorDataAccess DataAccess { set; }

        public bool ThrowExceptionIfNotFoundCustomFunction { get; set; }
    }
}

