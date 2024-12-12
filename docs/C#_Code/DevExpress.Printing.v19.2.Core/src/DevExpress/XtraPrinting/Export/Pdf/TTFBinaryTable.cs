namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTFBinaryTable : TTFTable
    {
        private string name;
        private byte[] data;

        public TTFBinaryTable(TTFFile ttfFile, string name) : base(ttfFile)
        {
            this.name = name;
        }

        protected override void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
            TTFBinaryTable table = pattern as TTFBinaryTable;
            this.name = table.Name;
            this.data = new byte[table.Data.Length];
            table.Data.CopyTo(this.data, 0);
        }

        protected override void ReadTable(TTFStream ttfStream)
        {
            this.data = ttfStream.ReadBytes(base.Entry.Length);
        }

        protected override void WriteTable(TTFStream ttfStream)
        {
            ttfStream.WriteBytes(this.data);
        }

        protected internal override string Tag =>
            this.name;

        public string Name =>
            this.name;

        public byte[] Data =>
            this.data;

        public override int Length =>
            this.data.Length;
    }
}

