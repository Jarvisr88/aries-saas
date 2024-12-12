namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm.UI;
    using System;
    using System.Reflection;

    internal static class ViewLocatorHelper
    {
        private const string ViewLocatorTypeName = "DevExpress.Mvvm.UI.ViewLocator";
        private static PropertyInfo ViewLocatorDefaultProperty;

        public static IViewLocator Default
        {
            get
            {
                if (ViewLocatorDefaultProperty == null)
                {
                    ViewLocatorDefaultProperty = DynamicAssemblyHelper.XpfCoreAssembly.GetType("DevExpress.Mvvm.UI.ViewLocator").GetProperty("Default", BindingFlags.Public | BindingFlags.Static);
                }
                return (IViewLocator) ViewLocatorDefaultProperty.GetValue(null, null);
            }
        }
    }
}

