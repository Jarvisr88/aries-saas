namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public class CriteriaCompiledContextDescriptorDescripted : CriteriaCompilerContextDescriptorDefaultBase
    {
        private readonly PropertyDescriptorCollection PDs;

        public CriteriaCompiledContextDescriptorDescripted(PropertyDescriptorCollection pds);
        private Expression CoreAccess(Expression baseExpression, string propertPath);
        public static Expression MakeAccessFromDescriptor(Expression baseExpression, PropertyDescriptor pd);
        protected override Expression MakePathAccess(Expression baseExpression, string propertyPath);
        protected override Expression MakePropertyAccessCore(Expression baseExpression, string property);

        public override Type ObjectType { get; }
    }
}

