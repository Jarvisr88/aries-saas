namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Linq.Expressions;

    public class CriteriaCompiledContextDescriptorTyped : CriteriaCompilerContextDescriptorDefaultBase
    {
        private readonly System.Type Type;

        public CriteriaCompiledContextDescriptorTyped(System.Type type);
        protected override Expression MakePropertyAccessCore(Expression baseExpression, string property);

        public override System.Type ObjectType { get; }
    }
}

