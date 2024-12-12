namespace DevExpress.Internal
{
    using Microsoft.Win32;
    using System;

    public abstract class RegistryViewerBase
    {
        protected RegistryViewerBase()
        {
        }

        public abstract string[] GetMultiSzValue(RegistryHive hive, string key, string name);
        public abstract string GetSzValue(RegistryHive hive, string key, string name);
        public abstract bool IsKeyExists(RegistryHive hive, string key);
    }
}

