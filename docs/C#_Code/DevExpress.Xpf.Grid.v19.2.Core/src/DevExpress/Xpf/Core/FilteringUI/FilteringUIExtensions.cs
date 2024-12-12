namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class FilteringUIExtensions
    {
        public static readonly Task CompletedTask = Task.FromResult<bool>(false);

        [AsyncStateMachine(typeof(<AddCancellation>d__6))]
        public static Task<T> AddCancellation<T>(this Task<T> task, CancellationToken token)
        {
            <AddCancellation>d__6<T> d__;
            d__.task = task;
            d__.token = token;
            d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<AddCancellation>d__6<T>>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<AwaitCore>d__7))]
        private static void AwaitCore(this Task task)
        {
            <AwaitCore>d__7 d__;
            d__.task = task;
            d__.<>t__builder = AsyncVoidMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<AwaitCore>d__7>(ref d__);
        }

        public static void AwaitIfNotCompleted(this Task task)
        {
            if (!task.IsCompleted)
            {
                task.AwaitCore();
            }
            else if (task.Status == TaskStatus.Faulted)
            {
                throw task.Exception;
            }
        }

        [AsyncStateMachine(typeof(<DoLockedTaskAsync>d__3))]
        public static Task DoLockedTaskAsync(this Locker locker, Func<Task> getTask)
        {
            <DoLockedTaskAsync>d__3 d__;
            d__.locker = locker;
            d__.getTask = getTask;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<DoLockedTaskAsync>d__3>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<DoLockedTaskIfNotLockedAsync>d__4))]
        public static Task DoLockedTaskIfNotLockedAsync(this Locker locker, Func<Task> getTask)
        {
            <DoLockedTaskIfNotLockedAsync>d__4 d__;
            d__.locker = locker;
            d__.getTask = getTask;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<DoLockedTaskIfNotLockedAsync>d__4>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static bool ListReallyChanged(List<object> oldList, List<object> newList) => 
            ((oldList != null) || (newList != null)) ? (((oldList != null) || (newList == null)) ? (((oldList == null) || (newList != null)) ? !ListHelper.AreEqual<object>(oldList, newList) : (oldList.Count != 0)) : (newList.Count != 0)) : false;

        public static Lazy<TOut> Select<TIn, TOut>(this Lazy<TIn> lazy, Func<TIn, TOut> select) => 
            new Lazy<TOut>(() => select(lazy.Value));

        [CompilerGenerated]
        private struct <AddCancellation>d__6<T> : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<T> <>t__builder;
            public CancellationToken token;
            public Task<T> task;
            private CancellationTokenRegistration <>7__wrap1;
            private object <>7__wrap2;
            private TaskAwaiter<Task> <>u__1;
            private TaskAwaiter<T> <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<T> awaiter2;
                    T local3;
                    TaskCompletionSource<object> completionSource;
                    if (num != 0)
                    {
                        if (num == 1)
                        {
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new TaskAwaiter<T>();
                            this.<>1__state = num = -1;
                            goto TR_0009;
                        }
                        else
                        {
                            FilteringUIExtensions.<>c__DisplayClass6_0<T> class_;
                            completionSource = new TaskCompletionSource<object>();
                            this.<>7__wrap1 = this.token.Register(new Action(class_.<AddCancellation>b__0));
                        }
                    }
                    try
                    {
                        TaskAwaiter<Task> awaiter;
                        Task task2;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new TaskAwaiter<Task>();
                            this.<>1__state = num = -1;
                            goto TR_000C;
                        }
                        else
                        {
                            this.<>7__wrap2 = this.task;
                            Task[] tasks = new Task[] { this.task, completionSource.Task };
                            awaiter = Task.WhenAny(tasks).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_000C;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<Task>, FilteringUIExtensions.<AddCancellation>d__6<T>>(ref awaiter, ref (FilteringUIExtensions.<AddCancellation>d__6<T>) ref this);
                            }
                        }
                        return;
                    TR_000C:
                        task2 = awaiter.GetResult();
                        awaiter = new TaskAwaiter<Task>();
                        Task task = task2;
                        if (this.<>7__wrap2 != task)
                        {
                            this.<>7__wrap2 = null;
                            throw new OperationCanceledException(this.token);
                        }
                        goto TR_000A;
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            this.<>7__wrap1.Dispose();
                        }
                    }
                    return;
                TR_0009:
                    local3 = awaiter2.GetResult();
                    awaiter2 = new TaskAwaiter<T>();
                    T result = local3;
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(result);
                    return;
                TR_000A:
                    this.<>7__wrap1 = new CancellationTokenRegistration();
                    awaiter2 = this.task.GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        goto TR_0009;
                    }
                    else
                    {
                        this.<>1__state = num = 1;
                        this.<>u__2 = awaiter2;
                        this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<T>, FilteringUIExtensions.<AddCancellation>d__6<T>>(ref awaiter2, ref (FilteringUIExtensions.<AddCancellation>d__6<T>) ref this);
                    }
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

        [CompilerGenerated]
        private struct <AwaitCore>d__7 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncVoidMethodBuilder <>t__builder;
            public Task task;
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
                        awaiter = this.task.GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, FilteringUIExtensions.<AwaitCore>d__7>(ref awaiter, ref this);
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

        [CompilerGenerated]
        private struct <DoLockedTaskAsync>d__3 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public Locker locker;
            public Func<Task> getTask;
            private Locker <>7__wrap1;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<>7__wrap1 = this.locker.Lock();
                    }
                    try
                    {
                        TaskAwaiter awaiter;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new TaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0008;
                        }
                        else
                        {
                            awaiter = this.getTask().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0008;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, FilteringUIExtensions.<DoLockedTaskAsync>d__3>(ref awaiter, ref this);
                            }
                        }
                        return;
                    TR_0008:
                        awaiter.GetResult();
                        awaiter = new TaskAwaiter();
                        goto TR_0007;
                    }
                    finally
                    {
                        if ((num < 0) && (this.<>7__wrap1 != null))
                        {
                            ((IDisposable) this.<>7__wrap1).Dispose();
                        }
                    }
                    return;
                TR_0007:
                    this.<>7__wrap1 = null;
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

        [CompilerGenerated]
        private struct <DoLockedTaskIfNotLockedAsync>d__4 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public Locker locker;
            public Func<Task> getTask;
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
                    }
                    else
                    {
                        if (this.locker.IsLocked)
                        {
                            goto TR_0003;
                        }
                        else
                        {
                            awaiter = this.locker.DoLockedTaskAsync(this.getTask).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0004;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, FilteringUIExtensions.<DoLockedTaskIfNotLockedAsync>d__4>(ref awaiter, ref this);
                            }
                        }
                        return;
                    }
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            TR_0003:
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

