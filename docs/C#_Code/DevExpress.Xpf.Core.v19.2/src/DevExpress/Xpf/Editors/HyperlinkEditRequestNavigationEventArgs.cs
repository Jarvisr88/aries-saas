namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class HyperlinkEditRequestNavigationEventArgs : RoutedEventArgs
    {
        public HyperlinkEditRequestNavigationEventArgs(object value, string navigationUrl)
        {
            this.Value = value;
            this.NavigationUrl = navigationUrl;
        }

        public object Value { get; private set; }

        public string NavigationUrl { get; set; }

        public bool Cancel { get; set; }
    }
}

