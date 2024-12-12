namespace DevExpress.Data.ODataLinq.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;

    internal class ElementTypeResolver : CriteriaTypeResolverBase, IClientCriteriaVisitor<CriteriaTypeResolverResult>, ICriteriaVisitor<CriteriaTypeResolverResult>
    {
        private Dictionary<string, Type> propertiesTypes;

        public ElementTypeResolver(Dictionary<string, Type> propertiesTypes);
        public Type Resolve(CriteriaOperator criteria);
        public CriteriaTypeResolverResult Visit(AggregateOperand theOperand);
        public CriteriaTypeResolverResult Visit(JoinOperand theOperand);
        public CriteriaTypeResolverResult Visit(OperandProperty theOperand);
    }
}

