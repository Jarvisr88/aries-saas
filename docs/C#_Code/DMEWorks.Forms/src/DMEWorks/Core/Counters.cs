namespace DMEWorks.Core
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class Counters
    {
        private Hashtable _hash = CollectionsUtil.CreateCaseInsensitiveHashtable();

        public void Clear()
        {
            this._hash.Clear();
        }

        public int Get(string name) => 
            this.Get(name, 0);

        public int Get(string name, int @default)
        {
            object obj2 = this._hash[name];
            return (!(obj2 is int) ? @default : ((int) obj2));
        }

        public int Increment(string name) => 
            this.Set(name, this.Get(name, 0) + 1);

        public int Increment(string name, int value) => 
            this.Set(name, this.Get(name, 0) + value);

        public void Remove(string name)
        {
            this._hash.Remove(name);
        }

        public int Set(string name, int value)
        {
            this._hash[name] = value;
            return value;
        }

        public int this[string name]
        {
            get => 
                this.Get(name);
            set => 
                this.Set(name, value);
        }

        public ICollection Names =>
            this._hash.Keys;
    }
}

