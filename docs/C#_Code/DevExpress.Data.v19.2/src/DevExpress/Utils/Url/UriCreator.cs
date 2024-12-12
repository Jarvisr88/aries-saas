namespace DevExpress.Utils.Url
{
    using DevExpress.Security.Resources;
    using System;
    using System.Runtime.InteropServices;

    public static class UriCreator
    {
        public static Uri CreateUri(string path, AccessSettings settings)
        {
            Uri uri = null;
            return ((string.IsNullOrEmpty(path) || !TryCreateUri(path, out uri)) ? null : (settings.CheckRules<IUriAccessRule>(x => x.CheckUri(uri)) ? uri : null));
        }

        private static bool TryCreateUri(string path, out Uri uri)
        {
            if (TryCreateUriCore(path, out uri))
            {
                return true;
            }
            PlatformID platform = Environment.OSVersion.Platform;
            return (((platform == PlatformID.Unix) || (platform == PlatformID.MacOSX)) ? (UnixPath.IsFullyQualified(path) && TryCreateUriCore("file://" + path, out uri)) : false);
        }

        private static bool TryCreateUriCore(string path, out Uri uri) => 
            Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out uri) && UriHasScheme(uri);

        private static bool UriHasScheme(Uri uri) => 
            uri.IsAbsoluteUri && !string.IsNullOrEmpty(uri.Scheme);

        public static Uri ValidateUriSecurity(string path, AccessSettings settings)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            Uri uri = CreateUri(path, settings);
            if (uri == null)
            {
                throw new UriSecurityAccessException(path);
            }
            return uri;
        }
    }
}

