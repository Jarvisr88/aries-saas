namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Versioning;

    internal static class TargetFramework
    {
        public static Version GetFrameworkVersion() => 
            GetFrameworkVersion(Assembly.GetEntryAssembly());

        public static Version GetFrameworkVersion(Assembly assembly)
        {
            if (assembly == null)
            {
                return null;
            }
            object[] customAttributes = assembly.GetCustomAttributes(typeof(TargetFrameworkAttribute), false);
            if (customAttributes == null)
            {
                return null;
            }
            TargetFrameworkAttribute attribute = customAttributes.FirstOrDefault<object>() as TargetFrameworkAttribute;
            return ((attribute != null) ? new FrameworkName(attribute.FrameworkName).Version : null);
        }
    }
}

