namespace DevExpress.Xpf.Office.Internal
{
    using System;
    using System.Drawing.Printing;
    using System.Resources;

    public static class OfficeLocalizationHelper
    {
        public static ResourceManager CreateResourceManager(Type resourceFinder)
        {
            string str = "PropertyNamesRes";
            return new ResourceManager(resourceFinder.Namespace + "." + str, resourceFinder.Assembly);
        }

        public static string GetPaperKindString(ResourceManager resourceManager, PaperKind value)
        {
            string fullName = "System.Drawing.Printing." + value.GetType().Name;
            return GetString(resourceManager, fullName, value);
        }

        public static string GetString(ResourceManager resourceManager, string fullName, object value)
        {
            string name = fullName + "." + value;
            return (resourceManager.GetString(name) ?? value.ToString());
        }
    }
}

