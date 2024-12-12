namespace ActiproSoftware.ComponentModel
{
    using #H;
    using ActiproSoftware.Products.Shared;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Serializable]
    public class LogicalTreeNodeCollection : DisposableObject, IList, ICollection, IEnumerable
    {
        private bool autoAssignParent = true;
        private ArrayList list;
        private ILogicalTreeNode owner;

        [Category("Action"), Description("Occurs after an object is added to the collection.")]
        public event LogicalTreeNodeCollectionEventHandler ObjectAdded;

        [Category("Action"), Description("Occurs before an object is added to the collection.")]
        public event LogicalTreeNodeCollectionEventHandler ObjectAdding;

        [Category("Action"), Description("Occurs after an object is removed from the collection.")]
        public event LogicalTreeNodeCollectionEventHandler ObjectRemoved;

        [Category("Action"), Description("Occurs before an object is removed from the collection.")]
        public event LogicalTreeNodeCollectionEventHandler ObjectRemoving;

        private int #0Wb(object #Ld) => 
            this.AddCore((ILogicalTreeNode) #Ld);

        private void #2gd()
        {
            this.ClearCore();
        }

        private void #2Wb(object #Ld)
        {
            this.RemoveCore((ILogicalTreeNode) #Ld);
        }

        private bool #3gd(object #Ld) => 
            this.Contains((ILogicalTreeNode) #Ld);

        private int #4gd(object #Ld) => 
            this.IndexOf((ILogicalTreeNode) #Ld);

        private void #5gd(int #ahb, object #Ld)
        {
            this.InsertCore(#ahb, (ILogicalTreeNode) #Ld);
        }

        private void #6gd(int #ahb)
        {
            this.RemoveAtCore(#ahb);
        }

        public LogicalTreeNodeCollection(ILogicalTreeNode owner)
        {
            this.owner = owner;
        }

        protected int AddCore(ILogicalTreeNode value)
        {
            int count = this.InnerList.Count;
            this.InsertCore(count, value);
            return count;
        }

        protected void ClearCore()
        {
            object[] objArray = new object[this.InnerList.Count];
            for (int i = this.InnerList.Count - 1; i >= 0; i--)
            {
                ILogicalTreeNode node = (ILogicalTreeNode) this.InnerList[i];
                this.OnObjectRemoving(i, node);
                objArray[i] = node;
            }
            this.InnerList.Clear();
            for (int j = objArray.Length - 1; j >= 0; j--)
            {
                this.OnObjectRemoved(j, (ILogicalTreeNode) objArray[j]);
            }
        }

        public virtual bool Contains(ILogicalTreeNode value) => 
            this.IndexOf(value) != -1;

        public virtual void CopyTo(Array array, int arrayIndex)
        {
            this.InnerList.CopyTo(array, arrayIndex);
        }

        protected override void Dispose(bool disposing)
        {
            this.Dispose(disposing);
            if (disposing)
            {
                this.owner = null;
            }
        }

        protected void DisposeAllChildObjects()
        {
            for (int i = this.InnerList.Count - 1; i >= 0; i--)
            {
                if (i < this.InnerList.Count)
                {
                    ILogicalTreeNode node = (ILogicalTreeNode) this.InnerList[i];
                    this.RemoveAtCore(i);
                    if (node is IDisposable)
                    {
                        ((IDisposable) node).Dispose();
                    }
                }
            }
        }

        public virtual IEnumerator GetEnumerator() => 
            this.InnerList.GetEnumerator();

        public virtual int IndexOf(ILogicalTreeNode value) => 
            this.InnerList.IndexOf(value);

        protected void InsertCore(int index, ILogicalTreeNode value)
        {
            if ((index < 0) || (index > this.InnerList.Count))
            {
                throw new ArgumentOutOfRangeException(#G.#eg(0x3119), value, string.Format(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x2a2c)), index));
            }
            this.OnValidate(value, -1);
            this.OnObjectAdding(index, value);
            this.InnerList.Insert(index, value);
            this.OnObjectAdded(index, value);
        }

        protected virtual void OnObjectAdded(int index, ILogicalTreeNode value)
        {
            if (this.AutoAssignParent)
            {
                value.Parent = this.owner;
            }
            if (this.ObjectAdded != null)
            {
                this.ObjectAdded(this, new LogicalTreeNodeCollectionEventArgs(index, value));
            }
        }

        protected virtual void OnObjectAdding(int index, ILogicalTreeNode value)
        {
            if (this.ObjectAdding != null)
            {
                this.ObjectAdding(this, new LogicalTreeNodeCollectionEventArgs(index, value));
            }
        }

        protected virtual void OnObjectRemoved(int index, ILogicalTreeNode value)
        {
            if (this.ObjectRemoved != null)
            {
                this.ObjectRemoved(this, new LogicalTreeNodeCollectionEventArgs(index, value));
            }
        }

        protected virtual void OnObjectRemoving(int index, ILogicalTreeNode value)
        {
            if (this.ObjectRemoving != null)
            {
                this.ObjectRemoving(this, new LogicalTreeNodeCollectionEventArgs(index, value));
            }
            if (this.AutoAssignParent)
            {
                value.Parent = null;
            }
        }

        protected virtual void OnValidate(ILogicalTreeNode value, int existingIndex)
        {
            if (value == null)
            {
                throw new ArgumentNullException(#G.#eg(0xa3));
            }
        }

        protected void RemoveAllOfTypeCore(Type type)
        {
            for (int i = this.InnerList.Count; i >= 0; i--)
            {
                if (this.InnerList[i].GetType().Equals(type))
                {
                    this.RemoveAtCore(i);
                }
            }
        }

        protected void RemoveAtCore(int index)
        {
            ILogicalTreeNode node = this.InnerList[index] as ILogicalTreeNode;
            this.OnObjectRemoving(index, node);
            this.InnerList.RemoveAt(index);
            this.OnObjectRemoved(index, node);
        }

        protected void RemoveCore(ILogicalTreeNode value)
        {
            int index = this.InnerList.IndexOf(value);
            if (index < 0)
            {
                throw new ArgumentException(string.Format(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x29e7)), value), #G.#eg(0xa3));
            }
            this.RemoveAtCore(index);
        }

        protected void SetCore(int index, ILogicalTreeNode value)
        {
            if ((index < 0) || (index >= this.InnerList.Count))
            {
                throw new ArgumentOutOfRangeException(#G.#eg(0x3119), value, string.Format(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x2a2c)), index));
            }
            this.OnValidate(value, index);
            ILogicalTreeNode objB = this.InnerList[index] as ILogicalTreeNode;
            if (!ReferenceEquals(value, objB))
            {
                this.OnObjectRemoving(index, objB);
                this.OnObjectAdding(index, value);
                this.InnerList[index] = value;
                this.OnObjectRemoved(index, objB);
                this.OnObjectAdded(index, value);
            }
        }

        // Warning: Properties with arguments are not supported in C#. Getter of a System.Collections.IList.Item property was decompiled as a method.
        private object #3Wb(int #ahb) => 
            this.InnerList[#ahb];

        // Warning: Properties with arguments are not supported in C#. Setter of a System.Collections.IList.Item property was decompiled as a method.
        private void #4Wb(int #ahb, object #Ld)
        {
            this.SetCore(#ahb, (ILogicalTreeNode) #Ld);
        }


        protected bool AutoAssignParent
        {
            get => 
                this.autoAssignParent;
            set
            {
                if (this.autoAssignParent != value)
                {
                    this.autoAssignParent = value;
                    using (IEnumerator enumerator = this.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            ((ILogicalTreeNode) enumerator.Current).Parent = this.autoAssignParent ? this.owner : null;
                        }
                    }
                }
            }
        }

        public virtual int Count =>
            this.InnerList.Count;

        protected ArrayList InnerList
        {
            get
            {
                if (this.list == null)
                {
                    this.list = new ArrayList();
                }
                return this.list;
            }
        }

        public virtual bool IsFixedSize =>
            false;

        public virtual bool IsReadOnly =>
            true;

        public virtual bool IsSynchronized =>
            this.InnerList.IsSynchronized;

        protected ILogicalTreeNode OwnerCore
        {
            get => 
                this.owner;
            set
            {
                if (!ReferenceEquals(this.owner, value))
                {
                    this.owner = value;
                    if (this.AutoAssignParent)
                    {
                        using (IEnumerator enumerator = this.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                ((ILogicalTreeNode) enumerator.Current).Parent = this.owner;
                            }
                        }
                    }
                }
            }
        }

        public virtual object SyncRoot =>
            this.InnerList.SyncRoot;
    }
}

