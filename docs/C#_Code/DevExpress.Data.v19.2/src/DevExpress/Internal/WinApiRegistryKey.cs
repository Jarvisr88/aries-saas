namespace DevExpress.Internal
{
    using Microsoft.Win32;
    using System;

    public class WinApiRegistryKey : DisposableBase
    {
        private IntPtr handle;

        public WinApiRegistryKey(RegistryHive hive, string key, WinApiRegistryHelper.ResigtryAccess access)
        {
            this.handle = WinApiRegistryHelper.OpenRegistryKey(hive, key, access);
        }

        protected override void DisposeUnmanaged()
        {
            if (this.handle != IntPtr.Zero)
            {
                WinApiRegistryHelper.CloseRegistryKey(this.handle);
            }
            this.handle = IntPtr.Zero;
            base.DisposeUnmanaged();
        }

        public string[] GetMultiSzValue(string name) => 
            this.Exists ? WinApiRegistryHelper.ReadRegistryKeyMultiSzValue(this.handle, name) : null;

        public string GetSzValue(string name) => 
            this.Exists ? WinApiRegistryHelper.ReadRegistryKeySzValue(this.handle, name) : null;

        public bool Exists =>
            this.handle != IntPtr.Zero;
    }
}

