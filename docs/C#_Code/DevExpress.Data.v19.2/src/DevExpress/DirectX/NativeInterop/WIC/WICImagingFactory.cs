namespace DevExpress.DirectX.NativeInterop.WIC
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Security;
    using System.Threading;

    public class WICImagingFactory : ComObject
    {
        private static Guid clsid = new Guid("cacaf262-9370-4615-a13b-9f5539da4c0a");
        private static Guid IWicImagingFactoryGuid = new Guid("EC5EC8A9-C395-4314-9C77-54D7A935FF70");
        private static readonly Guid GUID_ContainerFormatJpeg = new Guid(0x19e4a5aa, 0x5662, 0x4fc5, 160, 0xc0, 0x17, 0x58, 2, 0x8e, 0x10, 0x57);
        private static ThreadLocal<WICImagingFactory> instance = new ThreadLocal<WICImagingFactory>(new Func<WICImagingFactory>(WICImagingFactory.Create));

        internal WICImagingFactory(IntPtr nativeObject) : base(nativeObject)
        {
        }

        [SecuritySafeCritical]
        private static WICImagingFactory Create() => 
            new WICImagingFactory(Ole32Interop.CoCreateInstance(clsid, IWicImagingFactoryGuid));

        public WICBitmapScaler CreateBitmapScaler()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 11));
            return new WICBitmapScaler(ptr);
        }

        public WICColorContext CreateColorContext()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 15));
            return new WICColorContext(ptr);
        }

        public WICColorTransform CreateColorTransformer()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 0x10));
            return new WICColorTransform(ptr);
        }

        public WICFormatConverter CreateFormatConverter()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 10));
            return new WICFormatConverter(ptr);
        }

        public WICBitmapDecoder CreateJPEGDecoder()
        {
            IntPtr ptr;
            using (ArrayMarshaler marshaler = new ArrayMarshaler(GUID_ContainerFormatJpeg))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, IntPtr.Zero, out ptr, 7));
            }
            return new WICBitmapDecoder(ptr);
        }

        public WICPalette CreatePalette()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 9));
            return new WICPalette(ptr);
        }

        public static WICImagingFactory Instance =>
            instance.Value;
    }
}

