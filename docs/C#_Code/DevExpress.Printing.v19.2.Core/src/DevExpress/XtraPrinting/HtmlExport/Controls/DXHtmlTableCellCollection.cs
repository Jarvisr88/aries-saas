namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using System;
    using System.Collections;
    using System.Reflection;

    public sealed class DXHtmlTableCellCollection : ICollection, IEnumerable
    {
        private DXHtmlTableRow owner;

        internal DXHtmlTableCellCollection(DXHtmlTableRow owner)
        {
            this.owner = owner;
        }

        public void Add(DXHtmlTableCell cell)
        {
            this.Insert(-1, cell);
        }

        public void Clear()
        {
            if (this.owner.HasControls())
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

        public void Insert(int index, DXHtmlTableCell cell)
        {
            this.owner.Controls.AddAt(index, cell);
        }

        public void Remove(DXHtmlTableCell cell)
        {
            this.owner.Controls.Remove(cell);
        }

        public void RemoveAt(int index)
        {
            this.owner.Controls.RemoveAt(index);
        }

        public int Count =>
            !this.owner.HasControls() ? 0 : this.owner.Controls.Count;

        public DXHtmlTableCell this[int index] =>
            (DXHtmlTableCell) this.owner.Controls[index];

        public object SyncRoot =>
            this;

        public bool IsSynchronized =>
            false;
    }
}

