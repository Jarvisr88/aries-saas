namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    public class ByIntDictionaryPageForValueTypes<T> : ByIntDictionaryPage<T> where T: struct
    {
        protected readonly T?[] Data;

        public ByIntDictionaryPageForValueTypes(int dataSize);
        public override void Add(int index, T value);
        public override bool ContainsKey(int index);
        public override bool TryGetKeyByValue(T value, out int index);
        public override bool TryGetValue(int index, out T value);
    }
}

