namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Internal;
    using DevExpress.Internal.WinApi.Windows.UI.Notifications;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Media.Imaging;

    internal class WpfPredefinedToastNotificationContent : IPredefinedToastNotificationContent, IDisposable
    {
        private const int msDefaultDuration = 0x1770;
        private const int msLongDuration = 0x61a8;

        public WpfPredefinedToastNotificationContent(PredefinedToastNotificationVewModel viewModel)
        {
            this.ViewModel = viewModel;
            this.Duration = 0x1770;
        }

        public void Dispose()
        {
        }

        public void SetDuration(NotificationDuration duration)
        {
            this.Duration = (duration == NotificationDuration.Long) ? 0x61a8 : 0x1770;
        }

        public void SetImage(byte[] image)
        {
            BitmapImage image2 = new BitmapImage();
            image2.BeginInit();
            image2.StreamSource = new MemoryStream(image);
            image2.EndInit();
            this.ViewModel.Image = image2;
        }

        public void SetImage(Image image)
        {
            throw new NotImplementedException();
        }

        public void SetImage(string path)
        {
            throw new NotImplementedException();
        }

        public void SetSound(PredefinedSound sound)
        {
            PredefinedSound sound1 = sound;
        }

        public PredefinedToastNotificationVewModel ViewModel { get; private set; }

        public int Duration { get; set; }

        public IPredefinedToastNotificationInfo Info =>
            new WpfTToastNotificationInfo();

        private class WpfTToastNotificationInfo : IPredefinedToastNotificationInfo
        {
            public DevExpress.Internal.WinApi.Windows.UI.Notifications.ToastTemplateType ToastTemplateType { get; set; }

            public string[] Lines { get; set; }

            public string ImagePath { get; set; }

            public NotificationDuration Duration { get; set; }

            public PredefinedSound Sound { get; set; }
        }
    }
}

