namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.XtraReports.Parameters.Native;
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    [StructLayout(LayoutKind.Sequential), DebuggerDisplay("Range({Start},{End})"), DataContract]
    public struct Range<T> : IRange
    {
        [DataMember(Name="Start")]
        private readonly T start;
        [DataMember(Name="End")]
        private readonly T end;
        public T Start { get; }
        public T End { get; }
        object IRange.Start { get; }
        object IRange.End { get; }
        public Range(T start, T end);
        public override bool Equals(object obj);
        public override int GetHashCode();
    }
}

