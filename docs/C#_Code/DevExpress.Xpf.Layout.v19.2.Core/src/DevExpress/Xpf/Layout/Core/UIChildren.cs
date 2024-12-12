namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class UIChildren : INotifyCollectionChanged
    {
        private readonly List<IUIElement> elements = new List<IUIElement>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Add(IUIElement element)
        {
            this.AddOverride(element);
        }

        protected virtual void AddOverride(IUIElement element)
        {
            if (!this.elements.Contains(element))
            {
                this.elements.Add(element);
                this.RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, element));
            }
        }

        public T GetElement<T>() where T: class, IUIElement
        {
            T local;
            using (List<IUIElement>.Enumerator enumerator = this.elements.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IUIElement current = enumerator.Current;
                        if (!(current is T))
                        {
                            continue;
                        }
                        local = current as T;
                    }
                    else
                    {
                        return default(T);
                    }
                    break;
                }
            }
            return local;
        }

        public IUIElement[] GetElements() => 
            this.elements.ToArray();

        public void MakeLast(IUIElement element)
        {
            if (this.elements.Contains(element))
            {
                this.elements.Remove(element);
                this.elements.Add(element);
            }
        }

        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs ea)
        {
            NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
            if (collectionChanged != null)
            {
                collectionChanged(this, ea);
            }
        }

        public bool Remove(IUIElement element)
        {
            bool flag = this.elements.Remove(element);
            if (flag)
            {
                this.RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, element));
            }
            return flag;
        }

        public int Count =>
            this.elements.Count;
    }
}

