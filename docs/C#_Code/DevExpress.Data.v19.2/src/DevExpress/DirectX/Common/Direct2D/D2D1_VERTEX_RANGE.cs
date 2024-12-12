namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_VERTEX_RANGE
    {
        private int startVertex;
        private int vertexCount;
        public D2D1_VERTEX_RANGE(int startVertex, int vertexCount)
        {
            this.startVertex = startVertex;
            this.vertexCount = vertexCount;
        }
    }
}

