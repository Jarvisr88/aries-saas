namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class ByIntDictionaryPage<T>
    {
        protected ByIntDictionaryPage();
        public abstract void Add(int index, T value);
        public abstract bool ContainsKey(int index);
        public abstract bool TryGetKeyByValue(T value, out int index);
        public abstract bool TryGetValue(int index, out T value);
    }
}

