namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfArithmeticState
    {
        private readonly PdfBigEndianStreamReader reader;
        private uint c;
        private uint ct;
        private uint a;
        private uint buffer0;
        private uint buffer1;

        public PdfArithmeticState(PdfBigEndianStreamReader reader)
        {
            this.reader = reader;
            this.buffer0 = reader.ReadByte();
            this.buffer1 = reader.ReadByte();
            this.c = (uint) ((this.buffer0 ^ 0xff) << 0x10);
            this.ReadByte();
            this.c = this.c << 7;
            this.ct -= 7;
            this.a = 0x80000000;
        }

        internal int Decode(byte[] cx, int index)
        {
            PdfArithmeticQe qe = PdfArithmeticQe.Values[cx[index] >> 1];
            int num = cx[index] & 1;
            uint num2 = qe.Qe;
            this.a -= num2;
            bool flag = this.c < this.a;
            bool flag2 = this.a < num2;
            int num3 = flag ? 1 : 0;
            int num4 = flag2 ? 1 : 0;
            if (!flag)
            {
                this.c -= this.a;
                this.a = num2;
            }
            else if ((this.a & 0x80000000) != 0)
            {
                return num;
            }
            cx[index] = (flag ^ flag2) ? ((byte) (qe.MpsXor | num)) : ((byte) (qe.LpsXor | (num ^ qe.Switch)));
            this.Renormd();
            return (num ^ (num4 ^ (~num3 & 1)));
        }

        private void LoadBuffer()
        {
            this.buffer0 = this.buffer1;
            if (!this.reader.Finish)
            {
                this.buffer1 = this.reader.ReadByte();
            }
            else
            {
                this.buffer1 = 0xff;
            }
        }

        private void ReadByte()
        {
            if (this.buffer0 != 0xff)
            {
                this.LoadBuffer();
                this.c = (this.c + 0xff00) - (this.buffer0 << 8);
                this.ct = 8;
            }
            else if (this.buffer1 > 0x8f)
            {
                this.ct = 8;
            }
            else
            {
                this.LoadBuffer();
                this.c = (this.c + 0xfe00) - (this.buffer0 << 9);
                this.ct = 7;
            }
        }

        private void Renormd()
        {
            while (true)
            {
                if (this.ct == 0)
                {
                    this.ReadByte();
                }
                this.a = this.a << 1;
                this.c = this.c << 1;
                this.ct--;
                if ((this.a & 0x80000000) != 0)
                {
                    return;
                }
            }
        }
    }
}

