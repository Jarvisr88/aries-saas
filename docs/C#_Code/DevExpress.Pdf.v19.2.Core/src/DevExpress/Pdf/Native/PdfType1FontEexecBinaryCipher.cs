namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;

    public class PdfType1FontEexecBinaryCipher : PdfType1FontEexecCipher
    {
        public PdfType1FontEexecBinaryCipher(byte[] data) : base(data)
        {
        }

        public PdfType1FontEexecBinaryCipher(byte[] data, int startPosition, int dataLength, int lenIV) : base(data, startPosition, dataLength, lenIV)
        {
        }

        public byte[] Encrypt()
        {
            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
            {
                int skipBytesCount = this.SkipBytesCount;
                byte[] data = new byte[skipBytesCount];
                while (true)
                {
                    base.R = this.InitialR;
                    List<byte> list = new List<byte>();
                    generator.GetBytes(data);
                    int index = 0;
                    while (true)
                    {
                        byte[] buffer2;
                        if (index < skipBytesCount)
                        {
                            list.Add(this.Encrypt(data[index]));
                            index++;
                            continue;
                        }
                        if (IsASCIICipher(list.ToArray(), 0))
                        {
                            break;
                        }
                        while (true)
                        {
                            short num3 = base.NextByte();
                            if (num3 < 0)
                            {
                                buffer2 = list.ToArray();
                                break;
                            }
                            list.Add(this.Encrypt((byte) num3));
                        }
                        return buffer2;
                    }
                }
            }
        }

        private byte Encrypt(byte p)
        {
            int r = base.R;
            byte num2 = (byte) (p ^ (r >> 8));
            base.R = ((num2 + r) * 0xce6d) + 0x58bf;
            return num2;
        }

        protected override short NextChar() => 
            base.NextByte();

        protected override int BytesPerResultByte =>
            1;
    }
}

