namespace DevExpress.Internal
{
    using System;
    using System.Drawing;

    public interface IPredefinedToastNotificationContent : IDisposable
    {
        void SetDuration(NotificationDuration duration);
        void SetImage(byte[] image);
        void SetImage(Image image);
        void SetImage(string imagePath);
        void SetSound(PredefinedSound sound);

        IPredefinedToastNotificationInfo Info { get; }
    }
}

