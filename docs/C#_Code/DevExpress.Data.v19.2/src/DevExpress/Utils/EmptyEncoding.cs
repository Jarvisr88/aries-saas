namespace DevExpress.Utils
{
    using System;
    using System.Text;

    public class EmptyEncoding : Encoding
    {
        private static readonly EmptyEncoding instance = new EmptyEncoding();

        public override int GetByteCount(char[] chars, int index, int count) => 
            count;

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            int num = charIndex + charCount;
            while (charIndex < num)
            {
                char ch = chars[charIndex++];
                if (ch >= 'Ā')
                {
                    ch = '?';
                }
                bytes[byteIndex++] = (byte) ch;
            }
            return charCount;
        }

        public override int GetCharCount(byte[] bytes, int index, int count) => 
            count;

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            int num = byteIndex + byteCount;
            while (byteIndex < num)
            {
                chars[charIndex++] = (char) bytes[byteIndex++];
            }
            return byteCount;
        }

        public override int GetMaxByteCount(int charCount) => 
            charCount;

        public override int GetMaxCharCount(int byteCount) => 
            byteCount;

        public static EmptyEncoding Instance =>
            instance;

        public override bool IsSingleByte =>
            true;
    }
}

