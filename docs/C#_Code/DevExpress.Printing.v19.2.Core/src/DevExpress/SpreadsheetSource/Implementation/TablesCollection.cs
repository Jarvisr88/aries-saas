namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.SpreadsheetSource;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class TablesCollection : ITablesCollection, IEnumerable<ITable>, IEnumerable, ICollection
    {
        private readonly List<ITable> innerList = new List<ITable>();

        public void Add(ITable item)
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

        public IEnumerator<ITable> GetEnumerator() => 
            this.innerList.GetEnumerator();

        public IList<ITable> GetTables(string sheetName)
        {
            List<ITable> list = new List<ITable>();
            foreach (ITable table in this.innerList)
            {
                if (table.Range.SheetName == sheetName)
                {
                    list.Add(table);
                }
            }
            return list;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.innerList.GetEnumerator();

        public ITable this[string name]
        {
            get
            {
                ITable table2;
                using (List<ITable>.Enumerator enumerator = this.innerList.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            ITable current = enumerator.Current;
                            if (current.Name != name)
                            {
                                continue;
                            }
                            table2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
                return table2;
            }
        }

        public ITable this[int index] =>
            this.innerList[index];

        public int Count =>
            this.innerList.Count;

        public bool IsSynchronized =>
            ((ICollection) this.innerList).IsSynchronized;

        public object SyncRoot =>
            ((ICollection) this.innerList).SyncRoot;
    }
}

