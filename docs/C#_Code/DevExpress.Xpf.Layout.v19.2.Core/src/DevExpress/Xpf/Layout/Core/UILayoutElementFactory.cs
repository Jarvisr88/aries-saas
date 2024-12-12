namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public abstract class UILayoutElementFactory : ILayoutElementFactory
    {
        private readonly ElementHash Hash = new ElementHash();

        protected abstract ILayoutElement CreateElement(IUIElement uiKey);
        public ILayoutElement CreateLayoutHierarchy(object rootKey) => 
            (rootKey is IUIElement) ? this.CreateUIHierarchy((IUIElement) rootKey, this.Hash) : null;

        private ILayoutElement CreateUIHierarchy(IUIElement rootKey, ElementHash hash)
        {
            if (hash.Count > 0)
            {
                hash.Clear();
            }
            if (rootKey == null)
            {
                return null;
            }
            using (IEnumerator<IUIElement> enumerator = this.GetUIEnumerator(rootKey))
            {
                while (enumerator.MoveNext())
                {
                    IUIElement current = enumerator.Current;
                    ILayoutElement element = this.CreateElement(current);
                    ILayoutElement element3 = null;
                    if ((current.Scope != null) && hash.TryGetValue(current.Scope, out element3))
                    {
                        BaseLayoutContainer container = element3 as BaseLayoutContainer;
                        if (container != null)
                        {
                            container.AddInternal(element);
                        }
                    }
                    hash.Add(current, element);
                }
            }
            return hash[rootKey];
        }

        public ILayoutElement GetElement(object key)
        {
            if (!(key is IUIElement))
            {
                return null;
            }
            ILayoutElement element = null;
            return (this.Hash.TryGetValue((IUIElement) key, out element) ? element : null);
        }

        protected abstract IEnumerator<IUIElement> GetUIEnumerator(IUIElement rootKey);

        private class ElementHash
        {
            private IDictionary<IUIElement, ILayoutElement> hash = new Dictionary<IUIElement, ILayoutElement>();

            public void Add(IUIElement key, ILayoutElement element)
            {
                this.hash.Add(key, element);
                element.Disposed += new EventHandler(new Sink(key, this.hash).Disposed);
            }

            public void Clear()
            {
                ILayoutElement[] array = new ILayoutElement[this.hash.Count];
                this.hash.Values.CopyTo(array, 0);
                for (int i = 0; i < array.Length; i++)
                {
                    array[i].Dispose();
                }
            }

            public bool TryGetValue(IUIElement key, out ILayoutElement element) => 
                this.hash.TryGetValue(key, out element);

            public ILayoutElement this[IUIElement key] =>
                this.hash[key];

            public int Count =>
                this.hash.Count;

            private class Sink
            {
                private IUIElement Key;
                private IDictionary<IUIElement, ILayoutElement> Hash;

                public Sink(IUIElement key, IDictionary<IUIElement, ILayoutElement> hash)
                {
                    this.Key = key;
                    this.Hash = hash;
                }

                public void Disposed(object sender, EventArgs e)
                {
                    ((ILayoutElement) sender).Disposed -= new EventHandler(this.Disposed);
                    this.Hash.Remove(this.Key);
                    this.Hash = null;
                    this.Key = null;
                }
            }
        }
    }
}

