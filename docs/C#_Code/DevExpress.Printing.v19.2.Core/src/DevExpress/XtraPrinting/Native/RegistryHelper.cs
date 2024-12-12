namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;

    public static class RegistryHelper
    {
        public static bool AddRegistryValue(string item, string registryPath, string registryKey);
        public static void DeleteRegistryValue(string item, string registryPath, string registryKey);
        public static object GetRegistryValue(string keyPath, string name, object defaultValue);
        public static object[] GetRegistryValues(string keyPath);
        public static string[] LoadRegistryValue(string registryPath, string registryKey);
        public static void SaveRegistryValue(string[] items, string registryPath, string registryKey);
        public static bool SetRegistryValue(string keyPath, string name, object val);
        public static void SetRegistryValues(string keyPath, object[] values);

        private class SimpleStorage : IXtraSerializable
        {
            private string data;

            public SimpleStorage();
            void IXtraSerializable.OnEndDeserializing(string restoredVersion);
            void IXtraSerializable.OnEndSerializing();
            void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e);
            void IXtraSerializable.OnStartSerializing();
            public string[] GetData();
            public void SetData(string[] items);

            [XtraSerializableProperty]
            public string Data { get; set; }
        }
    }
}

