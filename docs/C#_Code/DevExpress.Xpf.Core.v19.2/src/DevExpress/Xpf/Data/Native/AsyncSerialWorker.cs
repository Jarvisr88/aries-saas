namespace DevExpress.Xpf.Data.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class AsyncSerialWorker : AsyncWorkerBase
    {
        private readonly Action shutdownTask;
        private volatile AsyncTask currentQueueTask;
        private volatile CancellationTokenSource currentCancellationTokenSource;
        private volatile bool terminate;
        private readonly List<AsyncTask> queue;
        private Thread thread;
        private ManualResetEvent resetEvent;

        public AsyncSerialWorker(Action workingChanged, Action shutdownTask) : base(workingChanged)
        {
            this.queue = new List<AsyncTask>();
            this.resetEvent = new ManualResetEvent(false);
            this.shutdownTask = shutdownTask;
        }

        private void DebugLog(string log)
        {
        }

        protected override void DisposeCore()
        {
            if (this.thread != null)
            {
                this.thread = null;
                this.terminate = true;
                if (this.currentCancellationTokenSource != null)
                {
                    this.currentCancellationTokenSource.Cancel();
                }
                this.resetEvent.Set();
            }
        }

        private void FinishCurrentTask()
        {
            if (this.currentQueueTask != null)
            {
                this.currentQueueTask.OnFinish();
            }
            this.currentQueueTask = null;
        }

        protected override void ReplaceOrAddTaskCore(AsyncTask task, bool forceAdd)
        {
            if ((this.currentQueueTask != null) && Equals(this.currentQueueTask.Token, task.Token))
            {
                if (!Equals(task.State, this.currentQueueTask.State))
                {
                    this.ThrottleCurrentTask();
                    this.queue.Add(task);
                }
            }
            else
            {
                int index = this.queue.IndexOf<AsyncTask>(x => Equals(x.Token, task.Token));
                if (index < 0)
                {
                    if (forceAdd)
                    {
                        this.queue.Add(task);
                    }
                }
                else
                {
                    AsyncTask task2 = this.queue[index];
                    if (!Equals(task.State, task2.State))
                    {
                        this.queue.RemoveAt(index);
                        this.queue.Add(task);
                    }
                }
            }
            this.TryStartWork();
        }

        private void ThrottleCurrentTask()
        {
            this.FinishCurrentTask();
            this.currentCancellationTokenSource.Cancel();
        }

        [IteratorStateMachine(typeof(<ThrottleTasksCore>d__6))]
        protected override IEnumerable<object> ThrottleTasksCore<T>(Predicate<T> condition)
        {
            if ((this.currentQueueTask != null) && IsMatchedTask<T>(condition, this.currentQueueTask))
            {
                yield return this.currentQueueTask.State;
                this.ThrottleCurrentTask();
            }
            AsyncTask[] <>7__wrap1 = this.queue.ToArray();
            int index = 0;
            while (true)
            {
                if (index >= <>7__wrap1.Length)
                {
                    <>7__wrap1 = null;
                }
                AsyncTask task = <>7__wrap1[index];
                if (IsMatchedTask<T>(condition, task))
                {
                    this.queue.Remove(task);
                }
                yield return task.State;
                index++;
            }
        }

        private void TryStartWork()
        {
            if ((this.queue.Count != 0) && !base.Working)
            {
                if (this.thread == null)
                {
                    SynchronizationContext syncContext = SynchronizationContext.Current;
                    this.thread = new Thread(delegate {
                        while (true)
                        {
                            this.DebugLog("Waiting for event");
                            this.resetEvent.WaitOne();
                            if (this.terminate)
                            {
                                this.shutdownTask();
                                return;
                            }
                            this.DebugLog("Event occured");
                            AsyncTask currentQueueTask = this.currentQueueTask;
                            object result = null;
                            Exception exception = null;
                            if (currentQueueTask != null)
                            {
                                this.DebugLog("Starting async job");
                                try
                                {
                                    Task<object> task2 = currentQueueTask.AsyncJob(this.currentCancellationTokenSource.Token, currentQueueTask.State);
                                    result = task2?.Result;
                                }
                                catch (OperationCanceledException)
                                {
                                }
                                catch (Exception exception1)
                                {
                                    exception = exception1;
                                }
                                this.DebugLog("Async job completed");
                            }
                            this.DebugLog("Resetting the event (BLOCK)");
                            if (!this.terminate)
                            {
                                this.resetEvent.Reset();
                            }
                            this.DebugLog("Posting result to the main thread");
                            syncContext.Post(delegate (object x) {
                                this.DebugLog("Result received in main thread");
                                if (((this.currentQueueTask != null) && ((result != null) || (exception != null))) && !this.terminate)
                                {
                                    this.DebugLog("Starting sync job");
                                    this.currentQueueTask.SyncJob(result, exception);
                                    this.DebugLog("Sync job completed");
                                }
                                this.Working = false;
                                this.FinishCurrentTask();
                                this.currentCancellationTokenSource.Dispose();
                                this.currentCancellationTokenSource = null;
                                this.TryStartWork();
                            }, null);
                        }
                    });
                    this.thread.Name = "AsyncWorkerThread";
                    this.thread.IsBackground = true;
                    this.thread.Start();
                }
                if (this.currentQueueTask != null)
                {
                    throw new InvalidOperationException("currentQueueTask should be null");
                }
                base.Working = true;
                this.currentQueueTask = this.queue[0];
                this.currentQueueTask.OnStart();
                this.currentCancellationTokenSource = new CancellationTokenSource();
                this.queue.RemoveAt(0);
                this.DebugLog("Setting the event (ALLOW)");
                this.resetEvent.Set();
            }
        }

        [CompilerGenerated]
        private sealed class <ThrottleTasksCore>d__6<T> : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            public AsyncSerialWorker <>4__this;
            private Predicate<T> condition;
            public Predicate<T> <>3__condition;
            private AsyncTask[] <>7__wrap1;
            private int <>7__wrap2;

            [DebuggerHidden]
            public <ThrottleTasksCore>d__6(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        if ((this.<>4__this.currentQueueTask == null) || !AsyncWorkerBase.IsMatchedTask<T>(this.condition, this.<>4__this.currentQueueTask))
                        {
                            break;
                        }
                        this.<>2__current = this.<>4__this.currentQueueTask.State;
                        this.<>1__state = 1;
                        return true;

                    case 1:
                        this.<>1__state = -1;
                        this.<>4__this.ThrottleCurrentTask();
                        break;

                    case 2:
                        this.<>1__state = -1;
                        this.<>7__wrap2++;
                        goto TR_0007;

                    default:
                        return false;
                }
                this.<>7__wrap1 = this.<>4__this.queue.ToArray();
                this.<>7__wrap2 = 0;
            TR_0007:
                if (this.<>7__wrap2 >= this.<>7__wrap1.Length)
                {
                    this.<>7__wrap1 = null;
                    return false;
                }
                AsyncTask task = this.<>7__wrap1[this.<>7__wrap2];
                if (AsyncWorkerBase.IsMatchedTask<T>(this.condition, task))
                {
                    this.<>4__this.queue.Remove(task);
                }
                this.<>2__current = task.State;
                this.<>1__state = 2;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                AsyncSerialWorker.<ThrottleTasksCore>d__6<T> d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (AsyncSerialWorker.<ThrottleTasksCore>d__6<T>) this;
                }
                else
                {
                    d__ = new AsyncSerialWorker.<ThrottleTasksCore>d__6<T>(0) {
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
    }
}

