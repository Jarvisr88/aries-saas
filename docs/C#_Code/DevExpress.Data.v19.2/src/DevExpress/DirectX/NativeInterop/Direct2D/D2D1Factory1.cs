namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.DXGI;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class D2D1Factory1 : D2D1Factory
    {
        private readonly HashSet<Guid> registeredEffects;

        public D2D1Factory1(IntPtr nativeObject) : base(nativeObject)
        {
            this.registeredEffects = new HashSet<Guid>();
        }

        public D2D1Device CreateDevice(DXGIDevice dxgiDevice)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, dxgiDevice.NativeObject, out ptr, 0x11));
            return new D2D1Device(ptr);
        }

        public void CreateDrawingStateBlock1()
        {
            throw new NotImplementedException();
        }

        public D2D1GdiMetafile CreateGdiMetafile(NativeStream metafileStream)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, metafileStream.ToNativeObject(), out ptr, 0x15));
            return new D2D1GdiMetafile(ptr);
        }

        public void CreatePathGeometry1()
        {
            throw new NotImplementedException();
        }

        public D2D1StrokeStyle CreateStrokeStyle(D2D1_STROKE_STYLE_PROPERTIES1 strokeStyleProperties, float[] dashes, int dashesCount)
        {
            IntPtr ptr;
            using (ArrayMarshaler marshaler = new ArrayMarshaler(dashes))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref strokeStyleProperties, marshaler.Pointer, dashesCount, out ptr, 0x12));
            }
            return new D2D1StrokeStyle(ptr);
        }

        public void GetEffectProperties()
        {
            throw new NotImplementedException();
        }

        public void GetRegisteredEffects()
        {
            throw new NotImplementedException();
        }

        public bool IsEffectRegistered(Guid effectGuid) => 
            this.registeredEffects.Contains(effectGuid);

        public void RegisterEffectFromStream()
        {
            throw new NotImplementedException();
        }

        [SecuritySafeCritical]
        public void RegisterEffectFromString(Guid effectId, string propertyXml, D2D1_PROPERTY_BINDING[] bindings, EventFactoryDelegate effectFactory)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(effectId.ToByteArray()))
            {
                using (StringMarshaler marshaler2 = new StringMarshaler(propertyXml))
                {
                    using (ArrayMarshaler marshaler3 = new ArrayMarshaler(bindings))
                    {
                        InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, marshaler2.Pointer, marshaler3.Pointer, bindings.Length, Marshal.GetFunctionPointerForDelegate<EventFactoryDelegate>(effectFactory), 0x17));
                    }
                }
            }
            this.registeredEffects.Add(effectId);
        }

        public void UnregisterEffect()
        {
            throw new NotImplementedException();
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int EventFactoryDelegate(IntPtr result);
    }
}

