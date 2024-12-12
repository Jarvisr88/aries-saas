namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public static class ThemedElementsDictionary
    {
        private static AssemblyPriorityComparer assemblyComparer = new AssemblyPriorityComparer();
        private static Dictionary<string, ThemeKeysDictionary> cache = new Dictionary<string, ThemeKeysDictionary>();
        private static Dictionary<string, bool> forcedAssemblies = new Dictionary<string, bool>();

        public static void ForceThemeKeysLoading(string themeName)
        {
            Func<Assembly, bool> predicate = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<Assembly, bool> local1 = <>c.<>9__7_0;
                predicate = <>c.<>9__7_0 = x => IsCustomThemeAssembly(x);
            }
            Func<Assembly, string> selector = <>c.<>9__7_1;
            if (<>c.<>9__7_1 == null)
            {
                Func<Assembly, string> local2 = <>c.<>9__7_1;
                selector = <>c.<>9__7_1 = x => x.FullName;
            }
            Func<string, string> keySelector = <>c.<>9__7_2;
            if (<>c.<>9__7_2 == null)
            {
                Func<string, string> local3 = <>c.<>9__7_2;
                keySelector = <>c.<>9__7_2 = x => x;
            }
            AssemblyHelper.GetLoadedAssemblies().Where<Assembly>(predicate).Select<Assembly, string>(selector).Union<string>(GetThemeAssembly(themeName)).OrderBy<string, string>(keySelector, assemblyComparer).ToList<string>().ForEach(x => ForceThemeKeysLoadingForAssembly(themeName, x));
        }

        public static void ForceThemeKeysLoadingForAssembly(string themeName, string assemblyName)
        {
            bool flag;
            if (!forcedAssemblies.TryGetValue(assemblyName, out flag))
            {
                forcedAssemblies[assemblyName] = true;
                DefaultStyleThemeKeyExtension extension1 = new DefaultStyleThemeKeyExtension();
                extension1.ThemeName = themeName;
                extension1.FullName = "System.Windows.Controls.NonexistantControl" + ((assemblyName != null) ? ("+" + assemblyName) : string.Empty);
                DefaultStyleThemeKeyExtension local1 = extension1;
                local1.AssemblyName = assemblyName;
                new Button().SetDefaultStyleKey(local1);
            }
        }

        private static string GetAssemblyFullName(object key)
        {
            Type type = key as Type;
            if (type != null)
            {
                return type.Assembly.FullName;
            }
            ResourceKey key2 = key as ResourceKey;
            return ((key2 == null) ? string.Empty : key2.Assembly.FullName);
        }

        public static object GetCachedResourceKey(string themeName, string fullName) => 
            GetCachedResourceKeyCore(themeName, fullName);

        public static object GetCachedResourceKey(string themeName, Type type) => 
            GetCachedResourceKeyCore(themeName, type);

        private static object GetCachedResourceKeyCore(string themeName, object key)
        {
            object obj2;
            return (GetDictionary(GetCorrectedThemeName(themeName)).TryGetValue(key, out obj2) ? obj2 : null);
        }

        private static string GetCorrectedThemeName(string themeName) => 
            (themeName == null) ? string.Empty : themeName;

        private static ThemeKeysDictionary GetDictionary(string correctedThemeName)
        {
            ThemeKeysDictionary dictionary;
            if (!cache.TryGetValue(correctedThemeName, out dictionary))
            {
                dictionary = new ThemeKeysDictionary();
                cache[correctedThemeName] = dictionary;
            }
            return dictionary;
        }

        private static IEnumerable<string> GetThemeAssembly(string themeName)
        {
            List<string> list = new List<string>();
            Theme theme = Theme.FindTheme(themeName);
            if ((theme != null) && (theme.Assembly != null))
            {
                list.Add(theme.Assembly.FullName);
            }
            return list;
        }

        public static bool IsCustomThemeAssembly(Assembly assembly)
        {
            try
            {
                return (!assembly.ReflectionOnly ? (AssemblyHelper.IsEntryAssembly(assembly) || AssemblyHelper.HasAttribute(assembly, typeof(DXThemeInfoAttribute))) : false);
            }
            catch
            {
                return false;
            }
        }

        public static void RegisterThemeType(string themeName, string fullName, object key)
        {
            RegisterThemeTypeCore(themeName, fullName, key);
        }

        public static void RegisterThemeType(string themeName, Type type, object key)
        {
            RegisterThemeTypeCore(themeName, type, key);
        }

        internal static void RegisterThemeTypeCore(string themeName, object type, object key)
        {
            object obj2;
            ThemeKeysDictionary dictionary = GetDictionary(GetCorrectedThemeName(themeName));
            if (dictionary.TryGetValue(type, out obj2))
            {
                if (string.IsNullOrEmpty(themeName))
                {
                    return;
                }
                if (assemblyComparer.Compare(GetAssemblyFullName(key), GetAssemblyFullName(obj2)) >= 0)
                {
                    return;
                }
            }
            dictionary[type] = key;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemedElementsDictionary.<>c <>9 = new ThemedElementsDictionary.<>c();
            public static Func<Assembly, bool> <>9__7_0;
            public static Func<Assembly, string> <>9__7_1;
            public static Func<string, string> <>9__7_2;

            internal bool <ForceThemeKeysLoading>b__7_0(Assembly x) => 
                ThemedElementsDictionary.IsCustomThemeAssembly(x);

            internal string <ForceThemeKeysLoading>b__7_1(Assembly x) => 
                x.FullName;

            internal string <ForceThemeKeysLoading>b__7_2(string x) => 
                x;
        }

        private class ThemeKeysDictionary : Dictionary<object, object>
        {
        }
    }
}

