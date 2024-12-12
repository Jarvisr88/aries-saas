namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using System;
    using System.Collections;
    using System.Reflection;

    public sealed class DXHtmlTableRowCollection : ICollection, IEnumerable
    {
        private DXHtmlTable owner;

        internal DXHtmlTableRowCollection(DXHtmlTable owner)
        {
            this.owner = owner;
        }

        public void Add(DXHtmlTableRow row)
        {
            this.Insert(-1, row);
        }

        public void Clear()
        {
            if (this.owner.HasChildren)
            {
                this.owner.Controls.Clear();
            }
        }

        public void CopyTo(Array array, int index)
        {
            IEnumerator enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                array.SetValue(enumerator.Current, index++);
            }
        }

        public IEnumerator GetEnumerator() => 
            this.owner.Controls.GetEnumerator();

        public void Insert(int index, DXHtmlTableRow row)
        {
            this.owner.Controls.AddAt(index, row);
        }

        public void Remove(DXHtmlTableRow row)
        {
            this.owner.Controls.Remove(row);
        }

        public void RemoveAt(int index)
        {
            this.owner.Controls.RemoveAt(index);
        }

        public int Count =>
            !this.owner.HasControls() ? 0 : this.owner.Controls.Count;

        public DXHtmlTableRow this[int index] =>
            (DXHtmlTableRow) this.owner.Controls[index];

        public object SyncRoot =>
            this;

        public bool IsSynchronized =>
            false;
    }
}

