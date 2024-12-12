namespace DevExpress.Office.Utils
{
    using DevExpress.Office.Crypto;
    using DevExpress.Utils;
    using DevExpress.Utils.Crypt;
    using System;
    using System.IO;

    public class XlsRC4EncryptedReader : XlReader
    {
        private const int blockSize = 0x400;
        private ARC4KeyGen keygen;
        private ARC4Cipher cipher;
        private int blockCount;
        private int bytesCount;

        public XlsRC4EncryptedReader(BinaryReader reader, ARC4KeyGen keygen) : base(reader)
        {
            Guard.ArgumentNotNull(keygen, "keygen");
            this.keygen = keygen;
            this.cipher = new ARC4Cipher(keygen.DeriveKey(0));
            this.ResetCipher(this.Position);
        }

        public XlsRC4EncryptedReader(BinaryReader reader, string password, byte[] salt) : base(reader)
        {
            this.keygen = new ARC4KeyGen(password, salt);
            this.cipher = new ARC4Cipher(this.keygen.DeriveKey(0));
            this.ResetCipher(this.Position);
        }

        private byte Decrypt(byte input)
        {
            byte num = this.cipher.Decrypt(input);
            this.bytesCount++;
            if (this.bytesCount == 0x400)
            {
                this.bytesCount = 0;
                this.blockCount++;
                this.cipher.UpdateKey(this.keygen.DeriveKey(this.blockCount));
            }
            return num;
        }

        private byte[] Decrypt(byte[] input)
        {
            int length = input.Length;
            for (int i = 0; i < length; i++)
            {
                input[i] = this.cipher.Decrypt(input[i]);
                this.bytesCount++;
                if (this.bytesCount == 0x400)
                {
                    this.bytesCount = 0;
                    this.blockCount++;
                    this.cipher.UpdateKey(this.keygen.DeriveKey(this.blockCount));
                }
            }
            return input;
        }

        protected override void Dispose(bool disposing)
        {
            this.keygen = null;
            this.cipher = null;
            base.Dispose(disposing);
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

        public override byte[] ReadNotCryptedBytes(int count)
        {
            byte[] buffer = base.BaseReader.ReadBytes(count);
            this.SeekKeyStream(buffer.Length);
            return buffer;
        }

        public override short ReadNotCryptedInt16()
        {
            this.SeekKeyStream(2);
            return base.BaseReader.ReadInt16();
        }

        public override int ReadNotCryptedInt32()
        {
            this.SeekKeyStream(4);
            return base.BaseReader.ReadInt32();
        }

        [CLSCompliant(false)]
        public override ushort ReadNotCryptedUInt16()
        {
            this.SeekKeyStream(2);
            return base.BaseReader.ReadUInt16();
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

        private void ResetCipher(long position)
        {
            this.blockCount = (int) (position / 0x400L);
            this.bytesCount = (int) (position % 0x400L);
            this.cipher.UpdateKey(this.keygen.DeriveKey(this.blockCount));
            this.cipher.Reset(this.bytesCount);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long position = base.BaseReader.BaseStream.Seek(offset, origin);
            if ((origin == SeekOrigin.Current) && ((offset > 0L) && (offset < (0x400 - this.bytesCount))))
            {
                this.SeekKeyStream((int) offset);
            }
            else
            {
                this.ResetCipher(position);
            }
            return position;
        }

        private void SeekKeyStream(int offset)
        {
            for (int i = 0; i < offset; i++)
            {
                this.Decrypt((byte) 0);
            }
        }

        public override long Position
        {
            get => 
                base.BaseReader.BaseStream.Position;
            set => 
                this.Seek(value, SeekOrigin.Begin);
        }
    }
}

