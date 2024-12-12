namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1FontArrayEncoding : PdfType1FontCustomEncoding
    {
        internal const byte Id = 0;
        private readonly byte[] array;

        public PdfType1FontArrayEncoding(PdfBinaryStream stream)
        {
            this.array = stream.ReadArray(stream.ReadByte());
        }

        protected override void WriteEncodingData(PdfBinaryStream stream)
        {
            stream.WriteByte((byte) this.array.Length);
            stream.WriteArray(this.array);
        }

        protected override byte EncodingID =>
            0;

        protected override short[] CodeToGIDMapping
        {
            get
            {
                short[] mapping = new short[0x100];
                int num = Math.Min(0x100, this.array.Length);
                short index = 0;
                short num3 = 1;
                while (index < num)
                {
                    short gid = num3;
                    num3 = (short) (gid + 1);
                    FillEntry(mapping, this.array[index], gid);
                    index = (short) (index + 1);
                }
                return mapping;
            }
        }

        public override int DataLength =>
            (this.array.Length + 2) + base.SupplementDataLength;
    }
}

