namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public class NodeToCriteriaProcessor : INodeVisitor
    {
        public CriteriaOperator Process(INode node);
        public virtual object Visit(IAggregateNode ign);
        public virtual object Visit(IClauseNode icn);
        public virtual object Visit(IGroupNode ign);
        public virtual object Visit(CriteriaOperator firstOperand, object functionType, CriteriaOperator additionalOperand);
        public virtual object Visit(ClauseType clauseType, CriteriaOperator firstOperand, IList<CriteriaOperator> additionalOperands);
    }
}

