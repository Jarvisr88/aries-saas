namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Services;
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public class WebUriStreamProvider : IUriStreamProvider
    {
        protected virtual WebRequest CreateWebRequest(string uri) => 
            WebRequest.Create(uri);

        public Stream GetStream(string uri) => 
            new WebUriStreamCreator(this.CreateWebRequest(uri)).CreateStream();
    }
}

