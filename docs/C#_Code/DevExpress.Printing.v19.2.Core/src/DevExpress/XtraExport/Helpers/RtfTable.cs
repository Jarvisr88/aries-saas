namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class RtfTable : RtfControl
    {
        private readonly List<RtfTableRow> rows = new List<RtfTableRow>();

        public RtfTable(RtfDocument document)
        {
            this.<Document>k__BackingField = document;
        }

        public override string Compile()
        {
            foreach (RtfTableRow row in this.rows)
            {
                base.WriteChild(row, true);
            }
            return base.Compile();
        }

        public RtfTableRow CreateRow()
        {
            RtfTableRow item = new RtfTableRow(this.Document);
            this.rows.Add(item);
            return item;
        }

        public RtfTableRow this[int rowIndex] =>
            (rowIndex < this.rows.Count) ? this.rows[rowIndex] : null;

        public RtfDocument Document { get; }
    }
}

