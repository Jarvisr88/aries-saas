namespace DevExpress.Security.Resources
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public abstract class UriAccessRule : IUriAccessRule, IAccessRule
    {
        private readonly AccessPermission permission;

        protected UriAccessRule(AccessPermission permission)
        {
            this.permission = permission;
        }

        protected abstract bool CheckUriCore(Uri uri);
        bool IUriAccessRule.CheckUri(Uri uri) => 
            this.CheckUriCore(uri);

        protected static string GetAbsoluteUri(Uri uri) => 
            uri.IsAbsoluteUri ? uri.AbsoluteUri : uri.OriginalString;

        protected static string GetLocalPath(Uri uri) => 
            uri.IsAbsoluteUri ? uri.LocalPath : uri.OriginalString;

        protected static bool TryGetScheme(Uri uri, out string scheme)
        {
            if (uri.IsAbsoluteUri)
            {
                scheme = uri.Scheme;
                return true;
            }
            if (Path.IsPathRooted(uri.OriginalString))
            {
                scheme = Uri.UriSchemeFile;
                return true;
            }
            scheme = string.Empty;
            return false;
        }

        AccessPermission IAccessRule.Permission =>
            this.permission;
    }
}

