namespace DevExpress.DXTemplateGallery.Extensions
{
    using System;

    public interface IGalleryNotificationSupports
    {
        void OnInitialized(string targetName);
        void OnTargetNameChanged(string oldName, string newName);
    }
}

