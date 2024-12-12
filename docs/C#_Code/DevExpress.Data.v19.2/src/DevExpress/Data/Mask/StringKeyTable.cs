namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal sealed class StringKeyTable
    {
        private readonly Dictionary<object, StringKey> inner;

        public StringKeyTable();
        public void Add(StringKey key);

        public StringKey this[SubstringWithHash index] { get; }
    }
}

