namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FontCharacterSet
    {
        public const int panoseLength = 10;
        private const int unicodeCharacterCount = 0x10000;
        private System.Collections.BitArray bitArray;
        private byte[] panose;

        public FontCharacterSet(List<FontCharacterRange> ranges, byte[] panose)
        {
            if (panose.Length != 10)
            {
                Exceptions.ThrowArgumentException("panose", panose);
            }
            this.panose = panose;
            this.bitArray = new System.Collections.BitArray(0x10000);
            int count = ranges.Count;
            for (int i = 0; i < count; i++)
            {
                FontCharacterRange fontCharacterRange = ranges[i];
                if (fontCharacterRange.Hi < 0x10000)
                {
                    this.AddRange(fontCharacterRange);
                }
            }
        }

        private void AddRange(FontCharacterRange fontCharacterRange)
        {
            for (int i = fontCharacterRange.Low; i <= fontCharacterRange.Hi; i++)
            {
                this.bitArray[i] = true;
            }
        }

        public static int CalculatePanoseDistance(FontCharacterSet font1, FontCharacterSet font2)
        {
            byte[] panose = font1.panose;
            byte[] buffer2 = font2.panose;
            int length = panose.Length;
            int num2 = 0;
            for (int i = 0; i < length; i++)
            {
                int num4 = panose[i] - buffer2[i];
                num2 += num4 * num4;
            }
            return num2;
        }

        public bool ContainsChar(char ch) => 
            this.bitArray[ch];

        internal System.Collections.BitArray BitArray =>
            this.bitArray;
    }
}

