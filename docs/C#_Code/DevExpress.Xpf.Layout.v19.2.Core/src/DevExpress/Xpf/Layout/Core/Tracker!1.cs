namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;

    internal class Tracker<T> : IObservable<T>
    {
        private ICollection<IObserver<T>> observers;

        public Tracker()
        {
            this.observers = new List<IObserver<T>>();
        }

        protected virtual Exception CheckError(T element) => 
            null;

        public void EndNotification()
        {
            foreach (IObserver<T> observer in this.GetObservers())
            {
                if (this.observers.Contains(observer))
                {
                    observer.OnCompleted();
                }
            }
            this.observers.Clear();
        }

        private IObserver<T>[] GetObservers()
        {
            IObserver<T>[] array = new IObserver<T>[this.observers.Count];
            this.observers.CopyTo(array, 0);
            return array;
        }

        public void Notify(T element)
        {
            Exception error = this.CheckError(element);
            foreach (IObserver<T> observer in this.GetObservers())
            {
                if (error != null)
                {
                    observer.OnError(error);
                }
                else
                {
                    observer.OnNext(element);
                }
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!this.observers.Contains(observer))
            {
                this.observers.Add(observer);
            }
            return new Unsubscriber<T>(this.observers, observer);
        }

        public bool InUse =>
            this.observers.Count > 0;

        private class Unsubscriber : IDisposable
        {
            private ICollection<IObserver<T>> observers;
            private IObserver<T> observer;

            public Unsubscriber(ICollection<IObserver<T>> observers, IObserver<T> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            public void Dispose()
            {
                if ((this.observer != null) && this.observers.Contains(this.observer))
                {
                    this.observers.Remove(this.observer);
                }
            }
        }
    }
}

