namespace DevExpress.Utils.Url
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class HttpUrlResolver : UrlResolver
    {
        private MethodInfo mapPathMethod;

        private bool CanMapPathCore(string url) => 
            (this.Server != null) && (!string.IsNullOrEmpty(url) && (url.StartsWith("~") || (url.StartsWith("/") || !base.IsAbsolutePath(url))));

        public static string ResolveUrl(string url)
        {
            if (!string.IsNullOrEmpty(url) && (url.StartsWith("~") && PSNativeMethods.HasHttpContext))
            {
                char[] trimChars = new char[] { '/' };
                url = HttpRuntimeAccessor.AppDomainAppVirtualPath.TrimEnd(trimChars) + "/" + TrimUrl(url);
            }
            return url;
        }

        public override bool TryMapPath(string url, out string value)
        {
            if (this.CanMapPathCore(url))
            {
                return (this.TryMapPathCore(url, out value) || this.TryResolveUrl(url, out value));
            }
            value = string.Empty;
            return false;
        }

        private bool TryMapPathCore(string url, out string value)
        {
            try
            {
                object[] parameters = new object[] { url };
                value = (string) this.MapPathMethod.Invoke(this.Server, parameters);
                if (File.Exists(value))
                {
                    return true;
                }
            }
            catch
            {
            }
            value = string.Empty;
            return false;
        }

        private bool TryResolveUrl(string url, out string value)
        {
            if (this.Url != null)
            {
                string str = ResolveUrl(url);
                value = this.Url.GetLeftPart(UriPartial.Authority) + "/" + TrimUrl(str);
                if (File.Exists(value))
                {
                    return true;
                }
            }
            value = string.Empty;
            return false;
        }

        private MethodInfo MapPathMethod
        {
            get
            {
                if (this.mapPathMethod == null)
                {
                    this.mapPathMethod = this.Server.GetType().GetMethod("MapPath", BindingFlags.Public | BindingFlags.Instance);
                }
                return this.mapPathMethod;
            }
        }

        protected virtual object Server =>
            HttpContextAccessor.Server;

        protected virtual Uri Url =>
            HttpContextAccessor.Url;
    }
}

