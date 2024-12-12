namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;

    public static class CellReferenceParserProvider
    {
        public static Dictionary<char, int> Letters = CreateLetters();
        public static Dictionary<char, int> Digits = CreateDigits();

        private static Dictionary<char, int> CreateDigits()
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>(11);
            for (int i = 0; i < 10; i++)
            {
                dictionary.Add((char) ((ushort) (0x30 + i)), i);
            }
            dictionary.Add('$', -1);
            return dictionary;
        }

        private static Dictionary<char, int> CreateLetters()
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>(0x35);
            for (int i = 0; i < 0x1a; i++)
            {
                dictionary.Add((char) ((ushort) (0x41 + i)), i + 1);
                dictionary.Add((char) ((ushort) (0x61 + i)), i + 1);
            }
            dictionary.Add('$', -1);
            return dictionary;
        }
    }
}

