namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfType1FontArrayCharset : PdfType1FontCharset
    {
        internal const byte Id = 0;
        private readonly short[] charset;
        private Dictionary<short, short> cidToGidMapping;

        public PdfType1FontArrayCharset(PdfBinaryStream stream, int size)
        {
            this.charset = stream.ReadShortArray(size);
        }

        public override void Write(PdfBinaryStream stream)
        {
            stream.WriteByte(0);
            stream.WriteShortArray(this.charset);
        }

        public override IDictionary<short, short> SidToGidMapping
        {
            get
            {
                if (this.cidToGidMapping == null)
                {
                    this.cidToGidMapping = new Dictionary<short, short>();
                    short num = 1;
                    foreach (short num3 in this.charset)
                    {
                        short num1 = num;
                        num = (short) (num1 + 1);
                        this.cidToGidMapping[num3] = num1;
                    }
                }
                return this.cidToGidMapping;
            }
        }

        public override int DataLength =>
            (this.charset.Length * 2) + 1;
    }
}

