namespace ActiproSoftware.ComponentModel
{
    using #Dqe;
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(#Cqe))]
    public struct DefaultableInt32
    {
        private static DefaultableInt32 #k7;
        private bool #owc;
        private int #2qe;
        static DefaultableInt32();
        private DefaultableInt32(bool isDefault);
        public DefaultableInt32(int value);
        public static DefaultableInt32 Default { get; }
        public override bool Equals(object obj);
        public override int GetHashCode();
        public bool IsDefault { get; }
        public int Value { get; }
        public static bool operator ==(DefaultableInt32 left, DefaultableInt32 right);
        public static bool operator !=(DefaultableInt32 left, DefaultableInt32 right);
        public static implicit operator DefaultableInt32(int value);
        public static explicit operator int(DefaultableInt32 value);
    }
}

