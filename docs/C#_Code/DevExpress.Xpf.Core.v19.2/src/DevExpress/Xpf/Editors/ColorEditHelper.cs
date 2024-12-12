namespace DevExpress.Xpf.Editors
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class ColorEditHelper
    {
        public static ImageSource CreateGlyph(System.Windows.Media.Color color, System.Windows.Media.Brush borderBrush, System.Windows.Size size)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            context.DrawRectangle(new SolidColorBrush(color), new System.Windows.Media.Pen(borderBrush, 2.0), new Rect(0.0, 0.0, size.Width, size.Height));
            context.Close();
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int) size.Width, (int) size.Height, 96.0, 96.0, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            return bitmap;
        }

        public static System.Windows.Media.Color GetColorFromValue(object value) => 
            !(value is System.Windows.Media.Color) ? ColorEdit.EmptyColor : ((System.Windows.Media.Color) value);

        public static string GetColorName(System.Windows.Media.Color color, ColorDisplayFormat format, bool includeAlpha = true)
        {
            string colorName = ColorEditDefaultColors.GetColorName(color);
            if (!string.IsNullOrEmpty(colorName) && (format == ColorDisplayFormat.Default))
            {
                return colorName;
            }
            if (format != ColorDisplayFormat.ARGB)
            {
                return (string) Text2ColorHelper.Convert(color, includeAlpha);
            }
            if (!includeAlpha)
            {
                return $"{color.R},{color.G},{color.B}";
            }
            return $"{color.A},{color.R},{color.G},{color.B}";
        }

        private static string GetDefaultColorName(System.Windows.Media.Color color)
        {
            string key;
            using (Dictionary<string, System.Drawing.Color>.Enumerator enumerator = DXColor.PredefinedColors.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<string, System.Drawing.Color> current = enumerator.Current;
                        System.Drawing.Color color2 = current.Value;
                        if (!System.Windows.Media.Color.AreClose(System.Windows.Media.Color.FromArgb(current.Value.A, current.Value.R, current.Value.G, color2.B), color))
                        {
                            continue;
                        }
                        key = current.Key;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return key;
        }

        public static void ShowColorChooserDialog(IColorEdit owner)
        {
            string str = EditorLocalizer.GetString(EditorStringId.ColorEdit_ColorChooserWindowTitle);
            ColorChooser chooser1 = new ColorChooser();
            chooser1.Color = owner.Color;
            chooser1.ColorMode = owner.ColorMode;
            chooser1.FlowDirection = ((FrameworkElement) owner).FlowDirection;
            chooser1.Width = 300.0;
            chooser1.ShowAlphaChannel = owner.ShowAlphaChannel;
            ColorChooser chooser = chooser1;
            FrameworkElement element = (FrameworkElement) owner;
            ThemedWindow window1 = new ThemedWindow();
            window1.Title = str;
            window1.Content = chooser;
            window1.SizeToContent = SizeToContent.WidthAndHeight;
            Window window2 = Window.GetWindow((FrameworkElement) owner);
            ThemedWindow window3 = window1;
            Window activeWindow = window2;
            if (window2 == null)
            {
                Window local1 = window2;
                activeWindow = WindowUtility.GetActiveWindow();
            }
            window3.Owner = activeWindow;
            ThemedWindow local2 = window3;
            local2.WindowStyle = WindowStyle.ToolWindow;
            local2.UseLayoutRounding = element.UseLayoutRounding;
            ThemedWindow window = local2;
            window.WindowStartupLocation = (window.Owner != null) ? WindowStartupLocation.CenterOwner : WindowStartupLocation.CenterScreen;
            MessageBoxResult? defaultButton = null;
            if (window.ShowDialog(MessageBoxButton.OKCancel, defaultButton) == MessageBoxResult.OK)
            {
                owner.AddCustomColor(chooser.Color);
                owner.ColorMode = chooser.ColorMode;
            }
        }
    }
}

