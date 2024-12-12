namespace DevExpress.Xpf.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ValueAndCount
    {
        public ValueAndCount(object value, int count)
        {
            this.<Value>k__BackingField = value;
            this.<Count>k__BackingField = count;
        }

        public object Value { get; }
        public int Count { get; }
        public override bool Equals(object obj)
        {
            if (!(obj is ValueAndCount))
            {
                return false;
            }
            ValueAndCount count = (ValueAndCount) obj;
            return (EqualityComparer<object>.Default.Equals(this.Value, count.Value) && (this.Count == count.Count));
        }

        public override int GetHashCode() => 
            (((0x6e70dc8e * -1521134295) + EqualityComparer<object>.Default.GetHashCode(this.Value)) * -1521134295) + this.Count.GetHashCode();

        public override string ToString() => 
            $"Value: {this.Value}, Count: {this.Count}";
    }
}

