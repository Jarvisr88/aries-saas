namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class NestedEnumerator : IEnumerator
    {
        private IEnumerable enumerable;
        private Stack<IEnumerable> enumerableStack;
        protected Stack<IEnumerator> enumeratorStack;

        public NestedEnumerator(IEnumerable enumerable)
        {
            this.enumerable = enumerable;
            this.enumerableStack = new Stack<IEnumerable>();
            this.enumeratorStack = new Stack<IEnumerator>();
        }

        public virtual bool MoveNext()
        {
            if (this.enumerableStack.Count == 0)
            {
                this.Push(this.enumerable);
                return this.CurrentEnumerator.MoveNext();
            }
            if (this.CurrentEnumerable != null)
            {
                this.Push(this.CurrentEnumerable);
                if (!this.CurrentEnumerator.MoveNext())
                {
                    this.Pop();
                    return (this.enumerableStack.Count > 0);
                }
            }
            return true;
        }

        private void Pop()
        {
            if (this.enumeratorStack.Count > 0)
            {
                this.enumerableStack.Pop();
                this.enumeratorStack.Pop();
            }
            if ((this.enumeratorStack.Count != 0) && !this.CurrentEnumerator.MoveNext())
            {
                this.Pop();
            }
        }

        private void Push(IEnumerable enumerable)
        {
            this.enumerableStack.Push(enumerable);
            this.enumeratorStack.Push(enumerable.GetEnumerator());
        }

        public void Reset()
        {
            this.enumerableStack.Clear();
            this.enumeratorStack.Clear();
        }

        public object Current =>
            this.CurrentEnumerator.Current;

        private IEnumerator CurrentEnumerator =>
            this.enumeratorStack.Peek();

        private IEnumerable CurrentEnumerable =>
            this.Current as IEnumerable;
    }
}

