namespace DevExpress.DirectX.NativeInterop.CCW
{
    using DevExpress.DirectX.Common;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;

    public class ComCallableWrapperBase : IDisposable, IUnknownCCW
    {
        public const int E_INVALIDARG = -2147024809;
        private const int E_NOINTERFACE = -2147467262;
        private const int S_OK = 0;
        private readonly int vtablesCount;
        private readonly IDictionary<Guid, IntPtr> vtables = new Dictionary<Guid, IntPtr>();
        private IntPtr nativeObjectData;
        private int referenceCount = 1;
        private GCHandle handle;
        private bool isDisposed;

        [SecuritySafeCritical]
        protected ComCallableWrapperBase()
        {
            this.handle = GCHandle.Alloc(this);
            IntPtr val = GCHandle.ToIntPtr(this.handle);
            IList<ComCallableWrapperVtable> typeComInterfaces = DynamicAssemblyHelper.GetTypeComInterfaces(base.GetType());
            this.vtablesCount = typeComInterfaces.Count;
            int num = Marshal.SizeOf<IntPtr>();
            int num2 = num * 2;
            this.nativeObjectData = Marshal.AllocCoTaskMem(num2 * this.vtablesCount);
            int num3 = 0;
            while (num3 < this.vtablesCount)
            {
                IList<IntPtr> methods = typeComInterfaces[num3].Methods;
                IntPtr ptr2 = Marshal.AllocCoTaskMem(methods.Count * num);
                int num5 = 0;
                int ofs = 0;
                while (true)
                {
                    if (num5 >= methods.Count)
                    {
                        int num4 = num2 * num3;
                        Marshal.WriteIntPtr(this.nativeObjectData, num4, ptr2);
                        Marshal.WriteIntPtr(this.nativeObjectData, num4 + num, val);
                        foreach (Guid guid in typeComInterfaces[num3].InterfaceIds)
                        {
                            if (!this.vtables.ContainsKey(guid))
                            {
                                this.vtables.Add(guid, IntPtr.Add(this.nativeObjectData, num2 * num3));
                            }
                        }
                        num3++;
                        break;
                    }
                    Marshal.WriteIntPtr(ptr2, ofs, methods[num5]);
                    num5++;
                    ofs += num;
                }
            }
        }

        public int AddRef()
        {
            int num = this.referenceCount + 1;
            this.referenceCount = num;
            return num;
        }

        public void Dispose()
        {
            this.Release();
            GC.SuppressFinalize(this);
        }

        ~ComCallableWrapperBase()
        {
            this.Release();
        }

        [SecuritySafeCritical]
        protected virtual void FreeResources()
        {
            int num = Marshal.SizeOf<IntPtr>();
            for (int i = 0; i < this.vtablesCount; i++)
            {
                Marshal.FreeCoTaskMem(Marshal.ReadIntPtr(this.nativeObjectData, (num * 2) * i));
            }
            Marshal.FreeCoTaskMem(this.nativeObjectData);
            this.handle.Free();
            this.nativeObjectData = IntPtr.Zero;
        }

        protected IntPtr QueryInterface<T>()
        {
            IntPtr ptr;
            if (!this.vtables.TryGetValue(typeof(T).GUID, out ptr))
            {
                throw new NotSupportedException();
            }
            return ptr;
        }

        [SecuritySafeCritical]
        public int QueryInterface(IntPtr riid, IntPtr ppOutput)
        {
            IntPtr ptr;
            Guid key = (Guid) Marshal.PtrToStructure(riid, typeof(Guid));
            if (!this.vtables.TryGetValue(key, out ptr))
            {
                return -2147467262;
            }
            this.AddRef();
            Marshal.WriteIntPtr(ppOutput, ptr);
            return 0;
        }

        public int Release()
        {
            this.referenceCount--;
            if (this.referenceCount == 0)
            {
                if (!this.isDisposed)
                {
                    this.FreeResources();
                }
                this.isDisposed = true;
            }
            return this.referenceCount;
        }
    }
}

