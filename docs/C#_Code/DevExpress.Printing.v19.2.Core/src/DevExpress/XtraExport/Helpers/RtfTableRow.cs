namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class RtfTableRow : RtfControl
    {
        internal List<RtfTableRowCell> cells = new List<RtfTableRowCell>();

        public RtfTableRow(RtfDocument document)
        {
            this.<Document>k__BackingField = document;
        }

        public override string Compile()
        {
            int? nullable = null;
            base.WriteKeyword(Keyword.RowStart, nullable, false, false);
            base.WriteKeyword(Keyword.RowGap, 30, false, false);
            base.WriteKeyword(Keyword.RowLeft, -30, false, false);
            foreach (RtfTableRowCell cell in this.cells)
            {
                base.WriteRawCompiled(cell.CompileCell(), false);
            }
            nullable = null;
            base.WriteKeyword(Keyword.ParagraphDefault, nullable, false, false);
            base.WriteSpace();
            nullable = null;
            base.WriteKeyword(Keyword.InTable, nullable, false, false);
            foreach (RtfTableRowCell cell2 in this.cells)
            {
                base.WriteRawCompiled(cell2.CompileCellData(), false);
            }
            base.WriteSpace();
            nullable = null;
            base.WriteKeyword(Keyword.ParagraphDefault, nullable, false, false);
            base.WriteSpace();
            nullable = null;
            base.WriteKeyword(Keyword.InTable, nullable, false, false);
            base.WriteSpace();
            nullable = null;
            base.WriteKeyword(Keyword.RowEnd, nullable, false, false);
            return base.Compile();
        }

        public RtfTableRowCell CreateCell()
        {
            RtfTableRowCell item = new RtfTableRowCell(this.Document, this);
            this.cells.Add(item);
            return item;
        }

        public RtfTableRowCell this[int cellIndex] =>
            (cellIndex < this.cells.Count) ? this.cells[cellIndex] : null;

        public RtfDocument Document { get; }
    }
}

