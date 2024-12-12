namespace DevExpress.DirectX.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class StringMarshaler : Marshaler
    {
        [SecuritySafeCritical]
        public StringMarshaler(string value)
        {
            base.Pointer = Marshal.StringToCoTaskMemUni(value);
        }

        [SecuritySafeCritical]
        protected override void FreePointer()
        {
            Marshal.FreeCoTaskMem(base.Pointer);
        }
    }
}

