namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Configuration.Internal;
    using System.Data.Common;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml;

    public static class DbEngineDetector
    {
        private static readonly string currentUserName = WindowsIdentity.GetCurrent().Name;
        private static readonly string networkServiceLocalizedName = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null).Translate(typeof(NTAccount)).Value;
        private static SqlServerDetector detector;

        private static bool ConfigureDefaultConnectionFactory(string connectionFactoryTypeName, string[] connectionFactoryArguments)
        {
            object obj2 = CreateDbConfiguration();
            if (obj2 == null)
            {
                return false;
            }
            object obj3 = Activator.CreateInstance(obj2.GetType().Assembly.GetType("System.Data.Entity.Infrastructure." + connectionFactoryTypeName), (object[]) connectionFactoryArguments);
            object[] parameters = new object[] { obj3 };
            obj2.GetType().GetMethod("SetDefaultConnectionFactory", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj2, parameters);
            object[] objArray2 = new object[] { obj2 };
            obj2.GetType().GetMethod("SetConfiguration", BindingFlags.Public | BindingFlags.Static).Invoke(null, objArray2);
            return true;
        }

        private static T ConfigureEntityFrameworkDefaultConnectionFactory<T>(Func<string, string[], T> configure)
        {
            string localDbVersion = LocalDbVersion;
            if (localDbVersion == null)
            {
                return configure("SqlConnectionFactory", new string[0]);
            }
            string[] textArray1 = new string[] { localDbVersion };
            return configure("LocalDbConnectionFactory", textArray1);
        }

        private static object CreateDbConfiguration()
        {
            Assembly entityFrameworkAssembly = GetEntityFrameworkAssembly();
            return entityFrameworkAssembly?.GetType("System.Data.Entity.DbConfiguration").GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null).Invoke(new object[0]);
        }

        private static Assembly GetEntityFrameworkAssembly() => 
            GetLoadedAssembly("EntityFramework") ?? Assembly.Load(Assembly.GetEntryAssembly().GetReferencedAssemblies().First<AssemblyName>(<>c.<>9__24_0 ??= x => (x.Name == "EntityFramework")));

        private static IEnumerable<Assembly> GetLoadedAssemblies() => 
            AppDomain.CurrentDomain.GetAssemblies();

        private static Assembly GetLoadedAssembly(string asmName)
        {
            Assembly assembly2;
            using (IEnumerator<Assembly> enumerator = GetLoadedAssemblies().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Assembly current = enumerator.Current;
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

        private static string GetPartialName(string asmName)
        {
            int index = asmName.IndexOf(',');
            return ((index < 0) ? asmName : asmName.Remove(index));
        }

        public static string[] GetSclCEInstalledVersions()
        {
            List<string> list = new List<string>();
            string str = @"SOFTWARE\Microsoft\Microsoft SQL Server Compact Edition\";
            if (RegistryViewer.Current.IsKeyExists(RegistryHive.LocalMachine, str + "v3.5"))
            {
                list.Add("v3.5");
            }
            if (RegistryViewer.Current.IsKeyExists(RegistryHive.LocalMachine, str + "v4.0"))
            {
                list.Add("v4.0");
            }
            return list.ToArray();
        }

        public static string GetSqlServerInstanceName()
        {
            string localDbVersion = LocalDbVersion;
            return (((localDbVersion == null) || IsExecutingUnderService) ? (!IsSqlExpressInstalled ? "(local)" : @".\SQLEXPRESS") : (@"(localdb)\" + localDbVersion));
        }

        private static bool PartialNameEquals(string asmName0, string asmName1) => 
            string.Equals(GetPartialName(asmName0), GetPartialName(asmName1), StringComparison.InvariantCultureIgnoreCase);

        private static void PatchAppConfigConnectionStringsAndDefaultConnectionFactory()
        {
            object obj2 = typeof(ConfigurationManager).GetNestedType("InitState", BindingFlags.NonPublic).GetEnumValues().GetValue(0);
            typeof(ConfigurationManager).GetField("s_initState", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, obj2);
            Type type = typeof(ConfigurationManager).Assembly.GetType("System.Configuration.ClientConfigurationSystem");
            typeof(ConfigurationManager).GetField("s_configSystem", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, null);
            IInternalConfigSettingsFactory factory = (IInternalConfigSettingsFactory) Activator.CreateInstance(typeof(ConfigurationManager).Assembly.GetType("System.Configuration.Internal.InternalConfigSettingsFactory"), true);
            IInternalConfigSystem system = (IInternalConfigSystem) Activator.CreateInstance(type, true);
            FieldInfo field = type.GetField("_configRoot", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(system, new ConnectionStringAndDefaultConnectionFactoryPatcherConfigRoot((IInternalConfigRoot) field.GetValue(system)));
            factory.SetConfigurationSystem(system, false);
        }

        public static string PatchConnectionString(string rawConnectionString, bool entitySyntax = false)
        {
            if (rawConnectionString.ToLower().Contains("sqlite"))
            {
                return rawConnectionString;
            }
            PatchSimpleString patchFunction = new PatchSimpleString();
            return PatchInnerString(rawConnectionString, entitySyntax, patchFunction);
        }

        public static bool PatchConnectionStringsAndConfigureEntityFrameworkDefaultConnectionFactory()
        {
            PatchAppConfigConnectionStringsAndDefaultConnectionFactory();
            return ConfigureEntityFrameworkDefaultConnectionFactory<bool>(new Func<string, string[], bool>(DbEngineDetector.ConfigureDefaultConnectionFactory));
        }

        private static string PatchInnerString(string connectionString, bool entitySyntax, PatchSimpleString patchFunction)
        {
            if (!entitySyntax)
            {
                return patchFunction.Patch(connectionString);
            }
            DbConnectionStringBuilder builder1 = new DbConnectionStringBuilder();
            builder1.ConnectionString = connectionString;
            DbConnectionStringBuilder builder = builder1;
            builder["provider connection string"] = patchFunction.Patch("");
            return builder.ConnectionString;
        }

        public static string SelectConnectionStringName(string sqlexpressName, string localdbName)
        {
            string localDbVersion = LocalDbVersion;
            return (!IsSqlExpressInstalled ? ((localDbVersion == null) ? sqlexpressName : localdbName) : sqlexpressName);
        }

        private static SqlServerDetector Detector
        {
            get
            {
                detector ??= new SqlServerDetector(Registry.LocalMachine);
                return detector;
            }
        }

        public static string LocalDbVersion =>
            Detector.GetLocalDBVersionInstalled();

        public static bool IsSqlExpressInstalled
        {
            get
            {
                string[] source = RegistryViewer.Current.GetMultiSzValue(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Microsoft SQL Server", "InstalledInstances");
                return ((source != null) && source.Contains<string>("SQLEXPRESS"));
            }
        }

        public static bool IsLocalDbInstalled =>
            !string.IsNullOrEmpty(LocalDbVersion);

        private static bool IsExecutingUnderService =>
            currentUserName.StartsWith(@"IIS APPPOOL\", StringComparison.InvariantCultureIgnoreCase) || string.Equals(currentUserName, networkServiceLocalizedName, StringComparison.InvariantCultureIgnoreCase);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DbEngineDetector.<>c <>9 = new DbEngineDetector.<>c();
            public static Func<AssemblyName, bool> <>9__24_0;

            internal bool <GetEntityFrameworkAssembly>b__24_0(AssemblyName x) => 
                x.Name == "EntityFramework";
        }

        private sealed class ConnectionStringAndDefaultConnectionFactoryPatcherConfigRecord : IInternalConfigRecord
        {
            private readonly IInternalConfigRecord internalRecord;
            private const string ConnectionStringsSectionName = "connectionStrings";
            private const string EntityFrameworkConnectionStringProviderName = "System.Data.EntityClient";
            private const string EntityFrameworkSectionName = "entityFramework";
            private ConnectionStringsSection connectionStringsSection;
            private object entityFrameworkSection;

            public ConnectionStringAndDefaultConnectionFactoryPatcherConfigRecord(IInternalConfigRecord innerRecord)
            {
                this.internalRecord = innerRecord;
            }

            private ConnectionStringsSection CreateConnectionStringsSection()
            {
                ConnectionStringsSection section2 = new ConnectionStringsSection();
                foreach (ConnectionStringSettings settings in ((ConnectionStringsSection) this.internalRecord.GetSection("connectionStrings")).ConnectionStrings.Cast<ConnectionStringSettings>())
                {
                    ConnectionStringSettings settings2 = new ConnectionStringSettings(settings.Name, DbEngineDetector.PatchConnectionString(settings.ConnectionString, settings.ProviderName == "System.Data.EntityClient"), settings.ProviderName);
                    section2.ConnectionStrings.Add(settings2);
                }
                return section2;
            }

            private object CreateEntityFrameworkSection() => 
                DbEngineDetector.ConfigureEntityFrameworkDefaultConnectionFactory<object>(delegate (string factoryTypeName, string[] factoryParameters) {
                    object section = this.internalRecord.GetSection("entityFramework");
                    Type type = section.GetType();
                    object[] parameters = new object[3];
                    parameters[1] = "entityFramework";
                    parameters[2] = 2;
                    XmlDocument document = SafeXml.CreateDocument((string) type.GetMethod("SerializeSection", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(section, parameters), null);
                    Func<XmlElement, bool> predicate = <>c.<>9__14_1;
                    if (<>c.<>9__14_1 == null)
                    {
                        Func<XmlElement, bool> local1 = <>c.<>9__14_1;
                        predicate = <>c.<>9__14_1 = x => x.Name == "defaultConnectionFactory";
                    }
                    XmlElement oldChild = document.DocumentElement.ChildNodes.OfType<XmlElement>().FirstOrDefault<XmlElement>(predicate);
                    if (oldChild != null)
                    {
                        oldChild.ParentNode.RemoveChild(oldChild);
                    }
                    XmlElement newChild = document.CreateElement("defaultConnectionFactory");
                    newChild.SetAttribute("type", $"System.Data.Entity.Infrastructure.{factoryTypeName}, EntityFramework");
                    document.DocumentElement.PrependChild(newChild);
                    if (factoryParameters.Any<string>())
                    {
                        XmlElement element3 = document.CreateElement("parameters");
                        string[] strArray = factoryParameters;
                        int index = 0;
                        while (true)
                        {
                            if (index >= strArray.Length)
                            {
                                newChild.AppendChild(element3);
                                break;
                            }
                            string str2 = strArray[index];
                            XmlElement element4 = document.CreateElement("parameter");
                            element4.SetAttribute("value", str2);
                            element3.AppendChild(element4);
                            index++;
                        }
                    }
                    MemoryStream outStream = new MemoryStream();
                    document.Save(outStream);
                    outStream.Seek(0L, SeekOrigin.Begin);
                    XmlReader reader = SafeXml.CreateReader(outStream, DtdProcessing.Prohibit, null);
                    object obj3 = Activator.CreateInstance(type, true);
                    object[] objArray2 = new object[] { reader };
                    type.GetMethod("DeserializeSection", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj3, objArray2);
                    return obj3;
                });

            object IInternalConfigRecord.GetLkgSection(string configKey)
            {
                throw new NotImplementedException();
            }

            object IInternalConfigRecord.GetSection(string configKey) => 
                (configKey == "connectionStrings") ? this.ConnectionStringSection : ((configKey == "entityFramework") ? this.EntityFrameworkSection : this.internalRecord.GetSection(configKey));

            void IInternalConfigRecord.RefreshSection(string configKey)
            {
                if (configKey == "connectionStrings")
                {
                    this.connectionStringsSection = null;
                }
                else if (configKey != "entityFramework")
                {
                    this.internalRecord.RefreshSection(configKey);
                }
                else
                {
                    this.entityFrameworkSection = null;
                }
            }

            void IInternalConfigRecord.Remove()
            {
                this.internalRecord.Remove();
            }

            void IInternalConfigRecord.ThrowIfInitErrors()
            {
                this.internalRecord.ThrowIfInitErrors();
            }

            string IInternalConfigRecord.ConfigPath =>
                this.internalRecord.ConfigPath;

            string IInternalConfigRecord.StreamName =>
                this.internalRecord.StreamName;

            bool IInternalConfigRecord.HasInitErrors =>
                this.internalRecord.HasInitErrors;

            private ConnectionStringsSection ConnectionStringSection
            {
                get
                {
                    this.connectionStringsSection ??= this.CreateConnectionStringsSection();
                    return this.connectionStringsSection;
                }
            }

            private object EntityFrameworkSection
            {
                get
                {
                    this.entityFrameworkSection ??= this.CreateEntityFrameworkSection();
                    return this.entityFrameworkSection;
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DbEngineDetector.ConnectionStringAndDefaultConnectionFactoryPatcherConfigRecord.<>c <>9 = new DbEngineDetector.ConnectionStringAndDefaultConnectionFactoryPatcherConfigRecord.<>c();
                public static Func<XmlElement, bool> <>9__14_1;

                internal bool <CreateEntityFrameworkSection>b__14_1(XmlElement x) => 
                    x.Name == "defaultConnectionFactory";
            }
        }

        private sealed class ConnectionStringAndDefaultConnectionFactoryPatcherConfigRoot : IInternalConfigRoot
        {
            private readonly IInternalConfigRoot internalRoot;

            event InternalConfigEventHandler IInternalConfigRoot.ConfigChanged
            {
                add
                {
                    throw new NotImplementedException();
                }
                remove
                {
                    throw new NotImplementedException();
                }
            }

            event InternalConfigEventHandler IInternalConfigRoot.ConfigRemoved
            {
                add
                {
                    throw new NotImplementedException();
                }
                remove
                {
                    throw new NotImplementedException();
                }
            }

            public ConnectionStringAndDefaultConnectionFactoryPatcherConfigRoot(IInternalConfigRoot internalRoot)
            {
                this.internalRoot = internalRoot;
            }

            IInternalConfigRecord IInternalConfigRoot.GetConfigRecord(string configPath)
            {
                IInternalConfigRecord configRecord = this.internalRoot.GetConfigRecord(configPath);
                return ((configRecord == null) ? null : new DbEngineDetector.ConnectionStringAndDefaultConnectionFactoryPatcherConfigRecord(configRecord));
            }

            object IInternalConfigRoot.GetSection(string section, string configPath)
            {
                throw new NotImplementedException();
            }

            string IInternalConfigRoot.GetUniqueConfigPath(string configPath)
            {
                throw new NotImplementedException();
            }

            IInternalConfigRecord IInternalConfigRoot.GetUniqueConfigRecord(string configPath)
            {
                throw new NotImplementedException();
            }

            void IInternalConfigRoot.Init(IInternalConfigHost host, bool isDesignTime)
            {
                throw new NotImplementedException();
            }

            void IInternalConfigRoot.RemoveConfig(string configPath)
            {
                throw new NotImplementedException();
            }

            bool IInternalConfigRoot.IsDesignTime
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }

        private class PatchSimpleString
        {
            public string Patch(string connectionString)
            {
                DbConnectionStringBuilder builder1 = new DbConnectionStringBuilder();
                builder1.ConnectionString = connectionString;
                DbConnectionStringBuilder builder = builder1;
                string sqlServerInstanceName = DbEngineDetector.GetSqlServerInstanceName();
                builder["Data Source"] = sqlServerInstanceName;
                if (!sqlServerInstanceName.ToUpper().Contains(@".\SQLEXPRESS"))
                {
                    builder.Remove("User Instance");
                }
                return builder.ConnectionString;
            }
        }
    }
}

