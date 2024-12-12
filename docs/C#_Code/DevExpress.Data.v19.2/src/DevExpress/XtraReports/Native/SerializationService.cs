namespace DevExpress.XtraReports.Native
{
    using System;
    using System.Runtime.InteropServices;

    public class SerializationService : InstanceProvider<IDataSerializer>
    {
        public const string Guid = "DataSerializationExtension";
        private static readonly object padlock;
        private static SerializationService instance;

        static SerializationService();
        private bool Deserialize(string value, string typeName, out object result, IExtensionsProvider extensionProvider);
        public static bool DeserializeObject(string value, string typeName, out object result, IExtensionsProvider rootComponent);
        public static bool DeserializeObject(string value, Type destType, out object result, IExtensionsProvider extensionProvider);
        private IDataSerializer GetSerializer(IExtensionsProvider extensionsProvider);
        public static void RegisterSerializer(string contextName, IDataSerializer serializer);
        private bool Serialize(object value, out string result, IExtensionsProvider extensionProvider);
        public static bool SerializeObject(object value, out string result, IExtensionsProvider extensionProvider);
        public static bool TryGetSerializer(string contextName, out IDataSerializer value);
        public static bool UnregisterSerializer(string contextName);
    }
}

