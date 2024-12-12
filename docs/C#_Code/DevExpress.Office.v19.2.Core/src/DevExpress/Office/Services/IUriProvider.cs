namespace DevExpress.Office.Services
{
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public interface IUriProvider
    {
        string CreateCssUri(string rootUri, string styleText, string relativeUri);
        string CreateImageUri(string rootUri, OfficeImage image, string relativeUri);
    }
}

