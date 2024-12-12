namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class NativeResourceManager
    {
        public const string AppDataEnvironmentVariable = "%_DATA_%";
        public const string CompanyNameEnvironmentVariable = "%_COMPANY_%";
        public const string ProductNameEnvironmentVariable = "%_PRODUCT_%";
        public const string VersionEnvironmentVariable = "%_VERSION_%";
        public const string ResourcesFolderName = "RDB817";
        private static object resourcesFolderLock = new object();
        private static string companyNameOverride;
        private static string productNameOverride;
        private static string versionOverride;
        private static DateTime? applicationCreateTime = null;
        private static string applicationExecutablePath;
        private static string applicationIdHash;
        private static Dictionary<string, Func<string>> variables;
        private static object variablesLock = new object();
        private static Tuple<string, string, string, string> configurationPathParts;
        private static Assembly entryAssembly;

        public static string CreateFileName(string source)
        {
            string str = $"%{new string(Path.GetInvalidPathChars())}{new string(Path.GetInvalidFileNameChars())}";
            return new Regex($"[{Regex.Escape(str)}]").Replace(source, "_");
        }

        public static string ExpandVariables(string name) => 
            Environment.ExpandEnvironmentVariables(ExpandVariablesCore(name, Variables));

        public static string ExpandVariablesCore(string name, Dictionary<string, Func<string>> variables)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            StringBuilder builder = new StringBuilder(name.Length);
            int startIndex = 0;
            while (true)
            {
                if (startIndex < name.Length)
                {
                    int index = name.IndexOf('%', startIndex);
                    if (index < 0)
                    {
                        builder.Append(name, startIndex, name.Length - startIndex);
                    }
                    else
                    {
                        builder.Append(name, startIndex, index - startIndex);
                        startIndex = index + 1;
                        int num3 = (startIndex == name.Length) ? -1 : name.IndexOf('%', startIndex);
                        if (num3 >= 0)
                        {
                            Func<string> func;
                            startIndex = num3 + 1;
                            string key = name.Substring(index, (num3 + 1) - index).ToUpperInvariant();
                            if (variables.TryGetValue(key, out func))
                            {
                                builder.Append(func());
                                continue;
                            }
                            builder.Append(key);
                            continue;
                        }
                        builder.Append(name, index, name.Length - index);
                    }
                }
                return builder.ToString();
            }
        }

        public static DateTime GetApplicationCreateTime()
        {
            if (applicationCreateTime == null)
            {
                applicationCreateTime = new DateTime?(DateTime.Now);
            }
            return applicationCreateTime.Value;
        }

        public static DateTime GetFileTime(string filePath)
        {
            DateTime creationTimeUtc = File.GetCreationTimeUtc(filePath);
            DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(filePath);
            return ((creationTimeUtc > lastWriteTimeUtc) ? creationTimeUtc : lastWriteTimeUtc);
        }

        public static string CompanyNameOverride
        {
            get
            {
                object resourcesFolderLock = NativeResourceManager.resourcesFolderLock;
                lock (resourcesFolderLock)
                {
                    return companyNameOverride;
                }
            }
            set
            {
                object resourcesFolderLock = NativeResourceManager.resourcesFolderLock;
                lock (resourcesFolderLock)
                {
                    companyNameOverride = value;
                }
            }
        }

        public static string ProductNameOverride
        {
            get
            {
                object resourcesFolderLock = NativeResourceManager.resourcesFolderLock;
                lock (resourcesFolderLock)
                {
                    return productNameOverride;
                }
            }
            set
            {
                object resourcesFolderLock = NativeResourceManager.resourcesFolderLock;
                lock (resourcesFolderLock)
                {
                    productNameOverride = value;
                }
            }
        }

        public static string VersionOverride
        {
            get
            {
                object resourcesFolderLock = NativeResourceManager.resourcesFolderLock;
                lock (resourcesFolderLock)
                {
                    return versionOverride;
                }
            }
            set
            {
                object resourcesFolderLock = NativeResourceManager.resourcesFolderLock;
                lock (resourcesFolderLock)
                {
                    versionOverride = value;
                }
            }
        }

        public static string ApplicationExecutablePath
        {
            get
            {
                if (applicationExecutablePath == null)
                {
                    Func<Assembly, string> evaluator = <>c.<>9__26_0;
                    if (<>c.<>9__26_0 == null)
                    {
                        Func<Assembly, string> local1 = <>c.<>9__26_0;
                        evaluator = <>c.<>9__26_0 = a => a.Location;
                    }
                    applicationExecutablePath = EntryAssembly.Return<Assembly, string>(evaluator, <>c.<>9__26_1 ??= () => Path.Combine(Environment.CurrentDirectory, Environment.GetCommandLineArgs()[0]));
                }
                return applicationExecutablePath;
            }
        }

        public static string ApplicationIdHash
        {
            get
            {
                if (applicationIdHash == null)
                {
                    using (SHA1 sha = SHA1.Create())
                    {
                        applicationIdHash = "H" + CreateFileName(Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(ApplicationExecutablePath))));
                    }
                }
                return applicationIdHash;
            }
        }

        public static string ResourcesFolder
        {
            get
            {
                string[] paths = new string[] { "%_DATA_%", "%_COMPANY_%", "%_PRODUCT_%", "%_VERSION_%", "RDB817" };
                return Path.Combine(paths);
            }
        }

        public static Dictionary<string, Func<string>> Variables
        {
            get
            {
                if (variables == null)
                {
                    object variablesLock = NativeResourceManager.variablesLock;
                    lock (variablesLock)
                    {
                        if (variables == null)
                        {
                            variables = new Dictionary<string, Func<string>>();
                            Func<string> func1 = <>c.<>9__32_0;
                            if (<>c.<>9__32_0 == null)
                            {
                                Func<string> local1 = <>c.<>9__32_0;
                                func1 = <>c.<>9__32_0 = () => ConfigurationPathParts.Item1;
                            }
                            variables.Add("%_DATA_%", func1);
                            Func<string> func2 = <>c.<>9__32_1;
                            if (<>c.<>9__32_1 == null)
                            {
                                Func<string> local2 = <>c.<>9__32_1;
                                func2 = <>c.<>9__32_1 = () => CompanyNameOverride ?? ConfigurationPathParts.Item2;
                            }
                            variables.Add("%_COMPANY_%", func2);
                            Func<string> func3 = <>c.<>9__32_2;
                            if (<>c.<>9__32_2 == null)
                            {
                                Func<string> local3 = <>c.<>9__32_2;
                                func3 = <>c.<>9__32_2 = () => ProductNameOverride ?? ConfigurationPathParts.Item3;
                            }
                            variables.Add("%_PRODUCT_%", func3);
                            Func<string> func4 = <>c.<>9__32_3;
                            if (<>c.<>9__32_3 == null)
                            {
                                Func<string> local4 = <>c.<>9__32_3;
                                func4 = <>c.<>9__32_3 = () => VersionOverride ?? ConfigurationPathParts.Item4;
                            }
                            variables.Add("%_VERSION_%", func4);
                        }
                    }
                }
                return variables;
            }
        }

        private static Tuple<string, string, string, string> ConfigurationPathParts
        {
            get
            {
                Tuple<string, string, string, string> tuple;
                object resourcesFolderLock = NativeResourceManager.resourcesFolderLock;
                lock (resourcesFolderLock)
                {
                    if (configurationPathParts == null)
                    {
                        string str = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\";
                        try
                        {
                            object obj3 = typeof(ConfigurationException).Assembly.GetType("System.Configuration.ConfigurationManagerInternalFactory").GetProperty("Instance", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null, null);
                            string str2 = (string) obj3.GetType().GetInterface("System.Configuration.Internal.IConfigurationManagerInternal").GetProperty("ExeLocalConfigDirectory").GetValue(obj3, null);
                            if (!str2.StartsWith(str, StringComparison.InvariantCultureIgnoreCase))
                            {
                                throw new IndexOutOfRangeException();
                            }
                            char[] separator = new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
                            string[] source = str2.Substring(str.Length).Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            if (source.Length < 3)
                            {
                                throw new IndexOutOfRangeException();
                            }
                            configurationPathParts = (source.Length != 3) ? new Tuple<string, string, string, string>(Path.Combine(str, Path.Combine(source.Take<string>((source.Length - 1)).ToArray<string>())), string.Empty, string.Empty, source[source.Length - 1]) : new Tuple<string, string, string, string>(str, source[0], source[1], source[2]);
                        }
                        catch
                        {
                            string applicationIdHash;
                            string text2;
                            string text3;
                            string source = EntryAssembly.With<Assembly, string>(<>c.<>9__34_0 ??= delegate (Assembly a) {
                                Func<AssemblyCompanyAttribute, string> func1 = <>c.<>9__34_1;
                                if (<>c.<>9__34_1 == null)
                                {
                                    Func<AssemblyCompanyAttribute, string> local1 = <>c.<>9__34_1;
                                    func1 = <>c.<>9__34_1 = t => t.Company;
                                }
                                return a.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false).Cast<AssemblyCompanyAttribute>().SingleOrDefault<AssemblyCompanyAttribute>().With<AssemblyCompanyAttribute, string>(func1);
                            });
                            Assembly entryAssembly = EntryAssembly;
                            if (<>c.<>9__34_2 == null)
                            {
                                Assembly local3 = EntryAssembly;
                                entryAssembly = (Assembly) (<>c.<>9__34_2 = delegate (Assembly a) {
                                    Func<AssemblyProductAttribute, string> func1 = <>c.<>9__34_3;
                                    if (<>c.<>9__34_3 == null)
                                    {
                                        Func<AssemblyProductAttribute, string> local1 = <>c.<>9__34_3;
                                        func1 = <>c.<>9__34_3 = t => t.Product;
                                    }
                                    return a.GetCustomAttributes(typeof(AssemblyProductAttribute), false).Cast<AssemblyProductAttribute>().SingleOrDefault<AssemblyProductAttribute>().With<AssemblyProductAttribute, string>(func1);
                                });
                            }
                            string str4 = ((Assembly) <>c.<>9__34_2).With<Assembly, string>((Func<Assembly, string>) entryAssembly);
                            Func<Assembly, string> evaluator = <>c.<>9__34_4;
                            if (<>c.<>9__34_4 == null)
                            {
                                Func<Assembly, string> local4 = <>c.<>9__34_4;
                                evaluator = <>c.<>9__34_4 = a => a.GetName().Version.ToString(4);
                            }
                            string str5 = EntryAssembly.With<Assembly, string>(evaluator);
                            if (source != null)
                            {
                                Func<char, bool> predicate = <>c.<>9__34_5;
                                if (<>c.<>9__34_5 == null)
                                {
                                    Func<char, bool> local5 = <>c.<>9__34_5;
                                    predicate = <>c.<>9__34_5 = c => char.IsLetterOrDigit(c);
                                }
                                if (source.Where<char>(predicate).Any<char>())
                                {
                                    applicationIdHash = CreateFileName(source);
                                    goto TR_0014;
                                }
                            }
                            applicationIdHash = ApplicationIdHash;
                            goto TR_0014;
                        TR_0005:
                            configurationPathParts = new Tuple<string, string, string, string>(str, source, str4, text3);
                            goto TR_0004;
                        TR_000C:
                            str4 = text2;
                            if (str5 != null)
                            {
                                Func<char, bool> predicate = <>c.<>9__34_7;
                                if (<>c.<>9__34_7 == null)
                                {
                                    Func<char, bool> local7 = <>c.<>9__34_7;
                                    predicate = <>c.<>9__34_7 = c => char.IsLetterOrDigit(c);
                                }
                                if (str5.Where<char>(predicate).Any<char>())
                                {
                                    text3 = CreateFileName(str5);
                                    goto TR_0005;
                                }
                            }
                            text3 = ApplicationIdHash;
                            goto TR_0005;
                        TR_0014:
                            source = applicationIdHash;
                            if (str4 != null)
                            {
                                Func<char, bool> predicate = <>c.<>9__34_6;
                                if (<>c.<>9__34_6 == null)
                                {
                                    Func<char, bool> local6 = <>c.<>9__34_6;
                                    predicate = <>c.<>9__34_6 = c => char.IsLetterOrDigit(c);
                                }
                                if (str4.Where<char>(predicate).Any<char>())
                                {
                                    text2 = CreateFileName(str4);
                                    goto TR_000C;
                                }
                            }
                            text2 = ApplicationIdHash;
                            goto TR_000C;
                        }
                    }
                TR_0004:
                    tuple = configurationPathParts;
                }
                return tuple;
            }
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
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NativeResourceManager.<>c <>9 = new NativeResourceManager.<>c();
            public static Func<Assembly, string> <>9__26_0;
            public static Func<string> <>9__26_1;
            public static Func<string> <>9__32_0;
            public static Func<string> <>9__32_1;
            public static Func<string> <>9__32_2;
            public static Func<string> <>9__32_3;
            public static Func<AssemblyCompanyAttribute, string> <>9__34_1;
            public static Func<Assembly, string> <>9__34_0;
            public static Func<AssemblyProductAttribute, string> <>9__34_3;
            public static Func<Assembly, string> <>9__34_2;
            public static Func<Assembly, string> <>9__34_4;
            public static Func<char, bool> <>9__34_5;
            public static Func<char, bool> <>9__34_6;
            public static Func<char, bool> <>9__34_7;

            internal string <get_ApplicationExecutablePath>b__26_0(Assembly a) => 
                a.Location;

            internal string <get_ApplicationExecutablePath>b__26_1() => 
                Path.Combine(Environment.CurrentDirectory, Environment.GetCommandLineArgs()[0]);

            internal string <get_ConfigurationPathParts>b__34_0(Assembly a)
            {
                Func<AssemblyCompanyAttribute, string> evaluator = <>9__34_1;
                if (<>9__34_1 == null)
                {
                    Func<AssemblyCompanyAttribute, string> local1 = <>9__34_1;
                    evaluator = <>9__34_1 = t => t.Company;
                }
                return a.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false).Cast<AssemblyCompanyAttribute>().SingleOrDefault<AssemblyCompanyAttribute>().With<AssemblyCompanyAttribute, string>(evaluator);
            }

            internal string <get_ConfigurationPathParts>b__34_1(AssemblyCompanyAttribute t) => 
                t.Company;

            internal string <get_ConfigurationPathParts>b__34_2(Assembly a)
            {
                Func<AssemblyProductAttribute, string> evaluator = <>9__34_3;
                if (<>9__34_3 == null)
                {
                    Func<AssemblyProductAttribute, string> local1 = <>9__34_3;
                    evaluator = <>9__34_3 = t => t.Product;
                }
                return a.GetCustomAttributes(typeof(AssemblyProductAttribute), false).Cast<AssemblyProductAttribute>().SingleOrDefault<AssemblyProductAttribute>().With<AssemblyProductAttribute, string>(evaluator);
            }

            internal string <get_ConfigurationPathParts>b__34_3(AssemblyProductAttribute t) => 
                t.Product;

            internal string <get_ConfigurationPathParts>b__34_4(Assembly a) => 
                a.GetName().Version.ToString(4);

            internal bool <get_ConfigurationPathParts>b__34_5(char c) => 
                char.IsLetterOrDigit(c);

            internal bool <get_ConfigurationPathParts>b__34_6(char c) => 
                char.IsLetterOrDigit(c);

            internal bool <get_ConfigurationPathParts>b__34_7(char c) => 
                char.IsLetterOrDigit(c);

            internal string <get_Variables>b__32_0() => 
                NativeResourceManager.ConfigurationPathParts.Item1;

            internal string <get_Variables>b__32_1() => 
                NativeResourceManager.CompanyNameOverride ?? NativeResourceManager.ConfigurationPathParts.Item2;

            internal string <get_Variables>b__32_2() => 
                NativeResourceManager.ProductNameOverride ?? NativeResourceManager.ConfigurationPathParts.Item3;

            internal string <get_Variables>b__32_3() => 
                NativeResourceManager.VersionOverride ?? NativeResourceManager.ConfigurationPathParts.Item4;
        }
    }
}

