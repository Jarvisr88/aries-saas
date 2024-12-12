namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Linq;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class CollectionViewDefaultDataView : ListServerDataView, IItemsProviderCollectionViewSupport
    {
        private readonly ICollectionViewHelper collectionViewHelper;
        private readonly Func<bool> syncWithCurrent;
        private readonly DataControllerICollectionViewSupport collectionViewSupport;
        private readonly bool useCollectionView;

        public event EventHandler<ItemsProviderCurrentChangedEventArgs> CurrentChanged;

        public CollectionViewDefaultDataView(bool selectNullValue, bool useCollectionView, Func<bool> syncWithCurrent, IListServer server, string valueMember, string displayMember, IEnumerable<GroupingInfo> groups, IEnumerable<SortingInfo> sorts, string filter) : base(selectNullValue, server, valueMember, displayMember, groups, sorts, filter)
        {
            this.useCollectionView = useCollectionView;
            this.syncWithCurrent = syncWithCurrent;
            this.collectionViewHelper = (ICollectionViewHelper) server;
            this.collectionViewSupport = new DataControllerICollectionViewSupport(this);
        }

        private void CollectionViewHelperFilterSortGroupInfoChanged(object sender, CollectionViewFilterSortGroupInfoChangedEventArgs e)
        {
            base.RaiseInconsistencyDetected();
        }

        private CurrentDataView CreateCollectionViewDataView(object handle, IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria, bool caseSensitiveFilter) => 
            (GetIsCurrentViewFIltered(groups, sorts, filterCriteria) || !string.IsNullOrEmpty(displayFilterCriteria)) ? ((CurrentDataView) new CollectionViewCurrentFilteredSortedDataView(base.selectNullValue, this, handle, base.DataAccessor.ValueMember, base.DataAccessor.DisplayMember, groups, sorts, filterCriteria, displayFilterCriteria, caseSensitiveFilter)) : ((CurrentDataView) new CollectionViewCurrentDataView(base.selectNullValue, this, handle, base.DataAccessor.ValueMember, base.DataAccessor.DisplayMember));

        public override CurrentDataView CreateCurrentDataView(object handle, IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria, bool caseSensitiveFilter) => 
            this.useCollectionView ? this.CreateCollectionViewDataView(handle, groups, sorts, filterCriteria, displayFilterCriteria, caseSensitiveFilter) : this.CreatePlainDataView(handle, groups, sorts, filterCriteria, displayFilterCriteria, caseSensitiveFilter);

        private CurrentDataView CreatePlainDataView(object handle, IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria, bool caseSensitiveFilter)
        {
            bool flag1;
            Func<IList<SortingInfo>, bool> evaluator = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                Func<IList<SortingInfo>, bool> local1 = <>c.<>9__26_0;
                evaluator = <>c.<>9__26_0 = x => x.Count > 0;
            }
            if (!sorts.If<IList<SortingInfo>>(evaluator).ReturnSuccess<IList<SortingInfo>>())
            {
                Func<IList<GroupingInfo>, bool> func2 = <>c.<>9__26_1;
                if (<>c.<>9__26_1 == null)
                {
                    Func<IList<GroupingInfo>, bool> local2 = <>c.<>9__26_1;
                    func2 = <>c.<>9__26_1 = x => x.Count > 0;
                }
                if (!groups.If<IList<GroupingInfo>>(func2).ReturnSuccess<IList<GroupingInfo>>() && string.IsNullOrEmpty(filterCriteria))
                {
                    flag1 = !string.IsNullOrEmpty(displayFilterCriteria);
                    goto TR_0000;
                }
            }
            flag1 = true;
        TR_0000:
            return (flag1 ? ((CurrentDataView) new LocalCurrentFilteredSortedDataView(base.selectNullValue, this, handle, base.DataAccessor.ValueMember, base.DataAccessor.DisplayMember, groups, sorts, filterCriteria, displayFilterCriteria, caseSensitiveFilter)) : ((CurrentDataView) new LocalCurrentPlainDataView(base.selectNullValue, this, handle, base.DataAccessor.ValueMember, base.DataAccessor.DisplayMember, false)));
        }

        void IItemsProviderCollectionViewSupport.RaiseCurrentChanged(object currentItem)
        {
            if (this.CurrentChanged != null)
            {
                this.CurrentChanged(this, new ItemsProviderCurrentChangedEventArgs(currentItem));
            }
        }

        void IItemsProviderCollectionViewSupport.SetCurrentItem(object currentItem)
        {
            this.collectionViewSupport.SetCurrentItem(currentItem);
        }

        void IItemsProviderCollectionViewSupport.SyncWithCurrentItem()
        {
            this.collectionViewSupport.SyncWithCurrent();
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            this.collectionViewSupport.Release();
            this.collectionViewHelper.FilterSortGroupInfoChanged -= new CollectionViewFilterSortGroupInfoChangedEventHandler(this.CollectionViewHelperFilterSortGroupInfoChanged);
        }

        public override IEnumerator<DataProxy> GetEnumerator() => 
            this.View.GetEnumerator();

        protected override void InitializeSource()
        {
            this.collectionViewHelper.AllowSyncSortingAndGrouping = false;
            this.collectionViewSupport.Initialize();
            if (this.useCollectionView)
            {
                this.collectionViewHelper.Initialize();
            }
            base.InitializeSource();
            this.collectionViewHelper.FilterSortGroupInfoChanged += new CollectionViewFilterSortGroupInfoChangedEventHandler(this.CollectionViewHelperFilterSortGroupInfoChanged);
        }

        protected override void InitializeView(object source)
        {
            this.InitializeSource();
            base.SetView(this.CreateDataProxyViewCache((IList) source));
        }

        public override bool ProcessInconsistencyDetected() => 
            false;

        private IEnumerable<DataProxy> View =>
            base.View;

        ICollectionViewHelper IItemsProviderCollectionViewSupport.DataSync =>
            this.collectionViewHelper;

        ICollectionView IItemsProviderCollectionViewSupport.ListSource =>
            this.collectionViewHelper.Collection;

        bool IItemsProviderCollectionViewSupport.IsSynchronizedWithCurrentItem =>
            this.syncWithCurrent();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CollectionViewDefaultDataView.<>c <>9 = new CollectionViewDefaultDataView.<>c();
            public static Func<IList<SortingInfo>, bool> <>9__26_0;
            public static Func<IList<GroupingInfo>, bool> <>9__26_1;

            internal bool <CreatePlainDataView>b__26_0(IList<SortingInfo> x) => 
                x.Count > 0;

            internal bool <CreatePlainDataView>b__26_1(IList<GroupingInfo> x) => 
                x.Count > 0;
        }
    }
}

