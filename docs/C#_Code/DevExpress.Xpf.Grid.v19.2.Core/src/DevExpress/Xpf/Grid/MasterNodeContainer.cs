namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class MasterNodeContainer : DevExpress.Xpf.Grid.DetailNodeContainer
    {
        private int maxItemCount;

        public MasterNodeContainer(DataTreeBuilder treeBuilder, int level) : base(treeBuilder, level)
        {
        }

        internal override int GenerateItems(int count)
        {
            this.maxItemCount += count;
            return base.GenerateItems(count);
        }

        [IteratorStateMachine(typeof(<GetBottomRowsEnumerator>d__14))]
        protected override IEnumerator<RowNode> GetBottomRowsEnumerator()
        {
            <GetBottomRowsEnumerator>d__14 d__1 = new <GetBottomRowsEnumerator>d__14(0);
            d__1.<>4__this = this;
            return d__1;
        }

        [IteratorStateMachine(typeof(<GetFixedBottomRowsEnumerator>d__13))]
        private IEnumerator<RowNode> GetFixedBottomRowsEnumerator()
        {
            <GetFixedBottomRowsEnumerator>d__13 d__1 = new <GetFixedBottomRowsEnumerator>d__13(0);
            d__1.<>4__this = this;
            return d__1;
        }

        internal override IEnumerator<RowNode> GetSortedItemsEnumerator()
        {
            if (!base.treeBuilder.View.HasFixedRows)
            {
                return base.GetSortedItemsEnumerator();
            }
            Func<RowNode, int> keySelector = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<RowNode, int> local1 = <>c.<>9__15_0;
                keySelector = <>c.<>9__15_0 = x => (x.FixedRowPosition == FixedRowPosition.Bottom) ? 1 : 0;
            }
            return base.Items.OrderBy<RowNode, int>(keySelector).GetEnumerator();
        }

        [IteratorStateMachine(typeof(<GetTopRowsEnumerator>d__12))]
        protected override IEnumerator<RowNode> GetTopRowsEnumerator()
        {
            IEnumerator<RowNode> fixedBottomRowsEnumerator;
            this.treeBuilder.View.ActualFixedTopRowsCount = 0;
            this.treeBuilder.View.ActualFixedBottomRowsCount = 0;
            if (!this.treeBuilder.View.HasFixedRows)
            {
            }
            int totalVisibleCount = this.TotalVisibleCount;
            bool shouldBreak = false;
            int visibleIndex = 0;
        Label_PostSwitchInIterator:;
            if ((visibleIndex < totalVisibleCount) && (this.treeBuilder.View.GetFixedRowByVisibleIndex(visibleIndex) == FixedRowPosition.Top))
            {
                DataViewBase view = this.treeBuilder.View;
                view.ActualFixedTopRowsCount++;
                RowNode node = this.DataIterator.GetRowNodeForCurrentLevel(this, visibleIndex, 0, true, ref shouldBreak);
                node.FixedRowPosition = FixedRowPosition.Top;
                node.IsLastFixedRow = ((visibleIndex + 1) == totalVisibleCount) || (this.treeBuilder.View.GetFixedRowByVisibleIndex(visibleIndex + 1) != FixedRowPosition.Top);
                yield return node;
                int num2 = visibleIndex;
                visibleIndex = num2 + 1;
                goto Label_PostSwitchInIterator;
            }
            int num3 = 0;
            while (true)
            {
                if (num3 < totalVisibleCount)
                {
                    object visibleIndexByScrollIndex = this.treeBuilder.View.DataProviderBase.GetVisibleIndexByScrollIndex((totalVisibleCount - num3) - 1);
                    if ((visibleIndexByScrollIndex is int) && (this.treeBuilder.View.GetFixedRowByVisibleIndex((int) visibleIndexByScrollIndex) == FixedRowPosition.Bottom))
                    {
                        DataViewBase view = this.treeBuilder.View;
                        view.ActualFixedBottomRowsCount++;
                        num3++;
                        continue;
                    }
                }
                if (this.treeBuilder.GenerateBottomFixedRowInEnd)
                {
                    break;
                }
                else
                {
                    fixedBottomRowsEnumerator = this.GetFixedBottomRowsEnumerator();
                }
                break;
            }
            while (true)
            {
                while (fixedBottomRowsEnumerator.MoveNext())
                {
                    yield return fixedBottomRowsEnumerator.Current;
                }
                fixedBottomRowsEnumerator = null;
            }
        }

        internal void OnDataChanged()
        {
            this.OnDataChangedCore();
            this.OnItemsGenerated();
        }

        protected internal override void OnDataChangedCore()
        {
            this.maxItemCount = 0;
            base.OnDataChangedCore();
        }

        protected override void OnItemsGenerated()
        {
            base.OnItemsGenerated();
            base.treeBuilder.SynchronizeMasterNode();
            base.oldVisibleRowCount = base.treeBuilder.View.DataControl.VisibleRowCount;
            this.UpdateLineLevel(base.treeBuilder.View.MasterRootRowsContainer.Items);
            base.treeBuilder.View.UpdateCellMergingPanels(false);
            base.treeBuilder.View.UpdateNewItemRowData();
            base.treeBuilder.View.DataControl.OnItemsGenerated();
        }

        internal void ReGenerateItemsCore()
        {
            if (base.treeBuilder.View.DataControl != null)
            {
                base.treeBuilder.View.layoutUpdatedLocker.DoLockedAction(() => base.ReGenerateItemsCore(base.StartScrollIndex, this.ItemCount));
                base.treeBuilder.ForceLayout();
            }
        }

        private void UpdateLineLevel(IList<RowDataBase> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                RowDataBase base2 = items[i];
                base2.UpdateLineLevel();
                if (base2.RowsContainer != null)
                {
                    this.UpdateLineLevel(base2.RowsContainer.Items);
                }
            }
        }

        public override int ItemCount =>
            Math.Min(base.ItemCount, this.maxItemCount);

        protected internal override int FixedTopRowsCount =>
            base.treeBuilder.View.ActualFixedTopRowsCount;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MasterNodeContainer.<>c <>9 = new MasterNodeContainer.<>c();
            public static Func<RowNode, int> <>9__15_0;

            internal int <GetSortedItemsEnumerator>b__15_0(RowNode x) => 
                (x.FixedRowPosition == FixedRowPosition.Bottom) ? 1 : 0;
        }

        [CompilerGenerated]
        private sealed class <GetBottomRowsEnumerator>d__14 : IEnumerator<RowNode>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private RowNode <>2__current;
            public MasterNodeContainer <>4__this;
            private IEnumerator<RowNode> <bottomRowsEnumerator>5__1;

            [DebuggerHidden]
            public <GetBottomRowsEnumerator>d__14(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    if (!this.<>4__this.treeBuilder.GenerateBottomFixedRowInEnd)
                    {
                        goto TR_0001;
                    }
                    else
                    {
                        this.<bottomRowsEnumerator>5__1 = this.<>4__this.GetFixedBottomRowsEnumerator();
                    }
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                }
                if (this.<bottomRowsEnumerator>5__1.MoveNext())
                {
                    this.<>2__current = this.<bottomRowsEnumerator>5__1.Current;
                    this.<>1__state = 1;
                    return true;
                }
                this.<bottomRowsEnumerator>5__1 = null;
            TR_0001:
                return false;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            RowNode IEnumerator<RowNode>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetFixedBottomRowsEnumerator>d__13 : IEnumerator<RowNode>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private RowNode <>2__current;
            public MasterNodeContainer <>4__this;
            private bool <shouldBreak>5__1;
            private int <lastVisibleCount>5__2;
            private int <i>5__3;

            [DebuggerHidden]
            public <GetFixedBottomRowsEnumerator>d__13(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<lastVisibleCount>5__2 = this.<>4__this.TotalVisibleCount;
                    this.<shouldBreak>5__1 = false;
                    this.<i>5__3 = this.<lastVisibleCount>5__2 - this.<>4__this.treeBuilder.View.ActualFixedBottomRowsCount;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    int num2 = this.<i>5__3;
                    this.<i>5__3 = num2 + 1;
                }
                if (this.<i>5__3 >= this.<lastVisibleCount>5__2)
                {
                    return false;
                }
                RowNode node = this.<>4__this.DataIterator.GetRowNodeForCurrentLevel(this.<>4__this, this.<i>5__3, 0, true, ref this.<shouldBreak>5__1);
                node.FixedRowPosition = FixedRowPosition.Bottom;
                node.IsLastFixedRow = this.<i>5__3 == (this.<lastVisibleCount>5__2 - this.<>4__this.treeBuilder.View.ActualFixedBottomRowsCount);
                this.<>2__current = node;
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            RowNode IEnumerator<RowNode>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

    }
}

