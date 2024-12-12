namespace DevExpress.Office
{
    using System;

    public class TranslationTableEntry<T> where T: struct
    {
        private T key;
        private string value;

        public TranslationTableEntry(T key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public T Key =>
            this.key;

        public string Value =>
            this.value;
    }
}

