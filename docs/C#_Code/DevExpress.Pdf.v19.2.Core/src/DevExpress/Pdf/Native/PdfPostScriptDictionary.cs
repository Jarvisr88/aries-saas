namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class PdfPostScriptDictionary : IEnumerable<PdfPostScriptDictionaryEntry>, IEnumerable
    {
        private readonly List<PdfPostScriptDictionaryEntry> list;

        public PdfPostScriptDictionary(int capacity)
        {
            this.list = new List<PdfPostScriptDictionaryEntry>(capacity);
        }

        public void Add(string key, object value)
        {
            this.list.Add(new PdfPostScriptDictionaryEntry(key, value));
        }

        public bool ContainsKey(string key)
        {
            bool flag;
            using (List<PdfPostScriptDictionaryEntry>.Enumerator enumerator = this.list.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfPostScriptDictionaryEntry current = enumerator.Current;
                        if (current.Key != key)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public IEnumerator<PdfPostScriptDictionaryEntry> GetEnumerator() => 
            this.list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.list.GetEnumerator();

        public object this[string key]
        {
            get
            {
                for (int i = this.list.Count - 1; i >= 0; i--)
                {
                    PdfPostScriptDictionaryEntry entry = this.list[i];
                    if (entry.Key == key)
                    {
                        return entry.Value;
                    }
                }
                return null;
            }
        }

        public int Count =>
            this.list.Count;
    }
}

