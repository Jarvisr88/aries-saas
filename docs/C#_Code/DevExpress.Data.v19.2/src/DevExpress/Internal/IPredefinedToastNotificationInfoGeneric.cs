namespace DevExpress.Internal
{
    using System;

    public interface IPredefinedToastNotificationInfoGeneric
    {
        string AppLogoImagePath { get; }

        string HeroImagePath { get; }

        ImageCropType AppLogoImageCrop { get; }

        string AttributionText { get; }

        DateTimeOffset? DisplayTimestamp { get; }

        Action<XmlDocument> UpdateToastContent { get; set; }
    }
}

