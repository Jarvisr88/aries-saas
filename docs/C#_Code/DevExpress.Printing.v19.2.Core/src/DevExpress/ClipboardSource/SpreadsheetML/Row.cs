namespace DevExpress.ClipboardSource.SpreadsheetML
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Xml.Linq;

    public class Row
    {
        private List<Cell> cells;
        private XElement rowElement;

        public Row(XElement rowElement)
        {
            this.rowElement = rowElement;
            this.GetCells();
        }

        private void GetCells()
        {
            this.cells = new List<Cell>();
            foreach (XElement element in this.rowElement.GetElements("Cell"))
            {
                this.cells.Add(new Cell(element));
            }
        }

        public Cell this[int index] =>
            this.Cells[index];

        public List<Cell> Cells =>
            this.cells;
    }
}

