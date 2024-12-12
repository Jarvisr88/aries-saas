namespace DevExpress.Office.Services
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public interface IUriStreamService
    {
        Stream GetStream(string url);
        void RegisterProvider(IUriStreamProvider provider);
        void UnregisterProvider(IUriStreamProvider provider);
    }
}

