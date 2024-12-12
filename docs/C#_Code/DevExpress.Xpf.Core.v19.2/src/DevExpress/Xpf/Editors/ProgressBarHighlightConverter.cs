namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class ProgressBarHighlightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double num3;
            if (values.Length != 4)
            {
                return null;
            }
            if ((values[0] == null) || !typeof(Brush).IsAssignableFrom(values[0].GetType()))
            {
                return null;
            }
            for (int i = 1; i < 4; i++)
            {
                if (!(values[i] is double))
                {
                    return null;
                }
            }
            Brush brush = (Brush) values[0];
            double d = (double) values[1];
            double num2 = (double) values[2];
            if (((double) values[3]) <= 0.0)
            {
                num3 = 1.0;
            }
            if ((d <= 0.0) || (double.IsInfinity(d) || (double.IsNaN(d) || ((num2 <= 0.0) || (double.IsInfinity(num2) || double.IsNaN(num2))))))
            {
                return null;
            }
            DrawingBrush brush2 = new DrawingBrush();
            double width = d * 2.0;
            Rect rect = new Rect(-d, 0.0, width, num2);
            brush2.Viewbox = rect;
            brush2.Viewport = rect;
            brush2.ViewportUnits = brush2.ViewboxUnits = BrushMappingMode.Absolute;
            brush2.TileMode = TileMode.None;
            brush2.Stretch = Stretch.None;
            DrawingGroup group = new DrawingGroup();
            DrawingContext context = group.Open();
            context.DrawRectangle(brush, null, new Rect(-d, 0.0, d, num2));
            TimeSpan keyTime = TimeSpan.FromSeconds((width / 200.0) / num3);
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames {
                BeginTime = new TimeSpan?(TimeSpan.Zero),
                Duration = new Duration(keyTime + TimeSpan.FromSeconds(1.0 / num3)),
                RepeatBehavior = RepeatBehavior.Forever,
                KeyFrames = { new LinearDoubleKeyFrame(width, keyTime) }
            };
            TranslateTransform transform = new TranslateTransform();
            transform.BeginAnimation(TranslateTransform.XProperty, animation);
            brush2.Transform = transform;
            context.Close();
            brush2.Drawing = group;
            return brush2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => 
            null;
    }
}

