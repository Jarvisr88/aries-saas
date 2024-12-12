namespace DevExpress.DirectX.NativeInterop.Direct3D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct3D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D3D11DeviceContext : D3D11DeviceChild
    {
        public D3D11DeviceContext(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void Begin()
        {
            throw new NotImplementedException();
        }

        public void ClearDepthStencilView()
        {
            throw new NotImplementedException();
        }

        public void ClearRenderTargetView()
        {
            throw new NotImplementedException();
        }

        public void ClearState()
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, 0x6f);
        }

        public void ClearUnorderedAccessViewFloat()
        {
            throw new NotImplementedException();
        }

        public void ClearUnorderedAccessViewUint()
        {
            throw new NotImplementedException();
        }

        public void CopyResource(D3D11Texture2D pDstResource, D3D11Texture2D pSrcResource)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, pDstResource.ToNativeObject(), pSrcResource.ToNativeObject(), 0x2f);
        }

        public void CopyStructureCount()
        {
            throw new NotImplementedException();
        }

        public void CopySubresourceRegion()
        {
            throw new NotImplementedException();
        }

        public void CSGetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void CSGetSamplers()
        {
            throw new NotImplementedException();
        }

        public void CSGetShader()
        {
            throw new NotImplementedException();
        }

        public void CSGetShaderResources()
        {
            throw new NotImplementedException();
        }

        public void CSGetUnorderedAccessViews()
        {
            throw new NotImplementedException();
        }

        public void CSSetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void CSSetSamplers()
        {
            throw new NotImplementedException();
        }

        public void CSSetShader()
        {
            throw new NotImplementedException();
        }

        public void CSSetShaderResources()
        {
            throw new NotImplementedException();
        }

        public void CSSetUnorderedAccessViews()
        {
            throw new NotImplementedException();
        }

        public void Dispatch()
        {
            throw new NotImplementedException();
        }

        public void DispatchIndirect()
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public void DrawAuto()
        {
            throw new NotImplementedException();
        }

        public void DrawIndexed()
        {
            throw new NotImplementedException();
        }

        public void DrawIndexedInstanced()
        {
            throw new NotImplementedException();
        }

        public void DrawIndexedInstancedIndirect()
        {
            throw new NotImplementedException();
        }

        public void DrawInstanced()
        {
            throw new NotImplementedException();
        }

        public void DrawInstancedIndirect()
        {
            throw new NotImplementedException();
        }

        public void DSGetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void DSGetSamplers()
        {
            throw new NotImplementedException();
        }

        public void DSGetShader()
        {
            throw new NotImplementedException();
        }

        public void DSGetShaderResources()
        {
            throw new NotImplementedException();
        }

        public void DSSetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void DSSetSamplers()
        {
            throw new NotImplementedException();
        }

        public void DSSetShader()
        {
            throw new NotImplementedException();
        }

        public void DSSetShaderResources()
        {
            throw new NotImplementedException();
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        public void ExecuteCommandList()
        {
            throw new NotImplementedException();
        }

        public void FinishCommandList()
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, 0x6f);
        }

        public void GenerateMips()
        {
            throw new NotImplementedException();
        }

        public void GetContextFlags()
        {
            throw new NotImplementedException();
        }

        public void GetData()
        {
            throw new NotImplementedException();
        }

        public void GetNativeType()
        {
            throw new NotImplementedException();
        }

        public void GetPredication()
        {
            throw new NotImplementedException();
        }

        public void GetResourceMinLOD()
        {
            throw new NotImplementedException();
        }

        public void GSGetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void GSGetSamplers()
        {
            throw new NotImplementedException();
        }

        public void GSGetShader()
        {
            throw new NotImplementedException();
        }

        public void GSGetShaderResources()
        {
            throw new NotImplementedException();
        }

        public void GSSetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void GSSetSamplers()
        {
            throw new NotImplementedException();
        }

        public void GSSetShader()
        {
            throw new NotImplementedException();
        }

        public void GSSetShaderResources()
        {
            throw new NotImplementedException();
        }

        public void HSGetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void HSGetSamplers()
        {
            throw new NotImplementedException();
        }

        public void HSGetShader()
        {
            throw new NotImplementedException();
        }

        public void HSGetShaderResources()
        {
            throw new NotImplementedException();
        }

        public void HSSetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void HSSetSamplers()
        {
            throw new NotImplementedException();
        }

        public void HSSetShader()
        {
            throw new NotImplementedException();
        }

        public void HSSetShaderResources()
        {
            throw new NotImplementedException();
        }

        public void IAGetIndexBuffer()
        {
            throw new NotImplementedException();
        }

        public void IAGetInputLayout()
        {
            throw new NotImplementedException();
        }

        public void IAGetPrimitiveTopology()
        {
            throw new NotImplementedException();
        }

        public void IAGetVertexBuffers()
        {
            throw new NotImplementedException();
        }

        public void IASetIndexBuffer()
        {
            throw new NotImplementedException();
        }

        public void IASetInputLayout()
        {
            throw new NotImplementedException();
        }

        public void IASetPrimitiveTopology()
        {
            throw new NotImplementedException();
        }

        public void IASetVertexBuffers()
        {
            throw new NotImplementedException();
        }

        public D3D11_MAPPED_SUBRESOURCE Map(D3D11Resource pResource, int subresource, D3D11_MAP mapType, D3D11_MAP_FLAG mapFlags)
        {
            D3D11_MAPPED_SUBRESOURCE dd_mapped_subresource = new D3D11_MAPPED_SUBRESOURCE();
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, pResource.ToNativeObject(), subresource, (int) mapType, (int) mapFlags, ref dd_mapped_subresource, 14));
            return dd_mapped_subresource;
        }

        public void OMGetBlendState()
        {
            throw new NotImplementedException();
        }

        public void OMGetDepthStencilState()
        {
            throw new NotImplementedException();
        }

        public void OMGetRenderTargets()
        {
            throw new NotImplementedException();
        }

        public void OMGetRenderTargetsAndUnorderedAccessViews()
        {
            throw new NotImplementedException();
        }

        public void OMSetBlendState()
        {
            throw new NotImplementedException();
        }

        public void OMSetDepthStencilState()
        {
            throw new NotImplementedException();
        }

        public void OMSetRenderTargets()
        {
            throw new NotImplementedException();
        }

        public void OMSetRenderTargetsAndUnorderedAccessViews()
        {
            throw new NotImplementedException();
        }

        public void PSGetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void PSGetSamplers()
        {
            throw new NotImplementedException();
        }

        public void PSGetShader()
        {
            throw new NotImplementedException();
        }

        public void PSGetShaderResources()
        {
            throw new NotImplementedException();
        }

        public void PSSetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void PSSetSamplers()
        {
            throw new NotImplementedException();
        }

        public void PSSetShader()
        {
            throw new NotImplementedException();
        }

        public void PSSetShaderResources()
        {
            throw new NotImplementedException();
        }

        public void ResolveSubresource()
        {
            throw new NotImplementedException();
        }

        public void RSGetScissorRects()
        {
            throw new NotImplementedException();
        }

        public void RSGetState()
        {
            throw new NotImplementedException();
        }

        public void RSGetViewports()
        {
            throw new NotImplementedException();
        }

        public void RSSetScissorRects()
        {
            throw new NotImplementedException();
        }

        public void RSSetState()
        {
            throw new NotImplementedException();
        }

        public void RSSetViewports()
        {
            throw new NotImplementedException();
        }

        public void SetPredication()
        {
            throw new NotImplementedException();
        }

        public void SetResourceMinLOD()
        {
            throw new NotImplementedException();
        }

        public void SOGetTargets()
        {
            throw new NotImplementedException();
        }

        public void SOSetTargets()
        {
            throw new NotImplementedException();
        }

        public void Unmap(D3D11Resource pResource, int subresource)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, pResource.NativeObject, subresource, 15);
        }

        public void UpdateSubresource()
        {
            throw new NotImplementedException();
        }

        public void VSGetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void VSGetSamplers()
        {
            throw new NotImplementedException();
        }

        public void VSGetShader()
        {
            throw new NotImplementedException();
        }

        public void VSGetShaderResources()
        {
            throw new NotImplementedException();
        }

        public void VSSetConstantBuffers()
        {
            throw new NotImplementedException();
        }

        public void VSSetSamplers()
        {
            throw new NotImplementedException();
        }

        public void VSSetShader()
        {
            throw new NotImplementedException();
        }

        public void VSSetShaderResources()
        {
            throw new NotImplementedException();
        }
    }
}

