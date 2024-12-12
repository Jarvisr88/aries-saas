namespace ActiproSoftware.ComponentModel
{
    using System;

    public interface IKeyedLogicalTreeNode : ILogicalTreeNode
    {
        string Key { get; }
    }
}

