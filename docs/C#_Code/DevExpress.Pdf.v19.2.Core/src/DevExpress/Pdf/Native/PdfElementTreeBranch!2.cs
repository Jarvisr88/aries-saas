namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class PdfElementTreeBranch<K, T> : PdfElementTreeNode<K, T> where T: class
    {
        private readonly PdfDeferredSortedDictionary<K, T> kids;

        internal PdfElementTreeBranch(PdfObjectCollection objects, IList<object> children, PdfCreateTreeKeyAction<K> createKey, PdfCreateTreeElementAction<T> createElement, string nodeName)
        {
            this.kids = new PdfDeferredSortedDictionary<K, T>();
            foreach (object obj2 in children)
            {
                PdfReaderDictionary dictionary = objects.TryResolve(obj2, null) as PdfReaderDictionary;
                if (dictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.kids.AddRange(Parse(dictionary, createKey, createElement, nodeName, true));
            }
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__4))]
        internal override IEnumerator<KeyValuePair<K, T>> GetEnumerator()
        {
            <GetEnumerator>d__4<K, T> d__1 = new <GetEnumerator>d__4<K, T>(0);
            d__1.<>4__this = (PdfElementTreeBranch<K, T>) this;
            return d__1;
        }

        protected override PdfDeferredSortedDictionary<K, T> Value =>
            this.kids;

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__4 : IEnumerator<KeyValuePair<K, T>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<K, T> <>2__current;
            public PdfElementTreeBranch<K, T> <>4__this;
            private IEnumerator<KeyValuePair<K, T>> <>7__wrap1;

            [DebuggerHidden]
            public <GetEnumerator>d__4(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.<>4__this.kids.GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        flag = false;
                    }
                    else
                    {
                        KeyValuePair<K, T> current = this.<>7__wrap1.Current;
                        this.<>2__current = current;
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            KeyValuePair<K, T> IEnumerator<KeyValuePair<K, T>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

