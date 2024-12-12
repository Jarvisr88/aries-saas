namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class GradientMultiSlider : Control
    {
        public static readonly DependencyProperty BrushProperty;
        internal static readonly DependencyPropertyKey ThumbsPropertyKey;
        public static readonly DependencyProperty ThumbsProperty;
        public static readonly DependencyProperty SelectedThumbProperty;
        public static readonly DependencyProperty SelectedThumbColorProperty;
        public static readonly DependencyProperty BrushTypeProperty;
        private readonly Locker selectedThumbLocker;
        private readonly Locker thumbsLocker;

        static GradientMultiSlider()
        {
            Type ownerType = typeof(GradientMultiSlider);
            BrushProperty = DependencyPropertyManager.Register("Brush", typeof(GradientBrush), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, (obj, args) => ((GradientMultiSlider) obj).OnBrushChanged((GradientBrush) args.NewValue)));
            ThumbsPropertyKey = DependencyPropertyManager.RegisterReadOnly("Thumbs", typeof(ObservableCollection<GradientMultiSliderThumb>), ownerType, new PropertyMetadata(null));
            ThumbsProperty = ThumbsPropertyKey.DependencyProperty;
            SelectedThumbProperty = DependencyPropertyManager.Register("SelectedThumb", typeof(GradientMultiSliderThumb), ownerType, new PropertyMetadata(null, (obj, args) => ((GradientMultiSlider) obj).OnSelectedThumbChanged((GradientMultiSliderThumb) args.NewValue)));
            SelectedThumbColorProperty = DependencyPropertyManager.Register("SelectedThumbColor", typeof(Color), ownerType, new PropertyMetadata(Colors.Black, (obj, args) => ((GradientMultiSlider) obj).OnSelectedThumbColorChanged((Color) args.NewValue)));
            BrushTypeProperty = DependencyPropertyManager.Register("BrushType", typeof(GradientBrushType), ownerType, new PropertyMetadata(GradientBrushType.Linear, (obj, args) => ((GradientMultiSlider) obj).OnBrushTypeChanged((GradientBrushType) args.NewValue)));
        }

        public GradientMultiSlider()
        {
            this.SetDefaultStyleKey(typeof(GradientMultiSlider));
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            this.selectedThumbLocker = new Locker();
            this.thumbsLocker = new Locker();
            this.FlipThumbsCommand = DelegateCommandFactory.Create<object>(obj => this.FlipThumbs(), false);
        }

        internal void AddThumb(GradientMultiSliderThumb thumb)
        {
            this.Thumbs.Add(thumb);
            this.thumbsLocker.DoLockedActionIfNotLocked(new Action(this.UpdateBrush));
        }

        internal GradientMultiSliderThumb AddThumb(double offset, Color color)
        {
            this.Thumbs ??= new ObservableCollection<GradientMultiSliderThumb>();
            GradientMultiSliderThumb thumb1 = new GradientMultiSliderThumb();
            thumb1.OwnerSlider = this;
            thumb1.Offset = offset;
            thumb1.Color = color;
            GradientMultiSliderThumb thumb = thumb1;
            this.SubscribeThumbEvents(thumb);
            this.Thumbs.Add(thumb);
            this.thumbsLocker.DoLockedActionIfNotLocked(new Action(this.UpdateBrush));
            return thumb;
        }

        private GradientBrush CloneBrush(GradientBrush brush, GradientBrushType brushType, GradientStopCollection gradientStops)
        {
            if ((brush == null) || !this.IsValidBrush(brush, brushType))
            {
                GradientBrushType type = this.BrushType;
                if (type == GradientBrushType.Linear)
                {
                    brush = new LinearGradientBrush(gradientStops, new Point(0.0, 0.5), new Point(1.0, 0.5));
                }
                else if (type == GradientBrushType.Radial)
                {
                    brush = new RadialGradientBrush(gradientStops);
                }
            }
            Func<GradientBrush, GradientBrush> evaluator = <>c.<>9__55_0;
            if (<>c.<>9__55_0 == null)
            {
                Func<GradientBrush, GradientBrush> local1 = <>c.<>9__55_0;
                evaluator = <>c.<>9__55_0 = x => x.Clone();
            }
            GradientBrush input = brush.With<GradientBrush, GradientBrush>(evaluator);
            input.Do<GradientBrush>(delegate (GradientBrush x) {
                x.GradientStops = gradientStops;
            });
            return input;
        }

        private void FlipThumbs()
        {
            foreach (GradientMultiSliderThumb thumb in this.Thumbs)
            {
                thumb.Offset = Math.Abs((double) (1.0 - thumb.Offset));
            }
        }

        internal Color GetColorAtOffset(double thumbOffset) => 
            this.Brush.GetColorAtPoint(this.GradientRectangle.ActualWidth, this.GradientRectangle.ActualHeight, new Point(thumbOffset, this.GradientRectangle.ActualHeight / 2.0));

        private bool IsValidBrush(GradientBrush brush, GradientBrushType brushType) => 
            (!(brush is LinearGradientBrush) || (brushType != GradientBrushType.Linear)) ? ((brush is RadialGradientBrush) && (brushType == GradientBrushType.Radial)) : true;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UnsubscribeEvents();
            this.GradientRectangle = (Rectangle) base.GetTemplateChild("PART_GradientRect");
            this.SubscribeEvents();
        }

        protected virtual void OnBrushChanged(GradientBrush newValue)
        {
            if (newValue != null)
            {
                this.thumbsLocker.DoLockedActionIfNotLocked(delegate {
                    this.UpdateThumbs();
                    this.SelectThumb(this.Thumbs.First<GradientMultiSliderThumb>());
                });
            }
        }

        protected virtual void OnBrushTypeChanged(GradientBrushType newValue)
        {
            this.thumbsLocker.DoLockedActionIfNotLocked(new Action(this.UpdateBrush));
        }

        private void OnGradientLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double x = e.GetPosition((Rectangle) sender).X;
            double offset = x / this.GradientRectangle.ActualWidth;
            this.SelectThumb(this.AddThumb(offset, this.GetColorAtOffset(x)));
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this.Brush == null)
            {
                this.AddThumb(0.0, Colors.Black);
                this.AddThumb(1.0, Colors.White);
                this.SelectThumb(this.Thumbs.FirstOrDefault<GradientMultiSliderThumb>());
            }
        }

        protected virtual void OnSelectedThumbChanged(GradientMultiSliderThumb newValue)
        {
            this.selectedThumbLocker.DoLockedActionIfNotLocked(() => this.SelectedThumbColor = newValue.Color);
            this.SelectThumb(newValue);
        }

        protected virtual void OnSelectedThumbColorChanged(Color newValue)
        {
            this.selectedThumbLocker.DoLockedActionIfNotLocked(() => this.SelectedThumb.Color = newValue);
            this.UpdateBrush(false);
        }

        private void OnThumbColorChanged(object sender, RoutedEventArgs e)
        {
            this.thumbsLocker.DoLockedActionIfNotLocked(new Action(this.UpdateBrush));
        }

        private void OnThumbPositionChanged(object sender, RoutedEventArgs e)
        {
            this.thumbsLocker.DoLockedActionIfNotLocked(new Action(this.UpdateBrush));
        }

        internal void RemoveThumb(GradientMultiSliderThumb thumb)
        {
            thumb.ThumbColorChanged -= new RoutedEventHandler(this.OnThumbColorChanged);
            thumb.ThumbPositionChanged -= new RoutedEventHandler(this.OnThumbPositionChanged);
            if (thumb.IsSelected)
            {
                this.SelectThumb(this.Thumbs.FirstOrDefault<GradientMultiSliderThumb>());
            }
            this.Thumbs.Remove(thumb);
            this.thumbsLocker.DoLockedActionIfNotLocked(new Action(this.UpdateBrush));
        }

        internal void SelectThumb(GradientMultiSliderThumb thumb)
        {
            Action<GradientMultiSliderThumb> action = <>c.<>9__47_0;
            if (<>c.<>9__47_0 == null)
            {
                Action<GradientMultiSliderThumb> local1 = <>c.<>9__47_0;
                action = <>c.<>9__47_0 = x => x.IsSelected = false;
            }
            this.Thumbs.ForEach<GradientMultiSliderThumb>(action);
            thumb.IsSelected = true;
            this.SelectedThumb = thumb;
        }

        private void SubscribeEvents()
        {
            this.GradientRectangle.Do<Rectangle>(delegate (Rectangle x) {
                x.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.OnGradientLeftButtonDown);
            });
        }

        private void SubscribeThumbEvents(GradientMultiSliderThumb thumb)
        {
            thumb.ThumbPositionChanged += new RoutedEventHandler(this.OnThumbPositionChanged);
            thumb.ThumbColorChanged += new RoutedEventHandler(this.OnThumbColorChanged);
        }

        private void UnsubscribeEvents()
        {
            this.GradientRectangle.Do<Rectangle>(delegate (Rectangle x) {
                x.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(this.OnGradientLeftButtonDown);
            });
        }

        private void UnsubscribeThumbEvents(GradientMultiSliderThumb thumb)
        {
            thumb.ThumbPositionChanged -= new RoutedEventHandler(this.OnThumbPositionChanged);
            thumb.ThumbColorChanged -= new RoutedEventHandler(this.OnThumbColorChanged);
        }

        private void UpdateBrush()
        {
            if (base.IsInitialized)
            {
                GradientStopCollection gradientStops = new GradientStopCollection();
                foreach (GradientMultiSliderThumb thumb in this.Thumbs)
                {
                    if (!thumb.IgnoreThumb)
                    {
                        gradientStops.Add(new GradientStop(thumb.Color, thumb.Offset));
                    }
                }
                this.UpdateBrush(gradientStops);
            }
        }

        internal void UpdateBrush(bool updateThumbs)
        {
            if (!updateThumbs)
            {
                this.thumbsLocker.DoLockedActionIfNotLocked(new Action(this.UpdateBrush));
            }
            else
            {
                this.UpdateBrush();
            }
        }

        private void UpdateBrush(GradientStopCollection gradientStops)
        {
            this.Brush = this.CloneBrush(this.Brush, this.BrushType, gradientStops);
        }

        internal void UpdateThumbs()
        {
            if (this.Thumbs != null)
            {
                this.Thumbs.ForEach<GradientMultiSliderThumb>(new Action<GradientMultiSliderThumb>(this.UnsubscribeThumbEvents));
            }
            this.Thumbs = new ObservableCollection<GradientMultiSliderThumb>();
            foreach (GradientStop stop in this.Brush.GradientStops)
            {
                this.AddThumb(stop.Offset, stop.Color);
            }
            this.thumbsLocker.DoLockedActionIfNotLocked(new Action(this.UpdateBrush));
        }

        public GradientBrush Brush
        {
            get => 
                (GradientBrush) base.GetValue(BrushProperty);
            set => 
                base.SetValue(BrushProperty, value);
        }

        public ObservableCollection<GradientMultiSliderThumb> Thumbs
        {
            get => 
                (ObservableCollection<GradientMultiSliderThumb>) base.GetValue(ThumbsProperty);
            internal set => 
                base.SetValue(ThumbsPropertyKey, value);
        }

        public GradientMultiSliderThumb SelectedThumb
        {
            get => 
                (GradientMultiSliderThumb) base.GetValue(SelectedThumbProperty);
            set => 
                base.SetValue(SelectedThumbProperty, value);
        }

        public Color SelectedThumbColor
        {
            get => 
                (Color) base.GetValue(SelectedThumbColorProperty);
            set => 
                base.SetValue(SelectedThumbColorProperty, value);
        }

        public GradientBrushType BrushType
        {
            get => 
                (GradientBrushType) base.GetValue(BrushTypeProperty);
            set => 
                base.SetValue(BrushTypeProperty, value);
        }

        public ICommand FlipThumbsCommand { get; private set; }

        internal Rectangle GradientRectangle { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GradientMultiSlider.<>c <>9 = new GradientMultiSlider.<>c();
            public static Action<GradientMultiSliderThumb> <>9__47_0;
            public static Func<GradientBrush, GradientBrush> <>9__55_0;

            internal void <.cctor>b__6_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((GradientMultiSlider) obj).OnBrushChanged((GradientBrush) args.NewValue);
            }

            internal void <.cctor>b__6_1(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((GradientMultiSlider) obj).OnSelectedThumbChanged((GradientMultiSliderThumb) args.NewValue);
            }

            internal void <.cctor>b__6_2(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((GradientMultiSlider) obj).OnSelectedThumbColorChanged((Color) args.NewValue);
            }

            internal void <.cctor>b__6_3(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((GradientMultiSlider) obj).OnBrushTypeChanged((GradientBrushType) args.NewValue);
            }

            internal GradientBrush <CloneBrush>b__55_0(GradientBrush x) => 
                x.Clone();

            internal void <SelectThumb>b__47_0(GradientMultiSliderThumb x)
            {
                x.IsSelected = false;
            }
        }
    }
}

