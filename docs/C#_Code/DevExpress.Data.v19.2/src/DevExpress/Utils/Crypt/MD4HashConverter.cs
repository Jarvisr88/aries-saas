namespace DevExpress.Utils.Crypt
{
    using System;

    [CLSCompliant(false)]
    public static class MD4HashConverter
    {
        private static void Pack(byte[] destination, int offset, uint value)
        {
            for (int i = 0; i < 4; i++)
            {
                destination[offset + i] = (byte) ((value >> ((8 * i) & 0x1f)) & 0xff);
            }
        }

        public static byte[] ToArray(uint[] digest)
        {
            byte[] destination = new byte[0x10];
            for (int i = 0; i < 4; i++)
            {
                Pack(destination, i * 4, digest[i]);
            }
            return destination;
        }
    }
}

