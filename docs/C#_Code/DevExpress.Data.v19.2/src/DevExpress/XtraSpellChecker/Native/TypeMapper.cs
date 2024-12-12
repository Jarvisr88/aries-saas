namespace DevExpress.XtraSpellChecker.Native
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.InteropServices;

    public class TypeMapper
    {
        private readonly ConcurrentDictionary<Type, Type> typeDictionary = new ConcurrentDictionary<Type, Type>();

        protected TypeMapper()
        {
        }

        public bool ContainsKey(Type key) => 
            this.typeDictionary.ContainsKey(key);

        public bool Register(Type key, Type value) => 
            this.typeDictionary.TryAdd(key, value);

        public bool TryGetValue(Type key, out Type value) => 
            this.typeDictionary.TryGetValue(key, out value);

        public Type Unregister(Type key)
        {
            Type type;
            return (!this.typeDictionary.TryRemove(key, out type) ? null : type);
        }
    }
}

