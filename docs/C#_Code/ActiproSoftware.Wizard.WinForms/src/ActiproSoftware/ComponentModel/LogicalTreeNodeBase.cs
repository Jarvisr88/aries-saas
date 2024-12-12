namespace ActiproSoftware.ComponentModel
{
    using System;
    using System.Collections;

    public abstract class LogicalTreeNodeBase : DisposableObject, ILogicalTreeNode
    {
        private IList #bj;
        private ILogicalTreeNode #Seb;

        private object #ewe(Type #4Ub) => 
            (this.#Seb != null) ? (!this.IsInstanceOfType(this.#Seb) ? this.#Seb.FindAncestor(#4Ub) : this.#Seb) : null;

        private ILogicalTreeNode #fwe(ILogicalTreeNode #Ld)
        {
            if ((#Ld.Parent != null) && (this.#Seb != null))
            {
                if (ReferenceEquals(#Ld, this))
                {
                    return #Ld;
                }
                if (((ILogicalTreeNode) this).IsAncestorOf(#Ld))
                {
                    return this;
                }
                if (((ILogicalTreeNode) this).IsDescendantOf(#Ld))
                {
                    return #Ld;
                }
                for (ILogicalTreeNode node = #Ld; node.Parent != null; node = node.Parent)
                {
                    if (node.Parent.IsAncestorOf(this))
                    {
                        return node.Parent;
                    }
                }
            }
            return null;
        }

        private bool #gwe(ILogicalTreeNode #Ld)
        {
            ILogicalTreeNode parent;
            if (0 != 0)
            {
                ILogicalTreeNode local1 = #Ld;
            }
            else
            {
                parent = #Ld;
            }
            while (parent.Parent != null)
            {
                parent = parent.Parent;
                if (ReferenceEquals(parent, this))
                {
                    return true;
                }
            }
            return false;
        }

        private bool #hwe(ILogicalTreeNode #Ld) => 
            #Ld.IsAncestorOf(this);

        public LogicalTreeNodeBase()
        {
            this.#bj = this.CreateChildren();
        }

        protected virtual IList CreateChildren() => 
            null;

        protected virtual void OnParentChanged()
        {
        }

        private IList ActiproSoftware.ComponentModel.ILogicalTreeNode.Children =>
            this.#bj;

        private ILogicalTreeNode ActiproSoftware.ComponentModel.ILogicalTreeNode.Parent
        {
            get => 
                this.#Seb;
            set
            {
                if (!ReferenceEquals(this.#Seb, value))
                {
                    this.#Seb = value;
                    this.OnParentChanged();
                }
            }
        }
    }
}

