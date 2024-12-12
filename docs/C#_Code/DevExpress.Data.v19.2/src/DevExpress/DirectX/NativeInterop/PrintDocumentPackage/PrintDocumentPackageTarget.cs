namespace DevExpress.DirectX.NativeInterop.PrintDocumentPackage
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class PrintDocumentPackageTarget : ComObject
    {
        public PrintDocumentPackageTarget(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void Cancel()
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, 5));
        }

        public void GetPackageTarget()
        {
            throw new NotImplementedException();
        }

        public void GetPackageTargetTypes()
        {
            throw new NotImplementedException();
        }
    }
}

