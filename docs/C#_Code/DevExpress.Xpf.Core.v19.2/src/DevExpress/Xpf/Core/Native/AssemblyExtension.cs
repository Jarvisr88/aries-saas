namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class AssemblyExtension
    {
        public static Stream GetEmbeddedResource(this Assembly assembly, string resourceName);
        public static bool GetEmbeddedResource(this Assembly assembly, string resourceName, out byte[] resource);
        public static ResourceDictionary GetResourceDictionary(this Assembly assembly, string resourceName);
    }
}

