namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public static class TaskLinq
    {
        public static SynchronizationContext RethrowAsyncExceptionsContext;

        public static Task Execute(this TaskLinq<UnitT> task, Action action, SchedulerFuture schedulerFuture) => 
            task.Execute<UnitT>(delegate (UnitT _) {
                action();
            }, schedulerFuture);

        public static Task Execute(this TaskLinq<UnitT> task, Action action, TaskScheduler scheduler = null) => 
            task.Execute<UnitT>(delegate (UnitT _) {
                action();
            }, scheduler);

        public static Task Execute<T>(this TaskLinq<T> task, Action<T> action, SchedulerFuture schedulerFuture) => 
            task.Select<T, T>(delegate (T r) {
                action(r);
                return r;
            }).Schedule<T>(schedulerFuture).Finish<T>();

        public static Task Execute<T>(this TaskLinq<T> task, Action<T> action, TaskScheduler scheduler = null) => 
            task.Select<T, T>(delegate (T r) {
                action(r);
                return r;
            }).Schedule<T>(scheduler).Finish<T>();

        public static Task Finish<T>(this Task<T> task)
        {
            Func<Task<T>, UnitT> continuationFunction = <>c__38<T>.<>9__38_0;
            if (<>c__38<T>.<>9__38_0 == null)
            {
                Func<Task<T>, UnitT> local1 = <>c__38<T>.<>9__38_0;
                continuationFunction = <>c__38<T>.<>9__38_0 = delegate (Task<T> t) {
                    if (t.IsFaulted)
                    {
                        RethrowAsyncExceptionsContext.Do<SynchronizationContext>(delegate (SynchronizationContext x) {
                            SendOrPostCallback <>9__2;
                            SendOrPostCallback d = <>9__2;
                            if (<>9__2 == null)
                            {
                                SendOrPostCallback local1 = <>9__2;
                                d = <>9__2 = delegate (object _) {
                                    throw new AggregateException(t.Exception.InnerExceptions);
                                };
                            }
                            UnitT state = new UnitT();
                            x.Post(d, state);
                        });
                    }
                    return new UnitT();
                };
            }
            return task.ContinueWith<UnitT>(continuationFunction, TaskContinuationOptions.ExecuteSynchronously);
        }

        public static Task<T> Future<T>(this T value)
        {
            TaskCompletionSource<T> source = new TaskCompletionSource<T>();
            source.SetResult(value);
            return source.Task;
        }

        public static Task<T> FutureCanceled<T>()
        {
            TaskCompletionSource<T> source = new TaskCompletionSource<T>();
            source.SetCanceled();
            return source.Task;
        }

        public static Task<T> FutureException<T>(this Exception e)
        {
            TaskCompletionSource<T> source = new TaskCompletionSource<T>();
            source.SetException(e);
            return source.Task;
        }

        public static TaskLinq<T> IfCanceled<T>(this TaskLinq<T> task, Func<Task<T>> handler)
        {
            Chain chain = task.Chain;
            TaskCompletionSource<T> taskSource = new TaskCompletionSource<T>();
            taskSource.SetResultFromTask<T>(chain.SchedulerFuture, task.Task, delegate (TaskCompletionSource<T> r) {
                r.SetResultFromTaskSafe<T>(chain.SchedulerFuture, handler, null, null);
            }, null);
            return taskSource.Task.Linq<T>(chain);
        }

        public static TaskLinq<T> IfCanceled<T>(this TaskLinq<T> task, Func<T> handler)
        {
            Chain chain = task.Chain;
            TaskCompletionSource<T> taskSource = new TaskCompletionSource<T>();
            taskSource.SetResultFromTask<T>(chain.SchedulerFuture, task.Task, delegate (TaskCompletionSource<T> r) {
                r.SetResultSafe<T>(handler);
            }, null);
            return taskSource.Task.Linq<T>(chain);
        }

        public static TaskLinq<T> IfException<T>(this TaskLinq<T> task, Func<Exception, TaskLinq<T>> handler)
        {
            Chain chain = task.Chain;
            TaskCompletionSource<T> taskSource = new TaskCompletionSource<T>();
            taskSource.SetResultFromTask<T>(chain.SchedulerFuture, task.Task, null, delegate (TaskCompletionSource<T> r, Exception e) {
                r.SetResultFromTaskSafe<T>(chain.SchedulerFuture, () => handler(e).Run<T>(chain), null, null);
            });
            return taskSource.Task.Linq<T>(chain);
        }

        public static TaskLinq<T> IfException<T>(this TaskLinq<T> task, Func<Exception, T> handler)
        {
            Chain chain = task.Chain;
            TaskCompletionSource<T> taskSource = new TaskCompletionSource<T>();
            taskSource.SetResultFromTask<T>(chain.SchedulerFuture, task.Task, null, delegate (TaskCompletionSource<T> r, Exception e) {
                r.SetResultSafe<T>(() => handler(e));
            });
            return taskSource.Task.Linq<T>(chain);
        }

        private static TaskScheduler InvalidScheduler()
        {
            throw new InvalidOperationException("TaskScheduler.Current == TaskScheduler.Default && SynchronizationContext.Current == null");
        }

        public static TaskLinq<T> Linq<T>(this Task<T> task, Chain chain) => 
            new TaskLinq<T>(task, chain);

        public static TaskLinq<T> Linq<T>(this Task<T> task, TaskScheduler scheduler = null) => 
            task.Linq<T>(new Chain(scheduler, null));

        public static TaskLinq<UnitT> LongRunning() => 
            StartNew(TaskCreationOptions.LongRunning);

        public static TaskLinq<T> MapException<T>(this TaskLinq<T> task, Func<Exception, Exception> transform)
        {
            Chain chain = task.Chain;
            TaskCompletionSource<T> taskSource = new TaskCompletionSource<T>();
            taskSource.SetResultFromTask<T>(chain.SchedulerFuture, task.Task, null, delegate (TaskCompletionSource<T> r, Exception e) {
                r.SetExceptionSafe<T>(() => transform(e));
            });
            return taskSource.Task.Linq<T>(chain);
        }

        public static TaskLinq<UnitT> On(Func<Action, Action> subscribe, Chain chain) => 
            On<UnitT>(x => subscribe(delegate {
                UnitT tt = new UnitT();
                x(tt);
            }), chain);

        public static TaskLinq<UnitT> On(Func<Action, Action> subscribe, TaskScheduler scheduler = null) => 
            On(subscribe, new Chain(scheduler, null));

        public static TaskLinq<T> On<T>(Func<Action<T>, Action> subscribe, Chain chain)
        {
            TaskCompletionSource<T> taskSource = new TaskCompletionSource<T>();
            unsubscribe => subscribe(delegate (T x) {
                unsubscribe.Value();
                taskSource.SetResult(x);
            }).WithReturnValue<Action>();
            return taskSource.Task.Linq<T>(chain);
        }

        public static TaskLinq<T> On<T>(Func<Action<T>, Action> subscribe, TaskScheduler scheduler = null) => 
            On<T>(subscribe, new Chain(scheduler, null));

        public static TaskLinq<T> Promise<T>(this T value, Chain chain) => 
            value.Future<T>().Linq<T>(chain);

        public static TaskLinq<T> Promise<T>(this T value, TaskScheduler scheduler = null) => 
            value.Promise<T>(new Chain(scheduler, null));

        public static TaskLinq<T> PromiseCanceled<T>(Chain chain) => 
            FutureCanceled<T>().Linq<T>(chain);

        public static TaskLinq<T> PromiseCanceled<T>(TaskScheduler scheduler = null) => 
            PromiseCanceled<T>(new Chain(scheduler, null));

        public static TaskLinq<T> PromiseException<T>(this Exception e, Chain chain) => 
            e.FutureException<T>().Linq<T>(chain);

        public static TaskLinq<T> PromiseException<T>(this Exception e, TaskScheduler scheduler = null) => 
            e.PromiseException<T>(new Chain(scheduler, null));

        private static Task<T> Run<T>(this TaskLinq<T> task, Chain chain)
        {
            chain.Continue(task.Chain);
            return task.Task;
        }

        public static Task<T> Schedule<T>(this TaskLinq<T> task, SchedulerFuture schedulerFuture)
        {
            schedulerFuture.Continue(new Action<TaskScheduler>(task.Chain.Run));
            return task.Task;
        }

        public static Task<T> Schedule<T>(this TaskLinq<T> task, TaskScheduler scheduler = null)
        {
            scheduler ??= (!ReferenceEquals(TaskScheduler.Current, TaskScheduler.Default) ? TaskScheduler.Current : ((SynchronizationContext.Current == null) ? InvalidScheduler() : TaskScheduler.FromCurrentSynchronizationContext()));
            return task.Schedule<T>(new SchedulerFuture(scheduler));
        }

        public static TaskLinq<T> Select<T>(this TaskLinq<T> task, Action selector) => 
            task.Select<T, T>(delegate (T x) {
                selector();
                return x;
            });

        public static TaskLinq<UnitT> Select<TI>(this TaskLinq<TI> task, Action<TI> selector) => 
            task.Select<TI, UnitT>(delegate (TI x) {
                selector(x);
                return new UnitT();
            });

        public static TaskLinq<TR> Select<TR>(this TaskLinq<UnitT> task, Func<TR> selector) => 
            task.Select<UnitT, TR>(_ => selector());

        public static TaskLinq<TR> Select<TI, TR>(this TaskLinq<TI> task, Func<TI, TR> selector)
        {
            Chain chain = task.Chain;
            TaskCompletionSource<TR> taskSource = new TaskCompletionSource<TR>();
            taskSource.SetResultFromTask<TI, TR>(chain.SchedulerFuture, task.Task, delegate (TaskCompletionSource<TR> ts, TI taskResult) {
                ts.SetResultSafe<TR>(() => selector(taskResult));
            }, null, null);
            return taskSource.Task.Linq<TR>(chain);
        }

        public static TaskLinq<TR> SelectMany<TR>(this TaskLinq<UnitT> task, Func<TaskLinq<TR>> selector) => 
            task.SelectMany<UnitT, TR>(_ => selector());

        public static TaskLinq<TR> SelectMany<TI, TR>(this TaskLinq<TI> task, Func<TI, TaskLinq<TR>> selector)
        {
            Func<TI, TR, TR> projector = <>c__34<TI, TR>.<>9__34_0;
            if (<>c__34<TI, TR>.<>9__34_0 == null)
            {
                Func<TI, TR, TR> local1 = <>c__34<TI, TR>.<>9__34_0;
                projector = <>c__34<TI, TR>.<>9__34_0 = (_, x) => x;
            }
            return task.SelectMany<TI, TR, TR>(selector, projector);
        }

        public static TaskLinq<TR> SelectMany<TI, TC, TR>(this TaskLinq<TI> task, Func<TI, TaskLinq<TC>> selector, Func<TI, TC, TR> projector)
        {
            Chain chain = task.Chain;
            TaskCompletionSource<TR> taskSource = new TaskCompletionSource<TR>();
            taskSource.SetResultFromTask<TI, TR>(chain.SchedulerFuture, task.Task, delegate (TaskCompletionSource<TR> ts, TI taskResult) {
                ts.SetResultFromTaskSafe<TC, TR>(chain.SchedulerFuture, () => selector(taskResult).Run<TC>(chain), (ts_, selectorResult) => ts_.SetResultSafe<TR>(() => projector(taskResult, selectorResult)), null, null);
            }, null, null);
            return taskSource.Task.Linq<TR>(chain);
        }

        public static TaskLinq<T> SelectUnit<T>(this TaskLinq<T> task, Func<TaskLinq<UnitT>> selector) => 
            task.SelectMany<T, T>(x => selector().Select<T>(() => x));

        private static void SetExceptionSafe<T>(this TaskCompletionSource<T> taskSource, Func<Exception> getException)
        {
            Action<TaskCompletionSource<T>, Exception> setResultAction = <>c__52<T>.<>9__52_1;
            if (<>c__52<T>.<>9__52_1 == null)
            {
                Action<TaskCompletionSource<T>, Exception> local1 = <>c__52<T>.<>9__52_1;
                setResultAction = <>c__52<T>.<>9__52_1 = (ts, result) => ts.SetException(result);
            }
            taskSource.SetResultSafe<Exception, T>(delegate {
                Exception exception = getException();
                if (exception == null)
                {
                    throw new InvalidOperationException("getException() == null");
                }
                return exception;
            }, setResultAction);
        }

        private static void SetResultFromTask<T>(this TaskCompletionSource<T> taskSource, SchedulerFuture continueWithScheduler, Task<T> task, Action<TaskCompletionSource<T>> setCanceledAction = null, Action<TaskCompletionSource<T>, Exception> setExceptionAction = null)
        {
            Action<TaskCompletionSource<T>, T> setResultAction = <>c__43<T>.<>9__43_0;
            if (<>c__43<T>.<>9__43_0 == null)
            {
                Action<TaskCompletionSource<T>, T> local1 = <>c__43<T>.<>9__43_0;
                setResultAction = <>c__43<T>.<>9__43_0 = (ts, taskResult) => ts.SetResult(taskResult);
            }
            taskSource.SetResultFromTask<T, T>(continueWithScheduler, task, setResultAction, setCanceledAction, setExceptionAction);
        }

        private static void SetResultFromTask<TI, TR>(this TaskCompletionSource<TR> taskSource, SchedulerFuture continueWithScheduler, Task<TI> task, Action<TaskCompletionSource<TR>, TI> setResultAction, Action<TaskCompletionSource<TR>> setCanceledAction = null, Action<TaskCompletionSource<TR>, Exception> setExceptionAction = null)
        {
            Action<TaskCompletionSource<TR>> action1 = setCanceledAction;
            if (setCanceledAction == null)
            {
                Action<TaskCompletionSource<TR>> local1 = setCanceledAction;
                action1 = <>c__44<TI, TR>.<>9__44_0;
                if (<>c__44<TI, TR>.<>9__44_0 == null)
                {
                    Action<TaskCompletionSource<TR>> local2 = <>c__44<TI, TR>.<>9__44_0;
                    action1 = <>c__44<TI, TR>.<>9__44_0 = t => t.SetCanceled();
                }
            }
            setCanceledAction = action1;
            Action<TaskCompletionSource<TR>, Exception> action2 = setExceptionAction;
            if (setExceptionAction == null)
            {
                Action<TaskCompletionSource<TR>, Exception> local3 = setExceptionAction;
                action2 = <>c__44<TI, TR>.<>9__44_1;
                if (<>c__44<TI, TR>.<>9__44_1 == null)
                {
                    Action<TaskCompletionSource<TR>, Exception> local4 = <>c__44<TI, TR>.<>9__44_1;
                    action2 = <>c__44<TI, TR>.<>9__44_1 = (t, e) => t.SetException(e);
                }
            }
            setExceptionAction = action2;
            continueWithScheduler.Continue(delegate (TaskScheduler scheduler) {
                Action<Task<TI>> <>9__3;
                Action<Task<TI>> continuationAction = <>9__3;
                if (<>9__3 == null)
                {
                    Action<Task<TI>> local1 = <>9__3;
                    continuationAction = <>9__3 = delegate (Task<TI> t) {
                        if (t.IsCanceled)
                        {
                            setCanceledAction(taskSource);
                        }
                        else if (t.IsFaulted)
                        {
                            setExceptionAction(taskSource, t.Exception.InnerException);
                        }
                        else
                        {
                            setResultAction(taskSource, t.Result);
                        }
                    };
                }
                task.ContinueWith(continuationAction, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, scheduler);
            });
        }

        private static void SetResultFromTaskSafe<T>(this TaskCompletionSource<T> taskSource, SchedulerFuture continueWithScheduler, Func<Task<T>> getTask, Action<TaskCompletionSource<T>> setCanceledAction = null, Action<TaskCompletionSource<T>, Exception> setExceptionAction = null)
        {
            Action<TaskCompletionSource<T>, T> setResultAction = <>c__45<T>.<>9__45_0;
            if (<>c__45<T>.<>9__45_0 == null)
            {
                Action<TaskCompletionSource<T>, T> local1 = <>c__45<T>.<>9__45_0;
                setResultAction = <>c__45<T>.<>9__45_0 = (ts, taskResult) => ts.SetResult(taskResult);
            }
            taskSource.SetResultFromTaskSafe<T, T>(continueWithScheduler, getTask, setResultAction, setCanceledAction, setExceptionAction);
        }

        private static void SetResultFromTaskSafe<TI, TR>(this TaskCompletionSource<TR> taskSource, SchedulerFuture continueWithScheduler, Func<Task<TI>> getTask, Action<TaskCompletionSource<TR>, TI> setResultAction, Action<TaskCompletionSource<TR>> setCanceledAction = null, Action<TaskCompletionSource<TR>, Exception> setExceptionAction = null)
        {
            Task<TI> task;
            try
            {
                task = getTask();
            }
            catch (Exception exception)
            {
                taskSource.SetException(exception);
                return;
            }
            taskSource.SetResultFromTask<TI, TR>(continueWithScheduler, task, setResultAction, setCanceledAction, setExceptionAction);
        }

        private static void SetResultSafe<T>(this TaskCompletionSource<T> taskSource, Func<T> getResult)
        {
            Action<TaskCompletionSource<T>, T> setResultAction = <>c__53<T>.<>9__53_0;
            if (<>c__53<T>.<>9__53_0 == null)
            {
                Action<TaskCompletionSource<T>, T> local1 = <>c__53<T>.<>9__53_0;
                setResultAction = <>c__53<T>.<>9__53_0 = (ts, result) => ts.SetResult(result);
            }
            taskSource.SetResultSafe<T, T>(getResult, setResultAction);
        }

        private static void SetResultSafe<TI, TR>(this TaskCompletionSource<TR> taskSource, Func<TI> getResult, Action<TaskCompletionSource<TR>, TI> setResultAction)
        {
            TI local;
            Exception exception;
            try
            {
                local = getResult();
                exception = null;
            }
            catch (Exception exception2)
            {
                local = default(TI);
                exception = exception2;
            }
            if (exception != null)
            {
                taskSource.SetException(exception);
            }
            else
            {
                setResultAction(taskSource, local);
            }
        }

        private static TaskLinq<UnitT> StartNew(TaskCreationOptions creationOptions)
        {
            Func<UnitT> function = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<UnitT> local1 = <>c.<>9__8_0;
                function = <>c.<>9__8_0 = () => new UnitT();
            }
            Task<UnitT> task = new Task<UnitT>(function, CancellationToken.None, creationOptions);
            Chain chain = new Chain(TaskScheduler.Default, delegate {
                task.Start(TaskScheduler.Default);
            });
            return task.Linq<UnitT>(chain);
        }

        public static TaskLinq<UnitT> ThreadPool() => 
            StartNew(TaskCreationOptions.None);

        public static TaskLinq<UnitT> Wait(Func<Action, Action> subscribe, Func<bool> ready, Chain chain)
        {
            if (!ready())
            {
                return On(subscribe, chain);
            }
            UnitT tt = new UnitT();
            return tt.Promise<UnitT>(((TaskScheduler) null));
        }

        public static TaskLinq<UnitT> Wait(Func<Action, Action> subscribe, Func<bool> ready, TaskScheduler scheduler = null) => 
            Wait(subscribe, ready, new Chain(scheduler, null));

        public static TaskLinq<UnitT> Where(this TaskLinq<UnitT> task, Func<bool> predicate) => 
            task.Where<UnitT>(_ => predicate());

        public static TaskLinq<T> Where<T>(this TaskLinq<T> task, Func<T, bool> predicate)
        {
            Chain chain = task.Chain;
            TaskCompletionSource<T> taskSource = new TaskCompletionSource<T>();
            taskSource.SetResultFromTask<T, T>(chain.SchedulerFuture, task.Task, delegate (TaskCompletionSource<T> ts, T taskResult) {
                ts.SetResultSafe<bool, T>(() => predicate(taskResult), delegate (TaskCompletionSource<T> ts_, bool predicateResult) {
                    if (predicateResult)
                    {
                        ts_.SetResult(taskResult);
                    }
                    else
                    {
                        ts_.SetCanceled();
                    }
                });
            }, null, null);
            return taskSource.Task.Linq<T>(chain);
        }

        public static TaskLinq<T> WithDefaultScheduler<T>(Func<TaskLinq<T>> action)
        {
            if (ReferenceEquals(TaskScheduler.Current, TaskScheduler.Default))
            {
                return action();
            }
            SynchronizationContext synchronizationContext = SynchronizationContext.Current;
            if (synchronizationContext == null)
            {
                throw new InvalidOperationException("SynchronizationContext.Current == null");
            }
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            int threadId = Thread.CurrentThread.ManagedThreadId;
            TaskCompletionSource<TaskLinq<T>> continueTask = new TaskCompletionSource<TaskLinq<T>>();
            TaskCompletionSource<UnitT> source = new TaskCompletionSource<UnitT>();
            UnitT result = new UnitT();
            source.SetResult(result);
            source.Task.ContinueWith(delegate (Task<UnitT> _) {
                if (Thread.CurrentThread.ManagedThreadId == threadId)
                {
                    continueTask.SetResultSafe<TaskLinq<T>>(action);
                }
                else
                {
                    SendOrPostCallback <>9__1;
                    SendOrPostCallback d = <>9__1;
                    if (<>9__1 == null)
                    {
                        SendOrPostCallback local1 = <>9__1;
                        d = <>9__1 = __ => continueTask.SetResultSafe<TaskLinq<T>>(action);
                    }
                    UnitT state = new UnitT();
                    synchronizationContext.Post(d, state);
                }
            }, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
            Func<TaskLinq<T>, TaskLinq<T>> selector = <>c__55<T>.<>9__55_2;
            if (<>c__55<T>.<>9__55_2 == null)
            {
                Func<TaskLinq<T>, TaskLinq<T>> local1 = <>c__55<T>.<>9__55_2;
                selector = <>c__55<T>.<>9__55_2 = x => x;
            }
            return continueTask.Task.Linq<TaskLinq<T>>(scheduler).SelectMany<TaskLinq<T>, T>(selector);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TaskLinq.<>c <>9 = new TaskLinq.<>c();
            public static Func<UnitT> <>9__8_0;

            internal UnitT <StartNew>b__8_0() => 
                new UnitT();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__34<TI, TR>
        {
            public static readonly TaskLinq.<>c__34<TI, TR> <>9;
            public static Func<TI, TR, TR> <>9__34_0;

            static <>c__34()
            {
                TaskLinq.<>c__34<TI, TR>.<>9 = new TaskLinq.<>c__34<TI, TR>();
            }

            internal TR <SelectMany>b__34_0(TI _, TR x) => 
                x;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__38<T>
        {
            public static readonly TaskLinq.<>c__38<T> <>9;
            public static Func<Task<T>, UnitT> <>9__38_0;

            static <>c__38()
            {
                TaskLinq.<>c__38<T>.<>9 = new TaskLinq.<>c__38<T>();
            }

            internal UnitT <Finish>b__38_0(Task<T> t)
            {
                if (t.IsFaulted)
                {
                    TaskLinq.RethrowAsyncExceptionsContext.Do<SynchronizationContext>(delegate (SynchronizationContext x) {
                        SendOrPostCallback <>9__2;
                        SendOrPostCallback d = <>9__2;
                        if (<>9__2 == null)
                        {
                            SendOrPostCallback local1 = <>9__2;
                            d = <>9__2 = delegate (object _) {
                                throw new AggregateException(t.Exception.InnerExceptions);
                            };
                        }
                        UnitT state = new UnitT();
                        x.Post(d, state);
                    });
                }
                return new UnitT();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__43<T>
        {
            public static readonly TaskLinq.<>c__43<T> <>9;
            public static Action<TaskCompletionSource<T>, T> <>9__43_0;

            static <>c__43()
            {
                TaskLinq.<>c__43<T>.<>9 = new TaskLinq.<>c__43<T>();
            }

            internal void <SetResultFromTask>b__43_0(TaskCompletionSource<T> ts, T taskResult)
            {
                ts.SetResult(taskResult);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__44<TI, TR>
        {
            public static readonly TaskLinq.<>c__44<TI, TR> <>9;
            public static Action<TaskCompletionSource<TR>> <>9__44_0;
            public static Action<TaskCompletionSource<TR>, Exception> <>9__44_1;

            static <>c__44()
            {
                TaskLinq.<>c__44<TI, TR>.<>9 = new TaskLinq.<>c__44<TI, TR>();
            }

            internal void <SetResultFromTask>b__44_0(TaskCompletionSource<TR> t)
            {
                t.SetCanceled();
            }

            internal void <SetResultFromTask>b__44_1(TaskCompletionSource<TR> t, Exception e)
            {
                t.SetException(e);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__45<T>
        {
            public static readonly TaskLinq.<>c__45<T> <>9;
            public static Action<TaskCompletionSource<T>, T> <>9__45_0;

            static <>c__45()
            {
                TaskLinq.<>c__45<T>.<>9 = new TaskLinq.<>c__45<T>();
            }

            internal void <SetResultFromTaskSafe>b__45_0(TaskCompletionSource<T> ts, T taskResult)
            {
                ts.SetResult(taskResult);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__52<T>
        {
            public static readonly TaskLinq.<>c__52<T> <>9;
            public static Action<TaskCompletionSource<T>, Exception> <>9__52_1;

            static <>c__52()
            {
                TaskLinq.<>c__52<T>.<>9 = new TaskLinq.<>c__52<T>();
            }

            internal void <SetExceptionSafe>b__52_1(TaskCompletionSource<T> ts, Exception result)
            {
                ts.SetException(result);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__53<T>
        {
            public static readonly TaskLinq.<>c__53<T> <>9;
            public static Action<TaskCompletionSource<T>, T> <>9__53_0;

            static <>c__53()
            {
                TaskLinq.<>c__53<T>.<>9 = new TaskLinq.<>c__53<T>();
            }

            internal void <SetResultSafe>b__53_0(TaskCompletionSource<T> ts, T result)
            {
                ts.SetResult(result);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__55<T>
        {
            public static readonly TaskLinq.<>c__55<T> <>9;
            public static Func<TaskLinq<T>, TaskLinq<T>> <>9__55_2;

            static <>c__55()
            {
                TaskLinq.<>c__55<T>.<>9 = new TaskLinq.<>c__55<T>();
            }

            internal TaskLinq<T> <WithDefaultScheduler>b__55_2(TaskLinq<T> x) => 
                x;
        }

        public sealed class Chain
        {
            private readonly DevExpress.Mvvm.Native.TaskLinq.RunFuture RunFuture;
            public readonly DevExpress.Mvvm.Native.TaskLinq.SchedulerFuture SchedulerFuture;

            public Chain(DevExpress.Mvvm.Native.TaskLinq.SchedulerFuture schedulerFuture, Action run = null)
            {
                this.RunFuture = new DevExpress.Mvvm.Native.TaskLinq.RunFuture();
                this.SchedulerFuture = schedulerFuture;
                this.RunFuture.Continue(run);
            }

            public Chain(TaskScheduler scheduler = null, Action run = null) : this(new DevExpress.Mvvm.Native.TaskLinq.SchedulerFuture(scheduler), run)
            {
            }

            public void Continue(TaskLinq.Chain chain)
            {
                this.SchedulerFuture.Continue(new Action<TaskScheduler>(chain.SchedulerFuture.Run));
                this.RunFuture.Continue(new Action(chain.RunFuture.Run));
            }

            public void Run(TaskScheduler scheduler)
            {
                if (scheduler == null)
                {
                    throw new ArgumentNullException("scheduler");
                }
                this.SchedulerFuture.Run(scheduler);
                this.RunFuture.Run();
            }
        }

        private sealed class RunFuture
        {
            private readonly object sync = new object();
            private Action @continue;
            private bool ran;

            public void Continue(Action action)
            {
                if (!this.ran)
                {
                    object sync = this.sync;
                    lock (sync)
                    {
                        if (!this.ran)
                        {
                            this.@continue += action;
                            return;
                        }
                    }
                }
                if (action != null)
                {
                    action();
                }
            }

            public void Run()
            {
                if (!this.ran)
                {
                    Action action;
                    object sync = this.sync;
                    lock (sync)
                    {
                        if (this.ran)
                        {
                            return;
                        }
                        else
                        {
                            this.ran = true;
                            action = this.@continue;
                            this.@continue = null;
                        }
                    }
                    if (action != null)
                    {
                        action();
                    }
                }
            }
        }

        public sealed class SchedulerFuture
        {
            private readonly object sync = new object();
            private TaskScheduler scheduler;
            private Action<TaskScheduler> @continue;

            public SchedulerFuture(TaskScheduler scheduler = null)
            {
                this.scheduler = scheduler;
            }

            private void CheckScheduler(TaskScheduler scheduler)
            {
                if (ReferenceEquals(this.scheduler, TaskScheduler.Default) != ReferenceEquals(scheduler, TaskScheduler.Default))
                {
                    int num = 0;
                    object[] objArray1 = new object[] { "SchedulerFuture: ", num, " ", this.scheduler.Id, " ", scheduler.Id, " ", TaskScheduler.Default.Id };
                    throw new InvalidOperationException(string.Concat(objArray1));
                }
            }

            public void Continue(Action<TaskScheduler> action)
            {
                if (this.scheduler == null)
                {
                    object sync = this.sync;
                    lock (sync)
                    {
                        if (this.scheduler == null)
                        {
                            this.@continue += action;
                            return;
                        }
                    }
                }
                action(this.scheduler);
            }

            public void Run(TaskScheduler scheduler)
            {
                if (scheduler == null)
                {
                    throw new ArgumentNullException("scheduler");
                }
                if (this.scheduler != null)
                {
                    this.CheckScheduler(scheduler);
                }
                else
                {
                    Action<TaskScheduler> action;
                    object sync = this.sync;
                    lock (sync)
                    {
                        if (this.scheduler != null)
                        {
                            this.CheckScheduler(scheduler);
                            return;
                        }
                        else
                        {
                            this.scheduler = scheduler;
                            action = this.@continue;
                            this.@continue = null;
                        }
                    }
                    if (action != null)
                    {
                        action(this.scheduler);
                    }
                }
            }
        }
    }
}

