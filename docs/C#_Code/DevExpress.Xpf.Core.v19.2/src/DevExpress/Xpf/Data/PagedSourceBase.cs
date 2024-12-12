namespace DevExpress.Xpf.Data
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Data.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class PagedSourceBase : VirtualSourceBase
    {
        protected readonly PageCache pageCache = new PageCache();
        private IList<object> tempList;
        private bool? hasMoreRows;
        private bool isResetting;

        internal PagedSourceBase()
        {
            this.PageSize = 20;
        }

        protected override Exception ApplyException(Exception exception, int skip)
        {
            this.pageCache.SetPageException(this.SkipToPageIndex(skip));
            return exception;
        }

        protected override void ApplyRows(int skip, object[] rows, ref bool hasMoreRows)
        {
            bool hasMoreRowsLocal = hasMoreRows;
            this.PerformPageCountAffectingAction(delegate {
                if (this.isResetting)
                {
                    this.pageCache.Clear();
                }
                int startPageIndex = this.SkipToPageIndex(skip);
                this.pageCache.ApplyRows(startPageIndex, this.PageSize, rows, this.HasMorePages, ref hasMoreRowsLocal);
                if (this.isResetting)
                {
                    this.PageIndex = startPageIndex;
                    this.isResetting = false;
                }
                if (startPageIndex == this.PageIndex)
                {
                    this.tempList = null;
                    this.NotifyCollectionReset();
                }
            });
            hasMoreRows = hasMoreRowsLocal;
        }

        protected override void ApplySuccessState(bool? hasMoreRows)
        {
            this.hasMoreRows = hasMoreRows;
            this.ApplySuccessStateCore();
        }

        private void ApplySuccessStateCore()
        {
            bool? hasMoreRows = this.hasMoreRows;
            this.HasMorePages = ((hasMoreRows != null) ? hasMoreRows.GetValueOrDefault() : !this.IsConsecutiveMode) && !this.IsTotalPageCountMode;
        }

        private AsyncTask CreateFetchRowsTask(int page) => 
            base.CreateFetchRowsTask(page * this.PageSize, new int?(this.PageSize));

        protected override void FinishFetchRows()
        {
            base.FinishFetchRows();
            this.UpdateIsCurrentPageLoading();
        }

        protected override string GetFetchRowsToken(int skip) => 
            VirtualSourceBase.FetchRowsToken + "_" + skip;

        protected override SummaryDefinition[] GetNeededSummaries() => 
            base.GetNeededSummaries().Concat<SummaryDefinition>(SummaryDefinition.Count.Yield<SummaryDefinition>()).ToArray<SummaryDefinition>();

        protected override Exception GetSkipTokenNotAllowedException() => 
            !this.IsConsecutiveMode ? new InvalidOperationException("Skip Tokens are allowed in the Consecutive page navigation mode only.") : null;

        protected virtual void HandlePendingPages()
        {
        }

        protected override bool HasMoreRows(int fetchedRowsCount) => 
            fetchedRowsCount >= this.PageSize;

        private void PerformPageCountAffectingAction(Action action)
        {
            long pageCount = this.PageCount;
            action();
            if (pageCount != this.PageCount)
            {
                this.RaisePageAndTotalItemCountChanged();
            }
        }

        private void RaisePageAndTotalItemCountChanged()
        {
            base.RaisePropertyChanged<long>(Expression.Lambda<Func<long>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_PageCount)), new ParameterExpression[0]));
            base.RaisePropertyChanged<long?>(Expression.Lambda<Func<long?>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_TotalItemCount)), new ParameterExpression[0]));
        }

        protected override void RaiseSummariesCalculated()
        {
            base.RaiseSummariesCalculated();
            if (this.IsTotalPageCountMode)
            {
                this.RaisePageAndTotalItemCountChanged();
            }
        }

        protected override void ResetRowsCore(bool manualReset = false)
        {
            if (this.pageCache.MaxPageIndex >= 0)
            {
                this.ThrottlePendingPages();
                this.isResetting = true;
                base.Worker.ReplaceOrAddTask(this.CreateFetchRowsTask((!manualReset || this.IsConsecutiveMode) ? 0 : this.PageIndex));
                this.IsCurrentPageLoading = true;
            }
        }

        protected int SkipToPageIndex(int skip) => 
            skip / this.PageSize;

        private void StartLoadCurrentPage()
        {
            if (!this.pageCache.ContainsPage(this.PageIndex))
            {
                this.PerformPageCountAffectingAction(delegate {
                    base.RequestSourceIfNeeded();
                    base.Worker.ReplaceOrAddTask(this.CreateFetchRowsTask(this.PageIndex));
                    this.pageCache.SetPagePending(this.PageIndex);
                });
                this.IsCurrentPageLoading = true;
                if (this.IsConsecutiveMode)
                {
                    this.HasMorePages = false;
                }
            }
        }

        protected VirtualSourceBase.GetRowsState[] ThrottlePendingPages()
        {
            Predicate<string> condition = <>c.<>9__46_0;
            if (<>c.<>9__46_0 == null)
            {
                Predicate<string> local1 = <>c.<>9__46_0;
                condition = <>c.<>9__46_0 = x => x.StartsWith(VirtualSourceBase.FetchRowsToken);
            }
            return base.Worker.ThrottleTasks<string>(condition).Cast<VirtualSourceBase.GetRowsState>().ToArray<VirtualSourceBase.GetRowsState>();
        }

        private void UpdateIsCurrentPageLoading()
        {
            this.IsCurrentPageLoading = this.pageCache.IsPageLoading(this.PageIndex);
        }

        public int PageIndex
        {
            get => 
                base.GetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_PageIndex)), new ParameterExpression[0]));
            set
            {
                if (value < 0)
                {
                    throw new IndexOutOfRangeException("PageIndex cannot be negative.");
                }
                if (this.IsConsecutiveMode && (value > this.PageCount))
                {
                    throw new IndexOutOfRangeException("PageIndex cannot be greater than the last loaded page's index in the Consecutive page navigation mode.");
                }
                if (this.IsTotalPageCountMode && (base.summaryCache.IsCountCalculated && ((value >= this.PageCount) && ((value != 0) || (this.PageCount != 0)))))
                {
                    throw new IndexOutOfRangeException("PageIndex cannot be greater than the total page count.");
                }
                int pageIndex = this.PageIndex;
                if (base.SetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_PageIndex)), new ParameterExpression[0]), value))
                {
                    base.FetchRowsException = null;
                    this.UpdateIsCurrentPageLoading();
                    this.pageCache.RemovePageWithException(this.PageIndex);
                    bool flag = this.pageCache.ContainsPage(this.PageIndex);
                    this.HandlePendingPages();
                    this.StartLoadCurrentPage();
                    if (flag)
                    {
                        this.tempList = null;
                    }
                    else if (this.pageCache.ContainsPage(pageIndex) && !this.pageCache.IsPageLoading(pageIndex))
                    {
                        this.tempList = this.pageCache.GetPageList(pageIndex);
                    }
                    if (flag)
                    {
                        base.NotifyCollectionReset();
                    }
                }
            }
        }

        public bool IsCurrentPageLoading
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_IsCurrentPageLoading)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_IsCurrentPageLoading)), new ParameterExpression[0]), value);
        }

        public bool HasMorePages
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_HasMorePages)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_HasMorePages)), new ParameterExpression[0]), value);
        }

        public int PageSize
        {
            get => 
                base.GetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_PageSize)), new ParameterExpression[0]));
            set
            {
                if (base.SetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_PageSize)), new ParameterExpression[0]), value))
                {
                    this.ResetRowsCore(false);
                }
            }
        }

        public DevExpress.Xpf.Data.PageNavigationMode PageNavigationMode
        {
            get => 
                base.GetProperty<DevExpress.Xpf.Data.PageNavigationMode>(Expression.Lambda<Func<DevExpress.Xpf.Data.PageNavigationMode>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_PageNavigationMode)), new ParameterExpression[0]));
            set
            {
                if (base.SetProperty<DevExpress.Xpf.Data.PageNavigationMode>(Expression.Lambda<Func<DevExpress.Xpf.Data.PageNavigationMode>>(Expression.Property(Expression.Constant(this, typeof(PagedSourceBase)), (MethodInfo) methodof(PagedSourceBase.get_PageNavigationMode)), new ParameterExpression[0]), value))
                {
                    this.RaisePageAndTotalItemCountChanged();
                    this.ApplySuccessStateCore();
                }
            }
        }

        private bool IsTotalPageCountMode =>
            this.PageNavigationMode == DevExpress.Xpf.Data.PageNavigationMode.ArbitraryWithTotalPageCount;

        private bool IsConsecutiveMode =>
            this.PageNavigationMode == DevExpress.Xpf.Data.PageNavigationMode.Consecutive;

        public long PageCount
        {
            get
            {
                base.VerifyAccess();
                long? totalItemCount = this.TotalItemCount;
                return ((totalItemCount == null) ? ((long) (this.pageCache.MaxPageIndex + 1)) : ((totalItemCount.Value / ((long) this.PageSize)) + (((totalItemCount.Value % ((long) this.PageSize)) == 0) ? ((long) 0) : ((long) 1))));
            }
        }

        public long? TotalItemCount
        {
            get
            {
                base.VerifyAccess();
                if (this.IsTotalPageCountMode)
                {
                    object count = base.summaryCache.GetCount();
                    if (!Equals(count, base.SummaryInProgressText))
                    {
                        return new long?(Convert.ToInt64(count));
                    }
                }
                return null;
            }
        }

        protected override IList<object> List
        {
            get
            {
                if (this.tempList != null)
                {
                    return this.tempList;
                }
                this.StartLoadCurrentPage();
                return this.pageCache.GetPageList(this.PageIndex);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PagedSourceBase.<>c <>9 = new PagedSourceBase.<>c();
            public static Predicate<string> <>9__46_0;

            internal bool <ThrottlePendingPages>b__46_0(string x) => 
                x.StartsWith(VirtualSourceBase.FetchRowsToken);
        }
    }
}

