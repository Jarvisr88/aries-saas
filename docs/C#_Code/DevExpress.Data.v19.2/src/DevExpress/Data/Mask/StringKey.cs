namespace DevExpress.Data.Mask
{
    using System;

    internal sealed class StringKey
    {
        private readonly int Hash;
        public readonly int Length;
        public readonly StringKey Next;
        public readonly char Symbol;
        public readonly DfaWave Wave;
        public const int HashSeed = 0;

        public StringKey(StringKey next, char symbol, DfaWave wave);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public static int GetNextHash(int prevHash, char nextChar);
        private static bool IsEqual(StringKey item, StringKey key);
        internal static bool IsEqual(string str, int length, StringKey key);
    }
}

