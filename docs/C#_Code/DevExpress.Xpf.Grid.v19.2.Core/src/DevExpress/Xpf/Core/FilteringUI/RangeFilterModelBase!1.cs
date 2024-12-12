namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public abstract class RangeFilterModelBase<T> : FilterModel where T: struct, IComparable<T>
    {
        private static Comparer<T> Comparer;
        protected Locker locker;

        static RangeFilterModelBase()
        {
            RangeFilterModelBase<T>.Comparer = Comparer<T>.Default;
        }

        internal RangeFilterModelBase(FilterModelClient client) : base(client)
        {
            this.locker = new Locker();
        }

        [CompilerGenerated, DebuggerHidden]
        private Task <>n__0() => 
            base.UpdateCoreAsync();

        internal sealed override CriteriaOperator BuildFilter()
        {
            CriteriaOperator @operator = null;
            if ((RangeFilterModelBase<T>.Comparer.Compare(this.Min, this.From) < 0) || (RangeFilterModelBase<T>.Comparer.Compare(this.To, this.Max) < 0))
            {
                @operator = this.BuildFilterCore();
            }
            return @operator;
        }

        protected abstract CriteriaOperator BuildFilterCore();
        private static T GetMax(T a, T b) => 
            (RangeFilterModelBase<T>.Comparer.Compare(a, b) > 0) ? a : b;

        private static T GetMin(T a, T b) => 
            (RangeFilterModelBase<T>.Comparer.Compare(a, b) < 0) ? a : b;

        protected abstract Tuple<T, T> ParseFilter();
        [AsyncStateMachine(typeof(<UpdateCoreAsync>d__18))]
        internal override Task UpdateCoreAsync()
        {
            <UpdateCoreAsync>d__18<T> d__;
            d__.<>4__this = (RangeFilterModelBase<T>) this;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<UpdateCoreAsync>d__18<T>>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected abstract Tuple<T, T> ValidateRange(Tuple<object, object> range);

        public T From
        {
            get => 
                base.GetValue<T>("From");
            set
            {
                this.locker.DoIfNotLocked(delegate {
                    value = RangeFilterModelBase<T>.GetMax(value, ((RangeFilterModelBase<T>) this).Min);
                    value = RangeFilterModelBase<T>.GetMin(value, ((RangeFilterModelBase<T>) this).To);
                });
                base.SetValue<T>(value, new Action(this.ApplyFilter), "From");
            }
        }

        public T To
        {
            get => 
                base.GetValue<T>("To");
            set
            {
                this.locker.DoIfNotLocked(delegate {
                    value = RangeFilterModelBase<T>.GetMin(value, ((RangeFilterModelBase<T>) this).Max);
                    value = RangeFilterModelBase<T>.GetMax(value, ((RangeFilterModelBase<T>) this).From);
                });
                base.SetValue<T>(value, new Action(this.ApplyFilter), "To");
            }
        }

        public T Min
        {
            get => 
                base.GetValue<T>("Min");
            private set => 
                base.SetValue<T>(value, "Min");
        }

        public T Max
        {
            get => 
                base.GetValue<T>("Max");
            private set => 
                base.SetValue<T>(value, "Max");
        }

        [CompilerGenerated]
        private struct <UpdateCoreAsync>d__18 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public RangeFilterModelBase<T> <>4__this;
            private RangeFilterModelBase<T>.<>c__DisplayClass18_0 <>8__1;
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
                        this.<>8__1 = new RangeFilterModelBase<T>.<>c__DisplayClass18_0();
                        this.<>8__1.<>4__this = this.<>4__this;
                        awaiter = this.<>4__this.<>n__0().GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RangeFilterModelBase<T>.<UpdateCoreAsync>d__18>(ref awaiter, ref (RangeFilterModelBase<T>.<UpdateCoreAsync>d__18) ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    this.<>8__1.range = this.<>4__this.ParseFilter();
                    this.<>4__this.locker.DoLockedAction(new Action(this.<>8__1.<UpdateCoreAsync>b__0));
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

