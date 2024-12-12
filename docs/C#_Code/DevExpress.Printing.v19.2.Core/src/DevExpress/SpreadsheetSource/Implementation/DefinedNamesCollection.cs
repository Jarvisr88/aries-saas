namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.SpreadsheetSource;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class DefinedNamesCollection : IDefinedNamesCollection, IEnumerable<IDefinedName>, IEnumerable, ICollection
    {
        private readonly List<IDefinedName> innerList = new List<IDefinedName>();

        public void Add(IDefinedName item)
        {
            this.innerList.Add(item);
        }

        public void Clear()
        {
            this.innerList.Clear();
        }

        public void CopyTo(Array array, int index)
        {
            Array.Copy(this.innerList.ToArray(), 0, array, index, this.innerList.Count);
        }

        public IDefinedName FindBy(string name, string scope)
        {
            IDefinedName name3;
            using (List<IDefinedName>.Enumerator enumerator = this.innerList.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IDefinedName current = enumerator.Current;
                        if ((current.Name != name) || ((current.Scope != scope) && (!string.IsNullOrEmpty(current.Scope) || !string.IsNullOrEmpty(scope))))
                        {
                            continue;
                        }
                        name3 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return name3;
        }

        public IEnumerator<IDefinedName> GetEnumerator() => 
            this.innerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.innerList.GetEnumerator();

        public IDefinedName this[int index] =>
            this.innerList[index];

        public int Count =>
            this.innerList.Count;

        public bool IsSynchronized =>
            ((ICollection) this.innerList).IsSynchronized;

        public object SyncRoot =>
            ((ICollection) this.innerList).SyncRoot;
    }
}

