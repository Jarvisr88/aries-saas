namespace DevExpress.Data.Mask
{
    using System;

    internal sealed class SubstringWithHash
    {
        public readonly string Str;
        public readonly int Length;
        public readonly int Hash;

        public SubstringWithHash(string str, int length, int hash);
        public override bool Equals(object obj);
        public override int GetHashCode();
    }
}

