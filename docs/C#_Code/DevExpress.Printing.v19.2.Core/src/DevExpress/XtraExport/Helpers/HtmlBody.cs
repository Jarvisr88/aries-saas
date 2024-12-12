namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class HtmlBody : HtmlTag
    {
        private HtmlTable table;

        internal HtmlBody(HtmlDocument document) : base(document)
        {
        }

        protected override string Compile(int level = 0)
        {
            base.WriteOpenTag(false, level);
            this.WriteStartFragment(level);
            this.WriteTable(level);
            this.WriteEndFragment(level);
            base.WriteCloseTag(level);
            return base.Compile(level);
        }

        public HtmlTable CreateTable()
        {
            this.table = new HtmlTable(base.document);
            return this.table;
        }

        protected override void PreCompile()
        {
            base.PreCompile(this.table);
        }

        private void WriteEndFragment(int level)
        {
            this.EndFragment = new HtmlComment("EndFragment");
            base.WriteChild(new HtmlComment("EndFragment"), level);
        }

        private void WriteStartFragment(int level)
        {
            this.StartFragment = new HtmlComment("StartFragment");
            base.WriteChild(this.StartFragment, level);
        }

        private void WriteTable(int level)
        {
            if (this.table != null)
            {
                base.WriteChild(this.table, level);
            }
        }

        protected override string Tag =>
            "body";

        internal HtmlComment EndFragment { get; private set; }

        internal HtmlComment StartFragment { get; private set; }
    }
}

