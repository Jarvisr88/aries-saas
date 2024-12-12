namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class LayoutElementEnumerator : IEnumerator<ILayoutElement>, IDisposable, IEnumerator
    {
        private Stack<ILayoutElement> Stack = new Stack<ILayoutElement>(0x20);
        private ILayoutElement Root;
        private ILayoutElement current;

        public LayoutElementEnumerator(ILayoutElement root)
        {
            this.Root = root;
        }

        private void ResetCore()
        {
            if (this.Stack != null)
            {
                this.Stack.Clear();
            }
            this.current = null;
        }

        bool IEnumerator.MoveNext()
        {
            if (this.current == null)
            {
                this.current = this.Root;
            }
            else
            {
                int length = this.current.Nodes.Length;
                if (length > 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        ILayoutElement item = this.current.Nodes[(length - 1) - i];
                        this.Stack.Push(item);
                    }
                }
                this.current = (this.Stack.Count > 0) ? this.Stack.Pop() : null;
            }
            return (this.current != null);
        }

        void IEnumerator.Reset()
        {
            this.ResetCore();
        }

        void IDisposable.Dispose()
        {
            this.ResetCore();
            this.Stack = null;
            this.Root = null;
            GC.SuppressFinalize(this);
        }

        ILayoutElement IEnumerator<ILayoutElement>.Current =>
            this.current;

        object IEnumerator.Current =>
            this.current;
    }
}

