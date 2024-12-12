namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;

    public static class DXAssemblyDetector
    {
        private static readonly ConcurrentDictionary<Assembly, bool> assemblyList = new ConcurrentDictionary<Assembly, bool>();
        private static readonly byte[] dxPublicKeyToken = typeof(DXAssemblyDetector).Assembly.GetName().GetPublicKeyToken();

        public static bool IsDevExpressAssembly(Assembly assembly) => 
            assemblyList.GetOrAdd(assembly, delegate (Assembly a) {
                byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
                return (publicKeyToken != null) && dxPublicKeyToken.SequenceEqual<byte>(publicKeyToken);
            });
    }
}

