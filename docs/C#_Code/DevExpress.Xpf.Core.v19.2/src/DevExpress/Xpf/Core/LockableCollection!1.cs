namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public class LockableCollection<T> : ObservableCollection<T>
    {
        private bool _IsChanged;
        private int _UpdateLockCount;

        public void Assign(IList<T> source)
        {
            if (!this.Equals(source))
            {
                this.BeginUpdate();
                try
                {
                    base.Clear();
                    foreach (T local in source)
                    {
                        base.Add(local);
                    }
                }
                finally
                {
                    this.EndUpdate();
                }
            }
        }

        public void BeginUpdate()
        {
            if (!this.IsUpdateLocked)
            {
                this._IsChanged = false;
            }
            this._UpdateLockCount++;
        }

        public void EndUpdate()
        {
            if (this.IsUpdateLocked)
            {
                this._UpdateLockCount--;
                if (!this.IsUpdateLocked && this._IsChanged)
                {
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IList<T>))
            {
                return base.Equals(obj);
            }
            IList<T> list = (IList<T>) obj;
            if (base.Count != list.Count)
            {
                return false;
            }
            for (int i = 0; i < base.Count; i++)
            {
                T local = base[i];
                if (!local.Equals(list[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public T Find(Predicate<T> match)
        {
            if (match != null)
            {
                int count = base.Count;
                for (int i = 0; i < count; i++)
                {
                    T local2 = base[i];
                    if (match(local2))
                    {
                        return local2;
                    }
                }
            }
            return default(T);
        }

        public void ForEach(Action<T> action)
        {
            for (int i = 0; i < base.Count; i++)
            {
                action(base[i]);
            }
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.IsUpdateLocked)
            {
                this._IsChanged = true;
            }
            else
            {
                base.OnCollectionChanged(e);
            }
        }

        protected override void SetItem(int index, T item)
        {
            if (!base[index].Equals(item))
            {
                base.SetItem(index, item);
            }
        }

        public bool IsUpdateLocked =>
            this._UpdateLockCount > 0;
    }
}

