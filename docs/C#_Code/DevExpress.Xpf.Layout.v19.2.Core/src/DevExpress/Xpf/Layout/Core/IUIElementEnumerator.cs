namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class IUIElementEnumerator : IEnumerator<IUIElement>, IDisposable, IEnumerator
    {
        private IUIElement Root;
        private Stack<IUIElement> Stack;
        private Predicate<IUIElement> Filter;
        private IUIElement current;

        public IUIElementEnumerator(IUIElement element) : this(element, null)
        {
        }

        public IUIElementEnumerator(IUIElement element, Predicate<IUIElement> filter)
        {
            this.Stack = new Stack<IUIElement>(8);
            this.Root = element;
            this.Filter = filter;
        }

        public void Dispose()
        {
            this.Reset();
            this.Root = null;
            this.Stack = null;
            this.Filter = null;
            GC.SuppressFinalize(this);
        }

        public bool MoveNext()
        {
            if (this.current == null)
            {
                this.current = this.Root;
            }
            else
            {
                if (this.current.Children != null)
                {
                    IUIElement[] elements = this.current.Children.GetElements();
                    if (elements.Length != 0)
                    {
                        for (int i = 0; i < elements.Length; i++)
                        {
                            IUIElement element = elements[(elements.Length - 1) - i];
                            if ((this.Filter == null) || this.Filter(element))
                            {
                                this.Stack.Push(element);
                            }
                        }
                    }
                }
                this.current = (this.Stack.Count > 0) ? this.Stack.Pop() : null;
            }
            return (this.current != null);
        }

        public void Reset()
        {
            if (this.Stack != null)
            {
                this.Stack.Clear();
            }
            this.current = null;
        }

        object IEnumerator.Current =>
            this.current;

        public IUIElement Current =>
            this.current;
    }
}

