namespace DevExpress.Xpf.Printing.DataNodes
{
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal abstract class DataNodeBase : IDataNode
    {
        private readonly Dictionary<int, IDataNode> childNodes = new Dictionary<int, IDataNode>();
        private IDataNode root;

        protected DataNodeBase(IDataNode parent, int index)
        {
            this.Parent = parent;
            this.Index = index;
        }

        public abstract bool CanGetChild(int index);
        protected abstract IDataNode CreateChildNode(int index);
        public IDataNode GetChild(int index)
        {
            IDataNode node;
            if (!this.CanGetChild(index))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (!this.childNodes.TryGetValue(index, out node))
            {
                node = this.CreateChildNode(index);
                this.childNodes.Add(index, node);
            }
            return node;
        }

        protected virtual int GetLevel()
        {
            int num = 0;
            for (IDataNode node = this.Parent; node.Parent != null; node = node.Parent)
            {
                num++;
            }
            return num;
        }

        private IDataNode GetRoot()
        {
            IDataNode parent = this;
            while (parent.Parent != null)
            {
                parent = parent.Parent;
            }
            return parent;
        }

        protected IDataNode Root
        {
            get
            {
                this.root ??= this.GetRoot();
                return this.root;
            }
        }

        public int Index { get; private set; }

        public IDataNode Parent { get; private set; }

        public virtual bool PageBreakAfter =>
            false;

        public virtual bool PageBreakBefore =>
            false;

        public virtual bool IsDetailContainer =>
            false;
    }
}

