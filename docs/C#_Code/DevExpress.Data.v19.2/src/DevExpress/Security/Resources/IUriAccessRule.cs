namespace DevExpress.Security.Resources
{
    using System;

    public interface IUriAccessRule : IAccessRule
    {
        bool CheckUri(Uri uri);
    }
}

