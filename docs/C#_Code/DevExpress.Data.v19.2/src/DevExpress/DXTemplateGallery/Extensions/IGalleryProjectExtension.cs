namespace DevExpress.DXTemplateGallery.Extensions
{
    using System;

    public interface IGalleryProjectExtension : IGalleryExtension
    {
        void DoWork(object dte, object solution, string projectName);
    }
}

