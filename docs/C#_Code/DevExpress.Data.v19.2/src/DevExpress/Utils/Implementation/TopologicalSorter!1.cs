namespace DevExpress.Utils.Implementation
{
    using System;
    using System.Collections.Generic;

    public class TopologicalSorter<T>
    {
        private int[] qLink;
        private Node<T>[] nodes;
        private bool success;

        protected internal virtual unsafe void AppendRelation(int successorIndex, int predecessorIndex)
        {
            int index = predecessorIndex + 1;
            int num2 = successorIndex + 1;
            int* numPtr1 = &(this.QLink[num2]);
            numPtr1[0]++;
            this.Nodes[index] = new Node<T>(num2, this.Nodes[index]);
        }

        protected internal virtual void CalculateRelations(IList<T> sourceObjects, IsDependOnDelegate isDependOn)
        {
            int count = sourceObjects.Count;
            int y = count - 1;
            while (y >= 0)
            {
                int x = count - 1;
                while (true)
                {
                    if (x < 0)
                    {
                        y--;
                        break;
                    }
                    if (isDependOn(y, x))
                    {
                        this.AppendRelation(y, x);
                    }
                    x--;
                }
            }
        }

        protected internal int CreateVirtualNoPredecessorsItemList()
        {
            int index = 0;
            int length = this.QLink.Length;
            for (int i = 1; i < length; i++)
            {
                if (this.QLink[i] == 0)
                {
                    this.QLink[index] = i;
                    index = i;
                }
            }
            return index;
        }

        protected internal void Initialize(int n)
        {
            int num = n + 1;
            this.qLink = new int[num];
            this.nodes = new Node<T>[num];
        }

        protected virtual IList<T> ProcessNodes(int lastNoPredecessorItemIndex, IList<T> sourceObjects)
        {
            int count = sourceObjects.Count;
            int num2 = count;
            int index = this.QLink[0];
            List<T> list = new List<T>(count);
            while (index > 0)
            {
                list.Add(sourceObjects[index - 1]);
                num2--;
                Node<T> node = this.Nodes[index];
                while (true)
                {
                    if (node == null)
                    {
                        index = this.QLink[index];
                        break;
                    }
                    node = this.RemoveRelation(node, ref lastNoPredecessorItemIndex);
                }
            }
            this.success = num2 == 0;
            return list;
        }

        private unsafe Node<T> RemoveRelation(Node<T> node, ref int lastNoPredecessorItemIndex)
        {
            int oneBasedSuccessorIndex = node.OneBasedSuccessorIndex;
            int* numPtr1 = &(this.QLink[oneBasedSuccessorIndex]);
            numPtr1[0]--;
            if (this.QLink[oneBasedSuccessorIndex] == 0)
            {
                this.QLink[lastNoPredecessorItemIndex] = oneBasedSuccessorIndex;
                lastNoPredecessorItemIndex = oneBasedSuccessorIndex;
            }
            return node.Next;
        }

        public IList<T> Sort(IList<T> sourceObjects)
        {
            DefaultDependencyCalculator<T> calculator = new DefaultDependencyCalculator<T>(sourceObjects, Comparer<T>.Default);
            return this.Sort(sourceObjects, new IsDependOnDelegate(calculator.IsDependOn));
        }

        public IList<T> Sort(IList<T> sourceObjects, IsDependOnDelegate isDependOn)
        {
            int count = sourceObjects.Count;
            if (count < 2)
            {
                this.success = true;
                return sourceObjects;
            }
            this.Initialize(count);
            this.CalculateRelations(sourceObjects, isDependOn);
            int lastNoPredecessorItemIndex = this.CreateVirtualNoPredecessorsItemList();
            IList<T> list = this.ProcessNodes(lastNoPredecessorItemIndex, sourceObjects);
            return ((list.Count > 0) ? list : sourceObjects);
        }

        public IList<T> Sort(IList<T> sourceObjects, IComparer<T> comparer)
        {
            DefaultDependencyCalculator<T> calculator = new DefaultDependencyCalculator<T>(sourceObjects, (comparer != null) ? comparer : Comparer<T>.Default);
            return this.Sort(sourceObjects, new IsDependOnDelegate(calculator.IsDependOn));
        }

        protected internal Node<T>[] Nodes =>
            this.nodes;

        protected internal int[] QLink =>
            this.qLink;

        public bool Success =>
            this.success;

        private protected class DefaultDependencyCalculator
        {
            private readonly IList<T> sourceObjects;
            private readonly IComparer<T> comparer;

            public DefaultDependencyCalculator(IList<T> sourceObjects, IComparer<T> comparer)
            {
                this.sourceObjects = sourceObjects;
                this.comparer = comparer;
            }

            public bool IsDependOn(int x, int y) => 
                this.comparer.Compare(this.sourceObjects[x], this.sourceObjects[y]) > 0;
        }

        private protected class Node
        {
            private readonly int oneBasedSuccessorIndex;
            private readonly TopologicalSorter<T>.Node next;

            public Node(int oneBasedSuccessorIndex, TopologicalSorter<T>.Node next)
            {
                this.oneBasedSuccessorIndex = oneBasedSuccessorIndex;
                this.next = next;
            }

            public int OneBasedSuccessorIndex =>
                this.oneBasedSuccessorIndex;

            public TopologicalSorter<T>.Node Next =>
                this.next;
        }
    }
}

