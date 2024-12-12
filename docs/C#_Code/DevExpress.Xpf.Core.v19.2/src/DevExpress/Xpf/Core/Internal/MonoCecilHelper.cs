namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Security;

    internal static class MonoCecilHelper
    {
        private static Assembly _mono;
        private static Type _assemblyDefinitionType;
        private static Type _embeddedResourceType;

        [SecurityCritical]
        public static IEmbeddedResource CreateEmbeddedResource(string name, object attributes, byte[] data);
        [SecurityCritical]
        public static IEmbeddedResource GetEmbeddedResource(object res);
        [SecurityCritical]
        public static IResource GetResource(object res);
        private static Type GetType(string typeName);
        [SecurityCritical]
        public static IAssemblyDefinition ReadAssembly(Stream stream);
        [SecurityCritical]
        public static IAssemblyDefinition ReadAssembly(string path);

        private static Type AssemblyDefinitionType { get; }

        private static Type EmbeddedResourceType { get; }

        private static Assembly Mono { get; }
    }
}

