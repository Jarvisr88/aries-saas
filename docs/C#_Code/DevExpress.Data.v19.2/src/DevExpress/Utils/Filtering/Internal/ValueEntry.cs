namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct ValueEntry : IEquatable<ValueEntry>
    {
        private readonly IGroupValuesSource source;
        private readonly int group;
        private readonly int index;
        public ValueEntry(int key, int index, IGroupValuesSource source);
        public object Value { get; }
        public bool Equals(ValueEntry entry);
        public override int GetHashCode();
        public override bool Equals(object obj);
        public override string ToString();
    }
}

