namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;

    [Guid("3d9f916b-27dc-4ad7-b4f1-64945340f563")]
    public class D2D1EffectContext : ComObject
    {
        internal D2D1EffectContext(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void CheckFeatureSupport()
        {
            throw new NotImplementedException();
        }

        public D2D1EffectContext Clone()
        {
            base.AddRef();
            return new D2D1EffectContext(base.NativeObject);
        }

        public void CreateBlendTransform()
        {
            throw new NotImplementedException();
        }

        public void CreateBorderTransform()
        {
            throw new NotImplementedException();
        }

        public void CreateBoundsAdjustmentTransform()
        {
            throw new NotImplementedException();
        }

        public void CreateColorContext()
        {
            throw new NotImplementedException();
        }

        public void CreateColorContextFromFilename()
        {
            throw new NotImplementedException();
        }

        public void CreateColorContextFromWicColorContext()
        {
            throw new NotImplementedException();
        }

        public void CreateEffect()
        {
            throw new NotImplementedException();
        }

        public void CreateOffsetTransform()
        {
            throw new NotImplementedException();
        }

        public D2D1ResourceTexture CreateResourceTexture(IntPtr resourceId, D2D1_RESOURCE_TEXTURE_PROPERTIES properties, IntPtr data, int dataSize)
        {
            IntPtr result = IntPtr.Zero;
            properties.Marshal(delegate (D2D1_RESOURCE_TEXTURE_PROPERTIES_COMMON props) {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(this.NativeObject, resourceId, ref props, data, IntPtr.Zero, dataSize, out result, 15));
            });
            return new D2D1ResourceTexture(result);
        }

        public void CreateTransformNodeFromEffect()
        {
            throw new NotImplementedException();
        }

        public D2D1VertexBuffer CreateVertexBuffer(ref D2D1_VERTEX_BUFFER_PROPERTIES vertexBufferProperties, IntPtr resourceId, ref D2D1_CUSTOM_VERTEX_BUFFER_PROPERTIES customVertexBufferProperties)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref vertexBufferProperties, resourceId, ref customVertexBufferProperties, out ptr, 0x11));
            return new D2D1VertexBuffer(ptr);
        }

        public void FindResourceTexture()
        {
            throw new NotImplementedException();
        }

        public int FindVertexBuffer(IntPtr resourceId, out D2D1VertexBuffer vertexBuffer)
        {
            throw new NotImplementedException();
        }

        public void GetDpi(out float dpiX, out float dpiY)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, out dpiX, out dpiY, 3);
        }

        public void GetMaximumSupportedFeatureLevel()
        {
            throw new NotImplementedException();
        }

        public void IsBufferPrecisionSupported()
        {
            throw new NotImplementedException();
        }

        public bool IsShaderLoaded(Guid resourceId)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(resourceId.ToByteArray()))
            {
                return (ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, 14) != 0);
            }
        }

        public void LoadComputeShader()
        {
            throw new NotImplementedException();
        }

        public void LoadPixelShader(IntPtr resourceId, IntPtr buffer, int bufferLenght)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, resourceId, buffer, bufferLenght, 11));
        }

        public void LoadVertexShader(IntPtr resourceId, IntPtr buffer, int bufferLenght)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, resourceId, buffer, bufferLenght, 12));
        }
    }
}

