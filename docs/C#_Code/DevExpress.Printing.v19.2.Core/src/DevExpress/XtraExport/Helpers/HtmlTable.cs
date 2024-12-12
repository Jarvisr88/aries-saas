namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;

    internal class HtmlTable : HtmlTag
    {
        private List<HtmlTableRow> rows;

        internal HtmlTable(HtmlDocument document) : base(document)
        {
            this.rows = new List<HtmlTableRow>();
        }

        protected override string Compile(int level = 0)
        {
            base.WriteOpenTag(false, level);
            this.WriteRows(level);
            base.WriteCloseTag(level);
            return base.Compile(level);
        }

        public HtmlTableRow CreateRow()
        {
            HtmlTableRow item = new HtmlTableRow(this);
            this.rows.Add(item);
            return item;
        }

        protected override void PreCompile()
        {
            base.WriteStyle("border-collapse", "collapse");
            base.document.Head.Style.AddStyle(base.styleSheet);
            foreach (HtmlTableRow row in this.rows)
            {
                base.PreCompile(row);
            }
        }

        private void WriteRows(int level)
        {
            foreach (HtmlTableRow row in this.rows)
            {
                base.WriteChild(row, level);
            }
        }

        protected override string Tag =>
            "table";

        public ReadOnlyCollection<HtmlTableRow> Rows =>
            this.rows.AsReadOnly();
    }
}

