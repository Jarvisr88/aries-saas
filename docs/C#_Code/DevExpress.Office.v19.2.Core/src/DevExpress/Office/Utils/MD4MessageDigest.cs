namespace DevExpress.Office.Utils
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MD4MessageDigest
    {
        private uint[] digest;
        public const int Size = 0x10;
        internal MD4MessageDigest(uint[] digest)
        {
            this.digest = digest;
        }

        public byte[] ToArray()
        {
            byte[] destination = new byte[0x10];
            for (int i = 0; i < 4; i++)
            {
                this.Pack(destination, i * 4, this.digest[i]);
            }
            return destination;
        }

        private void Pack(byte[] destination, int offset, uint value)
        {
            for (int i = 0; i < 4; i++)
            {
                destination[offset + i] = (byte) ((value >> ((8 * i) & 0x1f)) & 0xff);
            }
        }
    }
}

