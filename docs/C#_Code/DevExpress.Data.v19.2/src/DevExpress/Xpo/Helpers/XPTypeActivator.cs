namespace DevExpress.Xpo.Helpers
{
    using DevExpress.Data.Utils;
    using System;
    using System.Reflection;

    public static class XPTypeActivator
    {
        public const string AssemblyFoundExceptionDataKey = "AssemblyFound";
        public const string AssemblyLoadedExceptionDataKey = "AssemblyLoaded";

        public static void AuxRegistrationInvoker(string auxAssembly, string auxClass, string registerPublicStaticParameterlessMethodName)
        {
            try
            {
                Type type = GetType(auxAssembly, auxClass);
                if (type != null)
                {
                    MethodInfo method = type.GetMethod(registerPublicStaticParameterlessMethodName, new Type[0]);
                    if (method != null)
                    {
                        method.Invoke(null, null);
                    }
                }
            }
            catch
            {
            }
        }

        private static TypeLoadException CreateTypeLoadException(string message, bool assemblyFound, bool assemblyLoaded, Exception innerException)
        {
            TypeLoadException exception = (innerException == null) ? new TypeLoadException(message) : new TypeLoadException(message, innerException);
            exception.Data["AssemblyFound"] = assemblyFound;
            exception.Data["AssemblyLoaded"] = assemblyLoaded;
            return exception;
        }

        private static Assembly GetAssemblyFromAppDomain(string assemblyName)
        {
            string str = assemblyName + ",";
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = assemblies.Length - 1; i >= 0; i--)
            {
                Assembly assembly = assemblies[i];
                if (assembly.FullName.StartsWith(str))
                {
                    return assembly;
                }
            }
            return null;
        }

        public static Type GetType(string assemblyName, string typeName) => 
            GetType(assemblyName, typeName, false);

        public static Type GetType(string assemblyName, string typeName, bool throwException)
        {
            Assembly assemblyFromAppDomain = GetAssemblyFromAppDomain(assemblyName);
            if (!throwException)
            {
                if (assemblyFromAppDomain == null)
                {
                    assemblyFromAppDomain = Helpers.LoadWithPartialName(assemblyName);
                }
                return assemblyFromAppDomain?.GetType(typeName);
            }
            if (assemblyFromAppDomain == null)
            {
                try
                {
                    assemblyFromAppDomain = Helpers.LoadWithPartialName(assemblyName, true);
                }
                catch (BadImageFormatException exception)
                {
                    throw CreateTypeLoadException($"Could not load assembly '{exception.FileName}'. See InnerException for details.", true, false, exception);
                }
                catch (Exception exception2)
                {
                    throw CreateTypeLoadException($"Could not load assembly '{assemblyName}'. See InnerException for details.", true, false, exception2);
                }
            }
            if (assemblyFromAppDomain == null)
            {
                throw CreateTypeLoadException($"Could not find assembly '{assemblyName}'.", false, false, null);
            }
            Type type = assemblyFromAppDomain.GetType(typeName);
            if (type == null)
            {
                throw CreateTypeLoadException($"Type '{typeName}' is not found in assembly '{assemblyFromAppDomain.Location}'.", true, true, null);
            }
            return type;
        }
    }
}

