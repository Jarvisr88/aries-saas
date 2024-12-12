namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    internal class RtfDocument : RtfControl
    {
        public static string DefaultFont = "Calibri";
        public static int TableLeftOffset = 5;
        public static int TableMaxWidth = 0x2481;
        private RtfTable table;

        public override string Compile()
        {
            base.WriteOpenBrace();
            this.WriteRtfHeader();
            base.WriteChild(this.FontTable, true);
            int? nullable = null;
            base.WriteKeyword(Keyword.Plain, nullable, false, true);
            base.WriteChild(this.ColorTable, true);
            if (this.table != null)
            {
                base.WriteChild(this.table, false);
            }
            base.WriteCloseBrace();
            return base.Compile();
        }

        public RtfTable CreateTable()
        {
            this.table = new RtfTable(this);
            return this.table;
        }

        private void WriteRtfHeader()
        {
            base.WriteKeyword(Keyword.RTF, 1, false, false);
            int? nullable = null;
            base.WriteKeyword(Keyword.Ansi, nullable, false, false);
            base.WriteKeyword(Keyword.DefaultFont, new int?(this.FontTable.GetFontIndex(DefaultFont)), false, false);
        }

        public RtfColorTable ColorTable { get; } = new RtfColorTable()

        public RtfFontTable FontTable { get; } = new RtfFontTable()
    }
}

