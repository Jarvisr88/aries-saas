namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class SparklinePropertyProvider : ActualPropertyProvider
    {
        public static readonly DependencyProperty HighlightMinPointProperty;
        public static readonly DependencyProperty HighlightMaxPointProperty;
        public static readonly DependencyProperty HighlightStartPointProperty;
        public static readonly DependencyProperty HighlightEndPointProperty;
        public static readonly DependencyProperty BrushProperty;
        public static readonly DependencyProperty MaxPointBrushProperty;
        public static readonly DependencyProperty MinPointBrushProperty;
        public static readonly DependencyProperty StartPointBrushProperty;
        public static readonly DependencyProperty EndPointBrushProperty;
        public static readonly DependencyProperty NegativePointBrushProperty;
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
        public static readonly DependencyProperty BarDistanceProperty;
        public static readonly DependencyProperty AreaOpacityProperty;

        static SparklinePropertyProvider()
        {
            Type ownerType = typeof(SparklinePropertyProvider);
            HighlightMinPointProperty = DependencyPropertyManager.Register("HighlightMinPoint", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HighlightMaxPointProperty = DependencyPropertyManager.Register("HighlightMaxPoint", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HighlightStartPointProperty = DependencyPropertyManager.Register("HighlightStartPoint", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HighlightEndPointProperty = DependencyPropertyManager.Register("HighlightEndPoint", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            BrushProperty = DependencyPropertyManager.Register("Brush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
            MaxPointBrushProperty = DependencyPropertyManager.Register("MaxPointBrush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
            MinPointBrushProperty = DependencyPropertyManager.Register("MinPointBrush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
            StartPointBrushProperty = DependencyPropertyManager.Register("StartPointBrush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
            EndPointBrushProperty = DependencyPropertyManager.Register("EndPointBrush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
            NegativePointBrushProperty = DependencyPropertyManager.Register("NegativePointBrush", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
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
            BarDistanceProperty = DependencyProperty.Register("BarDistance", typeof(int), ownerType, new FrameworkPropertyMetadata(2));
            AreaOpacityProperty = DependencyPropertyManager.Register("AreaOpacity", typeof(double), ownerType, new FrameworkPropertyMetadata(0.52941176470588236));
        }

        public SparklinePropertyProvider(SparklineEdit editor) : base(editor)
        {
        }

        public bool HighlightMinPoint
        {
            get => 
                (bool) base.GetValue(HighlightMinPointProperty);
            set => 
                base.SetValue(HighlightMinPointProperty, value);
        }

        public bool HighlightMaxPoint
        {
            get => 
                (bool) base.GetValue(HighlightMaxPointProperty);
            set => 
                base.SetValue(HighlightMaxPointProperty, value);
        }

        public bool HighlightStartPoint
        {
            get => 
                (bool) base.GetValue(HighlightStartPointProperty);
            set => 
                base.SetValue(HighlightStartPointProperty, value);
        }

        public bool HighlightEndPoint
        {
            get => 
                (bool) base.GetValue(HighlightEndPointProperty);
            set => 
                base.SetValue(HighlightEndPointProperty, value);
        }

        public SolidColorBrush Brush
        {
            get => 
                (SolidColorBrush) base.GetValue(BrushProperty);
            set => 
                base.SetValue(BrushProperty, value);
        }

        public SolidColorBrush MaxPointBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(MaxPointBrushProperty);
            set => 
                base.SetValue(MaxPointBrushProperty, value);
        }

        public SolidColorBrush MinPointBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(MinPointBrushProperty);
            set => 
                base.SetValue(MinPointBrushProperty, value);
        }

        public SolidColorBrush StartPointBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(StartPointBrushProperty);
            set => 
                base.SetValue(StartPointBrushProperty, value);
        }

        public SolidColorBrush EndPointBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(EndPointBrushProperty);
            set => 
                base.SetValue(EndPointBrushProperty, value);
        }

        public SolidColorBrush NegativePointBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(NegativePointBrushProperty);
            set => 
                base.SetValue(NegativePointBrushProperty, value);
        }

        public bool HighlightNegativePoints
        {
            get => 
                (bool) base.GetValue(HighlightNegativePointsProperty);
            set => 
                base.SetValue(HighlightNegativePointsProperty, value);
        }

        public bool ShowMarkers
        {
            get => 
                (bool) base.GetValue(ShowMarkersProperty);
            set => 
                base.SetValue(ShowMarkersProperty, value);
        }

        public int LineWidth
        {
            get => 
                (int) base.GetValue(LineWidthProperty);
            set => 
                base.SetValue(LineWidthProperty, value);
        }

        public int MarkerSize
        {
            get => 
                (int) base.GetValue(MarkerSizeProperty);
            set => 
                base.SetValue(MarkerSizeProperty, value);
        }

        public int MaxPointMarkerSize
        {
            get => 
                (int) base.GetValue(MaxPointMarkerSizeProperty);
            set => 
                base.SetValue(MaxPointMarkerSizeProperty, value);
        }

        public int MinPointMarkerSize
        {
            get => 
                (int) base.GetValue(MinPointMarkerSizeProperty);
            set => 
                base.SetValue(MinPointMarkerSizeProperty, value);
        }

        public int StartPointMarkerSize
        {
            get => 
                (int) base.GetValue(StartPointMarkerSizeProperty);
            set => 
                base.SetValue(StartPointMarkerSizeProperty, value);
        }

        public int EndPointMarkerSize
        {
            get => 
                (int) base.GetValue(EndPointMarkerSizeProperty);
            set => 
                base.SetValue(EndPointMarkerSizeProperty, value);
        }

        public int NegativePointMarkerSize
        {
            get => 
                (int) base.GetValue(NegativePointMarkerSizeProperty);
            set => 
                base.SetValue(NegativePointMarkerSizeProperty, value);
        }

        public SolidColorBrush MarkerBrush
        {
            get => 
                (SolidColorBrush) base.GetValue(MarkerBrushProperty);
            set => 
                base.SetValue(MarkerBrushProperty, value);
        }

        public int BarDistance
        {
            get => 
                (int) base.GetValue(BarDistanceProperty);
            set => 
                base.SetValue(BarDistanceProperty, value);
        }

        public double AreaOpacity
        {
            get => 
                (double) base.GetValue(AreaOpacityProperty);
            set => 
                base.SetValue(AreaOpacityProperty, value);
        }
    }
}

