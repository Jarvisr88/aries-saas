namespace DevExpress.DirectX.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class StringMarshalerA : Marshaler
    {
        [SecuritySafeCritical]
        public StringMarshalerA(string value)
        {
            base.Pointer = Marshal.StringToCoTaskMemAnsi(value);
        }

        [SecuritySafeCritical]
        protected override void FreePointer()
        {
            Marshal.FreeCoTaskMem(base.Pointer);
        }
    }
}

