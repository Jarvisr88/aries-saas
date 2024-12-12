namespace DevExpress.DataAccess.Native
{
    using DevExpress.Data.Entity;
    using DevExpress.Data.Localization;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class RuntimeConnectionStringsProvider : IConnectionStringsProvider, IConnectionConfigProvider
    {
        private static object config;

        static RuntimeConnectionStringsProvider()
        {
            CreateDefault = () => new RuntimeConnectionStringsProvider();
        }

        public IConnectionStringInfo[] GetConfigFileConnections()
        {
            int count = ConfigurationManager.ConnectionStrings.Count;
            IConnectionStringInfo[] infoArray = new IConnectionStringInfo[count];
            for (int i = 0; i < count; i++)
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[i];
                ConnectionStringInfo info1 = new ConnectionStringInfo();
                info1.Name = settings.Name;
                info1.RunTimeConnectionString = settings.ConnectionString;
                info1.ProviderName = settings.ProviderName;
                infoArray[i] = info1;
            }
            return infoArray;
        }

        public virtual string GetConnectionConfigPath() => 
            string.Empty;

        public IConnectionStringInfo[] GetConnections() => 
            new IConnectionStringInfo[0];

        public string GetConnectionString(string connectionStringName)
        {
            IConnectionStringInfo connectionStringInfo = this.GetConnectionStringInfo(connectionStringName);
            return connectionStringInfo?.RunTimeConnectionString;
        }

        public virtual IConnectionStringInfo GetConnectionStringInfo(string connectionStringName)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().FirstOrDefault<ConnectionStringSettings>(ci => ci.Name == connectionStringName);
            if (settings != null)
            {
                ConnectionStringInfo info1 = new ConnectionStringInfo();
                info1.Name = settings.Name;
                info1.RunTimeConnectionString = settings.ConnectionString;
                info1.ProviderName = settings.ProviderName;
                return info1;
            }
            IConnectionStringInfo info = this.TryLoadFromJsonFile(connectionStringName);
            if (info == null)
            {
                throw new InvalidOperationException(string.Format(CommonLocalizer.GetString(CommonStringId.ConnectionNotFoundInConfig_ExceptionMessage), connectionStringName));
            }
            return info;
        }

        public static IConnectionStringInfo[] GetConnectionStringInfos() => 
            CreateDefault().GetConfigFileConnections();

        private IConnectionStringInfo TryLoadFromJsonFile(string connectionStringName)
        {
            try
            {
                object obj1;
                Type type = Assembly.Load("Microsoft.Extensions.Configuration.Abstractions")?.GetType("Microsoft.Extensions.Configuration.ConfigurationExtensions", false);
                if (type == null)
                {
                    obj1 = null;
                }
                else
                {
                    MethodInfo method = type.GetMethod("GetConnectionString");
                    if (method == null)
                    {
                        MethodInfo local1 = method;
                        obj1 = null;
                    }
                    else
                    {
                        object[] parameters = new object[] { Config, connectionStringName };
                        obj1 = method.Invoke(null, parameters);
                    }
                }
                object obj2 = obj1;
                if (obj2 != null)
                {
                    ConnectionStringInfo info2 = new ConnectionStringInfo();
                    info2.Name = obj2.ToString();
                    info2.RunTimeConnectionString = obj2.ToString();
                    return info2;
                }
            }
            catch
            {
            }
            return null;
        }

        public static Func<IConnectionStringsProvider> CreateDefault { get; set; }

        private static object Config
        {
            get
            {
                if (config == null)
                {
                    string str;
                    if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json")))
                    {
                        object obj1;
                        object obj7;
                        object obj8;
                        Assembly assembly4 = null;
                        try
                        {
                            assembly4 = Assembly.Load("Microsoft.Extensions.PlatformAbstractions");
                        }
                        catch
                        {
                        }
                        Type type3 = assembly4?.GetType("Microsoft.Extensions.PlatformAbstractions.PlatformServices");
                        Type type4 = assembly4?.GetType("Microsoft.Extensions.PlatformAbstractions.ApplicationEnvironment");
                        if (type3 == null)
                        {
                            obj1 = null;
                        }
                        else
                        {
                            PropertyInfo property = type3.GetProperty("Default");
                            if (property != null)
                            {
                                obj1 = property.GetValue(null);
                            }
                            else
                            {
                                PropertyInfo local2 = property;
                                obj1 = null;
                            }
                        }
                        object obj3 = obj1;
                        if (obj3 == null)
                        {
                            return null;
                        }
                        if (type3 == null)
                        {
                            obj7 = null;
                        }
                        else
                        {
                            PropertyInfo property = type3.GetProperty("Application");
                            if (property != null)
                            {
                                obj7 = property.GetValue(obj3);
                            }
                            else
                            {
                                PropertyInfo local3 = property;
                                obj7 = null;
                            }
                        }
                        object obj4 = obj7;
                        if (obj4 == null)
                        {
                            return null;
                        }
                        if (type4 == null)
                        {
                            obj8 = null;
                        }
                        else
                        {
                            PropertyInfo property = type4.GetProperty("ApplicationBasePath");
                            if (property != null)
                            {
                                obj8 = property.GetValue(obj4);
                            }
                            else
                            {
                                PropertyInfo local4 = property;
                                obj8 = null;
                            }
                        }
                        object obj5 = obj8;
                        if (string.IsNullOrWhiteSpace(obj5?.ToString()))
                        {
                            return null;
                        }
                        str = Path.Combine(obj5?.ToString(), "appsettings.json");
                        if (!File.Exists(str))
                        {
                            return null;
                        }
                    }
                    Assembly assembly = null;
                    Assembly assembly2 = null;
                    Assembly assembly3 = null;
                    try
                    {
                        assembly = Assembly.Load("Microsoft.Extensions.Configuration.Json");
                        assembly2 = Assembly.Load("Microsoft.Extensions.Configuration");
                        assembly3 = Assembly.Load("Microsoft.Extensions.Configuration.Abstractions");
                    }
                    catch
                    {
                        return null;
                    }
                    Type type = assembly2?.GetType("Microsoft.Extensions.Configuration.ConfigurationBuilder");
                    if (type == null)
                    {
                        return null;
                    }
                    object obj2 = Activator.CreateInstance(type);
                    if (obj2 == null)
                    {
                        return null;
                    }
                    Type type2 = assembly3?.GetType("Microsoft.Extensions.Configuration.IConfigurationBuilder");
                    if (type2 == null)
                    {
                        return null;
                    }
                    if (assembly != null)
                    {
                        Type type1 = assembly.GetType("Microsoft.Extensions.Configuration.JsonConfigurationExtensions");
                        if (type1 == null)
                        {
                            Type local6 = type1;
                        }
                        else
                        {
                            Type[] types = new Type[] { type2, typeof(string), typeof(bool) };
                            MethodInfo method = type1.GetMethod("AddJsonFile", types);
                            if (method == null)
                            {
                                MethodInfo local7 = method;
                            }
                            else
                            {
                                object[] parameters = new object[] { obj2, str, true };
                                method.Invoke(null, parameters);
                            }
                        }
                    }
                    config = type2?.GetMethod("Build")?.Invoke(obj2, null);
                }
                return config;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RuntimeConnectionStringsProvider.<>c <>9 = new RuntimeConnectionStringsProvider.<>c();

            internal IConnectionStringsProvider <.cctor>b__0_0() => 
                new RuntimeConnectionStringsProvider();
        }
    }
}

