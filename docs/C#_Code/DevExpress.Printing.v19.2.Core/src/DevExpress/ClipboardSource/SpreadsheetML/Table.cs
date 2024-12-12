namespace DevExpress.ClipboardSource.SpreadsheetML
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Xml.Linq;

    public class Table
    {
        private List<Row> rows;
        private XElement tableElement;

        public Table(XElement tableElement)
        {
            this.tableElement = tableElement;
            this.GetRows();
        }

        private void GetRows()
        {
            this.rows = new List<Row>();
            foreach (XElement element in this.tableElement.GetElements("Row"))
            {
                this.rows.Add(new Row(element));
            }
        }

        public Cell this[int row, int column] =>
            this.Rows.Get<List<Row>, Row>(x => x[row], null, "Unknown exception").Get<Row, Cell>(x => x[column], null, "Unknown exception");

        public Row this[int index] =>
            this.Rows.Get<List<Row>, Row>(x => x[index], null, "Unknown exception");

        public List<Row> Rows =>
            this.rows;
    }
}

