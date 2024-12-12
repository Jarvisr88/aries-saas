namespace DevExpress.Xpf.Data.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class AsyncParallelWorker : AsyncWorkerBase
    {
        private readonly TaskScheduler scheduler;
        private readonly List<AsyncTaskInfo> runningTasks;

        public AsyncParallelWorker(Action workingChanged) : base(workingChanged)
        {
            this.scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.runningTasks = new List<AsyncTaskInfo>();
        }

        protected override void DisposeCore()
        {
            foreach (AsyncTaskInfo info in this.runningTasks)
            {
                info.AsyncTask.OnFinish();
                info.CancellationTokenSource.Cancel();
            }
            this.runningTasks.Clear();
        }

        private void RemoveRunningTaskAndUpdateIsWorking(AsyncTaskInfo taskInfo)
        {
            this.runningTasks.Remove(taskInfo);
            base.Working = this.runningTasks.Any<AsyncTaskInfo>();
        }

        protected override void ReplaceOrAddTaskCore(AsyncTask task, bool forceAdd)
        {
            AsyncTaskInfo runningTaskInfo = this.runningTasks.FirstOrDefault<AsyncTaskInfo>(x => Equals(x.AsyncTask.Token, task.Token));
            if (runningTaskInfo != null)
            {
                if (Equals(task.State, runningTaskInfo.AsyncTask.State))
                {
                    return;
                }
                this.ThrottleTask(runningTaskInfo);
                forceAdd = true;
            }
            if (forceAdd)
            {
                base.Working = true;
                task.OnStart();
                CancellationTokenSource cancelletionSource = new CancellationTokenSource();
                AsyncTaskInfo taskInfo = new AsyncTaskInfo(task, cancelletionSource);
                this.runningTasks.Add(taskInfo);
                Task<object> task2 = null;
                try
                {
                    task2 = task.AsyncJob(cancelletionSource.Token, task.State);
                }
                catch (Exception exception1)
                {
                    task2 = exception1.FutureException<object>();
                }
                Task task3 = task2.ContinueWith(delegate (Task<object> t) {
                    if (this.runningTasks.Contains(taskInfo))
                    {
                        if (t.Exception == null)
                        {
                            task.SyncJob(t.Result, null);
                        }
                        else
                        {
                            task.SyncJob(null, t.Exception.InnerExceptions.FirstOrDefault<Exception>());
                        }
                        task.OnFinish();
                    }
                    this.RemoveRunningTaskAndUpdateIsWorking(taskInfo);
                    cancelletionSource.Dispose();
                }, this.scheduler);
            }
        }

        private void ThrottleTask(AsyncTaskInfo runningTaskInfo)
        {
            runningTaskInfo.CancellationTokenSource.Cancel();
            runningTaskInfo.AsyncTask.OnFinish();
            this.RemoveRunningTaskAndUpdateIsWorking(runningTaskInfo);
        }

        [IteratorStateMachine(typeof(<ThrottleTasksCore>d__5))]
        protected override IEnumerable<object> ThrottleTasksCore<T>(Predicate<T> condition)
        {
            <ThrottleTasksCore>d__5<T> d__1 = new <ThrottleTasksCore>d__5<T>(-2);
            d__1.<>4__this = this;
            d__1.<>3__condition = condition;
            return d__1;
        }

        [CompilerGenerated]
        private sealed class <ThrottleTasksCore>d__5<T> : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private Predicate<T> condition;
            public Predicate<T> <>3__condition;
            public AsyncParallelWorker <>4__this;
            private AsyncParallelWorker.AsyncTaskInfo[] <>7__wrap1;
            private int <>7__wrap2;

            [DebuggerHidden]
            public <ThrottleTasksCore>d__5(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num != 0)
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    this.<>7__wrap2++;
                }
                else
                {
                    AsyncParallelWorker.<>c__DisplayClass5_0<T> class_;
                    this.<>1__state = -1;
                    Predicate<T> condition = this.condition;
                    AsyncParallelWorker.AsyncTaskInfo[] infoArray = this.<>4__this.runningTasks.Where<AsyncParallelWorker.AsyncTaskInfo>(new Func<AsyncParallelWorker.AsyncTaskInfo, bool>(class_.<ThrottleTasksCore>b__0)).ToArray<AsyncParallelWorker.AsyncTaskInfo>();
                    this.<>7__wrap1 = infoArray;
                    this.<>7__wrap2 = 0;
                }
                if (this.<>7__wrap2 >= this.<>7__wrap1.Length)
                {
                    this.<>7__wrap1 = null;
                    return false;
                }
                AsyncParallelWorker.AsyncTaskInfo runningTaskInfo = this.<>7__wrap1[this.<>7__wrap2];
                this.<>4__this.ThrottleTask(runningTaskInfo);
                this.<>2__current = runningTaskInfo.AsyncTask.State;
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                AsyncParallelWorker.<ThrottleTasksCore>d__5<T> d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (AsyncParallelWorker.<ThrottleTasksCore>d__5<T>) this;
                }
                else
                {
                    d__ = new AsyncParallelWorker.<ThrottleTasksCore>d__5<T>(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.condition = this.<>3__condition;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private class AsyncTaskInfo
        {
            public readonly DevExpress.Xpf.Data.Native.AsyncTask AsyncTask;
            public readonly System.Threading.CancellationTokenSource CancellationTokenSource;

            public AsyncTaskInfo(DevExpress.Xpf.Data.Native.AsyncTask asyncTask, System.Threading.CancellationTokenSource cancellationTokenSource)
            {
                this.AsyncTask = asyncTask;
                this.CancellationTokenSource = cancellationTokenSource;
            }
        }
    }
}

