namespace System.ComponentModel
{
    using System;
    using System.Resources;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class)]
    public class ResDisplayNameAttribute : DisplayNameAttribute
    {
        public ResDisplayNameAttribute()
        {
        }

        public ResDisplayNameAttribute(Type resourceFinder, string resourceFile, string resourceName) : this(resourceFinder, resourceFile, resourceName, GetDisplayName(resourceName))
        {
        }

        public ResDisplayNameAttribute(Type resourceFinder, string resourceFile, string resourceName, string defaultDisplayName)
        {
            base.DisplayNameValue = GetResourceString(resourceFinder, resourceFile, resourceName, defaultDisplayName);
        }

        private static ResourceManager CreateResourceManager(Type resourceFinder, string resourceFileName) => 
            new ResourceManager(resourceFinder.Namespace + "." + resourceFileName, resourceFinder.Assembly);

        protected static string GetDisplayName(string resourceName)
        {
            int num = resourceName.LastIndexOf(".");
            return ((num > 0) ? resourceName.Substring(num + 1) : resourceName);
        }

        internal static string GetResourceString(Type resourceFinder, string resourceFileName, string resourceName, string defaultString)
        {
            try
            {
                string str = CreateResourceManager(resourceFinder, resourceFileName).GetString(resourceName);
                return (!string.IsNullOrEmpty(str) ? str : defaultString);
            }
            catch
            {
                return defaultString;
            }
        }
    }
}

