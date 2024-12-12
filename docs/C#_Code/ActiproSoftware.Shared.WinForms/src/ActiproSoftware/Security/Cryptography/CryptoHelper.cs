namespace ActiproSoftware.Security.Cryptography
{
    using System;
    using System.Text;

    public class CryptoHelper
    {
        private const int #nBb = 0x100;

        public string DecryptString(string source, int key)
        {
            int num2;
            int length = source.Length;
            int[] numArray = new int[length];
            int num3 = 11 + (key % 0xe9);
            int num4 = 7 + (key % 0xef);
            int num5 = 5 + (key % 0xf1);
            int num6 = 3 + (key % 0xfb);
            for (num2 = 0; num2 < length; num2++)
            {
                numArray[num2] = (source[num2] != 'Ā') ? source[num2] : 0;
            }
            for (num2 = 0; num2 < (length - 2); num2++)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 + 2]) ^ ((num6 * numArray[num2 + 1]) % 0x100);
            }
            for (num2 = length - 1; num2 >= 2; num2--)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 - 2]) ^ ((num5 * numArray[num2 - 1]) % 0x100);
            }
            for (num2 = 0; num2 < (length - 1); num2++)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 + 1]) ^ ((num4 * numArray[num2 + 1]) % 0x100);
            }
            for (num2 = length - 1; num2 >= 1; num2--)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 - 1]) ^ ((num3 * numArray[num2 - 1]) % 0x100);
            }
            StringBuilder builder = new StringBuilder();
            for (num2 = 0; num2 < length; num2++)
            {
                builder.Append((char) numArray[num2]);
            }
            return builder.ToString();
        }

        public string EncryptString(string source, int key)
        {
            int num2;
            int length = source.Length;
            int[] numArray = new int[length];
            int num3 = 11 + (key % 0xe9);
            int num4 = 7 + (key % 0xef);
            int num5 = 5 + (key % 0xf1);
            int num6 = 3 + (key % 0xfb);
            for (num2 = 0; num2 < length; num2++)
            {
                numArray[num2] = source[num2];
            }
            for (num2 = 1; num2 < length; num2++)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 - 1]) ^ ((num3 * numArray[num2 - 1]) % 0x100);
            }
            for (num2 = length - 2; num2 >= 0; num2--)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 + 1]) ^ ((num4 * numArray[num2 + 1]) % 0x100);
            }
            for (num2 = 2; num2 < length; num2++)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 - 2]) ^ ((num5 * numArray[num2 - 1]) % 0x100);
            }
            for (num2 = length - 3; num2 >= 0; num2--)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 + 2]) ^ ((num6 * numArray[num2 + 1]) % 0x100);
            }
            StringBuilder builder = new StringBuilder();
            for (num2 = 0; num2 < length; num2++)
            {
                if (numArray[num2] == 0)
                {
                    builder.Append('Ā');
                }
                else
                {
                    builder.Append((char) numArray[num2]);
                }
            }
            return builder.ToString();
        }
    }
}

