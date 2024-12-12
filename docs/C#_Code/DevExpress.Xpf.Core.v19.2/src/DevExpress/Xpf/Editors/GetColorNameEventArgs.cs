namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class GetColorNameEventArgs : RoutedEventArgs
    {
        public GetColorNameEventArgs(System.Windows.Media.Color color, string colorName)
        {
            this.Color = color;
            this.ColorName = colorName;
        }

        public System.Windows.Media.Color Color { get; private set; }

        public string ColorName { get; set; }
    }
}

