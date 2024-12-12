namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class TabsDetailInfo : DetailInfoWithContent
    {
        private int selectedTabIndex;
        private Dictionary<DetailDescriptorBase, DetailInfoWithContent> detailInfoCache;
        private DetailInfoWithContent selectedDetailInfo;

        public TabsDetailInfo(TabViewDetailDescriptor tabDetailDescriptor, RowDetailContainer container) : base(tabDetailDescriptor, container)
        {
            this.detailInfoCache = new Dictionary<DetailDescriptorBase, DetailInfoWithContent>();
        }

        public override int CalcTotalLevel() => 
            base.CalcTotalLevel() + ((this.selectedDetailInfo != null) ? this.selectedDetailInfo.CalcTotalLevel() : 0);

        internal override DetailInfoWithContent.DetailRowNode CreateDetailRowNode(DetailNodeKind detailNodeKind, Func<DetailNodeKind, RowDataBase> createRowDataDelegate) => 
            (detailNodeKind != DetailNodeKind.TabHeaders) ? base.CreateDetailRowNode(detailNodeKind, createRowDataDelegate) : new TabHeadersRowNode(this, createRowDataDelegate);

        public override DataViewBase FindFirstDetailView() => 
            this.selectedDetailInfo?.FindFirstDetailView();

        public override DataViewBase FindLastInnerDetailView() => 
            this.selectedDetailInfo?.FindLastInnerDetailView();

        public override bool FindViewAndVisibleIndexByScrollIndex(int scrollIndex, bool forwardIfServiceRow, out DataViewBase targetView, out int targetVisibleIndex)
        {
            if (this.selectedDetailInfo != null)
            {
                return this.selectedDetailInfo.FindViewAndVisibleIndexByScrollIndex(scrollIndex, forwardIfServiceRow, out targetView, out targetVisibleIndex);
            }
            targetView = null;
            targetVisibleIndex = -1;
            return false;
        }

        public override DataControlBase FindVisibleDetailDataControl() => 
            this.selectedDetailInfo?.FindVisibleDetailDataControl();

        public override DetailDescriptorBase FindVisibleDetailDescriptor() => 
            (this.selectedDetailInfo != null) ? this.selectedDetailInfo.FindVisibleDetailDescriptor() : this.TabDetailDescriptor;

        private void FocusViewInsideTabOrMaster()
        {
            DataViewBase base2 = null;
            if (this.selectedDetailInfo != null)
            {
                base2 = this.selectedDetailInfo.FindFirstDetailView();
            }
            if (base2 == null)
            {
                if (base.MasterDataControl.GetAllowInitiallyFocusedRow())
                {
                    base.MasterDataControl.DataView.MoveFocusedRow(base.MasterVisibleIndex);
                }
            }
            else if ((base2.DataControl == null) || base2.DataControl.GetAllowInitiallyFocusedRow())
            {
                base2.MoveFocusedRow(0);
            }
        }

        protected internal override DataControlDetailInfo GetActualDetailInfo(DataControlDetailDescriptor detailDescriptor) => 
            (DataControlDetailInfo) detailDescriptor.CreateRowDetailInfo(base.container);

        protected internal override int GetBottomServiceRowsCount() => 
            base.CalcBottomRowCount();

        protected internal override int GetNextLevelRowCount() => 
            (this.selectedDetailInfo != null) ? this.selectedDetailInfo.CalcRowsCount() : 0;

        protected internal override DetailNodeKind[] GetNodeScrollOrder() => 
            new DetailNodeKind[] { DetailNodeKind.TabHeaders, DetailNodeKind.DetailContent, DetailNodeKind.DetailHeader, DetailNodeKind.TopMarginContainer, DetailNodeKind.BottomMarginContainer };

        [IteratorStateMachine(typeof(<GetRowDetailInfoEnumerator>d__33))]
        internal override IEnumerable<RowDetailInfoBase> GetRowDetailInfoEnumerator()
        {
            <GetRowDetailInfoEnumerator>d__33 d__1 = new <GetRowDetailInfoEnumerator>d__33(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        [IteratorStateMachine(typeof(<GetRowNodesEnumeratorCore>d__18))]
        internal override IEnumerator<RowNode> GetRowNodesEnumeratorCore(int nextLevelStartVisibleIndex, int nextLevelVisibleRowsCount, int serviceVisibleRowsCount)
        {
            int serviceVisibleRowsCount;
            bool <isBottomMarginVisible>5__1 = this.detailDescriptor.HasBottomMargin && (serviceVisibleRowsCount > 0);
            if (<isBottomMarginVisible>5__1)
            {
                serviceVisibleRowsCount = serviceVisibleRowsCount;
                serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
            }
            if (this.detailDescriptor.HasTopMargin && (serviceVisibleRowsCount > 0))
            {
                serviceVisibleRowsCount = serviceVisibleRowsCount;
                serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                yield return this.GetDetailMarginNode(DetailNodeKind.TopMarginContainer, false);
            }
            if (this.detailDescriptor.ShowHeader && (serviceVisibleRowsCount > 0))
            {
                serviceVisibleRowsCount = serviceVisibleRowsCount;
                serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                yield return this.GetDetailHeaderNode(false);
            }
            if ((this.TabDetailDescriptor.ContentTemplate != null) && (serviceVisibleRowsCount > 0))
            {
                serviceVisibleRowsCount = serviceVisibleRowsCount;
                serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                yield return this.GetDetailContentNode(false);
            }
            if (serviceVisibleRowsCount > 0)
            {
                DetailInfoWithContent.DetailRowNode rowNode = this.GetRowNode(DetailNodeKind.TabHeaders, nodeKind => new TabHeadersRowData(() => base.CreateDetailHeaderControl(new Func<DetailHeaderControlBase>(base.MasterTableViewBehavior.CreateDetailTabHeadersElement))));
                if ((this.selectedDetailInfo != null) && (this.selectedDetailInfo.NodeContainer != null))
                {
                    if (nextLevelVisibleRowsCount > 0)
                    {
                        rowNode.UpdateExpandInfoEx(this.selectedDetailInfo.NodeContainer, this.selectedDetailInfo.RowsContainer, nextLevelStartVisibleIndex, true);
                    }
                    else
                    {
                        rowNode.UpdateExpandInfoEx(null, null, nextLevelStartVisibleIndex, true);
                    }
                }
                yield return rowNode;
            }
            while (true)
            {
                if (<isBottomMarginVisible>5__1)
                {
                    serviceVisibleRowsCount = serviceVisibleRowsCount;
                    serviceVisibleRowsCount = serviceVisibleRowsCount - 1;
                    yield return this.GetDetailMarginNode(DetailNodeKind.BottomMarginContainer, true);
                }
            }
        }

        public override RowsContainer GetRowsContainerAndUpdateMasterRowData(RowData masterRowData)
        {
            this.UpdateSelectedDetailInfoMasterRowData(masterRowData);
            return base.GetRowsContainerAndUpdateMasterRowData(masterRowData);
        }

        protected internal override int GetTopServiceRowsCount() => 
            (base.CalcHeaderRowCount() + base.CalcContentRowCount()) + 1;

        protected internal override int GetVisibleDataRowCount() => 
            (this.selectedDetailInfo != null) ? this.selectedDetailInfo.CalcVisibleDataRowCount() : 0;

        public override bool IsDetailRowExpanded(DetailDescriptorBase descriptor)
        {
            if (!this.IsExpanded)
            {
                return false;
            }
            if (descriptor == null)
            {
                return true;
            }
            int index = this.TabDetailDescriptor.DetailDescriptors.IndexOf(descriptor);
            return ((index > -1) && (index == this.SelectedTabIndex));
        }

        protected internal override void OnCollapsed()
        {
            if (this.selectedDetailInfo != null)
            {
                this.selectedDetailInfo.IsExpanded = false;
            }
        }

        protected internal override void OnExpanded()
        {
            base.OnExpanded();
            this.ValidateChildDetailInfo();
        }

        public override void SetDetailRowExpanded(bool expand, DetailDescriptorBase descriptor)
        {
            base.SetDetailRowExpanded(expand, descriptor);
            if (this.IsExpanded && (descriptor != null))
            {
                int index = this.TabDetailDescriptor.DetailDescriptors.IndexOf(descriptor);
                if (index > -1)
                {
                    this.SelectedTabIndex = index;
                }
            }
        }

        public override void UpdateMasterRowData(RowData masterRowData)
        {
            base.UpdateMasterRowData(masterRowData);
            this.UpdateSelectedDetailInfoMasterRowData(masterRowData);
        }

        private void UpdateSelectedDetailInfoMasterRowData(RowData masterRowData)
        {
            if (this.selectedDetailInfo != null)
            {
                this.selectedDetailInfo.UpdateMasterRowData(masterRowData);
            }
        }

        private void ValidateChildDetailInfo()
        {
            if (!this.IsExpanded)
            {
                throw new InvalidOperationException();
            }
            if (this.TabDetailDescriptor.DetailDescriptors.Count != 0)
            {
                if (this.selectedDetailInfo != null)
                {
                    this.selectedDetailInfo.IsExpanded = false;
                }
                DetailDescriptorBase key = this.TabDetailDescriptor.DetailDescriptors[this.SelectedTabIndex];
                if (!this.detailInfoCache.TryGetValue(key, out this.selectedDetailInfo))
                {
                    this.detailInfoCache[key] = this.selectedDetailInfo = key.CreateRowDetailInfo(base.container);
                }
                this.UpdateSelectedDetailInfoMasterRowData(this.RowsContainer.MasterRowData);
                this.selectedDetailInfo.IsExpanded = true;
            }
        }

        public int SelectedTabIndex
        {
            get => 
                this.selectedTabIndex;
            set
            {
                if (this.selectedTabIndex != value)
                {
                    this.selectedTabIndex = value;
                    this.ValidateChildDetailInfo();
                    base.detailDescriptor.InvalidateTree();
                    this.FocusViewInsideTabOrMaster();
                }
            }
        }

        private TabViewDetailDescriptor TabDetailDescriptor =>
            (TabViewDetailDescriptor) base.detailDescriptor;

        [CompilerGenerated]
        private sealed class <GetRowDetailInfoEnumerator>d__33 : IEnumerable<RowDetailInfoBase>, IEnumerable, IEnumerator<RowDetailInfoBase>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private RowDetailInfoBase <>2__current;
            private int <>l__initialThreadId;
            public TabsDetailInfo <>4__this;
            private Dictionary<DetailDescriptorBase, DetailInfoWithContent>.ValueCollection.Enumerator <>7__wrap1;
            private IEnumerator<RowDetailInfoBase> <>7__wrap2;

            [DebuggerHidden]
            public <GetRowDetailInfoEnumerator>d__33(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                this.<>7__wrap1.Dispose();
            }

            private void <>m__Finally2()
            {
                this.<>1__state = -3;
                if (this.<>7__wrap2 != null)
                {
                    this.<>7__wrap2.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            this.<>2__current = this.<>4__this;
                            this.<>1__state = 1;
                            return true;

                        case 1:
                            this.<>1__state = -1;
                            this.<>7__wrap1 = this.<>4__this.detailInfoCache.Values.GetEnumerator();
                            this.<>1__state = -3;
                            break;

                        case 2:
                            this.<>1__state = -4;
                            goto TR_0007;

                        default:
                            return false;
                    }
                    goto TR_000A;
                TR_0007:
                    if (this.<>7__wrap2.MoveNext())
                    {
                        RowDetailInfoBase current = this.<>7__wrap2.Current;
                        this.<>2__current = current;
                        this.<>1__state = 2;
                        flag = true;
                    }
                    else
                    {
                        this.<>m__Finally2();
                        this.<>7__wrap2 = null;
                        goto TR_000A;
                    }
                    return flag;
                TR_000A:
                    while (true)
                    {
                        if (this.<>7__wrap1.MoveNext())
                        {
                            RowDetailInfoBase current = this.<>7__wrap1.Current;
                            this.<>7__wrap2 = current.GetRowDetailInfoEnumerator().GetEnumerator();
                            this.<>1__state = -4;
                        }
                        else
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = new Dictionary<DetailDescriptorBase, DetailInfoWithContent>.ValueCollection.Enumerator();
                            return false;
                        }
                        break;
                    }
                    goto TR_0007;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<RowDetailInfoBase> IEnumerable<RowDetailInfoBase>.GetEnumerator()
            {
                TabsDetailInfo.<GetRowDetailInfoEnumerator>d__33 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new TabsDetailInfo.<GetRowDetailInfoEnumerator>d__33(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Grid.Native.RowDetailInfoBase>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if (((num == -4) || (num == -3)) || (num == 2))
                {
                    try
                    {
                        if ((num == -4) || (num == 2))
                        {
                            try
                            {
                            }
                            finally
                            {
                                this.<>m__Finally2();
                            }
                        }
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            RowDetailInfoBase IEnumerator<RowDetailInfoBase>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }


        public class TabHeadersRowData : DetailInfoWithContent.DetailRowData
        {
            public static readonly DependencyProperty SelectedTabIndexProperty;
            internal Locker SelectedIndexLocker;

            static TabHeadersRowData()
            {
                SelectedTabIndexProperty = DependencyProperty.Register("SelectedTabIndex", typeof(int), typeof(TabsDetailInfo.TabHeadersRowData), new PropertyMetadata(0, (d, e) => ((TabsDetailInfo.TabHeadersRowData) d).OnSelectedTabIndexChanged()));
            }

            public TabHeadersRowData(Func<FrameworkElement> createRowElementDelegate) : base(DetailNodeKind.TabHeaders, createRowElementDelegate)
            {
                this.SelectedIndexLocker = new Locker();
            }

            internal void AssignFrom(TabsDetailInfo currentTabsDetailInfo)
            {
                this.CurrentTabsDetailInfo = currentTabsDetailInfo;
                this.SelectedIndexLocker.DoLockedAction(() => this.SelectedTabIndex = currentTabsDetailInfo.SelectedTabIndex);
            }

            private void OnSelectedTabIndexChanged()
            {
                this.CurrentTabsDetailInfo.SelectedTabIndex = this.SelectedTabIndex;
            }

            public int SelectedTabIndex
            {
                get => 
                    (int) base.GetValue(SelectedTabIndexProperty);
                set => 
                    base.SetValue(SelectedTabIndexProperty, value);
            }

            internal TabsDetailInfo CurrentTabsDetailInfo { get; private set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly TabsDetailInfo.TabHeadersRowData.<>c <>9 = new TabsDetailInfo.TabHeadersRowData.<>c();

                internal void <.cctor>b__12_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
                {
                    ((TabsDetailInfo.TabHeadersRowData) d).OnSelectedTabIndexChanged();
                }
            }
        }

        internal class TabHeadersRowNode : DetailInfoWithContent.DetailRowNode
        {
            public TabHeadersRowNode(TabsDetailInfo owner, Func<DetailNodeKind, RowDataBase> createRowDataDelegate) : base(owner, DetailNodeKind.TabHeaders, createRowDataDelegate)
            {
            }

            internal override void AssignToRowData(RowDataBase rowData, RowDataBase masterRowData)
            {
                ((TabsDetailInfo.TabHeadersRowData) rowData).AssignFrom((TabsDetailInfo) base.owner);
                if (rowData is TabsDetailInfo.TabHeadersRowData)
                {
                    (rowData as TabsDetailInfo.TabHeadersRowData).SetMasterView(masterRowData.View);
                }
            }
        }
    }
}

