namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Linq.Expressions;

    public class CriteriaCompilerRootContext : CriteriaCompilerContext
    {
        private readonly CriteriaCompilerAuxSettings _AuxSettings;
        public readonly CriteriaCompilerDescriptor Descriptor;
        public readonly ParameterExpression ThisExpression;

        public CriteriaCompilerRootContext(CriteriaCompilerDescriptor descriptor, CriteriaCompilerAuxSettings auxSettings);
        public override CriteriaCompilerLocalContext GetLocalContext(int upLevels);

        public override CriteriaCompilerAuxSettings AuxSettings { get; }
    }
}

