namespace DevExpress.Internal
{
    using Microsoft.Win32;
    using System;

    public class RegistryViewer : RegistryViewerBase
    {
        public static RegistryViewer Current = new RegistryViewer();

        public override string[] GetMultiSzValue(RegistryHive hive, string key, string name)
        {
            using (WinApiRegistryMultiKey key2 = this.GetWKey(hive, key))
            {
                return key2.GetMultiSzValue(name);
            }
        }

        public override string GetSzValue(RegistryHive hive, string key, string name)
        {
            using (WinApiRegistryMultiKey key2 = this.GetWKey(hive, key))
            {
                return key2.GetSzValue(name);
            }
        }

        private WinApiRegistryMultiKey GetWKey(RegistryHive hive, string key)
        {
            key = key.Replace('/', '\\');
            return new WinApiRegistryMultiKey(hive, key, WinApiRegistryHelper.ResigtryAccess.Read);
        }

        public override bool IsKeyExists(RegistryHive hive, string key)
        {
            using (WinApiRegistryMultiKey key2 = this.GetWKey(hive, key))
            {
                return key2.Exists;
            }
        }
    }
}

