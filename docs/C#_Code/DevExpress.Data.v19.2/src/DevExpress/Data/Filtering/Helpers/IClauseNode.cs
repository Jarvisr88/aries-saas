namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System.Collections.Generic;

    public interface IClauseNode : INode
    {
        OperandProperty FirstOperand { get; }

        ClauseType Operation { get; }

        IList<CriteriaOperator> AdditionalOperands { get; }
    }
}

