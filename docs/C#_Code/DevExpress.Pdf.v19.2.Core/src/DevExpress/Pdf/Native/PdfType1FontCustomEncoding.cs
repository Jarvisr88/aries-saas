namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class PdfType1FontCustomEncoding : PdfType1FontEncoding
    {
        private const byte supplementDataFlag = 0x80;
        private const byte idMask = 0x7f;
        private SupplementDataEntry[] supplementData;

        protected PdfType1FontCustomEncoding()
        {
        }

        protected static void FillEntry(short[] mapping, byte code, short gid)
        {
            mapping[code] ??= gid;
        }

        public override short[] GetCodeToGIDMapping(PdfType1FontCharset charset, PdfCompactFontFormatStringIndex stringIndex)
        {
            short[] codeToGIDMapping = this.CodeToGIDMapping;
            if (this.supplementData != null)
            {
                foreach (SupplementDataEntry entry in this.supplementData)
                {
                    FillEntry(codeToGIDMapping, entry.Code, entry.GID);
                }
            }
            return codeToGIDMapping;
        }

        public static PdfType1FontCustomEncoding Parse(PdfBinaryStream stream)
        {
            PdfType1FontCustomEncoding encoding;
            byte num = stream.ReadByte();
            int num2 = num & 0x7f;
            if (num2 == 0)
            {
                encoding = new PdfType1FontArrayEncoding(stream);
            }
            else
            {
                if (num2 != 1)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    return null;
                }
                encoding = new PdfType1FontRangeEncoding(stream);
            }
            if ((num & 0x80) == 0x80)
            {
                encoding.ReadSupplementData(stream);
            }
            return encoding;
        }

        private void ReadSupplementData(PdfBinaryStream stream)
        {
            byte num = stream.ReadByte();
            this.supplementData = new SupplementDataEntry[num];
            for (int i = 0; i < num; i++)
            {
                this.supplementData[i] = new SupplementDataEntry(stream.ReadByte(), stream.ReadShort());
            }
        }

        public void Write(PdfBinaryStream stream)
        {
            bool flag = this.supplementData != null;
            stream.WriteByte((byte) (this.EncodingID | (flag ? 0x80 : 0)));
            this.WriteEncodingData(stream);
            if (flag)
            {
                int length = this.supplementData.Length;
                stream.WriteByte((byte) this.supplementData.Length);
                foreach (SupplementDataEntry entry in this.supplementData)
                {
                    stream.WriteByte(entry.Code);
                    stream.WriteShort(entry.GID);
                }
            }
        }

        protected abstract void WriteEncodingData(PdfBinaryStream stream);

        protected int SupplementDataLength =>
            (this.supplementData == null) ? 0 : ((this.supplementData.Length * 3) + 1);

        protected abstract byte EncodingID { get; }

        protected abstract short[] CodeToGIDMapping { get; }

        [StructLayout(LayoutKind.Sequential)]
        protected struct SupplementDataEntry
        {
            private readonly byte code;
            private readonly short gid;
            public byte Code =>
                this.code;
            public short GID =>
                this.gid;
            public SupplementDataEntry(byte code, short gid)
            {
                this.code = code;
                this.gid = gid;
            }
        }
    }
}

