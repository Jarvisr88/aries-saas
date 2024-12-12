namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.CCW;
    using System;
    using System.Runtime.InteropServices;

    [Guid("13d29038-c3e6-4034-9081-13b53a417992")]
    public class D2D1TransformGraph : ComObject
    {
        internal D2D1TransformGraph(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void AddNode(IComCallableWrapper<ID2D1TransformNodeCCW> node)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void ConnectNode(IComCallableWrapper<ID2D1TransformNodeCCW> fromNode, IComCallableWrapper<ID2D1TransformNodeCCW> toNode, int toNodeInputIndex)
        {
            throw new NotImplementedException();
        }

        public void ConnectToEffectInput(int toEffectInputIndex, IComCallableWrapper<ID2D1TransformNodeCCW> node, int toNodeInputIndex)
        {
            throw new NotImplementedException();
        }

        public int GetInputCount()
        {
            throw new NotImplementedException();
        }

        public void RemoveNode(IComCallableWrapper<ID2D1TransformNodeCCW> node)
        {
            throw new NotImplementedException();
        }

        public void SetOutputNode(IComCallableWrapper<ID2D1TransformNodeCCW> node)
        {
            throw new NotImplementedException();
        }

        public void SetPassthroughGraph(int effectInputIndex)
        {
            throw new NotImplementedException();
        }

        public void SetSingleTransformNode(IComCallableWrapper<ID2D1TransformNodeCCW> node)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, node.NativeObject, 4));
        }
    }
}

