namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm.UI;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class PredefinedToastNotificationVewModel
    {
        public NotificationTemplate ToastTemplate { get; set; }

        public string Text1 { get; set; }

        public string Text2 { get; set; }

        public string Text3 { get; set; }

        public ImageSource Image { get; set; }

        public ImageSource Icon { get; set; }

        public Color BackgroundColor { get; set; }
    }
}

