namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Crypto;
    using DevExpress.Utils.Crypt;
    using System;
    using System.IO;

    public class XlsRC4Encryptor
    {
        private const int blockSize = 0x400;
        private const short BOF = 0x809;
        private const short FilePass = 0x2f;
        private const short FileLock = 0x195;
        private const short BoundSheet8 = 0x85;
        private const short InterfaceHdr = 0xe1;
        private const short RRDHead = 0x138;
        private const short RRDInfo = 0x196;
        private const short UsrExcl = 0x194;
        private ARC4KeyGen keygen;
        private ARC4Cipher cipher;
        private int blockCount;
        private int bytesCount;
        private Stream stream;
        private short typeCode;
        private short size;
        private byte[] dataBuffer;
        private byte[] shortBuffer;

        public XlsRC4Encryptor(ARC4KeyGen keygen, ARC4Cipher cipher, Stream stream)
        {
            this.keygen = keygen;
            this.cipher = cipher;
            this.stream = stream;
            this.dataBuffer = new byte[0x2020];
            this.shortBuffer = new byte[2];
        }

        private byte Encrypt(byte input)
        {
            byte num = this.cipher.Encrypt(input);
            this.bytesCount++;
            if (this.bytesCount == 0x400)
            {
                this.bytesCount = 0;
                this.blockCount++;
                this.cipher.UpdateKey(this.keygen.DeriveKey(this.blockCount));
            }
            return num;
        }

        private byte[] Encrypt(byte[] input, int count)
        {
            for (int i = 0; i < count; i++)
            {
                input[i] = this.cipher.Encrypt(input[i]);
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

        public void Execute()
        {
            this.Execute(false);
        }

        public void Execute(bool hasFilePass)
        {
            this.stream.Position = 0L;
            long length = this.stream.Length;
            if (hasFilePass)
            {
                this.ResetCipher(this.stream.Position);
            }
            while (this.stream.Position < length)
            {
                this.typeCode = this.ReadInt16();
                this.size = this.ReadInt16();
                long position = this.stream.Position;
                if (this.size > 0)
                {
                    this.stream.Read(this.dataBuffer, 0, this.size);
                }
                if (!hasFilePass)
                {
                    if (this.typeCode == 0x2f)
                    {
                        hasFilePass = true;
                        this.ResetCipher(this.stream.Position);
                    }
                }
                else
                {
                    this.SeekKeyStream(4);
                    if (this.ShouldNotEncrypt(this.typeCode))
                    {
                        this.SeekKeyStream(this.size);
                    }
                    else if (this.typeCode != 0x85)
                    {
                        this.Encrypt(this.dataBuffer, this.size);
                        this.stream.Position = position;
                        this.stream.Write(this.dataBuffer, 0, this.size);
                    }
                    else
                    {
                        byte[] destinationArray = new byte[4];
                        Array.Copy(this.dataBuffer, destinationArray, 4);
                        this.Encrypt(this.dataBuffer, this.size);
                        Array.Copy(destinationArray, this.dataBuffer, 4);
                        this.stream.Position = position;
                        this.stream.Write(this.dataBuffer, 0, this.size);
                    }
                }
            }
        }

        private short ReadInt16()
        {
            this.stream.Read(this.shortBuffer, 0, 2);
            return BitConverter.ToInt16(this.shortBuffer, 0);
        }

        private void ResetCipher(long position)
        {
            this.blockCount = (int) (position / 0x400L);
            this.bytesCount = (int) (position % 0x400L);
            this.cipher.UpdateKey(this.keygen.DeriveKey(this.blockCount));
            this.cipher.Reset(this.bytesCount);
        }

        private void SeekKeyStream(int offset)
        {
            for (int i = 0; i < offset; i++)
            {
                this.Encrypt(0);
            }
        }

        private bool ShouldNotEncrypt(short typeCode) => 
            (typeCode == 0x809) || ((typeCode == 0x195) || ((typeCode == 0xe1) || ((typeCode == 0x138) || ((typeCode == 0x196) || ((typeCode == 0x194) || (typeCode == 0x2f))))));
    }
}

