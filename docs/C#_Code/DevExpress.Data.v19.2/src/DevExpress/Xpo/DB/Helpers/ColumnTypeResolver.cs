namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpo.DB;
    using System;

    internal class ColumnTypeResolver : CriteriaTypeResolverBase, IQueryCriteriaVisitor<CriteriaTypeResolverResult>, ICriteriaVisitor<CriteriaTypeResolverResult>
    {
        private readonly ConnectionProviderSql provider;

        internal ColumnTypeResolver(ConnectionProviderSql provider)
        {
            this.provider = provider;
        }

        CriteriaTypeResolverResult IQueryCriteriaVisitor<CriteriaTypeResolverResult>.Visit(QueryOperand theOperand) => 
            new CriteriaTypeResolverResult(DBColumn.GetType(theOperand.ColumnType));

        CriteriaTypeResolverResult IQueryCriteriaVisitor<CriteriaTypeResolverResult>.Visit(QuerySubQueryContainer subQueryContainer)
        {
            Aggregate aggregateType = subQueryContainer.AggregateType;
            switch (aggregateType)
            {
                case Aggregate.Exists:
                    return new CriteriaTypeResolverResult(typeof(bool));

                case Aggregate.Count:
                    return new CriteriaTypeResolverResult(typeof(int));

                case Aggregate.Max:
                case Aggregate.Min:
                    break;

                case Aggregate.Avg:
                {
                    CriteriaTypeResolverResult result = base.Process(subQueryContainer.AggregateProperty);
                    return (((result.Type == typeof(decimal)) || (result.Type == typeof(float))) ? result : new CriteriaTypeResolverResult(typeof(double)));
                }
                default:
                {
                    if (aggregateType != Aggregate.Custom)
                    {
                        break;
                    }
                    Type[] operands = new Type[subQueryContainer.CustomAggregateOperands.Count];
                    for (int i = 0; i < operands.Length; i++)
                    {
                        CriteriaTypeResolverResult result2 = base.Process(subQueryContainer.CustomAggregateOperands[i]);
                        operands[i] = result2.Type;
                    }
                    return new CriteriaTypeResolverResult(this.GetCustomAggregateType(subQueryContainer.CustomAggregateName, operands));
                }
            }
            return base.Process(subQueryContainer.AggregateProperty);
        }

        protected override Type GetCustomAggregateType(string customAggregateName, params Type[] operands)
        {
            ICustomAggregate customAggregate = null;
            if (this.provider != null)
            {
                customAggregate = this.provider.GetCustomAggregate(customAggregateName);
            }
            return ((customAggregate != null) ? customAggregate.ResultType(operands) : base.GetCustomAggregateType(customAggregateName, operands));
        }

        protected override Type GetCustomFunctionType(string functionName, params Type[] operands)
        {
            ICustomFunctionOperator customFunctionOperator = null;
            if (this.provider != null)
            {
                customFunctionOperator = this.provider.GetCustomFunctionOperator(functionName);
            }
            return ((customFunctionOperator != null) ? customFunctionOperator.ResultType(operands) : base.GetCustomFunctionType(functionName, operands));
        }

        public static Type ResolveType(CriteriaOperator criteria, ConnectionProviderSql provider) => 
            new ColumnTypeResolver(provider).ResolveTypeInternal(criteria);

        internal Type ResolveTypeInternal(CriteriaOperator criteria) => 
            base.Process(criteria).Type;
    }
}

