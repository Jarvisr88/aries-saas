namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows.Media;

    public static class BrushConverter
    {
        public static BrushType GetBrushType(object editValue, BrushType brushType)
        {
            if (brushType != BrushType.AutoDetect)
            {
                return brushType;
            }
            Type type = editValue?.GetType();
            return ((type != null) ? (!(type == typeof(SolidColorBrush)) ? (!(type == typeof(LinearGradientBrush)) ? (!(type == typeof(RadialGradientBrush)) ? BrushType.SolidColorBrush : BrushType.RadialGradientBrush) : BrushType.LinearGradientBrush) : BrushType.SolidColorBrush) : BrushType.None);
        }

        public static Brush ToBrushType(object brush, BrushType brushType) => 
            (brushType != BrushType.None) ? ((brushType != BrushType.SolidColorBrush) ? ((brushType != BrushType.LinearGradientBrush) ? ((brushType != BrushType.RadialGradientBrush) ? (brush as Brush) : brush.ToRadialGradientBrush()) : brush.ToLinearGradientBrush()) : brush.ToSolidColorBrush()) : null;
    }
}

