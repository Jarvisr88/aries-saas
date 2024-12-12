namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfType2CharstringBinaryWriter : IDisposable
    {
        private readonly PdfBinaryStream binaryStream = new PdfBinaryStream();

        public void Dispose()
        {
            this.binaryStream.Dispose();
        }

        public void WriteByte(byte b)
        {
            this.binaryStream.WriteByte(b);
        }

        public void WriteDouble(double value)
        {
            int num = (int) value;
            if (Math.Abs((double) (value - num)) < 1E-06)
            {
                this.WriteInt(num);
            }
            else
            {
                this.binaryStream.WriteByte(0xff);
                this.binaryStream.WriteInt((int) (value * 65536.0));
            }
        }

        public void WriteInt(int value)
        {
            if ((value >= -107) && (value <= 0x6b))
            {
                this.binaryStream.WriteByte((byte) (value + 0x8b));
            }
            else if ((value >= 0x6c) && (value <= 0x46b))
            {
                value -= 0x6c;
                byte num = (byte) ((value >> 8) + 0xf7);
                byte num2 = (byte) (value % 0x100);
                this.binaryStream.WriteByte(num);
                this.binaryStream.WriteByte(num2);
            }
            else if ((value >= -1131) && (value <= -108))
            {
                value += 0x6c;
                byte num3 = (byte) ((Math.Abs(value) >> 8) + 0xfb);
                byte num4 = (byte) (Math.Abs(value) % 0x100);
                this.binaryStream.WriteByte(num3);
                this.binaryStream.WriteByte(num4);
            }
            else if ((value >= -32768) && (value <= 0x7fff))
            {
                this.binaryStream.WriteByte(0x1c);
                this.binaryStream.WriteShort((short) value);
            }
            else
            {
                IList<double> list = new List<double>();
                int num5 = 0;
                while ((value < -32768) || (value > 0x7fff))
                {
                    value /= 0x7fff;
                    num5++;
                }
                this.WriteInt(value);
                for (int i = 0; i < num5; i++)
                {
                    this.WriteInt(0x7fff);
                    this.binaryStream.WriteByte(12);
                    this.binaryStream.WriteByte(0x18);
                }
            }
        }

        public void WritePoint(PdfPoint point)
        {
            this.WriteDouble(point.X);
            this.WriteDouble(point.Y);
        }

        public byte[] Data =>
            this.binaryStream.Data;
    }
}

