namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public interface IColorEdit
    {
        event RoutedEventHandler ColorChanged;

        void AddCustomColor(System.Windows.Media.Color color);

        System.Windows.Media.Color Color { get; set; }

        System.Windows.Media.Color DefaultColor { get; set; }

        ColorPickerColorMode ColorMode { get; set; }

        object EditValue { get; set; }

        CircularList<System.Windows.Media.Color> RecentColors { get; }

        PaletteCollection Palettes { get; set; }

        bool ShowAlphaChannel { get; set; }
    }
}

