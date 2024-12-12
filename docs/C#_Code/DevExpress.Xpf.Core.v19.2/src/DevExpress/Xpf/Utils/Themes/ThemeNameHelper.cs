namespace DevExpress.Xpf.Utils.Themes
{
    using System;
    using System.Linq;
    using System.Windows.Markup;

    public static class ThemeNameHelper
    {
        public static readonly string ThemeAssemblyPrefix = "/DevExpress.Xpf.Themes.";

        private static string EnsureThemeName(Uri baseUri)
        {
            string str2;
            try
            {
                string originalString = baseUri.OriginalString;
                if (!string.IsNullOrEmpty(originalString))
                {
                    int index = originalString.IndexOf(ThemeAssemblyPrefix);
                    if (index >= 0)
                    {
                        index += ThemeAssemblyPrefix.Length;
                        int num2 = originalString.IndexOf('.', index);
                        if (num2 > index)
                        {
                            return originalString.Substring(index, num2 - index);
                        }
                    }
                    goto TR_0000;
                }
                else
                {
                    str2 = null;
                }
            }
            catch
            {
                goto TR_0000;
            }
            return str2;
        TR_0000:
            return null;
        }

        public static string GetAssemblyName(IServiceProvider serviceProvider)
        {
            IUriContext service = (IUriContext) serviceProvider.GetService(typeof(IUriContext));
            if ((service == null) || ((service.BaseUri == null) || (!service.BaseUri.IsAbsoluteUri || !service.BaseUri.LocalPath.Contains<char>(';'))))
            {
                return null;
            }
            char[] separator = new char[] { ';' };
            return service.BaseUri.LocalPath.Split(separator)[0];
        }

        public static string GetThemeName(IServiceProvider serviceProvider)
        {
            IUriContext service = (IUriContext) serviceProvider.GetService(typeof(IUriContext));
            return (((service == null) || ((service.BaseUri == null) || !service.BaseUri.IsAbsoluteUri)) ? null : EnsureThemeName(service.BaseUri));
        }
    }
}

