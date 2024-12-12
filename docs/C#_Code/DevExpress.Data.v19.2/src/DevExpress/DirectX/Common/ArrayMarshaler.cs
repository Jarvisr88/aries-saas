namespace DevExpress.DirectX.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class ArrayMarshaler : Marshaler
    {
        private readonly GCHandle arrayHandle;

        [SecuritySafeCritical]
        public ArrayMarshaler(Array value)
        {
            this.arrayHandle = GCHandle.Alloc(value, GCHandleType.Pinned);
            base.Pointer = this.arrayHandle.AddrOfPinnedObject();
        }

        public ArrayMarshaler(Guid value) : this(value.ToByteArray())
        {
        }

        [SecuritySafeCritical]
        protected override void FreePointer()
        {
            this.arrayHandle.Free();
        }
    }
}

