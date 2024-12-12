namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Services;
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public class UriProviderService : IUriProviderService
    {
        private readonly UriProviderCollection providers = new UriProviderCollection();

        public UriProviderService(bool registerDefaultProviders = true)
        {
            if (registerDefaultProviders)
            {
                this.RegisterDefaultProviders();
            }
        }

        public string CreateCssUri(string rootUri, string styleText, string relativeUri)
        {
            int count = this.Providers.Count;
            for (int i = 0; i < count; i++)
            {
                string str = this.Providers[i].CreateCssUri(rootUri, styleText, relativeUri);
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
            }
            return string.Empty;
        }

        public string CreateImageUri(string rootUri, OfficeImage image, string relativeUri)
        {
            if (image != null)
            {
                int count = this.Providers.Count;
                for (int i = 0; i < count; i++)
                {
                    string str = this.Providers[i].CreateImageUri(rootUri, image, relativeUri);
                    if (!string.IsNullOrEmpty(str))
                    {
                        return str;
                    }
                }
            }
            return string.Empty;
        }

        protected internal virtual void RegisterDefaultProviders()
        {
            this.RegisterProvider(new FileBasedUriProvider());
        }

        public void RegisterProvider(IUriProvider provider)
        {
            if (provider != null)
            {
                this.Providers.Insert(0, provider);
            }
        }

        public void UnregisterProvider(IUriProvider provider)
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

        public UriProviderCollection Providers =>
            this.providers;
    }
}

