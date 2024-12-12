namespace DevExpress.Xpf.Utils
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Resources;
    using System.Xml.Linq;

    internal static class ApplicationHelper
    {
        public const string ManifestExtension = ".manifest";
        private const int ExtensionLenth = 0;
        private static string relativeManifestName;
        private static string relativePath;
        private static readonly EnvironmentStrategy environmentStrategy = GetEnvironmentStrategy();
        private static string[] dependentAssembliesCache;

        static ApplicationHelper()
        {
            Environment.Initialize();
        }

        public static List<string> GetAvailableAssemblies()
        {
            List<string> assemblies = new List<string>(AppDomain.CurrentDomain.GetAssemblies().Length);
            foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
            {
                assemblies.Add(assembly2.FullName);
            }
            Assembly entryAssembly = AssemblyHelper.EntryAssembly;
            if (entryAssembly != null)
            {
                foreach (AssemblyName name in entryAssembly.GetReferencedAssemblies())
                {
                    assemblies.Add(name.FullName);
                }
                Environment.AddAvailableAssemblies(ref assemblies);
            }
            return assemblies;
        }

        public static string[] GetDependentAssemblies()
        {
            dependentAssembliesCache ??= PopulateDependentAssemblies(RelativeManifestName);
            return dependentAssembliesCache;
        }

        internal static string[] GetDependentAssemblies(string manifestString) => 
            GetDependentAssemblies(manifestString, false);

        internal static string[] GetDependentAssemblies(string manifestString, bool extensionOnly)
        {
            Func<XElement, bool> predicate = <>c.<>9__23_0 ??= xElement => (xElement.Name.LocalName == "assemblyIdentity");
            Func<XElement, string> selectCondition = <>c.<>9__23_1 ??= element => element.Attribute("name").Value;
            try
            {
                List<string> list = new List<string>();
                foreach (string str in XmlHelper.GetDescendants<string>(predicate, selectCondition, manifestString))
                {
                    if (str.Length > 0)
                    {
                        list.Add(str.Substring(0, str.Length));
                    }
                }
                return list.ToArray();
            }
            catch
            {
                return new string[0];
            }
        }

        private static EnvironmentStrategy GetEnvironmentStrategy() => 
            !BrowserInteropHelper.IsBrowserHosted ? new IgnoreManifestStrategy() : (!ThemeManager.IgnoreManifest ? new BrowserStrategy() : new IgnoreManifestStrategy());

        private static string GetRelativeManifestName()
        {
            string entryManifestName;
            if (BrowserInteropHelper.Source == null)
            {
                return EntryManifestName;
            }
            try
            {
                StreamResourceInfo remoteStream = Application.GetRemoteStream(new Uri(BrowserInteropHelper.Source.Segments[BrowserInteropHelper.Source.Segments.Length - 1], UriKind.Relative));
                Func<XElement, bool> predicate = <>c.<>9__17_0 ??= xElement => (xElement.Name.LocalName == "dependentAssembly");
                Func<XElement, string> selectCondition = <>c.<>9__17_1 ??= element => element.Attribute("codebase").Value;
                using (Stream stream = remoteStream.Stream)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        entryManifestName = XmlHelper.GetElements<string>(predicate, selectCondition, reader.ReadToEnd()).Single<string>();
                    }
                }
            }
            catch
            {
                entryManifestName = EntryManifestName;
            }
            return entryManifestName;
        }

        private static string GetRelativePath()
        {
            string str = RelativeManifestName.Replace('\\', '/');
            return str.Substring(0, str.LastIndexOf('/') + 1);
        }

        internal static string[] PopulateDependentAssemblies(string manifestRelativeFileName)
        {
            StreamResourceInfo remoteStream = null;
            try
            {
                remoteStream = Application.GetRemoteStream(new Uri(manifestRelativeFileName, UriKind.Relative));
            }
            catch
            {
            }
            return (((remoteStream == null) || (remoteStream.Stream == null)) ? new string[0] : GetDependentAssemblies(remoteStream.Stream.ToStringWithDispose()));
        }

        private static string EntryManifestName =>
            AssemblyHelper.EntryAssembly.ManifestModule + ".manifest";

        private static EnvironmentStrategy Environment =>
            environmentStrategy;

        public static string RelativeManifestName
        {
            get
            {
                relativeManifestName ??= GetRelativeManifestName();
                return relativeManifestName;
            }
        }

        public static string RelativePath
        {
            get
            {
                relativePath ??= GetRelativePath();
                return relativePath;
            }
        }

        public static bool IsManifestAvailable =>
            GetDependentAssemblies().Length != 0;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ApplicationHelper.<>c <>9 = new ApplicationHelper.<>c();
            public static Func<XElement, bool> <>9__17_0;
            public static Func<XElement, string> <>9__17_1;
            public static Func<XElement, bool> <>9__23_0;
            public static Func<XElement, string> <>9__23_1;

            internal bool <GetDependentAssemblies>b__23_0(XElement xElement) => 
                xElement.Name.LocalName == "assemblyIdentity";

            internal string <GetDependentAssemblies>b__23_1(XElement element) => 
                element.Attribute("name").Value;

            internal bool <GetRelativeManifestName>b__17_0(XElement xElement) => 
                xElement.Name.LocalName == "dependentAssembly";

            internal string <GetRelativeManifestName>b__17_1(XElement element) => 
                element.Attribute("codebase").Value;
        }

        private class BrowserStrategy : ApplicationHelper.EnvironmentStrategy
        {
            public override void AddAvailableAssemblies(ref List<string> assemblies)
            {
                Add(ApplicationHelper.GetDependentAssemblies(), ref assemblies);
            }
        }

        private abstract class EnvironmentStrategy
        {
            private const string ResourceString = ".resource";

            protected EnvironmentStrategy()
            {
            }

            protected static void Add(IEnumerable<string> addingAssemblies, ref List<string> targetAssemblies)
            {
                foreach (string str in addingAssemblies)
                {
                    if (!targetAssemblies.Contains(str))
                    {
                        targetAssemblies.Add(str);
                    }
                }
            }

            public abstract void AddAvailableAssemblies(ref List<string> assemblies);
            public virtual void Initialize()
            {
            }
        }

        private class IgnoreManifestStrategy : ApplicationHelper.BrowserStrategy
        {
            public override void AddAvailableAssemblies(ref List<string> assemblies)
            {
            }
        }
    }
}

