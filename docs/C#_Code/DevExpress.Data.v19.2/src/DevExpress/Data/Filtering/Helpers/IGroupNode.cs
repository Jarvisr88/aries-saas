namespace DevExpress.Data.Filtering.Helpers
{
    using System.Collections.Generic;

    public interface IGroupNode : INode
    {
        GroupType NodeType { get; }

        IList<INode> SubNodes { get; }
    }
}

