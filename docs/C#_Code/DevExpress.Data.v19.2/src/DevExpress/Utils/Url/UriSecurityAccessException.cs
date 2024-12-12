namespace DevExpress.Utils.Url
{
    using System;
    using System.Runtime.CompilerServices;

    public class UriSecurityAccessException : Exception
    {
        public UriSecurityAccessException(string uri) : base(string.Format(CommonLocalizer.GetString(CommonStringId.UriSecurityAccessExceptionMessage), uri, "AccessSettings"))
        {
            this.Uri = uri;
        }

        public string Uri { get; private set; }
    }
}

