namespace DevExpress.DirectX.Common.Direct2D
{
    using DevExpress.DirectX.Common.DXGI;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_INPUT_ELEMENT_DESC
    {
        private IntPtr semanticName;
        private int semanticIndex;
        private DXGI_FORMAT format;
        private int inputSlot;
        private int alignedByteOffset;
        public D2D1_INPUT_ELEMENT_DESC(IntPtr semanticNamePtr, int semanticIndex, DXGI_FORMAT format, int inputSlot, int alignedByteOffset)
        {
            this.semanticName = semanticNamePtr;
            this.semanticIndex = semanticIndex;
            this.format = format;
            this.inputSlot = inputSlot;
            this.alignedByteOffset = alignedByteOffset;
        }
    }
}

