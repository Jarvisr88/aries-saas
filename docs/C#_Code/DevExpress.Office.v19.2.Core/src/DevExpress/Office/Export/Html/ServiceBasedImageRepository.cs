namespace DevExpress.Office.Export.Html
{
    using DevExpress.Office.Services;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;

    public class ServiceBasedImageRepository : IOfficeImageRepository, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private readonly string rootUri;
        private readonly string relativeUri;

        public ServiceBasedImageRepository(IServiceProvider serviceProvider, string rootUri, string relativeUri)
        {
            Guard.ArgumentNotNull(serviceProvider, "serviceProvider");
            this.serviceProvider = serviceProvider;
            this.rootUri = rootUri;
            this.relativeUri = relativeUri;
        }

        public void Dispose()
        {
        }

        public string GetImageSource(OfficeImage image, bool autoDisposeImage)
        {
            string str = string.Empty;
            IUriProviderService service = (IUriProviderService) this.serviceProvider.GetService(typeof(IUriProviderService));
            if (service != null)
            {
                str = service.CreateImageUri(this.rootUri, image, this.relativeUri);
            }
            if (autoDisposeImage)
            {
                image.Dispose();
            }
            return str;
        }
    }
}

