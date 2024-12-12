namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Media;

    public class LineSparklineControl : SparklineControl, ISupportNegativePointsControl
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
        private const bool highlightShowNegativePoints = false;
        private const bool defaultShowMarkers = false;
        private const int defaultLineWidth = 1;
        private const int defaultMarkerSize = 5;
        private const int defaultMaxPointMarkerSize = 5;
        private const int defaultMinPointMarkerSize = 5;
        private const int defaultStartPointMarkerSize = 5;
        private const int defaultEndPointMarkerSize = 5;
        private const int defaultNegativePointMarkerSize = 5;
        private bool highlightNegativePoints;
        private bool showMarkers;
        private int lineWidth = 1;
        private int markerSize = 5;
        private int maxPointMarkerSize = 5;
        private int minPointMarkerSize = 5;
        private int startPointMarkerSize = 5;
        private int endPointMarkerSize = 5;
        private int negativePointMarkerSize = 5;
        private SolidColorBrush markerBrush = new SolidColorBrush(Colors.Black);

        static LineSparklineControl()
        {
            LineWidthProperty = DependencyProperty.Register("LineWidth", typeof(int), typeof(LineSparklineControl), new FrameworkPropertyMetadata(1, (d, e) => ((LineSparklineControl) d).OnLineWidthChanged((int) e.NewValue)));
            HighlightNegativePointsProperty = DependencyProperty.Register("HighlightNegativePoints", typeof(bool), typeof(LineSparklineControl), new FrameworkPropertyMetadata(false, (d, e) => ((LineSparklineControl) d).OnHighlightNegativePointsChanged((bool) e.NewValue)));
            ShowMarkersProperty = DependencyProperty.Register("ShowMarkers", typeof(bool), typeof(LineSparklineControl), new FrameworkPropertyMetadata(false, (d, e) => ((LineSparklineControl) d).OnShowMarkersChanged((bool) e.NewValue)));
            MarkerSizeProperty = DependencyProperty.Register("MarkerSize", typeof(int), typeof(LineSparklineControl), new FrameworkPropertyMetadata(5, (d, e) => ((LineSparklineControl) d).OnMarkerSizeChanged((int) e.NewValue)));
            MaxPointMarkerSizeProperty = DependencyProperty.Register("MaxPointMarkerSize", typeof(int), typeof(LineSparklineControl), new FrameworkPropertyMetadata(5, (d, e) => ((LineSparklineControl) d).OnMaxPointMarkerSizeChanged((int) e.NewValue)));
            MinPointMarkerSizeProperty = DependencyProperty.Register("MinPointMarkerSize", typeof(int), typeof(LineSparklineControl), new FrameworkPropertyMetadata(5, (d, e) => ((LineSparklineControl) d).OnMinPointMarkerSizeChanged((int) e.NewValue)));
            StartPointMarkerSizeProperty = DependencyProperty.Register("StartPointMarkerSize", typeof(int), typeof(LineSparklineControl), new FrameworkPropertyMetadata(5, (d, e) => ((LineSparklineControl) d).OnStartPointMarkerSizeChanged((int) e.NewValue)));
            EndPointMarkerSizeProperty = DependencyProperty.Register("EndPointMarkerSize", typeof(int), typeof(LineSparklineControl), new FrameworkPropertyMetadata(5, (d, e) => ((LineSparklineControl) d).OnEndPointMarkerSizeChanged((int) e.NewValue)));
            NegativePointMarkerSizeProperty = DependencyProperty.Register("NegativePointMarkerSize", typeof(int), typeof(LineSparklineControl), new FrameworkPropertyMetadata(5, (d, e) => ((LineSparklineControl) d).OnNegativePointMarkerSizeChanged((int) e.NewValue)));
            MarkerBrushProperty = DependencyProperty.Register("MarkerBrush", typeof(SolidColorBrush), typeof(LineSparklineControl), new FrameworkPropertyMetadata(null, (d, e) => ((LineSparklineControl) d).OnMarkerBrushChanged((SolidColorBrush) e.NewValue)));
        }

        public LineSparklineControl()
        {
            base.DefaultStyleKey = typeof(LineSparklineControl);
        }

        public override void Assign(SparklineControl view)
        {
            base.Assign(view);
            LineSparklineControl control = view as LineSparklineControl;
            if (control != null)
            {
                this.showMarkers = control.showMarkers;
                this.lineWidth = control.lineWidth;
                this.markerSize = control.markerSize;
                this.maxPointMarkerSize = control.maxPointMarkerSize;
                this.minPointMarkerSize = control.minPointMarkerSize;
                this.startPointMarkerSize = control.startPointMarkerSize;
                this.endPointMarkerSize = control.endPointMarkerSize;
                this.negativePointMarkerSize = control.negativePointMarkerSize;
                this.markerBrush = control.markerBrush;
            }
            ISupportNegativePointsControl control2 = view as ISupportNegativePointsControl;
            if (control2 != null)
            {
                this.highlightNegativePoints = control2.HighlightNegativePoints;
            }
        }

        protected internal override BaseSparklinePainter CreatePainter() => 
            new LineSparklinePainter();

        protected override Padding GetMarkersPadding()
        {
            double actualLineWidth = this.ActualLineWidth;
            if (base.ActualHighlightStartPoint)
            {
                actualLineWidth = Math.Max(actualLineWidth, (double) this.ActualStartPointMarkerSize);
            }
            if (base.ActualHighlightEndPoint)
            {
                actualLineWidth = Math.Max(actualLineWidth, (double) this.ActualEndPointMarkerSize);
            }
            if (base.ActualHighlightMaxPoint)
            {
                actualLineWidth = Math.Max(actualLineWidth, (double) this.ActualMaxPointMarkerSize);
            }
            if (base.ActualHighlightMinPoint)
            {
                actualLineWidth = Math.Max(actualLineWidth, (double) this.ActualMinPointMarkerSize);
            }
            if (this.ActualHighlightNegativePoints)
            {
                actualLineWidth = Math.Max(actualLineWidth, (double) this.ActualNegativePointMarkerSize);
            }
            if (this.ActualShowMarkers)
            {
                actualLineWidth = Math.Max(actualLineWidth, (double) this.ActualMarkerSize);
            }
            return new Padding((int) Math.Ceiling((double) (0.5 * actualLineWidth)));
        }

        protected override string GetViewName() => 
            EditorLocalizer.GetString(EditorStringId.SparklineViewLine);

        private void OnEndPointMarkerSizeChanged(int endPointMarkerSize)
        {
            this.endPointMarkerSize = endPointMarkerSize;
            base.PropertyChanged();
        }

        private void OnHighlightNegativePointsChanged(bool highlightNegativePoints)
        {
            this.highlightNegativePoints = highlightNegativePoints;
            base.PropertyChanged();
        }

        private void OnLineWidthChanged(int lineWidth)
        {
            this.lineWidth = lineWidth;
            base.PropertyChanged();
        }

        private void OnMarkerBrushChanged(SolidColorBrush markerBrush)
        {
            this.markerBrush = markerBrush;
            base.PropertyChanged();
        }

        private void OnMarkerSizeChanged(int markerSize)
        {
            this.markerSize = markerSize;
            base.PropertyChanged();
        }

        private void OnMaxPointMarkerSizeChanged(int maxPointMarkerSize)
        {
            this.maxPointMarkerSize = maxPointMarkerSize;
            base.PropertyChanged();
        }

        private void OnMinPointMarkerSizeChanged(int minPointMarkerSize)
        {
            this.minPointMarkerSize = minPointMarkerSize;
            base.PropertyChanged();
        }

        private void OnNegativePointMarkerSizeChanged(int negativePointMarkerSize)
        {
            this.negativePointMarkerSize = negativePointMarkerSize;
            base.PropertyChanged();
        }

        private void OnShowMarkersChanged(bool showMarkers)
        {
            this.showMarkers = showMarkers;
            base.PropertyChanged();
        }

        private void OnStartPointMarkerSizeChanged(int startPointMarkerSize)
        {
            this.startPointMarkerSize = startPointMarkerSize;
            base.PropertyChanged();
        }

        public void SetSizeForAllMarkers(int markerSize)
        {
            this.MarkerSize = markerSize;
            this.MaxPointMarkerSize = markerSize;
            this.MinPointMarkerSize = markerSize;
            this.StartPointMarkerSize = markerSize;
            this.EndPointMarkerSize = markerSize;
            this.NegativePointMarkerSize = markerSize;
        }

        protected internal override bool ActualShowNegativePoint =>
            this.ActualHighlightNegativePoints;

        internal SolidColorBrush ActualMarkerBrush =>
            this.markerBrush;

        internal int ActualLineWidth =>
            this.lineWidth;

        internal bool ActualHighlightNegativePoints =>
            this.highlightNegativePoints;

        internal bool ActualShowMarkers =>
            this.showMarkers;

        internal int ActualMarkerSize =>
            this.markerSize;

        internal int ActualMaxPointMarkerSize =>
            this.maxPointMarkerSize;

        internal int ActualMinPointMarkerSize =>
            this.minPointMarkerSize;

        internal int ActualStartPointMarkerSize =>
            this.startPointMarkerSize;

        internal int ActualEndPointMarkerSize =>
            this.endPointMarkerSize;

        internal int ActualNegativePointMarkerSize =>
            this.negativePointMarkerSize;

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

        public override SparklineViewType Type =>
            SparklineViewType.Line;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LineSparklineControl.<>c <>9 = new LineSparklineControl.<>c();

            internal void <.cctor>b__99_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LineSparklineControl) d).OnLineWidthChanged((int) e.NewValue);
            }

            internal void <.cctor>b__99_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LineSparklineControl) d).OnHighlightNegativePointsChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__99_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LineSparklineControl) d).OnShowMarkersChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__99_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LineSparklineControl) d).OnMarkerSizeChanged((int) e.NewValue);
            }

            internal void <.cctor>b__99_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LineSparklineControl) d).OnMaxPointMarkerSizeChanged((int) e.NewValue);
            }

            internal void <.cctor>b__99_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LineSparklineControl) d).OnMinPointMarkerSizeChanged((int) e.NewValue);
            }

            internal void <.cctor>b__99_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LineSparklineControl) d).OnStartPointMarkerSizeChanged((int) e.NewValue);
            }

            internal void <.cctor>b__99_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LineSparklineControl) d).OnEndPointMarkerSizeChanged((int) e.NewValue);
            }

            internal void <.cctor>b__99_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LineSparklineControl) d).OnNegativePointMarkerSizeChanged((int) e.NewValue);
            }

            internal void <.cctor>b__99_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LineSparklineControl) d).OnMarkerBrushChanged((SolidColorBrush) e.NewValue);
            }
        }
    }
}

