namespace DevExpress.Security.Resources
{
    using System;
    using System.Linq;

    public sealed class UrlAccessRule : UriAccessRule
    {
        private static readonly string[] uriSchemes = new string[] { Uri.UriSchemeHttp, Uri.UriSchemeHttps, Uri.UriSchemeFtp };
        private readonly string[] urls;

        public UrlAccessRule(AccessPermission permission) : base(permission)
        {
        }

        public UrlAccessRule(AccessPermission permission, params string[] urls) : base(permission)
        {
            this.urls = urls;
        }

        public static UrlAccessRule Allow(params string[] urls) => 
            (urls.Length != 0) ? new UrlAccessRule(AccessPermission.Allow, urls) : new UrlAccessRule(AccessPermission.Allow);

        protected override bool CheckUriCore(Uri uri)
        {
            string str;
            return (TryGetScheme(uri, out str) && ((Array.IndexOf<string>(uriSchemes, str) >= 0) && ((this.urls == null) || this.UrlsContainPath(GetAbsoluteUri(uri)))));
        }

        public static UrlAccessRule Deny(params string[] urls) => 
            (urls.Length != 0) ? new UrlAccessRule(AccessPermission.Deny, urls) : new UrlAccessRule(AccessPermission.Deny);

        private bool UrlsContainPath(string path) => 
            this.urls.Any<string>(x => path.StartsWith(x.TrimEnd(new char[] { '/' }) + "/", StringComparison.OrdinalIgnoreCase));
    }
}

