namespace DevExpress.DirectX.Common.Direct3D
{
    using System;

    [Flags]
    public enum D3D11_BIND_FLAG
    {
        VertexBuffer = 1,
        IndexBuffer = 2,
        ConstantBuffer = 4,
        ShaderResource = 8,
        StreamOutput = 0x10,
        RenderTarget = 0x20,
        DepthStencil = 0x40,
        UnorderedAccess = 0x80,
        Decoder = 0x200,
        VideoEncoder = 0x400,
        None = 0
    }
}

