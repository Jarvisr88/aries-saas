namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfOpenTypeCFFTableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        internal const string EntryTag = "CFF ";
        private readonly byte[] originalTableData;
        private byte[] subsetData;
        private bool shouldWrite;

        public PdfOpenTypeCFFTableDirectoryEntry(byte[] tableData) : base("CFF ", tableData)
        {
            this.originalTableData = tableData;
        }

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            if (this.shouldWrite)
            {
                base.CreateNewStream().WriteArray(this.subsetData);
            }
        }

        public void CreateSubset(ICollection<int> glyphIndices)
        {
            PdfType1FontCompactFontProgram fontProgram = PdfType1FontCompactFontProgram.Parse(this.originalTableData);
            IList<byte[]> charStrings = fontProgram.CharStrings;
            if (charStrings != null)
            {
                for (int i = 0; i < charStrings.Count; i++)
                {
                    if (!glyphIndices.Contains(i))
                    {
                        charStrings[i] = PdfType1FontProgram.EmptyCharstring;
                    }
                }
            }
            this.subsetData = PdfCompactFontFormatTopDictIndexWriter.Write(fontProgram);
            this.shouldWrite = true;
        }
    }
}

