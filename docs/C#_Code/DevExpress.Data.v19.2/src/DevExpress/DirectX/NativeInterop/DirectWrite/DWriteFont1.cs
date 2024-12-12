namespace DevExpress.DirectX.NativeInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;

    [Guid("acd16696-8c14-4f5d-877e-fe3fc1d32738")]
    public class DWriteFont1 : DWriteFont
    {
        internal DWriteFont1(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public byte[] GetPanose()
        {
            byte[] buffer = new byte[10];
            using (ArrayMarshaler marshaler = new ArrayMarshaler(buffer))
            {
                ComObject.InvokeHelper.Calli(base.NativeObject, marshaler.Pointer, 15);
            }
            return buffer;
        }

        public DWRITE_UNICODE_RANGE[] GetUnicodeRanges()
        {
            int num;
            DWRITE_UNICODE_RANGE[] dwrite_unicode_rangeArray;
            int num2 = 0x400;
            while (true)
            {
                dwrite_unicode_rangeArray = new DWRITE_UNICODE_RANGE[num2];
                using (ArrayMarshaler marshaler = new ArrayMarshaler(dwrite_unicode_rangeArray))
                {
                    int num3 = ComObject.InvokeHelper.CalliInt(base.NativeObject, num2, marshaler.Pointer, out num, 0x10);
                    if (num3 != -2147024774)
                    {
                        break;
                    }
                    num2 += 0x400;
                }
            }
            DWRITE_UNICODE_RANGE[] destinationArray = new DWRITE_UNICODE_RANGE[num];
            Array.Copy(dwrite_unicode_rangeArray, destinationArray, num);
            return destinationArray;
        }

        public bool IsMonospacedFont() => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, 0x11) != 0;
    }
}

