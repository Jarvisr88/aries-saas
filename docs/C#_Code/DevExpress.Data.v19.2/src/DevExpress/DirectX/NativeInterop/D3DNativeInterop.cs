namespace DevExpress.DirectX.NativeInterop
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct3D;
    using DevExpress.DirectX.NativeInterop.Direct3D;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class D3DNativeInterop
    {
        private const int D3D11_SDK_VERSION = 7;

        [SecuritySafeCritical]
        public static D3D11Device CreateD3DDevice(bool useSoftwareRenderer, out D3D11DeviceContext result)
        {
            D3D11Device device;
            D3D_FEATURE_LEVEL[] pFeatureLevels = new D3D_FEATURE_LEVEL[] { D3D_FEATURE_LEVEL.LEVEL_11_0, D3D_FEATURE_LEVEL.LEVEL_10_1 };
            uint length = (uint) pFeatureLevels.Length;
            try
            {
                D3D_FEATURE_LEVEL dd_feature_level;
                IntPtr ptr;
                IntPtr ptr2;
                D3D11_CREATE_DEVICE_FLAG flags = D3D11_CREATE_DEVICE_FLAG.BgraSupport | D3D11_CREATE_DEVICE_FLAG.SingleThreaded;
                if (useSoftwareRenderer && (Environment.ProcessorCount == 1))
                {
                    flags |= D3D11_CREATE_DEVICE_FLAG.PreventThreadingOptimizations;
                }
                InteropHelpers.CheckHResult(D3D11CreateDevice(IntPtr.Zero, useSoftwareRenderer ? D3D_DRIVER_TYPE.WARP : D3D_DRIVER_TYPE.HARDWARE, IntPtr.Zero, flags, pFeatureLevels, length, 7, out ptr, out dd_feature_level, out ptr2));
                device = new D3D11Device(ptr);
                result = new D3D11DeviceContext(ptr2);
            }
            catch
            {
                result = null;
                return null;
            }
            return device;
        }

        [DllImport("d3d11.dll")]
        private static extern int D3D11CreateDevice(IntPtr pAdapter, D3D_DRIVER_TYPE DriverType, IntPtr Software, D3D11_CREATE_DEVICE_FLAG Flags, [In, MarshalAs(UnmanagedType.LPArray)] D3D_FEATURE_LEVEL[] pFeatureLevels, uint FeatureLevels, uint SDKVersion, out IntPtr ppDevice, out D3D_FEATURE_LEVEL pFeatureLevel, out IntPtr ppImmediateContext);
        [SecuritySafeCritical]
        public static D3DBlob D3DCompile(byte[] shader, string entryPoint, string target)
        {
            IntPtr ptr;
            IntPtr zero = IntPtr.Zero;
            using (ArrayMarshaler marshaler = new ArrayMarshaler(shader))
            {
                int hResult = D3DCompile(marshaler.Pointer, new IntPtr(shader.Length), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, entryPoint, target, 0, 0, out ptr, out zero);
                if (zero != IntPtr.Zero)
                {
                    using (new D3DBlob(zero))
                    {
                    }
                }
                InteropHelpers.CheckHResult(hResult);
            }
            return new D3DBlob(ptr);
        }

        [DllImport("D3DCompiler_47.dll", CallingConvention=CallingConvention.StdCall)]
        private static extern int D3DCompile(IntPtr pSrcData, IntPtr srcDataSize, IntPtr pSourceName, IntPtr pDefines, IntPtr pInclude, [MarshalAs(UnmanagedType.LPStr)] string pEntrypoint, [MarshalAs(UnmanagedType.LPStr)] string pTarget, int flags1, int flags2, out IntPtr ppCode, out IntPtr ppErrorMsgs);
    }
}

