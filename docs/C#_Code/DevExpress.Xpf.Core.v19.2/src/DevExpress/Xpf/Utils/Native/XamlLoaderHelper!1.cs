namespace DevExpress.Xpf.Utils.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Markup;
    using System.Xml;

    public static class XamlLoaderHelper<T> where T: class
    {
        [ThreadStatic]
        private static Dictionary<Uri, T> cache;
        private static Dictionary<Type, string> AssemblyNameCache;

        static XamlLoaderHelper()
        {
            XamlLoaderHelper<T>.AssemblyNameCache = new Dictionary<Type, string>();
        }

        private static string GetAssemblyName(Type type)
        {
            if (XamlLoaderHelper<T>.AssemblyNameCache.ContainsKey(type))
            {
                return XamlLoaderHelper<T>.AssemblyNameCache[type];
            }
            string fullName = string.Empty;
            object[] customAttributes = type.GetCustomAttributes(typeof(SupportDXThemeAttribute), true);
            if ((customAttributes == null) || (customAttributes.Length == 0))
            {
                fullName = type.Assembly.FullName;
            }
            else
            {
                Type typeInAssembly = ((SupportDXThemeAttribute) customAttributes[0]).TypeInAssembly;
                fullName = (typeInAssembly == null) ? type.Assembly.FullName : typeInAssembly.Assembly.FullName;
            }
            XamlLoaderHelper<T>.AssemblyNameCache[type] = fullName;
            return fullName;
        }

        public static T LoadFromApplicationResources(string path) => 
            XamlLoaderHelper<T>.LoadFromResource(new Uri(path, UriKind.RelativeOrAbsolute));

        public static T LoadFromAssembly(Assembly asm, string source)
        {
            string uriString = "/" + asm.FullName.Replace(" ", "") + ";component/" + source;
            if (!uriString.EndsWith(".xaml"))
            {
                uriString = uriString + ".xaml";
            }
            return XamlLoaderHelper<T>.LoadFromResource(new Uri(uriString, UriKind.RelativeOrAbsolute));
        }

        public static T LoadFromAssembly(string assemblyName, string source)
        {
            Assembly asm = null;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            int index = 0;
            while (true)
            {
                if (index < assemblies.Length)
                {
                    Assembly assembly2 = assemblies[index];
                    if (!assembly2.FullName.Contains(assemblyName))
                    {
                        index++;
                        continue;
                    }
                    asm = assembly2;
                }
                if (asm != null)
                {
                    return XamlLoaderHelper<T>.LoadFromAssembly(asm, source);
                }
                return default(T);
            }
        }

        public static T LoadFromFile(string source)
        {
            T local;
            try
            {
                using (XmlReader reader = new XmlTextReader(source))
                {
                    local = XamlReader.Load(reader) as T;
                }
            }
            catch
            {
                return default(T);
            }
            return local;
        }

        public static T LoadFromResource(Uri path)
        {
            T local2;
            try
            {
                if (!(typeof(T) == typeof(ResourceDictionary)))
                {
                    local2 = Application.LoadComponent(path) as T;
                }
                else
                {
                    T local;
                    XamlLoaderHelper<T>.cache ??= new Dictionary<Uri, T>();
                    if (!XamlLoaderHelper<T>.cache.TryGetValue(path, out local))
                    {
                        local = Application.LoadComponent(path) as T;
                        XamlLoaderHelper<T>.cache.Add(path, local);
                    }
                    local2 = ResourceDictionaryHelper.CloneResourceDictionary(local as ResourceDictionary) as T;
                }
            }
            catch
            {
                return default(T);
            }
            return local2;
        }

        public static T LoadFromResource(Type type, string source)
        {
            string uriString = "/" + XamlLoaderHelper<T>.GetAssemblyName(type).Replace(" ", "") + ";component/" + source;
            if (!uriString.EndsWith(".xaml"))
            {
                uriString = uriString + ".xaml";
            }
            return XamlLoaderHelper<T>.LoadFromResource(new Uri(uriString, UriKind.RelativeOrAbsolute));
        }

        public static Assembly EntryAssembly =>
            AssemblyHelper.EntryAssembly;
    }
}

