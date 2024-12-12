namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Security;

    public class D2D1PrintControl : ComObject
    {
        internal D2D1PrintControl(IntPtr nativeObject) : base(nativeObject)
        {
        }

        [SecuritySafeCritical]
        public void AddPage(D2D1CommandList commandList, D2D_SIZE_F pageSize, NativeStream pagePrintTicketStream)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, commandList.ToNativeObject(), pageSize, pagePrintTicketStream.ToNativeObject(), IntPtr.Zero, IntPtr.Zero, 3));
        }

        public void Close()
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, 4));
        }
    }
}

