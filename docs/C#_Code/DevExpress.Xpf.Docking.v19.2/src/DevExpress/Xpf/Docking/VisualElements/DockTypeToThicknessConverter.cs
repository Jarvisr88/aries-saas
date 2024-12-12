namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [ValueConversion(typeof(Dock), typeof(Thickness))]
    public class DockTypeToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Dock dock = (Dock) value;
            Thickness thickness = new Thickness(this.Inverted ? 0.0 : this.All);
            switch (dock)
            {
                case Dock.Left:
                    if (this.Inverted)
                    {
                        thickness.Right = this.All;
                    }
                    else
                    {
                        thickness.Left = 0.0;
                    }
                    break;

                case Dock.Top:
                    if (this.Inverted)
                    {
                        thickness.Bottom = this.All;
                    }
                    else
                    {
                        thickness.Top = 0.0;
                    }
                    break;

                case Dock.Right:
                    if (this.Inverted)
                    {
                        thickness.Left = this.All;
                    }
                    else
                    {
                        thickness.Right = 0.0;
                    }
                    break;

                case Dock.Bottom:
                    if (this.Inverted)
                    {
                        thickness.Top = this.All;
                    }
                    else
                    {
                        thickness.Bottom = 0.0;
                    }
                    break;

                default:
                    break;
            }
            return thickness;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness thickness = (Thickness) value;
            double num = this.Inverted ? this.All : 0.0;
            Dock dock = this.Inverted ? Dock.Right : Dock.Left;
            if (thickness.Top == num)
            {
                dock = this.Inverted ? Dock.Bottom : Dock.Top;
            }
            if (thickness.Right == num)
            {
                dock = this.Inverted ? Dock.Left : Dock.Right;
            }
            if (thickness.Bottom == num)
            {
                dock = this.Inverted ? Dock.Top : Dock.Bottom;
            }
            return dock;
        }

        public double All { get; set; }

        public bool Inverted { get; set; }
    }
}

