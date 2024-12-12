namespace DevExpress.Utils
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class UrlResolverExtensions
    {
        private static string[] schemes = new string[] { Uri.UriSchemeHttps, Uri.UriSchemeHttp, Uri.UriSchemeFtp, Uri.UriSchemeFile };

        public static string MapPath(this UrlResolver resolver, string url, string defaultDirectory)
        {
            Uri result = null;
            string str;
            return ((!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out result) || (!result.IsAbsoluteUri || !schemes.Contains<string>(result.Scheme))) ? (!resolver.TryMapPath(url, out str) ? ((string.IsNullOrEmpty(defaultDirectory) || (string.IsNullOrEmpty(url) || !url.StartsWith("~"))) ? url : Path.Combine(defaultDirectory, UrlResolver.TrimUrl(url))) : str) : url);
        }
    }
}

