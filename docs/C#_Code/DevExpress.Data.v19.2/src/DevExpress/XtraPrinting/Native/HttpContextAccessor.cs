namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Reflection;

    public static class HttpContextAccessor
    {
        private static readonly PropertyInfo property;
        private static object currentServer;

        static HttpContextAccessor()
        {
            if (SystemWebAssemblyLoader.SystemWeb != null)
            {
                try
                {
                    Type type = SystemWebAssemblyLoader.SystemWeb.GetType("System.Web.HttpContext");
                    if (type != null)
                    {
                        property = type.GetProperty("Current", BindingFlags.Public | BindingFlags.Static);
                        currentServer = GetPropertyValue(Current, "Server");
                    }
                }
                catch
                {
                }
            }
        }

        private static object GetPropertyValue(object obj, string name) => 
            obj?.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance).GetValue(obj, new object[0]);

        public static void StaticInitialize()
        {
        }

        public static object Current
        {
            get
            {
                try
                {
                    return property?.GetValue(null, new object[0]);
                }
                catch
                {
                    return null;
                }
            }
        }

        public static object Server
        {
            get
            {
                object propertyValue = GetPropertyValue(Current, "Server");
                if ((propertyValue != null) && (propertyValue != currentServer))
                {
                    currentServer = propertyValue;
                }
                return currentServer;
            }
        }

        public static object Request =>
            GetPropertyValue(Current, "Request");

        public static Uri Url =>
            GetPropertyValue(Request, "Url") as Uri;
    }
}

