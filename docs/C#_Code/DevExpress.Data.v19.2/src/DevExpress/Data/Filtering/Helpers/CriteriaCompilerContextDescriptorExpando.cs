namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Linq.Expressions;

    public class CriteriaCompilerContextDescriptorExpando : CriteriaCompilerContextDescriptorDefaultBase
    {
        public static readonly CriteriaCompilerContextDescriptorExpando Instance;

        static CriteriaCompilerContextDescriptorExpando();
        protected override Expression MakePropertyAccessCore(Expression baseExpression, string property);

        public override Type ObjectType { get; }
    }
}

