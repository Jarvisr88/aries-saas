namespace DevExpress.Xpf.Grid.EditForm
{
    using System;
    using System.Collections.Generic;

    internal class LayoutMatrix : List<Row>
    {
        private readonly int columnCount;

        public LayoutMatrix(int columnCount)
        {
            this.columnCount = columnCount;
        }

        public Row AddNewRow()
        {
            Row item = new Row(this.columnCount, base.Count);
            base.Add(item);
            return item;
        }

        public Row GetNextRow(int currentIndex) => 
            (currentIndex != (base.Count - 1)) ? base[currentIndex + 1] : this.AddNewRow();
    }
}

