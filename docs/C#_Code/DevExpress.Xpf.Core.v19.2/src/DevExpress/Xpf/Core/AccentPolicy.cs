namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public DevExpress.Xpf.Core.AccentState AccentState;
        public uint AccentFlags;
        public uint GradientColor;
        public uint AnimationId;
    }
}

