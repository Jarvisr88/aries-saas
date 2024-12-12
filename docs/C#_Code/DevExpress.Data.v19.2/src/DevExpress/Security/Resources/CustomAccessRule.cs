namespace DevExpress.Security.Resources
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public sealed class CustomAccessRule : UriAccessRule
    {
        public event Func<Uri, bool> CheckUri;

        public CustomAccessRule(AccessPermission permission) : base(permission)
        {
        }

        protected override bool CheckUriCore(Uri uri) => 
            (this.CheckUri != null) ? this.CheckUri(uri) : false;
    }
}

