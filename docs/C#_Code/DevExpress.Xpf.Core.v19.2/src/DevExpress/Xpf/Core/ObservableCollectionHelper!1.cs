namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ObservableCollectionHelper<T> : IDisposable
    {
        private readonly ObservableCollection<T> targetCollection;
        private readonly bool reset;

        public ObservableCollectionHelper(ObservableCollection<T> target, bool reset = false)
        {
            this.<NewElements>k__BackingField = new HashSet<T>();
            this.targetCollection = target;
            this.reset = reset;
            this.<ExistingElements>k__BackingField = new HashSet<T>(this.targetCollection);
        }

        public void Add(T element)
        {
            this.NewElements.Add(element);
        }

        public void Dispose()
        {
            if (this.ExistingElements.Count > 0)
            {
                if (this.reset)
                {
                    this.ResetTargetCollection();
                }
                else
                {
                    this.UpdateTargetCollection();
                }
            }
            else
            {
                foreach (T local in this.NewElements)
                {
                    this.targetCollection.Add(local);
                }
            }
        }

        private void ResetTargetCollection()
        {
            this.targetCollection.Clear();
            foreach (T local in this.NewElements)
            {
                this.targetCollection.Add(local);
            }
        }

        private void UpdateTargetCollection()
        {
            foreach (T local in this.NewElements)
            {
                if (!this.ExistingElements.Contains(local))
                {
                    this.targetCollection.Add(local);
                }
            }
            foreach (T local2 in this.ExistingElements)
            {
                if (!this.NewElements.Contains(local2))
                {
                    this.targetCollection.Remove(local2);
                }
            }
        }

        public HashSet<T> NewElements { get; }

        public HashSet<T> ExistingElements { get; }
    }
}

