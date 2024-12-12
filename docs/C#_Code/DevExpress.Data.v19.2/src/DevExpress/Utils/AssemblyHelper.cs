namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class AssemblyHelper
    {
        private static Dictionary<Assembly, string> defaultNamespaces = new Dictionary<Assembly, string>();
        private const string PublicKeyTokenString = "PublicKeyToken";
        private static Assembly entryAssembly;
        private const int PublicKeyTokenBytesLength = 8;

        private static bool AssertAssemblyName(string fullName, string assemblyName) => 
            !string.IsNullOrEmpty(assemblyName) ? fullName.ToLowerInvariant().Contains(assemblyName.ToLowerInvariant()) : false;

        public static string CombineUri(params string[] parts)
        {
            if (parts.Length == 0)
            {
                return string.Empty;
            }
            string str = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                str = str + "/" + parts[i];
            }
            str = str.Replace('\\', '/');
            while (str.Contains("//"))
            {
                str = str.Replace("//", "/");
            }
            if ((str.Length > 1) && (str[str.Length - 1] == '/'))
            {
                str = str.Remove(str.Length - 1);
            }
            if ((((parts[0].Length == 0) || (parts[0][0] != '/')) && (str.Length != 0)) && (str[0] == '/'))
            {
                str = str.Substring(1);
            }
            return str;
        }

        public static Assembly GetAssembly(string assemblyFullName, Func<string, Assembly> loadAssemblyHandler = null)
        {
            Assembly loadedAssembly = GetLoadedAssembly(assemblyFullName);
            if (loadedAssembly != null)
            {
                return loadedAssembly;
            }
            Assembly local1 = loadAssemblyHandler?.Invoke(assemblyFullName);
            Assembly local3 = local1;
            if (local1 == null)
            {
                Assembly local2 = local1;
                local3 = Assembly.Load(assemblyFullName);
            }
            return local3;
        }

        public static string GetAssemblyFullName(string name, string version, CultureInfo culture, string publicKeyToken)
        {
            AssemblyName name2 = new AssemblyName {
                Name = name,
                Version = new Version(version),
                CultureInfo = culture
            };
            if ((publicKeyToken != null) && (publicKeyToken.Length == 0x10))
            {
                name2.SetPublicKeyToken(StringToBytes(publicKeyToken));
            }
            return name2.FullName;
        }

        public static string GetCommonPart(string[] strings, string[] excludedSuffixes)
        {
            List<string> list = (from s in strings
                where (from e in excludedSuffixes
                    where s.EndsWith(e, StringComparison.Ordinal)
                    select e).FirstOrDefault<string>() == null
                select s).ToList<string>();
            if (list.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder(list[0].Length);
            int num = 0;
            while (true)
            {
                char? nullable;
                while (true)
                {
                    string str2;
                    nullable = null;
                    using (List<string>.Enumerator enumerator = list.GetEnumerator())
                    {
                        while (true)
                        {
                            if (enumerator.MoveNext())
                            {
                                string current = enumerator.Current;
                                if (num >= current.Length)
                                {
                                    str2 = builder.ToString();
                                }
                                else
                                {
                                    int? nullable1;
                                    if (nullable == null)
                                    {
                                        nullable = new char?(current[num]);
                                    }
                                    char? nullable3 = nullable;
                                    if (nullable3 != null)
                                    {
                                        nullable1 = new int?(nullable3.GetValueOrDefault());
                                    }
                                    else
                                    {
                                        nullable1 = null;
                                    }
                                    int? nullable2 = nullable1;
                                    if (!((current[num] == nullable2.GetValueOrDefault()) ? (nullable2 == null) : true))
                                    {
                                        continue;
                                    }
                                    str2 = builder.ToString();
                                }
                            }
                            else
                            {
                                break;
                            }
                            break;
                        }
                    }
                    return str2;
                }
                builder.Append(nullable);
                num++;
            }
        }

        public static string GetCoreAssemblyPublicKeyToken()
        {
            int index = GetDXAssemblyFullName().IndexOf("PublicKeyToken", StringComparison.Ordinal);
            int length = "31bf3856ad364e35".Length;
            return typeof(AssemblyHelper).Assembly.FullName.Substring(index + "PublicKeyToken=".Length, length);
        }

        public static string GetDefaultNamespace(Assembly assembly)
        {
            string defaultNamespaceCore = null;
            if (!defaultNamespaces.TryGetValue(assembly, out defaultNamespaceCore))
            {
                defaultNamespaceCore = GetDefaultNamespaceCore(assembly);
                defaultNamespaces.Add(assembly, defaultNamespaceCore);
            }
            return defaultNamespaceCore;
        }

        private static string GetDefaultNamespaceCore(Assembly assembly)
        {
            string[] manifestResourceNames = assembly.GetManifestResourceNames();
            if (manifestResourceNames.Length == 0)
            {
                return string.Empty;
            }
            if (manifestResourceNames.Length == 1)
            {
                return (GetPartialName(assembly) + ".");
            }
            string[] excludedSuffixes = new string[] { ".csdl", ".ssdl", ".msl" };
            return GetCommonPart(manifestResourceNames, excludedSuffixes);
        }

        private static string GetDXAssemblyFullName()
        {
            string fullName = typeof(AssemblyHelper).Assembly.FullName;
            if (!NameContains(Assembly.GetExecutingAssembly().FullName, "DevExpress.Data.v19.2") && !NameContains(Assembly.GetExecutingAssembly().FullName, "DevExpress.Mvvm.v19.2"))
            {
                throw new NotSupportedException($"Wrong DX assembly: {fullName}");
            }
            return fullName;
        }

        public static Stream GetEmbeddedResourceStream(Assembly assembly, string name, bool nameIsFull)
        {
            name = name.Replace('/', '.');
            Stream stream = GetEmbeddedResourceStreamCore(assembly, name, nameIsFull);
            if (stream == null)
            {
                stream = GetEmbeddedResourceStreamCore(assembly, name + ".zip", nameIsFull);
                if (stream != null)
                {
                    stream = new GZipStream(stream, CompressionMode.Decompress);
                }
            }
            return stream;
        }

        private static Stream GetEmbeddedResourceStreamCore(Assembly assembly, string name, bool nameIsFull)
        {
            string str2 = GetDefaultNamespace(assembly) + name;
            Stream manifestResourceStream = assembly.GetManifestResourceStream(str2);
            if ((manifestResourceStream != null) | nameIsFull)
            {
                return manifestResourceStream;
            }
            foreach (string str3 in assembly.GetManifestResourceNames())
            {
                if (str3.EndsWith("." + name, StringComparison.Ordinal))
                {
                    return assembly.GetManifestResourceStream(str3);
                }
            }
            return null;
        }

        public static string GetFullNameAppendix() => 
            ", Version=19.2.9.0, Culture=neutral, PublicKeyToken=" + GetCoreAssemblyPublicKeyToken();

        public static IEnumerable<Assembly> GetLoadedAssemblies() => 
            AppDomain.CurrentDomain.GetAssemblies();

        public static Assembly GetLoadedAssembly(string asmName)
        {
            Assembly assembly2;
            using (IEnumerator enumerator = GetLoadedAssemblies().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Assembly current = (Assembly) enumerator.Current;
                        if (!PartialNameEquals(current.FullName, asmName))
                        {
                            continue;
                        }
                        assembly2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return assembly2;
        }

        public static string GetNamespace(Type type)
        {
            string fullName = type.FullName;
            int startIndex = fullName.LastIndexOf('.');
            return ((startIndex < 0) ? string.Empty : fullName.Remove(startIndex));
        }

        public static string GetPartialName(Assembly assembly) => 
            GetPartialName(assembly.FullName);

        public static string GetPartialName(string asmName)
        {
            int index = asmName.IndexOf(',');
            return ((index < 0) ? asmName : asmName.Remove(index));
        }

        private static Assembly GetReflectionOnlyLoadedAssembly(string asmName)
        {
            try
            {
                return Assembly.ReflectionOnlyLoad(asmName);
            }
            catch
            {
                return null;
            }
        }

        public static ResourceSet GetResources(Assembly assembly) => 
            new ResourceManager(GetPartialName(assembly) + ".g", assembly).GetResourceSet(CultureInfo.InvariantCulture, true, false);

        public static IDictionaryEnumerator GetResourcesEnumerator(Assembly assembly) => 
            GetResources(assembly)?.GetEnumerator();

        public static Stream GetResourceStream(Assembly assembly, string path, bool pathIsFull)
        {
            path = path.ToLowerInvariant();
            Stream stream = GetResourceStreamCore(assembly, path, pathIsFull);
            if (stream == null)
            {
                stream = GetResourceStreamCore(assembly, path + ".zip", pathIsFull);
                if (stream != null)
                {
                    stream = new GZipStream(stream, CompressionMode.Decompress);
                }
            }
            return stream;
        }

        private static Stream GetResourceStreamCore(Assembly assembly, string path, bool pathIsFull)
        {
            if (pathIsFull)
            {
                ResourceSet resources = GetResources(assembly);
                return ((resources != null) ? (resources.GetObject(path, false) as Stream) : null);
            }
            IDictionaryEnumerator resourcesEnumerator = GetResourcesEnumerator(assembly);
            if (resourcesEnumerator != null)
            {
                while (resourcesEnumerator.MoveNext())
                {
                    string key = (string) resourcesEnumerator.Key;
                    if ((key == path) || key.EndsWith("/" + path, StringComparison.Ordinal))
                    {
                        return (resourcesEnumerator.Value as Stream);
                    }
                }
            }
            return null;
        }

        public static Uri GetResourceUri(Assembly assembly, string path) => 
            new Uri($"{"pack://application:,,,"}/{GetPartialName(assembly)};component/{path}");

        public static string GetShortNameWithoutVersion(Assembly assembly) => 
            GetShortNameWithoutVersion(assembly.FullName);

        public static string GetShortNameWithoutVersion(string fullName)
        {
            char[] separator = new char[] { ',' };
            return fullName.Split(separator)[0].Replace(".v19.2", string.Empty);
        }

        public static Assembly GetThemeAssembly(string themeName)
        {
            string themeAssemblyName = GetThemeAssemblyName(themeName);
            Assembly loadedAssembly = GetLoadedAssembly(themeAssemblyName);
            return ((loadedAssembly == null) ? LoadDXAssembly(themeAssemblyName) : loadedAssembly);
        }

        public static string GetThemeAssemblyFullName(string themeName) => 
            GetThemeAssemblyName(themeName) + GetFullNameAppendix();

        public static string GetThemeAssemblyName(string themeName)
        {
            char[] separator = new char[] { ';' };
            return ("DevExpress.Xpf.Themes." + themeName.Split(separator).First<string>() + ".v19.2");
        }

        public static bool HasAttribute(Assembly assembly, Type attributeType) => 
            (assembly != null) && Attribute.IsDefined(assembly, attributeType);

        public static bool HasAttribute(string assemblyName, Type attributeType) => 
            HasAttribute(GetLoadedAssembly(assemblyName), attributeType);

        public static bool IsDXProductAssembly(Assembly assembly) => 
            NameContains(assembly, "DevExpress.Xpf") && !IsDXThemeAssembly(assembly);

        public static bool IsDXProductAssembly(string assemblyFullName) => 
            NameContains(assemblyFullName, "DevExpress.Xpf") && !IsDXThemeAssembly(assemblyFullName);

        public static bool IsDXThemeAssembly(Assembly assembly) => 
            NameContains(assembly, "Themes");

        public static bool IsDXThemeAssembly(string assemblyName) => 
            NameContains(assemblyName, "Themes");

        public static bool IsEntryAssembly(Assembly assembly) => 
            EntryAssembly == assembly;

        public static bool IsEntryAssembly(string assemblyName)
        {
            Assembly entryAssembly = EntryAssembly;
            return ((entryAssembly != null) ? NameContains(entryAssembly, assemblyName) : false);
        }

        public static bool IsLoadedAssembly(string assemblyName) => 
            GetLoadedAssembly(assemblyName) != null;

        public static Assembly LoadDXAssembly(string assemblyName)
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.Load(assemblyName + GetFullNameAppendix());
            }
            catch
            {
            }
            return assembly;
        }

        public static bool NameContains(Assembly assembly, string assemblyName) => 
            AssertAssemblyName(assembly.FullName, assemblyName);

        public static bool NameContains(AssemblyName assembly, string assemblyName) => 
            AssertAssemblyName(assembly.FullName, assemblyName);

        public static bool NameContains(string assemblyFullName, string assemblyName) => 
            AssertAssemblyName(assemblyFullName, assemblyName);

        public static bool PartialNameEquals(string asmName0, string asmName1) => 
            string.Equals(GetPartialName(asmName0), GetPartialName(asmName1), StringComparison.InvariantCultureIgnoreCase);

        private static byte[] StringToBytes(string str)
        {
            int num = str.Length / 2;
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                buffer[i] = byte.Parse(str.Substring(2 * i, 2), NumberStyles.HexNumber);
            }
            return buffer;
        }

        public static Assembly EntryAssembly
        {
            get
            {
                if (entryAssembly == null)
                {
                    entryAssembly = Assembly.GetEntryAssembly();
                }
                return entryAssembly;
            }
            set => 
                entryAssembly = value;
        }
    }
}

