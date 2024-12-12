namespace DevExpress.DXTemplateGallery.Extensions
{
    using System;

    public class BaseGalleryExtensionException : Exception
    {
        public BaseGalleryExtensionException() : base(string.Empty)
        {
        }

        public BaseGalleryExtensionException(string msg) : base(msg)
        {
        }
    }
}

