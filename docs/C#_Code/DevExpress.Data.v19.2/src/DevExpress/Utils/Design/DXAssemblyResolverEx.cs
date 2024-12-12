namespace DevExpress.Utils.Design
{
    using DevExpress.Data.Extensions;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;
    using System.Text.RegularExpressions;

    public class DXAssemblyResolverEx
    {
        private static bool initialized;
        private static int locked;
        [ThreadStatic]
        private static Dictionary<string, Assembly> assemblies;
        private static Regex typeModuleRegEx;
        private const int CodeRushForRoslynVersionBuild = 0x7f;
        private static Regex versionRegEx;

        public static Assembly FindAssembly(string name) => 
            FindAssembly(name, true);

        public static Assembly FindAssembly(string name, bool patchVersion)
        {
            string validShortName = GetValidShortName(name, patchVersion);
            if ((DXAssemblyResolverEx.assemblies != null) && DXAssemblyResolverEx.assemblies.ContainsKey(validShortName))
            {
                return DXAssemblyResolverEx.assemblies[validShortName];
            }
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = assemblies.Length - 1; i >= 0; i--)
            {
                if (assemblies[i].FullName.StartsWith(validShortName, StringComparison.InvariantCulture))
                {
                    AssemblyName name2 = assemblies[i].GetName();
                    if ((name2.Name == validShortName) && (name2.Version.ToString() == "19.2.9.0"))
                    {
                        DXAssemblyResolverEx.assemblies ??= new Dictionary<string, Assembly>();
                        DXAssemblyResolverEx.assemblies[validShortName] = assemblies[i];
                        return assemblies[i];
                    }
                }
            }
            return null;
        }

        private static Regex GetTypeModuleRegEx()
        {
            typeModuleRegEx ??= new Regex(@"\[(?<TypeName>[\w\s\.=]+?)\,+\s*(?<AssemblyName>.+?)\]", RegexOptions.Compiled);
            return typeModuleRegEx;
        }

        internal static string GetValidAssemblyName(string assemblyName) => 
            GetValidAssemblyName(assemblyName, true);

        internal static string GetValidAssemblyName(string assemblyName, bool patchVersion)
        {
            if (assemblyName != null)
            {
                string str = assemblyName.ToLower(CultureInfo.InvariantCulture);
                if (str.StartsWith("devexpress"))
                {
                    if (str.StartsWith("devexpress.expressapp"))
                    {
                        return assemblyName;
                    }
                    if (str.StartsWith("devexpress.persistence"))
                    {
                        return assemblyName;
                    }
                    if (str.StartsWith("devexpress.persistent"))
                    {
                        return assemblyName;
                    }
                    if (str.StartsWith("devexpress.workflow"))
                    {
                        return assemblyName;
                    }
                    char[] separator = new char[] { ',' };
                    string[] typeParts = assemblyName.Split(separator);
                    if ((typeParts == null) || (typeParts.Length <= 1))
                    {
                        assemblyName = patchVersion ? GetValidModuleName(assemblyName) : assemblyName;
                    }
                    else
                    {
                        int index = 0;
                        while (true)
                        {
                            if (index >= typeParts.Length)
                            {
                                int num;
                                Version version = GetVersion(typeParts, out num);
                                if ((version != null) && (version.Build >= 0x7f))
                                {
                                    return assemblyName;
                                }
                                string str2 = patchVersion ? GetValidModuleName(typeParts[0]) : typeParts[0];
                                int num3 = 1;
                                while (true)
                                {
                                    if (num3 >= typeParts.Length)
                                    {
                                        assemblyName = str2;
                                        break;
                                    }
                                    if ((num3 != num) || !patchVersion)
                                    {
                                        str2 = str2 + ", " + typeParts[num3];
                                    }
                                    num3++;
                                }
                                break;
                            }
                            typeParts[index] = typeParts[index].Trim();
                            index++;
                        }
                    }
                    if (patchVersion)
                    {
                        assemblyName = assemblyName + ", Version=19.2.9.0";
                    }
                }
            }
            return assemblyName;
        }

        internal static string GetValidModuleName(string assemblyName)
        {
            Regex versionRegEx = GetVersionRegEx();
            return (!versionRegEx.IsMatch(assemblyName) ? ((assemblyName.IndexOf("3") == -1) ? ((assemblyName.IndexOf("4") == -1) ? ((assemblyName.IndexOf(".Design") == -1) ? (assemblyName + ".v19.2") : assemblyName.Replace(".Design", ".v19.2.Design")) : assemblyName.Replace("4", ".v19.2")) : assemblyName.Replace("3", ".v19.2")) : versionRegEx.Replace(assemblyName, ".v19.2"));
        }

        private static string GetValidShortName(string assemblyName, bool patchVersion)
        {
            string validAssemblyName = GetValidAssemblyName(assemblyName, patchVersion);
            char[] separator = new char[] { ',' };
            string[] strArray = validAssemblyName.Split(separator);
            return ((strArray.Length == 0) ? validAssemblyName : strArray[0]);
        }

        internal static string GetValidTypeName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName) || !typeName.Contains("DevExpress"))
            {
                return typeName;
            }
            MatchEvaluator evaluator = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                MatchEvaluator local1 = <>c.<>9__11_0;
                evaluator = <>c.<>9__11_0 = delegate (Match target) {
                    Group group = target.Groups["AssemblyName"];
                    Group group2 = target.Groups["TypeName"];
                    return ((group == null) || (!group.Success || ((group2 == null) || !group2.Success))) ? target.ToString() : $"[{group2.Value}, {GetValidAssemblyName(group.Value, true)}]";
                };
            }
            return GetTypeModuleRegEx().Replace(typeName, evaluator);
        }

        private static Version GetVersion(string[] typeParts, out int versionIndex)
        {
            Predicate<string> predicate = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Predicate<string> local1 = <>c.<>9__15_0;
                predicate = <>c.<>9__15_0 = x => x.ToLowerInvariant().StartsWith("version=", StringComparison.Ordinal);
            }
            versionIndex = typeParts.FindIndex<string>(predicate);
            if (versionIndex != -1)
            {
                try
                {
                    Version result = null;
                    if (Version.TryParse(typeParts[versionIndex].Substring("version=".Length), out result))
                    {
                        return result;
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        private static Regex GetVersionRegEx()
        {
            versionRegEx ??= new Regex(@".v\d+.\d", RegexOptions.Singleline | RegexOptions.Compiled);
            return versionRegEx;
        }

        public static void Init()
        {
            if (SecurityHelper.IsPermissionGranted(new SecurityPermission(SecurityPermissionFlag.ControlAppDomain)))
            {
                InitInternal();
            }
        }

        [SecuritySafeCritical]
        private static void InitInternal()
        {
            if (!initialized)
            {
                try
                {
                    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(DXAssemblyResolverEx.OnAssemblyResolve);
                }
                catch
                {
                }
                initialized = true;
            }
        }

        [SecuritySafeCritical]
        private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs e)
        {
            if ((locked == 0) && e.Name.StartsWith("DevExpress"))
            {
                if (e.Name.Contains("PublicKeyToken=null"))
                {
                    return null;
                }
                locked++;
                bool flag = e.Name.Contains(".Design");
                try
                {
                    Assembly assembly = FindAssembly(e.Name, !flag);
                    return ((assembly == null) ? (!e.Name.Contains(".resources") ? AssemblyCache.LoadWithPartialName(GetValidAssemblyName(e.Name, !flag)) : null) : assembly);
                }
                catch
                {
                }
                finally
                {
                    locked--;
                }
            }
            return null;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXAssemblyResolverEx.<>c <>9 = new DXAssemblyResolverEx.<>c();
            public static MatchEvaluator <>9__11_0;
            public static Predicate<string> <>9__15_0;

            internal string <GetValidTypeName>b__11_0(Match target)
            {
                Group group = target.Groups["AssemblyName"];
                Group group2 = target.Groups["TypeName"];
                return (((group == null) || (!group.Success || ((group2 == null) || !group2.Success))) ? target.ToString() : $"[{group2.Value}, {DXAssemblyResolverEx.GetValidAssemblyName(group.Value, true)}]");
            }

            internal bool <GetVersion>b__15_0(string x) => 
                x.ToLowerInvariant().StartsWith("version=", StringComparison.Ordinal);
        }
    }
}

