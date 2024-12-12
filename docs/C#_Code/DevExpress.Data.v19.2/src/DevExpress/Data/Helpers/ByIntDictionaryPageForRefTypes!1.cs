namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    public class ByIntDictionaryPageForRefTypes<T> : ByIntDictionaryPage<T> where T: class
    {
        protected readonly T[] Data;

        public ByIntDictionaryPageForRefTypes(int dataSize);
        public override void Add(int index, T value);
        public override bool ContainsKey(int index);
        public override bool TryGetKeyByValue(T value, out int index);
        public override bool TryGetValue(int index, out T value);
    }
}

