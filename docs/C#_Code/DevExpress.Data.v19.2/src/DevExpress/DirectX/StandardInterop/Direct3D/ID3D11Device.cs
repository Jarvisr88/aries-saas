namespace DevExpress.DirectX.StandardInterop.Direct3D
{
    using DevExpress.DirectX.Common.Direct3D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("db6f6ddb-ac77-4e88-8253-819df9bbf140"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID3D11Device
    {
        void CreateBuffer();
        void CreateTexture1D();
        ID3D11Texture2D CreateTexture2D(ref D3D11_TEXTURE2D_DESC descRef, IntPtr initialDataRef);
        void CreateTexture3D();
        void CreateShaderResourceView();
        void CreateUnorderedAccessView();
        void CreateRenderTargetView();
        void CreateDepthStencilView();
        void CreateInputLayout();
        void CreateVertexShader();
        void CreateGeometryShader();
        void CreateGeometryShaderWithStreamOutput();
        void CreatePixelShader();
        void CreateHullShader();
        void CreateDomainShader();
        void CreateComputeShader();
        void CreateClassLinkage();
        void CreateBlendState();
        void CreateDepthStencilState();
        void CreateRasterizerState();
        void CreateSamplerState();
        void CreateQuery();
        void CreatePredicate();
        void CreateCounter();
        void CreateDeferredContext();
        void OpenSharedResource();
        void CheckFormatSupport();
        void CheckMultisampleQualityLevels();
        void CheckCounterInfo();
        void CheckCounter();
        void CheckFeatureSupport();
        void GetPrivateData();
        void SetPrivateData();
        void SetPrivateDataInterface();
        void GetFeatureLevel();
        void GetCreationFlags();
        void GetDeviceRemovedReason();
        void GetImmediateContext();
        void SetExceptionMode();
        void GetExceptionMode();
    }
}

