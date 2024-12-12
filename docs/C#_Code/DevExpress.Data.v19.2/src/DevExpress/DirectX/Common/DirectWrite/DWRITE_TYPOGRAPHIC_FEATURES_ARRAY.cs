namespace DevExpress.DirectX.Common.DirectWrite
{
    using DevExpress.DirectX.Common;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    internal class DWRITE_TYPOGRAPHIC_FEATURES_ARRAY : IDisposable
    {
        private readonly DWRITE_TYPOGRAPHIC_FEATURES[] managedArray;

        [SecuritySafeCritical]
        public DWRITE_TYPOGRAPHIC_FEATURES_ARRAY(DWRITE_TYPOGRAPHIC_FEATURES[] features)
        {
            this.managedArray = features;
            IntPtr pointer = InteropHelpers.StructArrayToPtr<DWRITE_TYPOGRAPHIC_FEATURES>(features);
            this.ArrayPtr = new IntPtr[features.Length];
            int num = Marshal.SizeOf<DWRITE_TYPOGRAPHIC_FEATURES>();
            for (int i = 0; i < features.Length; i++)
            {
                this.ArrayPtr[i] = IntPtr.Add(pointer, i * num);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SecuritySafeCritical]
        private void Dispose(bool disposing)
        {
            if (this.ArrayPtr != null)
            {
                if (disposing)
                {
                    foreach (DWRITE_TYPOGRAPHIC_FEATURES dwrite_typographic_features in this.managedArray)
                    {
                        dwrite_typographic_features.Dispose();
                    }
                }
                Marshal.FreeHGlobal(this.ArrayPtr[0]);
                this.ArrayPtr = null;
            }
        }

        ~DWRITE_TYPOGRAPHIC_FEATURES_ARRAY()
        {
            this.Dispose(false);
        }

        internal IntPtr[] ArrayPtr { get; private set; }
    }
}

