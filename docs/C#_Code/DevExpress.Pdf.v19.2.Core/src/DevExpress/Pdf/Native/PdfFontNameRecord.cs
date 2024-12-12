namespace DevExpress.Pdf.Native
{
    using System;
    using System.Text;

    public class PdfFontNameRecord
    {
        private readonly PdfFontPlatformID platformID;
        private readonly PdfFontLanguageID languageID;
        private readonly PdfFontNameID nameID;
        private readonly PdfFontEncodingID encodingID;
        private readonly byte[] nameBytes;
        private readonly string name;

        public PdfFontNameRecord(PdfBinaryStream stream, int dataOffset)
        {
            this.platformID = (PdfFontPlatformID) stream.ReadUshort();
            this.encodingID = (PdfFontEncodingID) stream.ReadUshort();
            this.languageID = (PdfFontLanguageID) stream.ReadUshort();
            this.nameID = (PdfFontNameID) stream.ReadUshort();
            int length = stream.ReadUshort();
            int num2 = stream.ReadUshort();
            long num3 = dataOffset + num2;
            if ((num3 + length) <= stream.Length)
            {
                long position = stream.Position;
                stream.Position = num3;
                this.nameBytes = stream.ReadArray(length);
                this.name = (this.platformID == PdfFontPlatformID.Microsoft) ? Encoding.BigEndianUnicode.GetString(this.nameBytes) : Encoding.UTF8.GetString(this.nameBytes);
                stream.Position = position;
            }
        }

        public PdfFontNameRecord(PdfFontPlatformID platformID, PdfFontLanguageID languageID, PdfFontNameID nameID, PdfFontEncodingID encodingID, byte[] nameBytes)
        {
            this.platformID = platformID;
            this.languageID = languageID;
            this.nameID = nameID;
            this.encodingID = encodingID;
            this.nameBytes = nameBytes;
            this.name = (platformID == PdfFontPlatformID.Microsoft) ? Encoding.BigEndianUnicode.GetString(nameBytes) : Encoding.UTF8.GetString(nameBytes);
        }

        public PdfFontPlatformID PlatformID =>
            this.platformID;

        public PdfFontLanguageID LanguageID =>
            this.languageID;

        public PdfFontNameID NameID =>
            this.nameID;

        public PdfFontEncodingID EncodingID =>
            this.encodingID;

        public byte[] NameBytes =>
            this.nameBytes;

        public string Name =>
            this.name;
    }
}

