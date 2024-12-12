namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Media;

    public class FormatConditionRuleBase : IFormatConditionRuleBase
    {
        public static System.Drawing.Color GetColor(System.Windows.Media.Color color) => 
            System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

        public static System.Drawing.Color GetColor(System.Windows.Media.Brush brush, bool isPositiveBrush = true)
        {
            if (brush is SolidColorBrush)
            {
                return GetColor(((SolidColorBrush) brush).Color);
            }
            LinearGradientBrush brush2 = brush as LinearGradientBrush;
            return (((brush2 == null) || (brush2.GradientStops.Count <= 0)) ? System.Drawing.Color.Empty : (!isPositiveBrush ? GetColor(brush2.GradientStops.Last<GradientStop>().Color) : GetColor(brush2.GradientStops.First<GradientStop>().Color)));
        }

        public virtual bool IsValid =>
            true;

        internal Type ColumnType { get; set; }
    }
}

