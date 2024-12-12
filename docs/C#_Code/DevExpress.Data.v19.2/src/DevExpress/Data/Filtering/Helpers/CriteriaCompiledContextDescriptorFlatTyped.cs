namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Linq.Expressions;

    public class CriteriaCompiledContextDescriptorFlatTyped : CriteriaCompiledContextDescriptorTyped
    {
        public CriteriaCompiledContextDescriptorFlatTyped(Type typeWithFlatProperties);
        protected override Expression MakePathAccess(Expression baseExpression, string propertyPath);
    }
}

