namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Reflection;

    internal class TTFHMtx : TTFTable
    {
        private TTFHMtxEntry[] entries;

        public TTFHMtx(TTFFile ttfFile) : base(ttfFile)
        {
        }

        protected override void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
            TTFHMtx mtx = pattern as TTFHMtx;
            this.entries = new TTFHMtxEntry[mtx.entries.Length];
            mtx.entries.CopyTo(this.entries, 0);
        }

        protected override void ReadTable(TTFStream ttfStream)
        {
            int num = Math.Max(base.Owner.MaxP.NumGlyphs, base.Owner.HHea.NumberOfHMetrics);
            this.entries = new TTFHMtxEntry[num];
            for (int i = 0; i < base.Owner.HHea.NumberOfHMetrics; i++)
            {
                this.entries[i].AdvanceWidth = ttfStream.ReadUFWord();
                this.entries[i].LeftSideBearing = ttfStream.ReadFWord();
            }
            ushort advanceWidth = this.entries[base.Owner.HHea.NumberOfHMetrics - 1].AdvanceWidth;
            for (int j = base.Owner.HHea.NumberOfHMetrics; j < num; j++)
            {
                this.entries[j].AdvanceWidth = advanceWidth;
                this.entries[j].LeftSideBearing = ttfStream.ReadFWord();
            }
        }

        protected override void WriteTable(TTFStream ttfStream)
        {
            int num = Math.Max(base.Owner.MaxP.NumGlyphs, base.Owner.HHea.NumberOfHMetrics);
            for (int i = 0; i < base.Owner.HHea.NumberOfHMetrics; i++)
            {
                ttfStream.WriteUFWord(this.entries[i].AdvanceWidth);
                ttfStream.WriteFWord(this.entries[i].LeftSideBearing);
            }
            for (int j = base.Owner.HHea.NumberOfHMetrics; j < num; j++)
            {
                ttfStream.WriteFWord(this.entries[j].LeftSideBearing);
            }
        }

        protected internal override string Tag =>
            "hmtx";

        public int Count =>
            this.entries.Length;

        public TTFHMtxEntry this[ushort index] =>
            this.entries[index];

        public override int Length =>
            TTFHMtxEntry.SizeOf * this.Count;
    }
}

