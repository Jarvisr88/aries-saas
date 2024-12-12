namespace DevExpress.Office.Services
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public interface IUriStreamProvider
    {
        Stream GetStream(string uri);
    }
}

