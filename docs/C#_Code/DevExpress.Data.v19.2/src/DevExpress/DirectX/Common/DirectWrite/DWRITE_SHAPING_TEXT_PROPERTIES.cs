namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_SHAPING_TEXT_PROPERTIES
    {
        private short vector;
        public bool isShapedAlone =>
            (this.vector & 1) != 0;
        public bool canBreakShapingAfter =>
            (this.vector & 4) != 0;
        public override string ToString() => 
            $"{this.vector} - isShapedAlone:{this.isShapedAlone}, canBreakShapingAfter:{this.canBreakShapingAfter}";
    }
}

