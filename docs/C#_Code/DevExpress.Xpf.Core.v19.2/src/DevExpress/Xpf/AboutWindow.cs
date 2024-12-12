namespace DevExpress.Xpf
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class AboutWindow : Window
    {
        public AboutWindow()
        {
            base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            base.SizeToContent = SizeToContent.WidthAndHeight;
            base.ResizeMode = ResizeMode.NoResize;
            base.ShowActivated = true;
            base.WindowStyle = WindowStyle.None;
            base.AllowsTransparency = true;
            base.Background = Brushes.Transparent;
            base.ShowInTaskbar = false;
            base.Topmost = true;
        }
    }
}

