namespace DevExpress.Data.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class AssemblyCache
    {
        private static Dictionary<string, Assembly> loadedAssemblies;
        private static bool loading;
        private static readonly object padlock;

        static AssemblyCache();
        public static void Clear();
        public static Assembly Load(AssemblyName assemName);
        public static Assembly LoadDXAssembly(string name);
        public static Assembly LoadWithPartialName(string partialName);

        private static Dictionary<string, Assembly> LoadedAssemblies { get; }
    }
}

