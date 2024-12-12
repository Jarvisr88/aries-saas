namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontMaxpTableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        public const string EntryTag = "maxp";
        private const int numGlyphsOffset = 4;

        public PdfFontMaxpTableDirectoryEntry(byte[] tableData) : base("maxp", tableData)
        {
        }

        public PdfFontMaxpTableDirectoryEntry(int glyphCount) : base("maxp")
        {
            PdfBinaryStream tableStream = base.TableStream;
            tableStream.WriteInt(0x5000);
            tableStream.WriteShort((short) glyphCount);
        }

        public int NumGlyphs
        {
            get
            {
                PdfBinaryStream tableStream = base.TableStream;
                tableStream.Position = 4L;
                return tableStream.ReadUshort();
            }
            set
            {
                PdfBinaryStream tableStream = base.TableStream;
                tableStream.Position = 4L;
                tableStream.WriteShort((short) value);
            }
        }
    }
}

