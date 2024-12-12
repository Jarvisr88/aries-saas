namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;
    using System.Text;

    public class PdfBinaryStream : PdfDisposableObject
    {
        private readonly MemoryStream stream;

        public PdfBinaryStream()
        {
            this.stream = new MemoryStream();
        }

        public PdfBinaryStream(byte[] data)
        {
            this.stream = new MemoryStream(data);
        }

        public PdfBinaryStream(int length)
        {
            this.stream = new MemoryStream(length);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.stream.Dispose();
            }
        }

        public int Get24BitInt() => 
            ((this.stream.ReadByte() << 0x10) + (this.stream.ReadByte() << 8)) + this.stream.ReadByte();

        public byte[] ReadArray(int length)
        {
            byte[] buffer = new byte[length];
            this.stream.Read(buffer, 0, length);
            return buffer;
        }

        public byte ReadByte() => 
            (byte) this.stream.ReadByte();

        public float ReadFixed() => 
            ((float) this.ReadInt()) / 65536f;

        public int ReadInt() => 
            (((this.stream.ReadByte() << 0x18) + (this.stream.ReadByte() << 0x10)) + (this.stream.ReadByte() << 8)) + this.stream.ReadByte();

        public long ReadLong() => 
            (((((((this.stream.ReadByte() << 0x18) + (this.stream.ReadByte() << 0x10)) + (this.stream.ReadByte() << 8)) + this.stream.ReadByte()) + (this.stream.ReadByte() << 0x18)) + (this.stream.ReadByte() << 0x10)) + (this.stream.ReadByte() << 8)) + this.stream.ReadByte();

        public int ReadOffSet(int length)
        {
            switch (length)
            {
                case 2:
                    return this.ReadUshort();

                case 3:
                    return (((this.ReadByte() << 0x10) + (this.ReadByte() << 8)) + this.ReadByte());

                case 4:
                    return this.ReadInt();
            }
            return this.ReadByte();
        }

        public short ReadShort() => 
            (short) ((this.stream.ReadByte() << 8) + this.stream.ReadByte());

        public short[] ReadShortArray(int length)
        {
            short[] numArray = new short[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this.ReadShort();
            }
            return numArray;
        }

        public string ReadString(int length)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append((char) this.stream.ReadByte());
            }
            return builder.ToString();
        }

        public int ReadUshort() => 
            (this.stream.ReadByte() << 8) + this.stream.ReadByte();

        public byte[] ToAlignedArray()
        {
            this.stream.Position = 0L;
            int length = (int) this.stream.Length;
            int num2 = length % 4;
            byte[] buffer = new byte[length + ((num2 > 0) ? (4 - num2) : 0)];
            this.stream.Read(buffer, 0, length);
            return buffer;
        }

        public void Write24BitInt(int value)
        {
            this.stream.WriteByte((byte) ((value & 0xff0000) >> 0x10));
            this.stream.WriteByte((byte) ((value & 0xff00) >> 8));
            this.stream.WriteByte((byte) (value & 0xff));
        }

        public void WriteArray(byte[] array)
        {
            this.stream.Write(array, 0, array.Length);
        }

        public void WriteByte(byte value)
        {
            this.stream.WriteByte(value);
        }

        public void WriteFixed(float value)
        {
            this.WriteInt((int) (value * 65536f));
        }

        public void WriteInt(int value)
        {
            this.stream.WriteByte((byte) ((value & 0xff000000UL) >> 0x18));
            this.stream.WriteByte((byte) ((value & 0xff0000) >> 0x10));
            this.stream.WriteByte((byte) ((value & 0xff00) >> 8));
            this.stream.WriteByte((byte) (value & 0xff));
        }

        public void WriteLong(long value)
        {
            this.stream.WriteByte((byte) ((value & -72057594037927936L) >> 0x38));
            this.stream.WriteByte((byte) ((value & 0xff000000000000L) >> 0x30));
            this.stream.WriteByte((byte) ((value & 0xff0000000000L) >> 40));
            this.stream.WriteByte((byte) ((value & 0xff00000000L) >> 0x20));
            this.stream.WriteByte((byte) ((value & 0xff000000UL) >> 0x18));
            this.stream.WriteByte((byte) ((value & 0xff0000L) >> 0x10));
            this.stream.WriteByte((byte) ((value & 0xff00L) >> 8));
            this.stream.WriteByte((byte) (value & 0xffL));
        }

        public void WriteShort(short value)
        {
            this.stream.WriteByte((byte) ((value & 0xff00) >> 8));
            this.stream.WriteByte((byte) (value & 0xff));
        }

        public void WriteShortArray(short[] array)
        {
            foreach (short num2 in array)
            {
                this.WriteShort(num2);
            }
        }

        public void WriteString(string str)
        {
            foreach (char ch in str)
            {
                this.stream.WriteByte((byte) ch);
            }
        }

        public void WriteUShort(int value)
        {
            this.WriteShort((short) value);
        }

        public byte[] Data =>
            this.stream.ToArray();

        public long Length =>
            this.stream.Length;

        public long Position
        {
            get => 
                this.stream.Position;
            set => 
                this.stream.Position = value;
        }
    }
}

