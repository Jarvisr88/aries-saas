namespace DevExpress.XtraPrinting.Export.Pdf.Compression
{
    using System;
    using System.Collections;
    using System.IO;

    public class BitBuffer
    {
        private ArrayList byteList = new ArrayList();
        private const int ItemLength = 0x1000;
        private int end;
        private int bitCount;
        private uint bits;

        public BitBuffer()
        {
            this.UpdateByteList();
        }

        public void AlignToByte()
        {
            if (this.bitCount > 0)
            {
                this.SetByte((byte) this.bits);
                if (this.bitCount > 8)
                {
                    this.SetByte((byte) (this.bits >> 8));
                }
            }
            this.bits = 0;
            this.bitCount = 0;
        }

        private byte[] GetBytes(int index) => 
            (byte[]) this.byteList[index];

        private void SetByte(byte value)
        {
            int end = this.end;
            this.end = end + 1;
            this.CurrentBytes[end] = value;
            if (this.end >= this.CurrentBytes.Length)
            {
                this.UpdateByteList();
            }
        }

        private void TailToStream(Stream stream)
        {
            if (this.bitCount != 0)
            {
                stream.WriteByte((byte) this.bits);
                if (this.bitCount > 8)
                {
                    stream.WriteByte((byte) (this.bits >> 8));
                }
            }
        }

        public void ToStream(Stream stream)
        {
            if (this.Count != 0)
            {
                for (int i = 0; i < (this.Count - 1); i++)
                {
                    byte[] bytes = this.GetBytes(i);
                    stream.Write(bytes, 0, bytes.Length);
                }
                if (this.end > 0)
                {
                    stream.Write(this.CurrentBytes, 0, this.end);
                }
                this.TailToStream(stream);
            }
        }

        private void UpdateByteList()
        {
            this.byteList.Add(new byte[0x1000]);
            this.end = 0;
        }

        public void WriteBits(int b, int count)
        {
            this.bits |= (uint) (b << (this.bitCount & 0x1f));
            this.bitCount += count;
            if (this.bitCount >= 0x10)
            {
                this.SetByte((byte) this.bits);
                this.SetByte((byte) (this.bits >> 8));
                this.bits = this.bits >> 0x10;
                this.bitCount -= 0x10;
            }
        }

        public void WriteShortMSB(int s)
        {
            this.SetByte((byte) (s >> 8));
            this.SetByte((byte) s);
        }

        private byte[] CurrentBytes =>
            (byte[]) this.byteList[this.Count - 1];

        private int Count =>
            this.byteList.Count;
    }
}

