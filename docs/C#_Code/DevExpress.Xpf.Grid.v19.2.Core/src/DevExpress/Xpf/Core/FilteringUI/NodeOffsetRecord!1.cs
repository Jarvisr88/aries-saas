namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class NodeOffsetRecord<TNode> where TNode: class
    {
        public NodeOffsetRecord(TNode node, double verticalOffset)
        {
            Guard.ArgumentNotNull(node, "node");
            this.<Node>k__BackingField = node;
            this.<VerticalOffset>k__BackingField = verticalOffset;
        }

        public TNode Node { get; }

        public double VerticalOffset { get; }
    }
}

