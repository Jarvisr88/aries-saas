namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapUnicodeVariationSelectorRecord
    {
        private readonly int varSelector;
        private readonly PdfFontCmapUnicodeVariationSelectorDefaultTable[] defaultTables;
        private readonly PdfFontCmapUnicodeVariationSelectorNonDefaultTable[] nonDefaultTables;

        public PdfFontCmapUnicodeVariationSelectorRecord(int varSelector, PdfFontCmapUnicodeVariationSelectorDefaultTable[] defaultTables, PdfFontCmapUnicodeVariationSelectorNonDefaultTable[] nonDefaultTables)
        {
            this.varSelector = varSelector;
            this.defaultTables = defaultTables;
            this.nonDefaultTables = nonDefaultTables;
        }

        public int Write(PdfBinaryStream tableStream, int offset)
        {
            tableStream.Write24BitInt(this.varSelector);
            if (this.defaultTables == null)
            {
                tableStream.WriteInt(0);
            }
            else
            {
                tableStream.WriteInt(offset);
                offset += (4 * this.defaultTables.Length) + 4;
            }
            if (this.nonDefaultTables == null)
            {
                tableStream.WriteInt(0);
            }
            else
            {
                tableStream.WriteInt(offset);
                offset += (5 * this.nonDefaultTables.Length) + 4;
            }
            return offset;
        }

        public int VarSelector =>
            this.varSelector;

        public PdfFontCmapUnicodeVariationSelectorDefaultTable[] DefaultTables =>
            this.defaultTables;

        public PdfFontCmapUnicodeVariationSelectorNonDefaultTable[] NonDefaultTables =>
            this.nonDefaultTables;
    }
}

