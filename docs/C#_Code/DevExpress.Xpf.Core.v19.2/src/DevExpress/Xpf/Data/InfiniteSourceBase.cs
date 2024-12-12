namespace DevExpress.Xpf.Data
{
    using DevExpress.Xpf.Data.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public abstract class InfiniteSourceBase : VirtualSourceBase
    {
        protected readonly List<object> innerList = new List<object>();
        private bool initialized;

        internal InfiniteSourceBase()
        {
        }

        protected override Exception ApplyException(Exception exception, int skip)
        {
            bool allowRetry = false;
            if (exception is AllowRetryWrapperException)
            {
                allowRetry = ((AllowRetryWrapperException) exception).AllowRetry;
                exception = ((AllowRetryWrapperException) exception).Exception;
            }
            this.RowsFetchState = allowRetry ? DevExpress.Xpf.Data.RowsFetchState.ErrorRetry : DevExpress.Xpf.Data.RowsFetchState.ErrorFinished;
            return exception;
        }

        protected override void ApplyRows(int skip, object[] rows, ref bool hasMoreRows)
        {
            if (this.IsResetting)
            {
                this.innerList.Clear();
                base.NotifyCollectionReset();
                this.IsResetting = false;
            }
            foreach (object obj2 in rows)
            {
                this.innerList.Add(obj2);
                base.NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, obj2, this.innerList.Count - 1));
            }
            base.RaisePropertyChanged<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(InfiniteSourceBase)), (MethodInfo) methodof(VirtualSourceBase.get_Count)), new ParameterExpression[0]));
        }

        protected override void ApplySuccessState(bool? hasMoreRows)
        {
            bool? nullable = hasMoreRows;
            this.RowsFetchState = ((nullable != null) ? ((DevExpress.Xpf.Data.RowsFetchState) nullable.GetValueOrDefault()) : ((DevExpress.Xpf.Data.RowsFetchState) true)) ? DevExpress.Xpf.Data.RowsFetchState.InProgress : DevExpress.Xpf.Data.RowsFetchState.Finished;
        }

        private AsyncTask CreateFetchRowsTask(int skip)
        {
            int? take = null;
            return base.CreateFetchRowsTask(skip, take);
        }

        public void FetchMoreRows()
        {
            base.VerifyAccess();
            this.StartLoadMoreRows(true);
        }

        protected override string GetFetchRowsToken(int skip) => 
            VirtualSourceBase.FetchRowsToken;

        protected override object GetItem(int index)
        {
            if ((index == (this.innerList.Count - 1)) && (this.FetchMode == DevExpress.Xpf.Data.FetchMode.Auto))
            {
                this.StartLoadMoreRows(false);
            }
            return base.GetItem(index);
        }

        protected override bool HasMoreRows(int fetchedRowsCount) => 
            fetchedRowsCount > 0;

        protected override void OnCountRequested()
        {
            this.initialized = true;
            if ((this.innerList.Count == 0) && (this.FetchMode == DevExpress.Xpf.Data.FetchMode.Auto))
            {
                this.StartLoadMoreRows(false);
            }
            base.OnCountRequested();
        }

        protected override void ResetRowsCore(bool manualReset)
        {
            if (this.initialized)
            {
                base.Worker.ReplaceOrAddTask(this.CreateFetchRowsTask(0));
                this.IsResetting = true;
            }
        }

        private void StartLoadMoreRows(bool force = false)
        {
            if (!base.AreRowsFetching)
            {
                base.RequestSourceIfNeeded();
                if (force || (this.RowsFetchState == DevExpress.Xpf.Data.RowsFetchState.InProgress))
                {
                    base.Worker.ReplaceOrAddTask(this.CreateFetchRowsTask(this.innerList.Count));
                }
            }
        }

        public DevExpress.Xpf.Data.RowsFetchState RowsFetchState
        {
            get => 
                base.GetProperty<DevExpress.Xpf.Data.RowsFetchState>(Expression.Lambda<Func<DevExpress.Xpf.Data.RowsFetchState>>(Expression.Property(Expression.Constant(this, typeof(InfiniteSourceBase)), (MethodInfo) methodof(InfiniteSourceBase.get_RowsFetchState)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<DevExpress.Xpf.Data.RowsFetchState>(Expression.Lambda<Func<DevExpress.Xpf.Data.RowsFetchState>>(Expression.Property(Expression.Constant(this, typeof(InfiniteSourceBase)), (MethodInfo) methodof(InfiniteSourceBase.get_RowsFetchState)), new ParameterExpression[0]), value);
        }

        public DevExpress.Xpf.Data.FetchMode FetchMode
        {
            get => 
                base.GetProperty<DevExpress.Xpf.Data.FetchMode>(Expression.Lambda<Func<DevExpress.Xpf.Data.FetchMode>>(Expression.Property(Expression.Constant(this, typeof(InfiniteSourceBase)), (MethodInfo) methodof(InfiniteSourceBase.get_FetchMode)), new ParameterExpression[0]));
            set => 
                base.SetProperty<DevExpress.Xpf.Data.FetchMode>(Expression.Lambda<Func<DevExpress.Xpf.Data.FetchMode>>(Expression.Property(Expression.Constant(this, typeof(InfiniteSourceBase)), (MethodInfo) methodof(InfiniteSourceBase.get_FetchMode)), new ParameterExpression[0]), value);
        }

        public bool IsResetting
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(InfiniteSourceBase)), (MethodInfo) methodof(InfiniteSourceBase.get_IsResetting)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(InfiniteSourceBase)), (MethodInfo) methodof(InfiniteSourceBase.get_IsResetting)), new ParameterExpression[0]), value);
        }

        protected override IList<object> List =>
            this.innerList;

        protected class AllowRetryWrapperException : System.Exception
        {
            public readonly System.Exception Exception;
            public readonly bool AllowRetry;

            private AllowRetryWrapperException(System.Exception exception, bool allowRetry)
            {
                this.Exception = exception;
                this.AllowRetry = allowRetry;
            }

            public static InfiniteSourceBase.AllowRetryWrapperException Wrap(System.Exception e, FetchRowsEventArgsBase args) => 
                new InfiniteSourceBase.AllowRetryWrapperException(e, args.AllowRetry);
        }
    }
}

