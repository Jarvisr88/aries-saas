namespace DevExpress.Internal
{
    using System;
    using System.Drawing;

    public interface IPredefinedToastNotificationContentGeneric
    {
        void SetAppLogoImageCrop(ImageCropType appLogoImageCrop);
        void SetAttributionText(string attributionText);
        void SetDisplayTimestamp(DateTimeOffset? displayTimestamp);
        void SetImage(Image image, ImagePlacement placement);
        void SetImage(string imagePath, ImagePlacement placement);
        void SetUpdateToastContentAction(Action<XmlDocument> updateToastContentAction);
    }
}

