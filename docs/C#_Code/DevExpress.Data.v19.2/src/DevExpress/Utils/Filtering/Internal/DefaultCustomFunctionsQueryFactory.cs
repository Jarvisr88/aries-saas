namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    internal sealed class DefaultCustomFunctionsQueryFactory : ICustomFunctionsQueryFactory
    {
        internal static readonly ICustomFunctionsQueryFactory Instance = new DefaultCustomFunctionsQueryFactory();

        private DefaultCustomFunctionsQueryFactory()
        {
        }

        DevExpress.Data.Filtering.CustomFunctionEventArgs ICustomFunctionsQueryFactory.Create(ICustomUIFilters filters) => 
            CriteriaOperator.DoQueryCustomFunctions(new CustomFunctionEventArgs(filters));

        private sealed class CustomFunctionEventArgs : DevExpress.Data.Filtering.CustomFunctionEventArgs
        {
            public CustomFunctionEventArgs(ICustomUIFilters filters) : base(filters.Metric.Path, filters.Metric.Type, false)
            {
            }
        }
    }
}

