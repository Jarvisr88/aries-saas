namespace DevExpress.Xpf.Editors
{
    using DevExpress.Utils.Design;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;

    public class LineSparklineStyleSettings : SparklineStyleSettings
    {
        public static readonly DependencyProperty LineWidthProperty;
        public static readonly DependencyProperty HighlightNegativePointsProperty;
        public static readonly DependencyProperty ShowMarkersProperty;
        public static readonly DependencyProperty MarkerSizeProperty;
        public static readonly DependencyProperty MaxPointMarkerSizeProperty;
        public static readonly DependencyProperty MinPointMarkerSizeProperty;
        public static readonly DependencyProperty StartPointMarkerSizeProperty;
        public static readonly DependencyProperty EndPointMarkerSizeProperty;
        public static readonly DependencyProperty NegativePointMarkerSizeProperty;
        public static readonly DependencyProperty MarkerBrushProperty;

        static LineSparklineStyleSettings()
        {
            Type ownerType = typeof(LineSparklineStyleSettings);
            LineWidthProperty = DependencyProperty.Register("LineWidth", typeof(int), ownerType, new FrameworkPropertyMetadata(1));
            HighlightNegativePointsProperty = DependencyProperty.Register("HighlightNegativePoints", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            ShowMarkersProperty = DependencyProperty.Register("ShowMarkers", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            MarkerSizeProperty = DependencyProperty.Register("MarkerSize", typeof(int), ownerType, new FrameworkPropertyMetadata(5));
            MaxPointMarkerSizeProperty = DependencyProperty.Register("MaxPointMarkerSize", typeof(int), ownerType, new FrameworkPropertyMetadata(5));
            MinPointMarkerSizeProperty = DependencyProperty.Register("MinPointMarkerSize", typeof(int), ownerType, new FrameworkPropertyMetadata(5));
            StartPointMarkerSizeProperty = DependencyProperty.Register("StartPointMarkerSize", typeof(int), ownerType, new FrameworkPropertyMetadata(5));
            EndPointMarkerSizeProperty = DependencyProperty.Register("EndPointMarkerSize", typeof(int), ownerType, new FrameworkPropertyMetadata(5));
            NegativePointMarkerSizeProperty = DependencyProperty.Register("NegativePointMarkerSize", typeof(int), ownerType, new FrameworkPropertyMetadata(5));
            MarkerBrushProperty = DependencyProperty.Register("MarkerBrush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            SolidColorBrush markerBrush;
            base.ApplyToEdit(editor);
            SparklinePropertyProvider propertyProvider = (SparklinePropertyProvider) editor.PropertyProvider;
            propertyProvider.LineWidth = this.LineWidth;
            propertyProvider.HighlightNegativePoints = this.HighlightNegativePoints;
            propertyProvider.ShowMarkers = this.ShowMarkers;
            propertyProvider.MarkerSize = this.MarkerSize;
            propertyProvider.MaxPointMarkerSize = this.MaxPointMarkerSize;
            propertyProvider.MinPointMarkerSize = this.MinPointMarkerSize;
            propertyProvider.StartPointMarkerSize = this.StartPointMarkerSize;
            propertyProvider.EndPointMarkerSize = this.EndPointMarkerSize;
            propertyProvider.NegativePointMarkerSize = this.NegativePointMarkerSize;
            if (this.IsPropertySet(MarkerBrushProperty))
            {
                markerBrush = this.MarkerBrush;
            }
            else
            {
                SparklineEditThemeKeyExtension resourceKey = new SparklineEditThemeKeyExtension();
                resourceKey.ResourceKey = SparklineEditThemeKeys.MarkerBrush;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(editor);
                markerBrush = base.FindResource(resourceKey) as SolidColorBrush;
            }
            propertyProvider.MarkerBrush = markerBrush;
        }

        protected override SparklineViewType ViewType =>
            SparklineViewType.Line;

        [TypeConverter(typeof(BooleanTypeConverter)), Category("Behavior")]
        public bool HighlightNegativePoints
        {
            get => 
                (bool) base.GetValue(HighlightNegativePointsProperty);
            set => 
                base.SetValue(HighlightNegativePointsProperty, value);
        }

        [Category("Behavior"), TypeConverter(typeof(BooleanTypeConverter))]
        public bool ShowMarkers
        {
            get => 
                (bool) base.GetValue(ShowMarkersProperty);
            set => 
                base.SetValue(ShowMarkersProperty, value);
        }

        [Category("Appearance")]
        public int LineWidth
        {
            get => 
                (int) base.GetValue(LineWidthProperty);
            set => 
                base.SetValue(LineWidthProperty, value);
        }

        [Category("Appearance")]
        public int MarkerSize
        {
            get => 
                (int) base.GetValue(MarkerSizeProperty);
            set => 
                base.SetValue(MarkerSizeProperty, value);
        }

        [Category("Appearance")]
        public int MaxPointMarkerSize
        {
            get => 
                (int) base.GetValue(MaxPointMarkerSizeProperty);
            set => 
                base.SetValue(MaxPointMarkerSizeProperty, value);
        }

        [Category("Appearance")]
        public int MinPointMarkerSize
        {
            get => 
                (int) base.GetValue(MinPointMarkerSizeProperty);
            set => 
                base.SetValue(MinPointMarkerSizeProperty, value);
        }

        [Category("Appearance")]
        public int StartPointMarkerSize
        {
            get => 
                (int) base.GetValue(StartPointMarkerSizeProperty);
            set => 
                base.SetValue(StartPointMarkerSizeProperty, value);
        }

        [Category("Appearance")]
        public int EndPointMarkerSize
        {
            get => 
                (int) base.GetValue(EndPointMarkerSizeProperty);
            set => 
                base.SetValue(EndPointMarkerSizeProperty, value);
        }

        [Category("Appearance")]
        public int NegativePointMarkerSize
        {
            get => 
                (int) base.GetValue(NegativePointMarkerSizeProperty);
            set => 
                base.SetValue(NegativePointMarkerSizeProperty, value);
        }

        [Category("Appearance")]
        public SolidColorBrush MarkerBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(MarkerBrushProperty);
            set => 
                base.SetValue(MarkerBrushProperty, value);
        }
    }
}

