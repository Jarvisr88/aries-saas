namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;

    public static class DictionaryUtils
    {
        public static Dictionary<string, T> CreateBackTranslationTable<T>(Dictionary<T, string> table)
        {
            Dictionary<string, T> dictionary = new Dictionary<string, T>();
            foreach (T local in table.Keys)
            {
                dictionary.Add(table[local], local);
            }
            return dictionary;
        }

        public static Dictionary<string, T> CreateBackTranslationTable<T>(Dictionary<T, string> table, IEqualityComparer<string> comparer)
        {
            Dictionary<string, T> dictionary = new Dictionary<string, T>(comparer);
            foreach (T local in table.Keys)
            {
                dictionary.Add(table[local], local);
            }
            return dictionary;
        }
    }
}

