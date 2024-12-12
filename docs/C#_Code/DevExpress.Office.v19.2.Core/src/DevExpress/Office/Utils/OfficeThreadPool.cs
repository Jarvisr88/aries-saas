namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class OfficeThreadPool : IDisposable
    {
        private int maxThreadsCount = 3;
        private Thread poolManager;
        private List<ThreadPoolItem> threads;
        private Queue<ThreadPoolItem> freeThreads;
        private Queue<WaitCallback> jobQueue = new Queue<WaitCallback>();
        private EventWaitHandle[] completionHandles;

        public OfficeThreadPool()
        {
            this.Start();
        }

        protected internal virtual void CreateCompletionHandles()
        {
            int count = this.threads.Count;
            this.completionHandles = new EventWaitHandle[this.threads.Count + 1];
            for (int i = 0; i < count; i++)
            {
                this.completionHandles[i] = this.threads[i].Complete;
            }
            this.completionHandles[count] = new ManualResetEvent(false);
        }

        protected internal virtual void CreatePoolThreads()
        {
            this.threads = new List<ThreadPoolItem>(this.maxThreadsCount);
            this.freeThreads = new Queue<ThreadPoolItem>(this.maxThreadsCount);
            for (int i = 0; i < this.maxThreadsCount; i++)
            {
                ThreadPoolItem item = new ThreadPoolItem();
                this.threads.Add(item);
                this.freeThreads.Enqueue(item);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Shutdown();
            }
        }

        protected internal virtual void DisposeCompletionHandles()
        {
            this.completionHandles[this.completionHandles.Length - 1].Dispose();
        }

        protected internal virtual void DisposePoolThreads()
        {
            int count = this.threads.Count;
            for (int i = 0; i < count; i++)
            {
                this.threads[i].Dispose();
            }
            this.threads.Clear();
            this.freeThreads.Clear();
        }

        protected internal virtual void EnqueueNextJob(int poolItemIndex)
        {
            WaitCallback nextJobFromQueue = this.GetNextJobFromQueue();
            ThreadPoolItem poolItem = this.threads[poolItemIndex];
            poolItem.CurrentJob = nextJobFromQueue;
            if (nextJobFromQueue != null)
            {
                this.ExecuteJob(poolItem, nextJobFromQueue);
            }
            else
            {
                this.MakeAvailable(poolItem);
            }
        }

        protected internal virtual void ExecuteJob(ThreadPoolItem poolItem, WaitCallback job)
        {
            poolItem.CurrentJob = job;
            poolItem.CanExecute.Set();
        }

        protected internal virtual ThreadPoolItem GetAvailablePoolItem()
        {
            Queue<ThreadPoolItem> freeThreads = this.freeThreads;
            lock (freeThreads)
            {
                return ((this.freeThreads.Count <= 0) ? null : this.freeThreads.Dequeue());
            }
        }

        private WaitCallback GetNextJobFromQueue()
        {
            Queue<WaitCallback> jobQueue = this.jobQueue;
            lock (jobQueue)
            {
                return ((this.jobQueue.Count <= 0) ? null : this.jobQueue.Dequeue());
            }
        }

        protected internal virtual void MakeAvailable(ThreadPoolItem poolItem)
        {
            Queue<ThreadPoolItem> freeThreads = this.freeThreads;
            lock (freeThreads)
            {
                this.freeThreads.Enqueue(poolItem);
            }
        }

        protected internal virtual void PoolManagerWorker()
        {
            while (true)
            {
                try
                {
                    int poolItemIndex = WaitHandle.WaitAny(this.completionHandles);
                    if (poolItemIndex == this.threads.Count)
                    {
                        break;
                    }
                    if ((poolItemIndex >= 0) && (poolItemIndex < this.completionHandles.Length))
                    {
                        this.EnqueueNextJob(poolItemIndex);
                    }
                }
                catch
                {
                }
            }
        }

        public void QueueJob(WaitCallback job)
        {
            ThreadPoolItem availablePoolItem = this.GetAvailablePoolItem();
            if (availablePoolItem != null)
            {
                this.ExecuteJob(availablePoolItem, job);
            }
            else
            {
                Queue<WaitCallback> jobQueue = this.jobQueue;
                lock (jobQueue)
                {
                    this.jobQueue.Enqueue(job);
                }
            }
        }

        public void Reset()
        {
            this.Shutdown();
            this.Start();
        }

        protected internal virtual void Shutdown()
        {
            this.StopPoolManager();
            this.jobQueue.Clear();
            this.DisposePoolThreads();
            this.DisposeCompletionHandles();
        }

        protected internal virtual void Start()
        {
            this.CreatePoolThreads();
            this.CreateCompletionHandles();
            this.StartPoolManager();
        }

        protected internal virtual void StartPoolManager()
        {
            this.poolManager = new Thread(new ThreadStart(this.PoolManagerWorker));
            this.poolManager.IsBackground = true;
            this.poolManager.Start();
        }

        protected internal virtual void StopPoolManager()
        {
            this.completionHandles[this.completionHandles.Length - 1].Set();
            this.poolManager.Join();
        }
    }
}

