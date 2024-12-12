namespace DevExpress.Utils.Internal
{
    using System;
    using System.IO;

    public class BigEndianStreamWriter
    {
        private System.IO.Stream stream;
        private byte[] buffer;

        public BigEndianStreamWriter(System.IO.Stream stream)
        {
            this.stream = stream;
            this.buffer = new byte[4];
        }

        internal void WriteByte(byte value)
        {
            this.Stream.WriteByte(value);
        }

        public void WriteFixed32(float value)
        {
            short num = (short) value;
            float num2 = Math.Abs((float) (value - num));
            this.WriteShort(num);
            this.WriteUShort((ushort) (num2 * 65535f));
        }

        public void WriteInt32(int value)
        {
            this.buffer[0] = (byte) ((value >> 0x18) & 0xff);
            this.buffer[1] = (byte) ((value >> 0x10) & 0xff);
            this.buffer[2] = (byte) ((value >> 8) & 0xff);
            this.buffer[3] = (byte) (value & 0xff);
            this.Stream.Write(this.buffer, 0, 4);
        }

        public void WriteShort(short value)
        {
            this.buffer[0] = (byte) (value >> 8);
            this.buffer[1] = (byte) value;
            this.Stream.Write(this.buffer, 0, 2);
        }

        [CLSCompliant(false)]
        public void WriteUInt32(uint value)
        {
            this.WriteInt32((int) value);
        }

        [CLSCompliant(false)]
        public void WriteUShort(ushort value)
        {
            this.WriteShort((short) value);
        }

        public System.IO.Stream Stream =>
            this.stream;
    }
}

