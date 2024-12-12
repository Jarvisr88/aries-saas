namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.SpreadsheetSource;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class WorksheetCollection : IWorksheetCollection, IEnumerable<IWorksheet>, IEnumerable, ICollection
    {
        private readonly List<IWorksheet> innerList = new List<IWorksheet>();

        public void Add(IWorksheet sheet)
        {
            this.innerList.Add(sheet);
        }

        public void Clear()
        {
            this.innerList.Clear();
        }

        public void CopyTo(Array array, int index)
        {
            Array.Copy(this.innerList.ToArray(), 0, array, index, this.innerList.Count);
        }

        public IEnumerator<IWorksheet> GetEnumerator() => 
            this.innerList.GetEnumerator();

        public void RemoveAt(int index)
        {
            this.innerList.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.innerList.GetEnumerator();

        public IWorksheet this[string name]
        {
            get
            {
                IWorksheet worksheet2;
                using (List<IWorksheet>.Enumerator enumerator = this.innerList.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            IWorksheet current = enumerator.Current;
                            if (current.Name != name)
                            {
                                continue;
                            }
                            worksheet2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
                return worksheet2;
            }
        }

        public IWorksheet this[int index] =>
            this.innerList[index];

        public int Count =>
            this.innerList.Count;

        public bool IsSynchronized =>
            ((ICollection) this.innerList).IsSynchronized;

        public object SyncRoot =>
            ((ICollection) this.innerList).SyncRoot;
    }
}

