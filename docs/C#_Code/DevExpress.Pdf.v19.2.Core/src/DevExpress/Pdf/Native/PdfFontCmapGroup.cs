namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapGroup
    {
        private readonly int startCharCode;
        private readonly int endCharCode;
        private readonly int glyphID;

        public PdfFontCmapGroup(int startCharCode, int endCharCode, int glyphID)
        {
            this.startCharCode = startCharCode;
            this.endCharCode = endCharCode;
            this.glyphID = glyphID;
        }

        public static PdfFontCmapGroup[] ReadGroups(PdfBinaryStream stream, int groupsCount)
        {
            PdfFontCmapGroup[] groupArray = new PdfFontCmapGroup[groupsCount];
            for (int i = 0; i < groupsCount; i++)
            {
                int startCharCode = stream.ReadInt();
                int endCharCode = stream.ReadInt();
                int glyphID = stream.ReadInt();
                groupArray[i] = new PdfFontCmapGroup(startCharCode, endCharCode, glyphID);
            }
            return groupArray;
        }

        public static void WriteGroups(PdfFontCmapGroup[] groups, PdfBinaryStream tableStream)
        {
            foreach (PdfFontCmapGroup group in groups)
            {
                tableStream.WriteInt(group.startCharCode);
                tableStream.WriteInt(group.endCharCode);
                tableStream.WriteInt(group.glyphID);
            }
        }

        public int StartCharCode =>
            this.startCharCode;

        public int EndCharCode =>
            this.endCharCode;

        public int GlyphID =>
            this.glyphID;
    }
}

