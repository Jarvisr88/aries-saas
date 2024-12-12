namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public static class ExportSettingDefaultValue
    {
        public static readonly DevExpress.Xpf.Printing.TargetType TargetType = DevExpress.Xpf.Printing.TargetType.None;
        public static readonly Color Background = Colors.Transparent;
        public static readonly Color Foreground = Colors.Black;
        public static readonly Color BorderColor = Colors.Black;
        public static readonly Thickness BorderThickness = new Thickness(0.0);
        public static readonly string Url = string.Empty;
        public static readonly IOnPageUpdater OnPageUpdater = null;
        public static readonly object MergeValue = null;
        public static readonly System.Windows.FlowDirection FlowDirection = System.Windows.FlowDirection.LeftToRight;
        public static readonly bool? BooleanValue = null;
        public static readonly string CheckText = null;
        public static readonly System.Windows.Media.ImageSource ImageSource = null;
        public static readonly string PageNumberFormat = string.Empty;
        public static readonly DevExpress.Xpf.Printing.PageNumberKind PageNumberKind = DevExpress.Xpf.Printing.PageNumberKind.NumberOfTotal;
        public static readonly System.Windows.HorizontalAlignment HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
        public static readonly System.Windows.VerticalAlignment VerticalAlignment = System.Windows.VerticalAlignment.Top;
        public static readonly string Text = null;
        public static readonly object TextValue = null;
        public static readonly string TextValueFormatString = string.Empty;
        public static readonly System.Windows.Media.FontFamily FontFamily = new System.Windows.Media.FontFamily("Arial");
        public static readonly System.Windows.FontStyle FontStyle = FontStyles.Normal;
        public static readonly System.Windows.FontWeight FontWeight = FontWeights.Normal;
        public static readonly double FontSize = 10.0;
        public static readonly Thickness Padding = new Thickness(0.0);
        public static readonly System.Windows.TextWrapping TextWrapping = System.Windows.TextWrapping.Wrap;
        public static readonly bool NoTextExport = false;
        public static readonly bool? XlsExportNativeFormat = null;
        public static readonly string XlsxFormatString = null;
        public static readonly DevExpress.Xpf.Printing.ImageRenderMode ImageRenderMode = DevExpress.Xpf.Printing.ImageRenderMode.MakeScreenshot;
        public static readonly object ImageKey = null;
        public static readonly DevExpress.XtraPrinting.BorderDashStyle BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
        public static readonly TextDecorationCollection TextDecorations = new TextDecorationCollection();
        public static readonly System.Windows.TextTrimming TextTrimming = System.Windows.TextTrimming.None;
        public static readonly int ProgressBarPosition = 0;
        public static readonly int TrackBarPosition = 0;
        public static readonly int TrackBarMinimum = 0;
        public static readonly int TrackBarMaximum = 0;
    }
}

