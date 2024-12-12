namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections;

    internal class StirlitzHashTable : Hashtable
    {
        public override void Add(object key, object value)
        {
        }

        public override void Clear()
        {
        }

        public override bool Contains(object key) => 
            false;

        public override bool ContainsKey(object key) => 
            false;

        public override bool ContainsValue(object value) => 
            false;

        public override void Remove(object key)
        {
        }

        public override int Count =>
            0;
    }
}

