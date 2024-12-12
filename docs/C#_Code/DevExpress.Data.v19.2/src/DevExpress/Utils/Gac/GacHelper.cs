namespace DevExpress.Utils.Gac
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    [SecuritySafeCritical]
    public static class GacHelper
    {
        [DllImport("fusion.dll")]
        private static extern int CreateAssemblyCache(out IAssemblyCache ppAsmCache, int reserved);
        [DllImport("fusion.dll")]
        private static extern int CreateAssemblyEnum(out IAssemblyEnum ppEnum, IntPtr pUnkReserved, IAssemblyName pName, AssemblyCacheFlags flags, IntPtr pvReserved);
        [DllImport("fusion.dll")]
        private static extern int CreateAssemblyNameObject(out IAssemblyName ppAssemblyNameObj, [MarshalAs(UnmanagedType.LPWStr)] string szAssemblyName, CreateAssemblyNameObjectFlags flags, IntPtr pvReserved);
        public static IEnumerable<string> GetAssembliesList(string name)
        {
            IAssemblyName ppAssemblyNameObj = null;
            IAssemblyEnum ppEnum = null;
            List<string> list = new List<string>();
            int num = CreateAssemblyNameObject(out ppAssemblyNameObj, name, CreateAssemblyNameObjectFlags.CANOF_PARSE_DISPLAY_NAME, IntPtr.Zero);
            if (num >= 0)
            {
                num = CreateAssemblyEnum(out ppEnum, IntPtr.Zero, ppAssemblyNameObj, AssemblyCacheFlags.GAC, IntPtr.Zero);
            }
            if (num >= 0)
            {
                while (true)
                {
                    num = ppEnum.GetNextAssembly(IntPtr.Zero, out ppAssemblyNameObj, 0);
                    if ((num < 0) || (ppAssemblyNameObj == null))
                    {
                        break;
                    }
                    list.Add(GetFullName(ppAssemblyNameObj));
                }
            }
            return list;
        }

        public static string GetAssemblyPath(string assemblyName)
        {
            AssemblyInfo assemblyInfo = new AssemblyInfo {
                cchBuf = 0x400
            };
            assemblyInfo.currentAssemblyPath = new string('\0', assemblyInfo.cchBuf);
            IAssemblyCache ppAsmCache = null;
            int num = CreateAssemblyCache(out ppAsmCache, 0);
            if (assemblyName.Contains("PublicKey"))
            {
                assemblyName = assemblyName.Remove(assemblyName.IndexOf("PublicKey") - 2);
            }
            if (num >= 0)
            {
                num = ppAsmCache.QueryAssemblyInfo(0, assemblyName, ref assemblyInfo);
            }
            return ((num < 0) ? string.Empty : assemblyInfo.currentAssemblyPath);
        }

        private static string GetFullName(IAssemblyName assemblyName)
        {
            StringBuilder pDisplayName = new StringBuilder(capacity);
            return ((assemblyName.GetDisplayName(pDisplayName, 0x400, 0xa7) < 0) ? string.Empty : pDisplayName.ToString());
        }
    }
}

