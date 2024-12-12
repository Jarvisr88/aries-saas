namespace DevExpress.Utils
{
    using System;
    using System.IO;
    using System.Reflection;

    public static class ResourceStreamHelper
    {
        public static byte[] GetBytes(string name, Assembly asm)
        {
            using (Stream stream = GetStream(name, asm))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public static string GetResourceName(Type baseType, string name) => 
            $"{baseType.Namespace}.{name}";

        public static Stream GetStream(string name, Assembly asm) => 
            asm.GetManifestResourceStream(name);

        public static Stream GetStream(string name, Type type) => 
            GetStream(GetResourceName(type, name), type.Assembly);
    }
}

