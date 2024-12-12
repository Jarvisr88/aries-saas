namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    public interface INodeVisitor
    {
        object Visit(IAggregateNode aggregate);
        object Visit(IClauseNode clause);
        object Visit(IGroupNode group);
    }
}

