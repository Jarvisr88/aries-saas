namespace DevExpress.Xpf.Utils.Native
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Resources;

    public class ResourceLocator
    {
        private Stream resourceStream;
        private readonly WebClient webClient;
        private readonly Uri uri;

        public event EventHandler<ResourceLoadedEventArgs> Loaded;

        public event EventHandler<ResourceLoadingProgressEventArgs> LoadingProgressChanged;

        public ResourceLocator(Uri uri)
        {
            Guard.ArgumentNotNull(uri, "uri");
            this.uri = uri;
            this.webClient = new WebClient();
            this.webClient.Headers.Add(HttpRequestHeader.UserAgent, "DevExpress WPF Resource Locator");
            this.webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.OnWebClientDownloadProgressChanged);
            this.webClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(this.OnWebClientDownloadDataCompleted);
        }

        public Stream LoadStreamSync() => 
            !this.TryGetStreamFromApp() ? new MemoryStream(this.webClient.DownloadData(this.uri)) : this.resourceStream;

        private void OnWebClientDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                this.resourceStream = new MemoryStream(e.Result);
            }
            this.RaiseLoaded(ReferenceEquals(e.Error, null));
        }

        private void OnWebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (this.LoadingProgressChanged != null)
            {
                this.LoadingProgressChanged(this, new ResourceLoadingProgressEventArgs(e.BytesReceived, e.TotalBytesToReceive));
            }
        }

        private void RaiseLoaded(bool loaded)
        {
            if (this.Loaded != null)
            {
                this.Loaded(this, new ResourceLoadedEventArgs(loaded, this.resourceStream));
            }
        }

        private bool TryGetStreamFromApp()
        {
            StreamResourceInfo resourceStream;
            try
            {
                resourceStream = Application.GetResourceStream(this.uri);
            }
            catch
            {
                return false;
            }
            if (resourceStream == null)
            {
                return false;
            }
            this.resourceStream = resourceStream.Stream;
            return true;
        }
    }
}

