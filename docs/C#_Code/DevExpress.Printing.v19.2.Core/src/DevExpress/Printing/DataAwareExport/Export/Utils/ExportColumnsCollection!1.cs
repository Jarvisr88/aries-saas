namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ExportColumnsCollection<TCol> : IEnumerable<TCol>, IEnumerable where TCol: class, IColumn
    {
        private Dictionary<int, TCol> _indexTable;
        private Dictionary<string, TCol> _columnTable;
        private Dictionary<string, List<object>> _itemsTable;
        private Dictionary<string, List<object>> _valueMembersTable;
        private int columnIndex;

        public ExportColumnsCollection()
        {
            this._indexTable = new Dictionary<int, TCol>();
            this._columnTable = new Dictionary<string, TCol>();
            this._itemsTable = new Dictionary<string, List<object>>();
            this._valueMembersTable = new Dictionary<string, List<object>>();
        }

        public void Add(TCol column, bool addLookupValues = true)
        {
            if (column != null)
            {
                if (!this._indexTable.ContainsKey(this.columnIndex))
                {
                    this._indexTable.Add(this.columnIndex, column);
                }
                if (!string.IsNullOrEmpty(column.FieldName))
                {
                    if (!this._columnTable.ContainsKey(column.FieldName))
                    {
                        this._columnTable.Add(column.FieldName, column);
                    }
                    if (addLookupValues)
                    {
                        IDictionary<object, object> dataValidationItems = column.DataValidationItems;
                        if ((dataValidationItems != null) && ((dataValidationItems.Count > 0) && (!this._itemsTable.ContainsKey(column.FieldName) && !this._valueMembersTable.ContainsKey(column.FieldName))))
                        {
                            this._itemsTable.Add(column.FieldName, dataValidationItems.Values.ToList<object>());
                            this._valueMembersTable.Add(column.FieldName, dataValidationItems.Keys.ToList<object>());
                        }
                    }
                }
                this.columnIndex++;
            }
        }

        public bool ColumnHasLookupItems(string fieldName) => 
            this._itemsTable.ContainsKey(fieldName);

        public List<object> ColumnLookupItemsByFieldName(string fieldName)
        {
            List<object> list;
            return (!this._itemsTable.TryGetValue(fieldName, out list) ? null : list);
        }

        public List<object> ColumnValueMembersByFieldName(string fieldName)
        {
            List<object> list;
            return (!this._valueMembersTable.TryGetValue(fieldName, out list) ? null : list);
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__15))]
        public IEnumerator<TCol> GetEnumerator()
        {
            <GetEnumerator>d__15<TCol> d__1 = new <GetEnumerator>d__15<TCol>(0);
            d__1.<>4__this = (ExportColumnsCollection<TCol>) this;
            return d__1;
        }

        public int IndexOf(TCol col)
        {
            if (col != null)
            {
                for (int i = 0; i < this._indexTable.Count; i++)
                {
                    if (Equals(this[i], col) || (!string.IsNullOrEmpty(col.FieldName) && string.Equals(this[i].FieldName, col.FieldName)))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public int IndexOf(string fieldName)
        {
            for (int i = 0; i < this._indexTable.Count; i++)
            {
                if (string.Equals(this[i].FieldName, fieldName))
                {
                    return i;
                }
            }
            return -1;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public TCol this[int index]
        {
            get
            {
                TCol local;
                if ((index >= 0) && this._indexTable.TryGetValue(index, out local))
                {
                    return local;
                }
                return default(TCol);
            }
        }

        public TCol this[string fieldName]
        {
            get
            {
                TCol local;
                if (!string.IsNullOrEmpty(fieldName) && this._columnTable.TryGetValue(fieldName, out local))
                {
                    return local;
                }
                return default(TCol);
            }
        }

        public int Count =>
            this._indexTable.Count;

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__15 : IEnumerator<TCol>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TCol <>2__current;
            public ExportColumnsCollection<TCol> <>4__this;
            private int <i>5__1;

            [DebuggerHidden]
            public <GetEnumerator>d__15(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<i>5__1 = 0;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    int num2 = this.<i>5__1;
                    this.<i>5__1 = num2 + 1;
                }
                if (this.<i>5__1 >= this.<>4__this._indexTable.Count)
                {
                    return false;
                }
                this.<>2__current = this.<>4__this[this.<i>5__1];
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            TCol IEnumerator<TCol>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

