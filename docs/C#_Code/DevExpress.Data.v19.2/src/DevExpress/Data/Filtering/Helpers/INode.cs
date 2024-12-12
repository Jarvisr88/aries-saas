namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    public interface INode
    {
        object Accept(INodeVisitor visitor);
        void SetParentNode(IGroupNode parentNode);

        IGroupNode ParentNode { get; }
    }
}

