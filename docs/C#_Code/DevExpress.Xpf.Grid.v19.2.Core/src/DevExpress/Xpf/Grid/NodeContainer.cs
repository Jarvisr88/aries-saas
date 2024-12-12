namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class NodeContainer
    {
        internal static readonly IEnumerator<RowNode> EmptyEnumerator = new RowNode[0].GetEnumerator();
        private int startScrollIndex;
        private IEnumerator<RowNode> rowNodeEnumerator;
        private RowNode currentItem;
        internal int oldVisibleRowCount;

        public NodeContainer()
        {
            this.Items = new List<RowNode>();
        }

        internal virtual int GenerateItems(int count)
        {
            // Invalid method body.
        }

        private void GetNextItem()
        {
            if (this.RowNodeEnumerator.MoveNext())
            {
                this.currentItem = this.RowNodeEnumerator.Current;
            }
            else
            {
                this.currentItem = null;
            }
        }

        internal virtual RowNode GetNodeToScroll()
        {
            Func<RowNode, bool> predicate = <>c.<>9__34_0;
            if (<>c.<>9__34_0 == null)
            {
                Func<RowNode, bool> local1 = <>c.<>9__34_0;
                predicate = <>c.<>9__34_0 = x => x.FixedRowPosition == FixedRowPosition.None;
            }
            Func<RowNode, RowNode> evaluator = <>c.<>9__34_1;
            if (<>c.<>9__34_1 == null)
            {
                Func<RowNode, RowNode> local2 = <>c.<>9__34_1;
                evaluator = <>c.<>9__34_1 = x => x.GetNodeToScroll();
            }
            return this.Items.FirstOrDefault<RowNode>(predicate).Return<RowNode, RowNode>(evaluator, (<>c.<>9__34_2 ??= ((Func<RowNode>) (() => null))));
        }

        protected abstract IEnumerator<RowNode> GetRowDataEnumerator();
        internal virtual IEnumerator<RowNode> GetSortedItemsEnumerator() => 
            this.Items.GetEnumerator();

        protected internal virtual void OnDataChangedCore()
        {
            this.Items = new List<RowNode>();
            this.rowNodeEnumerator = null;
            this.currentItem = null;
        }

        protected virtual void OnItemsGenerated()
        {
        }

        internal virtual void ReGenerateExpandItems(int startScrollIndex, int itemsCount)
        {
            this.ReGenerateItemsCore(startScrollIndex, itemsCount);
        }

        internal void ReGenerateItemsCore(int startScrollIndex, int itemsCount)
        {
            this.startScrollIndex = startScrollIndex;
            this.OnDataChangedCore();
            this.GenerateItems(itemsCount);
        }

        private IEnumerator<RowNode> RowNodeEnumerator
        {
            get
            {
                this.rowNodeEnumerator ??= this.GetRowDataEnumerator();
                return this.rowNodeEnumerator;
            }
        }

        private RowNode CurrentItem
        {
            get
            {
                if (this.currentItem == null)
                {
                    this.GetNextItem();
                }
                return this.currentItem;
            }
        }

        public IList<RowNode> Items { get; private set; }

        internal int StartScrollIndex =>
            this.startScrollIndex;

        internal bool Initialized =>
            this.RowNodeEnumerator != null;

        public virtual int ItemCount
        {
            get
            {
                int num = 0;
                if (this.Items != null)
                {
                    foreach (RowNode node in this.Items)
                    {
                        num += node.ItemsCount;
                    }
                }
                return num;
            }
        }

        internal bool IsEnumeratorFinished =>
            ReferenceEquals(this.CurrentItem, null);

        protected internal virtual bool IsEnumeratorValid =>
            true;

        internal bool IsFinished
        {
            get
            {
                bool flag;
                if (!this.Initialized)
                {
                    return false;
                }
                using (IEnumerator<RowNode> enumerator = this.Items.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            RowNode current = enumerator.Current;
                            if (current.IsFinished)
                            {
                                continue;
                            }
                            flag = false;
                        }
                        else
                        {
                            return this.IsEnumeratorFinished;
                        }
                        break;
                    }
                }
                return flag;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NodeContainer.<>c <>9 = new NodeContainer.<>c();
            public static Func<RowNode, bool> <>9__34_0;
            public static Func<RowNode, RowNode> <>9__34_1;
            public static Func<RowNode> <>9__34_2;

            internal bool <GetNodeToScroll>b__34_0(RowNode x) => 
                x.FixedRowPosition == FixedRowPosition.None;

            internal RowNode <GetNodeToScroll>b__34_1(RowNode x) => 
                x.GetNodeToScroll();

            internal RowNode <GetNodeToScroll>b__34_2() => 
                null;
        }
    }
}

