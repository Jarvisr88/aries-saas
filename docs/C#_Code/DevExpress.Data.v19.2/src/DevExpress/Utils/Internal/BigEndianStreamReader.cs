namespace DevExpress.Utils.Internal
{
    using System;
    using System.IO;
    using System.Text;

    public class BigEndianStreamReader
    {
        private System.IO.Stream stream;

        public BigEndianStreamReader(System.IO.Stream stream)
        {
            this.stream = stream;
        }

        internal byte ReadByte() => 
            (byte) this.stream.ReadByte();

        public float ReadFixed32()
        {
            int num = this.ReadShort();
            int num2 = this.ReadUShort();
            if (num < 0)
            {
                num2 *= -1;
            }
            return (Convert.ToSingle(num) + (Convert.ToSingle(num2) / 65535f));
        }

        public string ReadFixedString(int length)
        {
            byte[] buffer = new byte[length];
            this.stream.Read(buffer, 0, length);
            return Encoding.UTF8.GetString(buffer, 0, length);
        }

        public int ReadInt32()
        {
            int num2 = this.stream.ReadByte();
            int num3 = this.stream.ReadByte();
            int num4 = this.stream.ReadByte();
            return ((((this.stream.ReadByte() << 0x18) | (num2 << 0x10)) | (num3 << 8)) | num4);
        }

        public short ReadShort()
        {
            int num2 = this.stream.ReadByte();
            return (short) ((this.stream.ReadByte() << 8) | num2);
        }

        [CLSCompliant(false)]
        public ushort ReadUInt16() => 
            (ushort) this.ReadShort();

        [CLSCompliant(false)]
        public uint ReadUInt32() => 
            (uint) this.ReadInt32();

        [CLSCompliant(false)]
        public ushort ReadUShort() => 
            (ushort) this.ReadShort();

        public System.IO.Stream Stream =>
            this.stream;
    }
}

