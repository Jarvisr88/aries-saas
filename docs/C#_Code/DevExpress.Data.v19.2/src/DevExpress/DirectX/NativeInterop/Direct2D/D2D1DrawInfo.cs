namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;

    [Guid("693ce632-7f2f-45de-93fe-18d88b37aa21")]
    public class D2D1DrawInfo : ComObject
    {
        internal D2D1DrawInfo(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1DrawInfo Clone()
        {
            base.AddRef();
            return new D2D1DrawInfo(base.NativeObject);
        }

        public void SetCached()
        {
            throw new NotImplementedException();
        }

        public void SetInputDescription()
        {
            throw new NotImplementedException();
        }

        public void SetInstructionCountHint()
        {
            throw new NotImplementedException();
        }

        public void SetOutputBuffer()
        {
            throw new NotImplementedException();
        }

        public void SetPixelShader(IntPtr shaderId, D2D1_PIXEL_OPTIONS pixelOptions)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, shaderId, (int) pixelOptions, 10));
        }

        public void SetPixelShaderConstantBuffer(IntPtr buffer, int bufferCount)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, buffer, bufferCount, 7));
        }

        public void SetResourceTexture(int textureIndex, D2D1ResourceTexture texture)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, textureIndex, texture.NativeObject, 8));
        }

        public void SetVertexProcessing(D2D1VertexBuffer vertexBuffer, D2D1_VERTEX_OPTIONS vertexOptions, IntPtr blendDescription, ref D2D1_VERTEX_RANGE vertexRange, IntPtr vertexShader)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, vertexBuffer.ToNativeObject(), (int) vertexOptions, blendDescription, ref vertexRange, vertexShader, 11));
        }

        public void SetVertexShaderConstantBuffer(IntPtr buffer, int bufferCount)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, buffer, bufferCount, 9));
        }
    }
}

