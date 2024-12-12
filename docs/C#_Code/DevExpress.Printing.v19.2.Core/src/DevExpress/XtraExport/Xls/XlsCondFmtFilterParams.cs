namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsCondFmtFilterParams
    {
        private const int fixedPartSize = 2;

        public XlsCondFmtFilterParams()
        {
            this.IsEmpty = true;
        }

        public int GetSize()
        {
            int num = 2;
            if (!this.IsEmpty)
            {
                num += 4;
            }
            return num;
        }

        public void Write(BinaryWriter writer)
        {
            int num = this.IsEmpty ? 0 : 4;
            writer.Write((ushort) num);
            if (num > 0)
            {
                writer.Write((byte) 0);
                byte num2 = 0;
                if (this.Top)
                {
                    num2 = (byte) (num2 | 1);
                }
                if (this.Percent)
                {
                    num2 = (byte) (num2 | 2);
                }
                writer.Write(num2);
                writer.Write((ushort) this.Value);
            }
        }

        public bool IsEmpty { get; set; }

        public bool Top { get; set; }

        public bool Percent { get; set; }

        public int Value { get; set; }
    }
}

