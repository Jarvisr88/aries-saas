namespace ActiproSoftware.ComponentModel
{
    using System;

    public class LogicalTreeNodeCollectionEventArgs : EventArgs
    {
        private int #ahb;
        private ILogicalTreeNode #3qe;

        public LogicalTreeNodeCollectionEventArgs(int index, ILogicalTreeNode logicalTreeNode);

        public int Index { get; }

        public ILogicalTreeNode LogicalTreeNode { get; }
    }
}

