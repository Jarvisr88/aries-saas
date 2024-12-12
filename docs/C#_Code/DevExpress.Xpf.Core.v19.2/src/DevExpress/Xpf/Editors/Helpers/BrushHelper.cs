namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public static class BrushHelper
    {
        public static LinearGradientBrush ToLinearGradientBrush(this object value)
        {
            if (value is LinearGradientBrush)
            {
                return (LinearGradientBrush) value;
            }
            Color white = Colors.White;
            if (value is Color)
            {
                white = (Color) value;
            }
            else if (value is SolidColorBrush)
            {
                white = ((SolidColorBrush) value).Color;
            }
            GradientStopCollection gradientStopCollection = new GradientStopCollection();
            RadialGradientBrush brush = value as RadialGradientBrush;
            if (brush != null)
            {
                gradientStopCollection = brush.GradientStops ?? new GradientStopCollection();
            }
            if (gradientStopCollection.Count == 0)
            {
                gradientStopCollection.Add(new GradientStop(white, 0.0));
                gradientStopCollection.Add(new GradientStop(Colors.Black, 1.0));
            }
            else if (gradientStopCollection.Count == 1)
            {
                gradientStopCollection.Add(new GradientStop(Colors.Black, 1.0));
            }
            LinearGradientBrush brush2 = new LinearGradientBrush(gradientStopCollection);
            if (brush != null)
            {
                brush2.ColorInterpolationMode = brush.ColorInterpolationMode;
                brush2.MappingMode = brush.MappingMode;
                brush2.SpreadMethod = brush.SpreadMethod;
            }
            return brush2;
        }

        public static RadialGradientBrush ToRadialGradientBrush(this object value)
        {
            if (value is RadialGradientBrush)
            {
                return (RadialGradientBrush) value;
            }
            Color white = Colors.White;
            if (value is Color)
            {
                white = (Color) value;
            }
            else if (value is SolidColorBrush)
            {
                white = ((SolidColorBrush) value).Color;
            }
            GradientStopCollection gradientStopCollection = new GradientStopCollection();
            LinearGradientBrush brush = value as LinearGradientBrush;
            if (brush != null)
            {
                gradientStopCollection = brush.GradientStops ?? new GradientStopCollection();
            }
            if (gradientStopCollection.Count == 0)
            {
                gradientStopCollection.Add(new GradientStop(white, 0.0));
                gradientStopCollection.Add(new GradientStop(Colors.Black, 1.0));
            }
            else if (gradientStopCollection.Count == 1)
            {
                gradientStopCollection.Add(new GradientStop(Colors.Black, 1.0));
            }
            RadialGradientBrush brush2 = new RadialGradientBrush(gradientStopCollection);
            if (brush != null)
            {
                brush2.ColorInterpolationMode = brush.ColorInterpolationMode;
                brush2.MappingMode = brush.MappingMode;
                brush2.SpreadMethod = brush.SpreadMethod;
            }
            return brush2;
        }

        public static SolidColorBrush ToSolidColorBrush(this object value)
        {
            if (value is Color)
            {
                return new SolidColorBrush((Color) value);
            }
            if (value is SolidColorBrush)
            {
                return (SolidColorBrush) value;
            }
            if (!(value is GradientBrush))
            {
                return new SolidColorBrush(Text2ColorHelper.DefaultColor);
            }
            Func<GradientStopCollection, bool> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<GradientStopCollection, bool> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.Count > 0;
            }
            Func<GradientStopCollection, Color> func2 = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                Func<GradientStopCollection, Color> local2 = <>c.<>9__0_1;
                func2 = <>c.<>9__0_1 = x => x.First<GradientStop>().Color;
            }
            return new SolidColorBrush(((GradientBrush) value).GradientStops.If<GradientStopCollection>(evaluator).Return<GradientStopCollection, Color>(func2, <>c.<>9__0_2 ??= () => Text2ColorHelper.DefaultColor));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BrushHelper.<>c <>9 = new BrushHelper.<>c();
            public static Func<GradientStopCollection, bool> <>9__0_0;
            public static Func<GradientStopCollection, Color> <>9__0_1;
            public static Func<Color> <>9__0_2;

            internal bool <ToSolidColorBrush>b__0_0(GradientStopCollection x) => 
                x.Count > 0;

            internal Color <ToSolidColorBrush>b__0_1(GradientStopCollection x) => 
                x.First<GradientStop>().Color;

            internal Color <ToSolidColorBrush>b__0_2() => 
                Text2ColorHelper.DefaultColor;
        }
    }
}

