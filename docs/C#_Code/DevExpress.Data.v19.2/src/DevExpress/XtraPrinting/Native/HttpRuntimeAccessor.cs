namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Reflection;

    public static class HttpRuntimeAccessor
    {
        public static readonly string AppDomainAppVirtualPath;

        static HttpRuntimeAccessor()
        {
            if (SystemWebAssemblyLoader.SystemWeb != null)
            {
                try
                {
                    AppDomainAppVirtualPath = (string) SystemWebAssemblyLoader.SystemWeb.GetType("System.Web.HttpRuntime").GetProperty("AppDomainAppVirtualPath", BindingFlags.Public | BindingFlags.Static).GetValue(null, new object[0]);
                }
                catch
                {
                }
            }
        }
    }
}

