namespace DevExpress.DirectX.NativeInterop.Direct3D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct3D;
    using DevExpress.DirectX.Common.DXGI;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.DXGI;
    using System;

    public class D3D11Device : ComObject
    {
        public D3D11Device(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public DXGIDevice AsDXGIDevice() => 
            new DXGIDevice(base.QueryInterface<DXGIDevice>());

        public void CheckCounter()
        {
            throw new NotImplementedException();
        }

        public void CheckCounterInfo()
        {
            throw new NotImplementedException();
        }

        public void CheckFeatureSupport()
        {
            throw new NotImplementedException();
        }

        public void CheckFormatSupport(DXGI_FORMAT Format, int pFormatSupport)
        {
            throw new NotImplementedException();
        }

        public void CheckMultisampleQualityLevels(DXGI_FORMAT Format, int SampleCount, int pNumQualityLevels)
        {
            throw new NotImplementedException();
        }

        public void CreateBlendState()
        {
            throw new NotImplementedException();
        }

        public void CreateBuffer()
        {
            throw new NotImplementedException();
        }

        public void CreateClassLinkage()
        {
            throw new NotImplementedException();
        }

        public void CreateComputeShader()
        {
            throw new NotImplementedException();
        }

        public void CreateCounter()
        {
            throw new NotImplementedException();
        }

        public D3D11DeviceContext CreateDeferredContext(int ContextFlags)
        {
            throw new NotImplementedException();
        }

        public void CreateDepthStencilState()
        {
            throw new NotImplementedException();
        }

        public void CreateDepthStencilView()
        {
            throw new NotImplementedException();
        }

        public void CreateDomainShader()
        {
            throw new NotImplementedException();
        }

        public void CreateGeometryShader()
        {
            throw new NotImplementedException();
        }

        public void CreateGeometryShaderWithStreamOutput()
        {
            throw new NotImplementedException();
        }

        public void CreateHullShader()
        {
            throw new NotImplementedException();
        }

        public void CreateInputLayout()
        {
            throw new NotImplementedException();
        }

        public void CreatePixelShader()
        {
            throw new NotImplementedException();
        }

        public void CreatePredicate()
        {
            throw new NotImplementedException();
        }

        public void CreateQuery()
        {
            throw new NotImplementedException();
        }

        public void CreateRasterizerState()
        {
            throw new NotImplementedException();
        }

        public void CreateRenderTargetView()
        {
            throw new NotImplementedException();
        }

        public void CreateSamplerState()
        {
            throw new NotImplementedException();
        }

        public void CreateShaderResourceView()
        {
            throw new NotImplementedException();
        }

        public void CreateTexture1D()
        {
            throw new NotImplementedException();
        }

        public D3D11Texture2D CreateTexture2D(D3D11_TEXTURE2D_DESC pDesc)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref pDesc, IntPtr.Zero, out ptr, 5));
            return new D3D11Texture2D(ptr);
        }

        public void CreateTexture3D()
        {
            throw new NotImplementedException();
        }

        public void CreateUnorderedAccessView()
        {
            throw new NotImplementedException();
        }

        public void CreateVertexShader()
        {
            throw new NotImplementedException();
        }

        public int GetCreationFlags()
        {
            throw new NotImplementedException();
        }

        public void GetDeviceRemovedReason()
        {
            throw new NotImplementedException();
        }

        public int GetExceptionMode()
        {
            throw new NotImplementedException();
        }

        public D3D_FEATURE_LEVEL GetFeatureLevel() => 
            (D3D_FEATURE_LEVEL) ComObject.InvokeHelper.CalliInt(base.NativeObject, 0x25);

        public D3D11DeviceContext GetImmediateContext()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 40));
            return new D3D11DeviceContext(ptr);
        }

        public void GetPrivateData(Guid guid, int pDataSize, IntPtr pData)
        {
            throw new NotImplementedException();
        }

        public void OpenSharedResource(IntPtr hResource, Guid returnedInterface)
        {
            throw new NotImplementedException();
        }

        public void SetExceptionMode(int RaiseFlags)
        {
            throw new NotImplementedException();
        }

        public void SetPrivateData(Guid guid, int DataSize, IntPtr pData)
        {
            throw new NotImplementedException();
        }

        public void SetPrivateDataInterface(Guid guid, object pData)
        {
            throw new NotImplementedException();
        }
    }
}

