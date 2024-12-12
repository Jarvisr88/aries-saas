namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Services;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public class UriStreamService : IUriStreamService
    {
        private UriStreamProviderCollection providers = new UriStreamProviderCollection();

        public UriStreamService()
        {
            this.RegisterDefaultProviders();
        }

        public Stream GetStream(string uri)
        {
            Stream stream2;
            if (string.IsNullOrEmpty(uri))
            {
                return null;
            }
            int count = this.Providers.Count;
            int num2 = 0;
            while (true)
            {
                if (num2 >= count)
                {
                    return null;
                }
                try
                {
                    Stream stream = this.Providers[num2].GetStream(uri);
                    if (stream != null)
                    {
                        stream2 = stream;
                        break;
                    }
                }
                catch
                {
                }
                num2++;
            }
            return stream2;
        }

        protected internal virtual void RegisterDefaultProviders()
        {
            this.RegisterProvider(new WebUriStreamProvider());
        }

        public void RegisterProvider(IUriStreamProvider provider)
        {
            if (provider != null)
            {
                this.Providers.Insert(0, provider);
            }
        }

        public void UnregisterProvider(IUriStreamProvider provider)
        {
            if (provider != null)
            {
                int index = this.Providers.IndexOf(provider);
                if (index >= 0)
                {
                    this.Providers.RemoveAt(index);
                }
            }
        }

        public UriStreamProviderCollection Providers =>
            this.providers;
    }
}

