namespace DevExpress.Office
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class TranslationTable<T> where T: struct
    {
        private Collection<TranslationTableEntry<T>> innerCollection;

        public TranslationTable()
        {
            this.innerCollection = new Collection<TranslationTableEntry<T>>();
        }

        public void Add(T key, string value)
        {
            TranslationTableEntry<T> item = new TranslationTableEntry<T>(key, value);
            this.innerCollection.Add(item);
        }

        public T GetEnumValue(string str, T defaultValue) => 
            this.GetEnumValue(str, defaultValue, false);

        public T GetEnumValue(string str, T defaultValue, bool caseSensitive)
        {
            T key;
            if (string.IsNullOrEmpty(str))
            {
                return defaultValue;
            }
            if (!caseSensitive)
            {
                str = str.ToLowerInvariant();
            }
            using (IEnumerator<TranslationTableEntry<T>> enumerator = this.innerCollection.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        TranslationTableEntry<T> current = enumerator.Current;
                        if (str != current.Value)
                        {
                            continue;
                        }
                        key = current.Key;
                    }
                    else
                    {
                        return defaultValue;
                    }
                    break;
                }
            }
            return key;
        }

        public string GetStringValue(T key, T defaultKey)
        {
            string str2;
            string str = string.Empty;
            using (IEnumerator<TranslationTableEntry<T>> enumerator = this.innerCollection.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        TranslationTableEntry<T> current = enumerator.Current;
                        if (!key.Equals(current.Key))
                        {
                            if (!string.IsNullOrEmpty(str) || !defaultKey.Equals(current.Key))
                            {
                                continue;
                            }
                            str = current.Value;
                            continue;
                        }
                        str2 = current.Value;
                    }
                    else
                    {
                        return str;
                    }
                    break;
                }
            }
            return str2;
        }

        public Collection<TranslationTableEntry<T>> InnerCollection =>
            this.innerCollection;
    }
}

