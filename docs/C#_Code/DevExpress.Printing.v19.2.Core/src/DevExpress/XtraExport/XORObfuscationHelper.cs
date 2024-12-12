namespace DevExpress.XtraExport
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public static class XORObfuscationHelper
    {
        private static readonly byte[] padArray = new byte[] { 0xbb, 0xff, 0xff, 0xba, 0xff, 0xff, 0xb9, 0x80, 0, 190, 15, 0, 0xbf, 15, 0 };

        public static int CalculatePasswordVerifier(string password) => 
            CalculatePasswordVerifier(GetPasswordBytes(password));

        private static ushort CalculatePasswordVerifier(byte[] passwordBytes)
        {
            ushort num = 0;
            int length = passwordBytes.Length;
            if (length > 0)
            {
                int index = length - 1;
                while (true)
                {
                    if (index < 0)
                    {
                        num = (ushort) (((ushort) (((ushort) (((num >> 14) & 1) | ((num << 1) & 0x7fff))) ^ ((ushort) length))) ^ 0xce4b);
                        break;
                    }
                    num = (ushort) (((ushort) (((num >> 14) & 1) | ((num << 1) & 0x7fff))) ^ passwordBytes[index]);
                    index--;
                }
            }
            return num;
        }

        public static byte[] CreateXORArray(string password, int key)
        {
            if (string.IsNullOrEmpty(password))
            {
                password = string.Empty;
            }
            if (password.Length > 15)
            {
                password = password.Substring(0, 15);
            }
            byte[] passwordBytes = GetPasswordBytes(password);
            byte[] buffer2 = new byte[0x10];
            byte two = (byte) (key >> 8);
            byte num2 = (byte) key;
            int length = passwordBytes.Length;
            int index = length;
            if ((index % 2) == 1)
            {
                buffer2[index] = XorRor(padArray[0], two);
                index--;
                buffer2[index] = XorRor(passwordBytes[length - 1], num2);
            }
            while (index > 0)
            {
                index--;
                buffer2[index] = XorRor(passwordBytes[index], two);
                index--;
                buffer2[index] = XorRor(passwordBytes[index], num2);
            }
            index = 15;
            for (int i = 15 - length; i > 0; i--)
            {
                buffer2[index] = XorRor(padArray[i], two);
                index--;
                i--;
                buffer2[index] = XorRor(padArray[i], num2);
                index--;
            }
            return buffer2;
        }

        private static byte[] GetPasswordBytes(string password)
        {
            Encoding encoding = DXEncoding.Default.Clone() as Encoding;
            encoding.EncoderFallback = EncoderFallback.ReplacementFallback;
            encoding.DecoderFallback = DecoderFallback.ReplacementFallback;
            return encoding.GetBytes(password);
        }

        private static byte Ror(byte value) => 
            ((value & 1) == 0) ? ((byte) (value >> 1)) : ((byte) ((value >> 1) | 0x80));

        private static byte XorRor(byte one, byte two) => 
            Ror((byte) (one ^ two));
    }
}

