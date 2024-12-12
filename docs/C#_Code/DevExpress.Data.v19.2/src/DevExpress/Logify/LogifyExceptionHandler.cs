namespace DevExpress.Logify
{
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class LogifyExceptionHandler
    {
        private static bool isWebEnvironment;
        private static LogifyExceptionHandler instance;
        private bool isInitialized;
        private bool isSuccessfullyInstalled;
        private CancelEventHandler onCanReportException;
        private Assembly logifyAssembly;
        private object logifyClientInstance;

        public event CancelEventHandler CanReportException
        {
            add
            {
                this.onCanReportException += value;
            }
            remove
            {
                this.onCanReportException -= value;
            }
        }

        public void AddDesktopTraceListener(string apiKey, string applicationName)
        {
            object obj2 = this.TryGetLogifyAlertTraceListener("DevExpress.Logify.Win.LogifyAlertTraceListener");
            if (obj2 != null)
            {
                this.SetApplicationInfo(apiKey, applicationName);
                ((TraceListener) obj2).Name = "LogifyAlertTraceListener";
                Trace.Listeners.Add((TraceListener) obj2);
            }
        }

        public void AddWebFormsTraceListener(string apiKey, string applicationName)
        {
            object obj2 = this.TryGetLogifyAlertTraceListener("DevExpress.Logify.Web.LogifyAlertTraceListener");
            if (obj2 != null)
            {
                this.SetApplicationInfo(apiKey, applicationName);
                ((TraceListener) obj2).Name = "LogifyAlertTraceListener";
                Trace.Listeners.Add((TraceListener) obj2);
            }
        }

        public bool Initialize(string logId, string lastExecptionReportFileName)
        {
            if (!this.isInitialized)
            {
                if (this.IsWebEnvironment())
                {
                    this.isSuccessfullyInstalled = this.TryInstallWebExceptionHandler(logId);
                }
                else if (this.IsDesktopEnvironment())
                {
                    this.isSuccessfullyInstalled = this.TryInstallDesktopExceptionHandler(logId, lastExecptionReportFileName);
                }
                this.isInitialized = true;
            }
            return this.isSuccessfullyInstalled;
        }

        private bool IsDesktopEnvironment() => 
            Environment.UserInteractive || (Assembly.GetEntryAssembly() != null);

        private bool IsWebEnvironment() => 
            isWebEnvironment;

        private Assembly LoadLogifyAssembly()
        {
            Assembly assembly = this.TryLoadPublicLogifyAssembly();
            return ((assembly == null) ? this.TryLoadDefaultLogifyAssembly() : assembly);
        }

        private void OnBeforeReportException(object sender, EventArgs e)
        {
            try
            {
                LogifyState.Instance.ExecuteCustomDataProviders();
            }
            catch
            {
            }
        }

        private void OnCanReportException(object sender, CancelEventArgs e)
        {
            this.RaiseCanReportException(e);
        }

        private void RaiseCanReportException(CancelEventArgs args)
        {
            if (this.onCanReportException != null)
            {
                this.onCanReportException(this, args);
            }
        }

        public bool Send(Exception ex)
        {
            bool flag;
            try
            {
                if (this.logifyClientInstance == null)
                {
                    flag = false;
                }
                else
                {
                    Type[] types = new Type[] { typeof(Exception) };
                    MethodInfo method = this.logifyClientInstance.GetType().GetMethod("Send", types);
                    if (method == null)
                    {
                        flag = false;
                    }
                    else
                    {
                        object[] parameters = new object[] { ex };
                        method.Invoke(this.logifyClientInstance, parameters);
                        flag = true;
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private void SetApplicationInfo(string apiKey, string applicationName)
        {
            if (this.logifyClientInstance != null)
            {
                PropertyInfo property = this.logifyClientInstance.GetType().BaseType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
                if (property != null)
                {
                    object obj2 = property.GetValue(null, null);
                    if (obj2 != null)
                    {
                        PropertyInfo info2 = obj2.GetType().GetProperty("ApiKey", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
                        if (info2 != null)
                        {
                            info2.SetValue(obj2, apiKey, null);
                        }
                        PropertyInfo info3 = obj2.GetType().GetProperty("AppName", BindingFlags.Public | BindingFlags.Instance);
                        if (info3 != null)
                        {
                            info3.SetValue(obj2, applicationName, null);
                        }
                    }
                }
            }
        }

        public static void SetWebEnvironment(bool webEnvironment = true)
        {
            isWebEnvironment = webEnvironment;
        }

        private void SubscribeBeforeReportExceptionEvent(Type clientType, object clientInstance)
        {
            try
            {
                EventInfo info = clientType.GetEvent("BeforeReportException");
                if (info != null)
                {
                    MethodInfo method = base.GetType().GetMethod("OnBeforeReportException", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (method != null)
                    {
                        info.AddEventHandler(clientInstance, Delegate.CreateDelegate(info.EventHandlerType, this, method));
                    }
                }
            }
            catch
            {
            }
        }

        private void SubscribeCanReportExceptionEvent(Type clientType, object clientInstance)
        {
            try
            {
                EventInfo info = clientType.GetEvent("CanReportException");
                if (info != null)
                {
                    MethodInfo method = base.GetType().GetMethod("OnCanReportException", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (method != null)
                    {
                        info.AddEventHandler(clientInstance, Delegate.CreateDelegate(info.EventHandlerType, this, method));
                    }
                }
            }
            catch
            {
            }
        }

        private object TryGetLogifyAlertTraceListener(string logifyTraceListnerTypeName)
        {
            if (this.LogifyAssembly == null)
            {
                return null;
            }
            Type type = this.LogifyAssembly.GetType(logifyTraceListnerTypeName);
            if (type == null)
            {
                return null;
            }
            return type.GetConstructor(new Type[0])?.Invoke(new object[0]);
        }

        private bool TryInstallDesktopExceptionHandler(string logId, string lastExecptionReportFileName)
        {
            bool flag;
            try
            {
                if (this.LogifyAssembly == null)
                {
                    flag = false;
                }
                else
                {
                    Type clientType = this.LogifyAssembly.GetType("DevExpress.Logify.Win.DxLogifyClient");
                    if (clientType == null)
                    {
                        flag = false;
                    }
                    else
                    {
                        ConstructorInfo constructor = clientType.GetConstructor(new Type[0]);
                        if (constructor == null)
                        {
                            flag = false;
                        }
                        else
                        {
                            object obj2 = constructor.Invoke(new object[0]);
                            if (obj2 == null)
                            {
                                flag = false;
                            }
                            else
                            {
                                this.logifyClientInstance = obj2;
                                MethodInfo method = clientType.GetMethod("Run");
                                if (method == null)
                                {
                                    flag = false;
                                }
                                else
                                {
                                    method.Invoke(obj2, new object[0]);
                                    MethodInfo info3 = clientType.GetMethod("ReportToDevExpress2");
                                    if (info3 != null)
                                    {
                                        object[] parameters = new object[] { logId, lastExecptionReportFileName, base.GetType().Assembly, LogifyState.Instance.CustomData };
                                        info3.Invoke(obj2, parameters);
                                    }
                                    else
                                    {
                                        MethodInfo info4 = clientType.GetMethod("ReportToDevExpress");
                                        if (info4 != null)
                                        {
                                            object[] parameters = new object[] { logId, lastExecptionReportFileName, base.GetType().Assembly };
                                            info4.Invoke(obj2, parameters);
                                        }
                                    }
                                    this.SubscribeCanReportExceptionEvent(clientType, obj2);
                                    this.SubscribeBeforeReportExceptionEvent(clientType, obj2);
                                    flag = true;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private bool TryInstallWebExceptionHandler(string logId)
        {
            bool flag;
            try
            {
                if (this.LogifyAssembly == null)
                {
                    flag = false;
                }
                else
                {
                    Type clientType = this.LogifyAssembly.GetType("DevExpress.Logify.Web.LogifyAlert");
                    if (clientType == null)
                    {
                        flag = false;
                    }
                    else
                    {
                        PropertyInfo property = clientType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
                        if (property == null)
                        {
                            flag = false;
                        }
                        else
                        {
                            object clientInstance = property.GetValue(null, null);
                            if (clientInstance == null)
                            {
                                flag = false;
                            }
                            else
                            {
                                this.logifyClientInstance = clientInstance;
                                this.SubscribeCanReportExceptionEvent(clientType, clientInstance);
                                this.SubscribeBeforeReportExceptionEvent(clientType, clientInstance);
                                flag = true;
                            }
                        }
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private Assembly TryLoadDefaultLogifyAssembly()
        {
            Assembly assembly;
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\DevExpress\Components\v19.2");
                if (key == null)
                {
                    assembly = null;
                }
                else
                {
                    string str = key.GetValue("RootDirectory") as string;
                    if (string.IsNullOrEmpty(str))
                    {
                        assembly = null;
                    }
                    else
                    {
                        string path = Path.Combine(str, @"System\Components\Logify\Logify.Client.Core.dll");
                        string str3 = this.IsWebEnvironment() ? Path.Combine(str, @"System\Components\Logify\Logify.Client.Web.dll") : Path.Combine(str, @"System\Components\Logify\Logify.Client.Win.dll");
                        if (!File.Exists(path) || !File.Exists(str3))
                        {
                            assembly = null;
                        }
                        else
                        {
                            Assembly.LoadFrom(path);
                            assembly = Assembly.LoadFrom(str3);
                        }
                    }
                }
            }
            catch
            {
                assembly = null;
            }
            return assembly;
        }

        private Assembly TryLoadPublicLogifyAssembly()
        {
            Assembly assembly;
            try
            {
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                if (!Directory.Exists(folderPath))
                {
                    assembly = null;
                }
                else
                {
                    folderPath = Path.Combine(folderPath, @"DevExpress\Logify19.2");
                    if (!Directory.Exists(folderPath))
                    {
                        assembly = null;
                    }
                    else
                    {
                        string path = Path.Combine(folderPath, "Logify.Client.Core.dll");
                        string str3 = this.IsWebEnvironment() ? Path.Combine(folderPath, "Logify.Client.Web.dll") : Path.Combine(folderPath, "Logify.Client.Win.dll");
                        if (!File.Exists(path) || !File.Exists(str3))
                        {
                            assembly = null;
                        }
                        else
                        {
                            Assembly.LoadFrom(path);
                            assembly = Assembly.LoadFrom(str3);
                        }
                    }
                }
            }
            catch
            {
                assembly = null;
            }
            return assembly;
        }

        public static LogifyExceptionHandler Instance
        {
            get
            {
                LogifyExceptionHandler instance;
                if (LogifyExceptionHandler.instance != null)
                {
                    return LogifyExceptionHandler.instance;
                }
                Type type = typeof(LogifyExceptionHandler);
                lock (type)
                {
                    if (LogifyExceptionHandler.instance != null)
                    {
                        instance = LogifyExceptionHandler.instance;
                    }
                    else
                    {
                        LogifyExceptionHandler.instance = new LogifyExceptionHandler();
                        instance = LogifyExceptionHandler.instance;
                    }
                }
                return instance;
            }
        }

        private Assembly LogifyAssembly
        {
            get
            {
                if (this.logifyAssembly == null)
                {
                    this.logifyAssembly = this.LoadLogifyAssembly();
                }
                return this.logifyAssembly;
            }
        }
    }
}

