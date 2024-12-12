namespace DevExpress.Pdf.Native
{
    using System;
    using System.Text;

    public class PdfNameTreeEncoding : Encoding
    {
        public override int GetByteCount(char[] chars, int index, int count) => 
            count;

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            for (int i = 0; i < charCount; i++)
            {
                bytes[byteIndex++] = (byte) chars[charIndex++];
            }
            return charCount;
        }

        public override int GetCharCount(byte[] bytes, int index, int count) => 
            count;

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            for (int i = 0; i < byteCount; i++)
            {
                chars[charIndex++] = (char) bytes[byteIndex++];
            }
            return byteCount;
        }

        public override int GetMaxByteCount(int charCount) => 
            charCount;

        public override int GetMaxCharCount(int byteCount) => 
            byteCount;
    }
}

