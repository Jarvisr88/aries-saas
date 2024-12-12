namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Reflection;

    public abstract class NestedObjectEnumeratorBase : IEnumerator
    {
        protected EnumStack stack;
        private IEnumerator enumerator;

        protected NestedObjectEnumeratorBase(IEnumerator enumerator)
        {
            this.enumerator = enumerator;
            this.stack = new EnumStack();
        }

        protected virtual IEnumerator GetEnumerator(IEnumerator source) => 
            new SimpleEnumerator(source);

        protected abstract IEnumerator GetNestedObjects();
        public int[] GetStackIndices()
        {
            int[] numArray = new int[this.stack.Count - 1];
            for (int i = 1; i < this.stack.Count; i++)
            {
                numArray[i - 1] = this.stack[i].CurrentIndex;
            }
            return numArray;
        }

        public virtual bool MoveNext()
        {
            if (this.stack.IsEmpty)
            {
                this.stack.Push(this.GetEnumerator(this.enumerator));
                return this.stack.Enumerator.MoveNext();
            }
            IEnumerator nestedObjects = this.GetNestedObjects();
            if (nestedObjects.MoveNext())
            {
                nestedObjects.Reset();
                this.stack.Push(this.GetEnumerator(nestedObjects));
                return this.stack.Enumerator.MoveNext();
            }
            while (!this.stack.Enumerator.MoveNext())
            {
                this.stack.Pop();
                if (this.stack.IsEmpty)
                {
                    return false;
                }
            }
            return true;
        }

        public virtual void Reset()
        {
            this.stack.Clear();
        }

        object IEnumerator.Current =>
            this.stack.Enumerator.Current;

        protected class EnumStack : StackBase
        {
            public SimpleEnumerator Enumerator =>
                !base.IsEmpty ? ((SimpleEnumerator) base.Peek()) : null;

            public SimpleEnumerator this[int index] =>
                (SimpleEnumerator) base.list[index];
        }
    }
}

