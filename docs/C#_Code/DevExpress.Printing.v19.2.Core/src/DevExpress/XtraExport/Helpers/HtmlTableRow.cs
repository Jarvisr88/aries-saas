namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class HtmlTableRow : HtmlTag
    {
        private List<HtmlTableCell> cells;

        internal HtmlTableRow(HtmlTable table) : base(table.document)
        {
            this.cells = new List<HtmlTableCell>();
            this.<Table>k__BackingField = table;
        }

        protected override string Compile(int level = 0)
        {
            base.WriteOpenTag(false, level);
            this.WriteCells(level);
            base.WriteCloseTag(level);
            return base.Compile(level);
        }

        public HtmlTableCell CreateCell()
        {
            HtmlTableCell item = new HtmlTableCell(this);
            this.cells.Add(item);
            return item;
        }

        protected override void PreCompile()
        {
            foreach (HtmlTableCell cell in this.cells)
            {
                base.PreCompile(cell);
            }
        }

        private void WriteCells(int level)
        {
            foreach (HtmlTableCell cell in this.cells)
            {
                base.WriteChild(cell, level);
            }
        }

        protected override string Tag =>
            "tr";

        public ReadOnlyCollection<HtmlTableCell> Cells =>
            this.cells.AsReadOnly();

        public HtmlTable Table { get; }
    }
}

