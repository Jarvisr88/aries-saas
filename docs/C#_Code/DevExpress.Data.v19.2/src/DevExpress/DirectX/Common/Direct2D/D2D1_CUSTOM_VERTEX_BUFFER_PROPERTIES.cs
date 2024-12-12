namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_CUSTOM_VERTEX_BUFFER_PROPERTIES
    {
        private IntPtr shaderBufferWithInputSignature;
        private int shaderBufferSize;
        private IntPtr inputElements;
        private int elementCount;
        private int stride;
        public D2D1_CUSTOM_VERTEX_BUFFER_PROPERTIES(IntPtr shaderBufferWithInputSignature, int shaderBufferSize, IntPtr inputElements, int elementCount, int stride)
        {
            this.shaderBufferWithInputSignature = shaderBufferWithInputSignature;
            this.shaderBufferSize = shaderBufferSize;
            this.inputElements = inputElements;
            this.elementCount = elementCount;
            this.stride = stride;
        }
    }
}

