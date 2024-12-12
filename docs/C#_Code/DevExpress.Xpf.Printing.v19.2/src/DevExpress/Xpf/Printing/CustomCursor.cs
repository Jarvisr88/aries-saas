namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public abstract class CustomCursor : ContentControl
    {
        public CustomCursor()
        {
            Image image1 = new Image();
            image1.Source = this.ImageSource;
            image1.Stretch = Stretch.None;
            base.Content = image1;
        }

        private static string GetUriString(string cursorFilePath, string rootNamespace) => 
            $"/{rootNamespace}{".v19.2"};component/{cursorFilePath}";

        protected static BitmapImage LoadCursorFile(string cursorFilePath, string rootNamespace)
        {
            cursorFilePath = GetUriString(cursorFilePath, rootNamespace);
            UriKind relative = UriKind.Relative;
            return new BitmapImage(new Uri(cursorFilePath, relative));
        }

        public abstract System.Windows.Media.ImageSource ImageSource { get; }

        public abstract Point HotSpot { get; }
    }
}

