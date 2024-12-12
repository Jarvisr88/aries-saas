namespace DevExpress.DirectX.StandardInterop.PrintDocumentPackage
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("1b8efec4-3019-4c27-964e-367202156906"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPrintDocumentPackageTarget
    {
        void GetPackageTargetTypes();
        void GetPackageTarget();
        void Cancel();
    }
}

