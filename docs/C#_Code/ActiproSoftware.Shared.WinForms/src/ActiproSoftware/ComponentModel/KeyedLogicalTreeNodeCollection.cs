namespace ActiproSoftware.ComponentModel
{
    using System;

    public class KeyedLogicalTreeNodeCollection : LogicalTreeNodeCollection
    {
        public KeyedLogicalTreeNodeCollection(ILogicalTreeNode owner);
        public virtual bool Contains(string key);
        public virtual int IndexOf(string key);
        protected override void OnValidate(ILogicalTreeNode value, int existingIndex);

        public virtual bool AllowDuplicateKeys { get; }

        public virtual bool AllowEmptyKeys { get; }
    }
}

