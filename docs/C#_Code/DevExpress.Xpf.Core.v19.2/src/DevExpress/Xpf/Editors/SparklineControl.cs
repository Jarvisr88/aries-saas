namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Points")]
    public abstract class SparklineControl : System.Windows.Controls.Control, ISparklineRangeContainer, IRangeContainer
    {
        public static readonly DependencyProperty PointsProperty;
        public static readonly DependencyProperty HighlightMaxPointProperty;
        public static readonly DependencyProperty HighlightMinPointProperty;
        public static readonly DependencyProperty HighlightStartPointProperty;
        public static readonly DependencyProperty HighlightEndPointProperty;
        public static readonly DependencyProperty BrushProperty;
        public static readonly DependencyProperty MaxPointBrushProperty;
        public static readonly DependencyProperty MinPointBrushProperty;
        public static readonly DependencyProperty StartPointBrushProperty;
        public static readonly DependencyProperty EndPointBrushProperty;
        public static readonly DependencyProperty NegativePointBrushProperty;
        public static readonly DependencyProperty AutoRangeProperty;
        public static readonly DependencyProperty ArgumentRangeProperty;
        public static readonly DependencyProperty ValueRangeProperty;
        private const bool defaultAutoRange = true;
        private const bool defaultHighlightMaxPoint = false;
        private const bool defaultHighlightMinPoint = false;
        private const bool defaultHighlightStartPoint = false;
        private const bool defaultHighlightEndPoint = false;
        private SparklinePointCollection actualPoints;
        private bool highlightMaxPoint;
        private bool highlightMinPoint;
        private bool highlightStartPoint;
        private bool highlightEndPoint;
        private SolidColorBrush defaultBrush = new SolidColorBrush(Colors.Black);
        private SolidColorBrush brush;
        private SolidColorBrush maxPointBrush = new SolidColorBrush(Colors.Black);
        private SolidColorBrush minPointBrush = new SolidColorBrush(Colors.Black);
        private SolidColorBrush startPointBrush = new SolidColorBrush(Colors.Black);
        private SolidColorBrush endPointBrush = new SolidColorBrush(Colors.Black);
        private SolidColorBrush negativePointBrush = new SolidColorBrush(Colors.Black);
        private BaseSparklinePainter painter;
        private SparklinePointCollection randomPoints;
        private SparklinePointArgumentComparer argumentComparer;
        private RangeDirector rangeDirector;
        private ExtremePointIndexes extremeIndexes;

        public event SparklineArgumentRangeChangedEventHandler SparklineArgumentRangeChanged;

        public event SparklinePointsChangedEventHandler SparklinePointsChanged;

        public event SparklineValueRangeChangedEventHandler SparklineValueRangeChanged;

        static SparklineControl()
        {
            PointsProperty = DependencyProperty.Register("Points", typeof(SparklinePointCollection), typeof(SparklineControl), new FrameworkPropertyMetadata(null, (d, e) => ((SparklineControl) d).OnPointsPropertyChanged((SparklinePointCollection) e.NewValue)));
            HighlightMaxPointProperty = DependencyProperty.Register("HighlightMaxPoint", typeof(bool), typeof(SparklineControl), new FrameworkPropertyMetadata(false, (d, e) => ((SparklineControl) d).OnHighlightMaxPointChanged((bool) e.NewValue)));
            HighlightMinPointProperty = DependencyProperty.Register("HighlightMinPoint", typeof(bool), typeof(SparklineControl), new FrameworkPropertyMetadata(false, (d, e) => ((SparklineControl) d).OnHighlightMinPointChanged((bool) e.NewValue)));
            HighlightStartPointProperty = DependencyProperty.Register("HighlightStartPoint", typeof(bool), typeof(SparklineControl), new FrameworkPropertyMetadata(false, (d, e) => ((SparklineControl) d).OnHighlightStartPointChanged((bool) e.NewValue)));
            HighlightEndPointProperty = DependencyProperty.Register("HighlightEndPoint", typeof(bool), typeof(SparklineControl), new FrameworkPropertyMetadata(false, (d, e) => ((SparklineControl) d).OnHighlightEndPointChanged((bool) e.NewValue)));
            BrushProperty = DependencyProperty.Register("Brush", typeof(SolidColorBrush), typeof(SparklineControl), new FrameworkPropertyMetadata(null, (d, e) => ((SparklineControl) d).OnBrushChanged((SolidColorBrush) e.NewValue)));
            MaxPointBrushProperty = DependencyProperty.Register("MaxPointBrush", typeof(SolidColorBrush), typeof(SparklineControl), new FrameworkPropertyMetadata(null, (d, e) => ((SparklineControl) d).OnMaxPointBrushChanged((SolidColorBrush) e.NewValue)));
            MinPointBrushProperty = DependencyProperty.Register("MinPointBrush", typeof(SolidColorBrush), typeof(SparklineControl), new FrameworkPropertyMetadata(null, (d, e) => ((SparklineControl) d).OnMinPointBrushChanged((SolidColorBrush) e.NewValue)));
            StartPointBrushProperty = DependencyProperty.Register("StartPointBrush", typeof(SolidColorBrush), typeof(SparklineControl), new FrameworkPropertyMetadata(null, (d, e) => ((SparklineControl) d).OnStartPointBrushChanged((SolidColorBrush) e.NewValue)));
            EndPointBrushProperty = DependencyProperty.Register("EndPointBrush", typeof(SolidColorBrush), typeof(SparklineControl), new FrameworkPropertyMetadata(null, (d, e) => ((SparklineControl) d).OnEndPointBrushChanged((SolidColorBrush) e.NewValue)));
            NegativePointBrushProperty = DependencyProperty.Register("NegativePointBrush", typeof(SolidColorBrush), typeof(SparklineControl), new FrameworkPropertyMetadata(null, (d, e) => ((SparklineControl) d).OnNegativePointBrushChanged((SolidColorBrush) e.NewValue)));
            AutoRangeProperty = DependencyProperty.Register("AutoRange", typeof(bool), typeof(SparklineControl), new FrameworkPropertyMetadata(true, (d, e) => ((SparklineControl) d).OnAutoRangeChanged((bool) e.NewValue)));
            ArgumentRangeProperty = DependencyProperty.Register("ArgumentRange", typeof(DevExpress.Xpf.Editors.Range), typeof(SparklineControl), new FrameworkPropertyMetadata(null, (d, e) => ((SparklineControl) d).OnArgumentRangeChanged((DevExpress.Xpf.Editors.Range) e.NewValue)));
            ValueRangeProperty = DependencyProperty.Register("ValueRange", typeof(DevExpress.Xpf.Editors.Range), typeof(SparklineControl), new FrameworkPropertyMetadata(null, (d, e) => ((SparklineControl) d).OnValueRangeChanged((DevExpress.Xpf.Editors.Range) e.NewValue)));
        }

        public SparklineControl()
        {
            base.DefaultStyleKey = typeof(SparklineControl);
            this.rangeDirector = new RangeDirector(this);
            this.painter = this.CreatePainter();
            SparklinePointCollection points = new SparklinePointCollection();
            this.actualPoints = points;
            base.SetValue(PointsProperty, points);
            DevExpress.Xpf.Editors.Range range1 = new DevExpress.Xpf.Editors.Range(this);
            range1.Limit1 = 0;
            range1.Limit2 = 0;
            range1.Auto = true;
            base.SetValue(ArgumentRangeProperty, range1);
            DevExpress.Xpf.Editors.Range range2 = new DevExpress.Xpf.Editors.Range(this);
            range2.Limit1 = 0;
            range2.Limit2 = 0;
            range2.Auto = true;
            base.SetValue(ValueRangeProperty, range2);
            points.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemsCollectionChanged);
            this.randomPoints = this.GenerateRandomValues();
            this.argumentComparer = new SparklinePointArgumentComparer();
            base.ClipToBounds = true;
        }

        public virtual void Assign(SparklineControl view)
        {
            if (view != null)
            {
                this.highlightMaxPoint = view.highlightMaxPoint;
                this.highlightMinPoint = view.highlightMinPoint;
                this.highlightStartPoint = view.highlightStartPoint;
                this.highlightEndPoint = view.highlightEndPoint;
                this.brush = view.brush;
                this.maxPointBrush = view.maxPointBrush;
                this.minPointBrush = view.minPointBrush;
                this.startPointBrush = view.startPointBrush;
                this.endPointBrush = view.endPointBrush;
                this.negativePointBrush = view.negativePointBrush;
            }
        }

        private void CalculateRanges()
        {
            if ((this.extremeIndexes != null) && ((this.ArgumentRange != null) && (this.ValueRange != null)))
            {
                this.rangeDirector.CalculateRanges(this.ActualPoints, this.extremeIndexes, this.ArgumentRange.InternalRange, this.ValueRange.InternalRange);
            }
        }

        protected internal abstract BaseSparklinePainter CreatePainter();
        void IRangeContainer.OnRangeChanged()
        {
            this.CalculateRanges();
            this.PropertyChanged();
        }

        void ISparklineRangeContainer.RaiseArgumentRangeChanged(SparklineRangeChangedEventArgs e)
        {
            if (this.SparklineArgumentRangeChanged != null)
            {
                this.SparklineArgumentRangeChanged(this, e);
            }
        }

        void ISparklineRangeContainer.RaiseValueRangeChanged(SparklineRangeChangedEventArgs e)
        {
            if (this.SparklineValueRangeChanged != null)
            {
                this.SparklineValueRangeChanged(this, e);
            }
        }

        private SparklinePointCollection GenerateRandomValues()
        {
            Random random = new Random(0);
            SparklinePointCollection points = new SparklinePointCollection();
            for (int i = 0; i < 10; i++)
            {
                points.Add(new SparklinePoint((double) i, random.NextDouble() - 0.5));
            }
            return points;
        }

        private Bounds GetDrawingBounds(Bounds bounds)
        {
            Padding markersPadding = this.GetMarkersPadding();
            return new Bounds(bounds.X + markersPadding.Left, bounds.Y + markersPadding.Top, (bounds.Width - markersPadding.Left) - markersPadding.Right, (bounds.Height - markersPadding.Top) - markersPadding.Bottom);
        }

        protected virtual Padding GetMarkersPadding() => 
            new Padding();

        protected abstract string GetViewName();
        private void OnArgumentRangeChanged(DevExpress.Xpf.Editors.Range range)
        {
            ((IRangeContainer) this).OnRangeChanged();
        }

        private void OnAutoRangeChanged(bool autoRange)
        {
            this.PropertyChanged();
        }

        private void OnBrushChanged(SolidColorBrush brush)
        {
            this.brush = brush;
            this.PropertyChanged();
        }

        private void OnEndPointBrushChanged(SolidColorBrush endPointBrush)
        {
            this.endPointBrush = endPointBrush;
            this.PropertyChanged();
        }

        private void OnHighlightEndPointChanged(bool highlightEndPoint)
        {
            this.highlightEndPoint = highlightEndPoint;
            this.PropertyChanged();
        }

        private void OnHighlightMaxPointChanged(bool highlightMaxPoint)
        {
            this.highlightMaxPoint = highlightMaxPoint;
            this.PropertyChanged();
        }

        private void OnHighlightMinPointChanged(bool highlightMinPoint)
        {
            this.highlightMinPoint = highlightMinPoint;
            this.PropertyChanged();
        }

        private void OnHighlightStartPointChanged(bool highlightStartPoint)
        {
            this.highlightStartPoint = highlightStartPoint;
            this.PropertyChanged();
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.extremeIndexes = new ExtremePointIndexes(this.ActualPoints);
            if (this.SparklinePointsChanged != null)
            {
                this.SparklinePointsChanged(this, new EventArgs());
            }
            this.CalculateRanges();
            this.PropertyChanged();
        }

        private void OnMaxPointBrushChanged(SolidColorBrush maxPointBrush)
        {
            this.maxPointBrush = maxPointBrush;
            this.PropertyChanged();
        }

        private void OnMinPointBrushChanged(SolidColorBrush minPointBrush)
        {
            this.minPointBrush = minPointBrush;
            this.PropertyChanged();
        }

        private void OnNegativePointBrushChanged(SolidColorBrush negativePointBrush)
        {
            this.negativePointBrush = negativePointBrush;
            this.PropertyChanged();
        }

        private void OnPointsPropertyChanged(SparklinePointCollection sparklineItemCollection)
        {
            if (this.actualPoints != null)
            {
                this.actualPoints.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnItemsCollectionChanged);
            }
            this.actualPoints = sparklineItemCollection;
            if (sparklineItemCollection == null)
            {
                this.actualPoints = new SparklinePointCollection();
            }
            this.actualPoints.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemsCollectionChanged);
            this.OnItemsCollectionChanged(this, null);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Bounds bounds = new Bounds(0, 0, (int) base.ActualWidth, (int) base.ActualHeight);
            Bounds drawingBounds = this.GetDrawingBounds(bounds);
            if (this.extremeIndexes != null)
            {
                SparklineMappingBase mapping = SparklineMappingBase.CreateMapping(this.Type, this.ActualPoints, drawingBounds, this.rangeDirector.ArgumentRange, this.rangeDirector.ValueRange);
                this.painter.Initialize(this.ActualPoints, this, mapping, this.extremeIndexes);
                this.painter.Draw(drawingContext);
            }
        }

        private void OnStartPointBrushChanged(SolidColorBrush startPointBrush)
        {
            this.startPointBrush = startPointBrush;
            this.PropertyChanged();
        }

        private void OnValueRangeChanged(DevExpress.Xpf.Editors.Range range)
        {
            ((IRangeContainer) this).OnRangeChanged();
        }

        protected void PropertyChanged()
        {
            base.InvalidateVisual();
        }

        public override string ToString() => 
            this.GetViewName();

        internal SparklinePointCollection ActualPoints
        {
            get
            {
                if (((this.actualPoints != null) && (this.actualPoints.Count != 0)) || !this.DesignMode)
                {
                    return this.actualPoints;
                }
                return this.randomPoints;
            }
        }

        internal SolidColorBrush ActualBrush =>
            (this.brush == null) ? (((base.Foreground == null) || !(base.Foreground is SolidColorBrush)) ? this.defaultBrush : (base.Foreground as SolidColorBrush)) : this.brush;

        internal SolidColorBrush ActualMaxPointBrush =>
            this.maxPointBrush;

        internal SolidColorBrush ActualMinPointBrush =>
            this.minPointBrush;

        internal SolidColorBrush ActualStartPointBrush =>
            this.startPointBrush;

        internal SolidColorBrush ActualEndPointBrush =>
            this.endPointBrush;

        internal SolidColorBrush ActualNegativePointBrush =>
            this.negativePointBrush;

        internal bool ActualHighlightMaxPoint =>
            this.highlightMaxPoint;

        internal bool ActualHighlightMinPoint =>
            this.highlightMinPoint;

        internal bool ActualHighlightStartPoint =>
            this.highlightStartPoint;

        internal bool ActualHighlightEndPoint =>
            this.highlightEndPoint;

        internal bool DesignMode =>
            DesignerProperties.GetIsInDesignMode(this);

        public bool HighlightMaxPoint
        {
            get => 
                (bool) base.GetValue(HighlightMaxPointProperty);
            set => 
                base.SetValue(HighlightMaxPointProperty, value);
        }

        public bool HighlightMinPoint
        {
            get => 
                (bool) base.GetValue(HighlightMinPointProperty);
            set => 
                base.SetValue(HighlightMinPointProperty, value);
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

        public SparklinePointCollection Points
        {
            get => 
                (SparklinePointCollection) base.GetValue(PointsProperty);
            set => 
                base.SetValue(PointsProperty, value);
        }

        public bool AutoRange
        {
            get => 
                (bool) base.GetValue(AutoRangeProperty);
            set => 
                base.SetValue(AutoRangeProperty, value);
        }

        public DevExpress.Xpf.Editors.Range ArgumentRange
        {
            get => 
                (DevExpress.Xpf.Editors.Range) base.GetValue(ArgumentRangeProperty);
            set => 
                base.SetValue(ArgumentRangeProperty, value);
        }

        public DevExpress.Xpf.Editors.Range ValueRange
        {
            get => 
                (DevExpress.Xpf.Editors.Range) base.GetValue(ValueRangeProperty);
            set => 
                base.SetValue(ValueRangeProperty, value);
        }

        protected internal abstract bool ActualShowNegativePoint { get; }

        [Browsable(false)]
        public abstract SparklineViewType Type { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SparklineControl.<>c <>9 = new SparklineControl.<>c();

            internal void <.cctor>b__144_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnPointsPropertyChanged((SparklinePointCollection) e.NewValue);
            }

            internal void <.cctor>b__144_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnHighlightMaxPointChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__144_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnNegativePointBrushChanged((SolidColorBrush) e.NewValue);
            }

            internal void <.cctor>b__144_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnAutoRangeChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__144_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnArgumentRangeChanged((DevExpress.Xpf.Editors.Range) e.NewValue);
            }

            internal void <.cctor>b__144_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnValueRangeChanged((DevExpress.Xpf.Editors.Range) e.NewValue);
            }

            internal void <.cctor>b__144_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnHighlightMinPointChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__144_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnHighlightStartPointChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__144_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnHighlightEndPointChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__144_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnBrushChanged((SolidColorBrush) e.NewValue);
            }

            internal void <.cctor>b__144_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnMaxPointBrushChanged((SolidColorBrush) e.NewValue);
            }

            internal void <.cctor>b__144_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnMinPointBrushChanged((SolidColorBrush) e.NewValue);
            }

            internal void <.cctor>b__144_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnStartPointBrushChanged((SolidColorBrush) e.NewValue);
            }

            internal void <.cctor>b__144_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SparklineControl) d).OnEndPointBrushChanged((SolidColorBrush) e.NewValue);
            }
        }
    }
}

