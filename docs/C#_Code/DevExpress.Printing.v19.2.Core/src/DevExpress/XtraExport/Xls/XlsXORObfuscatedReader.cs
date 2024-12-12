namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport;
    using System;
    using System.IO;

    public class XlsXORObfuscatedReader : XlReader
    {
        private readonly byte[] xorArray;
        private int recordSize;

        public XlsXORObfuscatedReader(BinaryReader reader, string password, int key) : base(reader)
        {
            this.xorArray = XORObfuscationHelper.CreateXORArray(password, key);
        }

        private byte Decrypt(byte input)
        {
            int index = (int) (((this.Position + this.recordSize) - 1L) & 15);
            byte num2 = (byte) (input ^ this.xorArray[index]);
            return (byte) ((num2 << 3) | (num2 >> 5));
        }

        private byte[] Decrypt(byte[] input)
        {
            int index = (int) (((this.Position + this.recordSize) - input.Length) & 15);
            int length = input.Length;
            for (int i = 0; i < length; i++)
            {
                byte num4 = (byte) (input[i] ^ this.xorArray[index]);
                input[i] = (byte) ((num4 << 3) | (num4 >> 5));
                index = (index + 1) & 15;
            }
            return input;
        }

        public override bool ReadBoolean()
        {
            byte input = base.BaseReader.ReadByte();
            return (this.Decrypt(input) != 0);
        }

        public override byte ReadByte()
        {
            byte input = base.BaseReader.ReadByte();
            return this.Decrypt(input);
        }

        public override byte[] ReadBytes(int count)
        {
            byte[] input = base.BaseReader.ReadBytes(count);
            return this.Decrypt(input);
        }

        public override double ReadDouble()
        {
            byte[] input = base.BaseReader.ReadBytes(8);
            return BitConverter.ToDouble(this.Decrypt(input), 0);
        }

        public override short ReadInt16()
        {
            byte[] input = base.BaseReader.ReadBytes(2);
            return BitConverter.ToInt16(this.Decrypt(input), 0);
        }

        public override int ReadInt32()
        {
            byte[] input = base.BaseReader.ReadBytes(4);
            return BitConverter.ToInt32(this.Decrypt(input), 0);
        }

        public override long ReadInt64()
        {
            byte[] input = base.BaseReader.ReadBytes(8);
            return BitConverter.ToInt64(this.Decrypt(input), 0);
        }

        [CLSCompliant(false)]
        public override ushort ReadUInt16()
        {
            byte[] input = base.BaseReader.ReadBytes(2);
            return BitConverter.ToUInt16(this.Decrypt(input), 0);
        }

        [CLSCompliant(false)]
        public override uint ReadUInt32()
        {
            byte[] input = base.BaseReader.ReadBytes(4);
            return BitConverter.ToUInt32(this.Decrypt(input), 0);
        }

        [CLSCompliant(false)]
        public override ulong ReadUInt64()
        {
            byte[] input = base.BaseReader.ReadBytes(8);
            return BitConverter.ToUInt64(this.Decrypt(input), 0);
        }

        public override void SetRecordSize(int size)
        {
            this.recordSize = size;
        }
    }
}

