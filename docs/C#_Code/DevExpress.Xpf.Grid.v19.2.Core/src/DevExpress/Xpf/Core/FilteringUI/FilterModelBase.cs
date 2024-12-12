namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public abstract class FilterModelBase : BindableBase
    {
        private bool showAllLookUpFilterValues = true;
        internal readonly FilteringColumn Column;
        internal readonly FilterModelClient client;
        private readonly Locker updateLocker = new Locker();
        private Lazy<CriteriaOperator> filterLazy;

        internal FilterModelBase(FilterModelClient client)
        {
            Func<CriteriaOperator> valueFactory = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                Func<CriteriaOperator> local1 = <>c.<>9__27_0;
                valueFactory = <>c.<>9__27_0 = (Func<CriteriaOperator>) (() => null);
            }
            this.filterLazy = new Lazy<CriteriaOperator>(valueFactory);
            this.client = client;
            this.Column = client.GetColumn();
            this.CanBuildFilter = true;
        }

        protected internal void ApplyFilter()
        {
            this.ApplyFilter(new Lazy<CriteriaOperator>(new Func<CriteriaOperator>(this.BuildFilter)));
        }

        protected void ApplyFilter(Lazy<CriteriaOperator> filter)
        {
            this.updateLocker.DoLockedActionIfNotLocked(delegate {
                this.filterLazy = filter;
                this.client.ApplyFilter(filter);
            });
        }

        internal abstract CriteriaOperator BuildFilter();
        internal abstract bool CanBuildFilterCore();
        protected void ForceUpdate()
        {
            this.Update();
        }

        internal void Update()
        {
            this.UpdateContentAsync(delegate {
                <<Update>b__36_0>d local;
                local.<>4__this = this;
                local.<>t__builder = AsyncTaskMethodBuilder.Create();
                local.<>1__state = -1;
                local.<>t__builder.Start<<<Update>b__36_0>d>(ref local);
                return local.<>t__builder.Task;
            }).AwaitIfNotCompleted();
        }

        internal void Update(CriteriaOperator filter)
        {
            this.filterLazy = new Lazy<CriteriaOperator>(() => filter);
            this.Update();
        }

        protected virtual void UpdateAllowLiveDataShaping()
        {
        }

        protected void UpdateCanBuildFilter()
        {
            this.CanBuildFilter = this.CanBuildFilterCore();
        }

        [AsyncStateMachine(typeof(<UpdateContentAsync>d__37))]
        protected Task UpdateContentAsync(Func<Task> updateAsync)
        {
            <UpdateContentAsync>d__37 d__;
            d__.<>4__this = this;
            d__.updateAsync = updateAsync;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<UpdateContentAsync>d__37>(ref d__);
            return d__.<>t__builder.Task;
        }

        internal abstract Task UpdateCoreAsync();
        internal virtual void UpdateEditSettings()
        {
        }

        internal virtual void UpdateFormatConditionFilters()
        {
        }

        protected virtual void UpdateShowAllLookUpFilterValues()
        {
            this.Update();
        }

        protected virtual void UpdateShowCounts()
        {
            this.Update();
        }

        protected internal string PropertyName =>
            this.client.PropertyName;

        protected bool ActualShowCounts
        {
            get
            {
                bool? showCounts = this.ShowCounts;
                return ((showCounts != null) ? showCounts.GetValueOrDefault() : true);
            }
        }

        internal CountsIncludeMode ActualCountsInculedMode =>
            this.ActualShowCounts ? CountsIncludeMode.Include : CountsIncludeMode.Exclude;

        public string FieldDisplayName =>
            this.PropertyName;

        public bool CanBuildFilter
        {
            get => 
                base.GetValue<bool>("CanBuildFilter");
            private set => 
                base.SetValue<bool>(value, "CanBuildFilter");
        }

        public DevExpress.Xpf.Core.FilteringUI.FilterValuesSortMode FilterValuesSortMode
        {
            get => 
                base.GetValue<DevExpress.Xpf.Core.FilteringUI.FilterValuesSortMode>("FilterValuesSortMode");
            set => 
                base.SetValue<DevExpress.Xpf.Core.FilteringUI.FilterValuesSortMode>(value, () => this.Update(), "FilterValuesSortMode");
        }

        internal bool ShowAllLookUpFilterValues
        {
            get => 
                this.showAllLookUpFilterValues;
            set => 
                base.SetValue<bool>(ref this.showAllLookUpFilterValues, value, new Action(this.UpdateShowAllLookUpFilterValues), "ShowAllLookUpFilterValues");
        }

        internal bool? ShowCounts
        {
            get => 
                base.GetValue<bool?>("ShowCounts");
            set => 
                base.SetValue<bool?>(value, new Action(this.UpdateShowCounts), "ShowCounts");
        }

        internal bool AllowLiveDataShaping
        {
            get => 
                base.GetValue<bool>("AllowLiveDataShaping");
            set => 
                base.SetValue<bool>(value, new Action(this.UpdateAllowLiveDataShaping), "AllowLiveDataShaping");
        }

        protected CriteriaOperator Filter =>
            this.filterLazy.Value;

        protected bool HasFilter =>
            this.Filter != null;

        [CompilerGenerated]
        private struct <<Update>b__36_0>d : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public FilterModelBase <>4__this;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = this.<>4__this.UpdateCoreAsync().GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, FilterModelBase.<<Update>b__36_0>d>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    this.<>4__this.UpdateCanBuildFilter();
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult();
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterModelBase.<>c <>9 = new FilterModelBase.<>c();
            public static Func<CriteriaOperator> <>9__27_0;

            internal CriteriaOperator <.ctor>b__27_0() => 
                null;
        }

        [CompilerGenerated]
        private struct <UpdateContentAsync>d__37 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public FilterModelBase <>4__this;
            public Func<Task> updateAsync;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = this.<>4__this.updateLocker.DoLockedTaskIfNotLockedAsync(this.updateAsync).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, FilterModelBase.<UpdateContentAsync>d__37>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult();
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

