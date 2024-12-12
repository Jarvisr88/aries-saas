namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct RenderStyleTarget
    {
        public static bool operator ==(RenderStyleTarget left, RenderStyleTarget right);
        public static bool operator !=(RenderStyleTarget left, RenderStyleTarget right);
        public Type TargetType { get; set; }
        public string TargetName { get; set; }
        public bool Equals(RenderStyleTarget other);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public byte ComputeMatch(string name, Type type);
    }
}

