namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FlowLayoutItemPosition
    {
        public double LayerOffset;
        public double ItemOffset;
        public FlowLayoutItemPosition(double layerOffset, double itemOffset)
        {
            this.LayerOffset = layerOffset;
            this.ItemOffset = itemOffset;
        }
    }
}

