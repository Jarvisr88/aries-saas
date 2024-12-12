namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class XlReadonlyCollection<T, U> : IXlReadonlyCollection<T>, IEnumerable<T>, IEnumerable where T: class where U: class, T, IXlNamedObject
    {
        private readonly List<U> innerList;

        public XlReadonlyCollection(List<U> list)
        {
            this.innerList = list;
        }

        public IEnumerator<T> GetEnumerator() => 
            new XlEnumeratorImpl<T, U>(this.innerList.GetEnumerator());

        IEnumerator IEnumerable.GetEnumerator() => 
            this.innerList.GetEnumerator();

        public T this[int index] =>
            this.innerList[index];

        public T this[string name]
        {
            get
            {
                T local2;
                using (List<U>.Enumerator enumerator = this.innerList.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            U current = enumerator.Current;
                            if (!string.Equals(current.Name, name))
                            {
                                continue;
                            }
                            local2 = (T) current;
                        }
                        else
                        {
                            return default(T);
                        }
                        break;
                    }
                }
                return local2;
            }
        }

        public int Count =>
            this.innerList.Count;

        private class XlEnumeratorImpl : IEnumerator<T>, IDisposable, IEnumerator
        {
            private IEnumerator<U> innerEnumerator;

            public XlEnumeratorImpl(IEnumerator<U> enumerator)
            {
                this.innerEnumerator = enumerator;
            }

            public void Dispose()
            {
                this.innerEnumerator.Dispose();
            }

            public bool MoveNext() => 
                this.innerEnumerator.MoveNext();

            public void Reset()
            {
                this.innerEnumerator.Reset();
            }

            public T Current =>
                this.innerEnumerator.Current;

            object IEnumerator.Current =>
                this.Current;
        }
    }
}

