namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class ColorChangedEventArgs : RoutedEventArgs
    {
        public ColorChangedEventArgs(System.Windows.Media.Color color) : base(ColorPicker.ColorChangedEvent)
        {
            this.Color = color;
        }

        public System.Windows.Media.Color Color { get; private set; }
    }
}

