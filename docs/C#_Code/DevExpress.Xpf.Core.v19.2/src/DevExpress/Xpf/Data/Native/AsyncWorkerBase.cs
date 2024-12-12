namespace DevExpress.Xpf.Data.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AsyncWorkerBase : IDisposable
    {
        private readonly Action workingChanged;
        private bool working;
        private bool disposed;

        public AsyncWorkerBase(Action workingChanged)
        {
            this.workingChanged = workingChanged;
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.disposed = true;
                this.DisposeCore();
            }
        }

        protected abstract void DisposeCore();
        protected static bool IsMatchedTask<T>(Predicate<T> condition, AsyncTask task) => 
            (task.Token is T) && condition((T) task.Token);

        public void ReplaceOrAddTask(AsyncTask task)
        {
            this.ReplaceOrAddTask(task, true);
        }

        private void ReplaceOrAddTask(AsyncTask task, bool forceAdd)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(base.GetType().Name);
            }
            this.ReplaceOrAddTaskCore(task, forceAdd);
        }

        protected abstract void ReplaceOrAddTaskCore(AsyncTask task, bool forceAdd);
        public void ReplaceTask(AsyncTask task)
        {
            this.ReplaceOrAddTask(task, false);
        }

        public object[] ThrottleTasks<T>(Predicate<T> condition) => 
            this.ThrottleTasksCore<T>(condition).ToArray<object>();

        protected abstract IEnumerable<object> ThrottleTasksCore<T>(Predicate<T> condition);

        public bool Working
        {
            get => 
                this.working;
            protected set
            {
                if (this.working != value)
                {
                    this.working = value;
                    this.workingChanged();
                }
            }
        }
    }
}

