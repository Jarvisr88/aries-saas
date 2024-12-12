namespace DevExpress.Office.Services
{
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public interface IUriProviderService
    {
        string CreateCssUri(string url, string styleText, string relativeUri);
        string CreateImageUri(string rootUri, OfficeImage image, string relativeUri);
        void RegisterProvider(IUriProvider provider);
        void UnregisterProvider(IUriProvider provider);
    }
}

