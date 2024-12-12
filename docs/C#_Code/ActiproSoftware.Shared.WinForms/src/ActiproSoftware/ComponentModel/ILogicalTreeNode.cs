namespace ActiproSoftware.ComponentModel
{
    using System;
    using System.Collections;

    public interface ILogicalTreeNode
    {
        object FindAncestor(Type type);
        ILogicalTreeNode GetCommonAncestor(ILogicalTreeNode value);
        bool IsAncestorOf(ILogicalTreeNode value);
        bool IsDescendantOf(ILogicalTreeNode value);

        IList Children { get; }

        ILogicalTreeNode Parent { get; set; }
    }
}

