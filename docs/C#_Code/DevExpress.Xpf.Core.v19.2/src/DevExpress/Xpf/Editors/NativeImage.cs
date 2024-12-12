namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class NativeImage : Control, INativeImageRendererCallback, INotifyPropertyChanged
    {
        private readonly NativeRenderer2 nativeRenderer;
        public static readonly DependencyProperty RendererProperty;
        private INativeImageRenderer renderer;
        private SolidColorBrush cachedBackground;
        private int fps;
        private bool isInitialized;
        private readonly Stopwatch stopwatch = Stopwatch.StartNew();
        private int count;
        private long last;
        private Rect invalidateRect = Rect.Empty;
        private bool whole;
        private bool requiresUpdate;

        public event PropertyChangedEventHandler PropertyChanged;

        static NativeImage()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(NativeImage), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            RendererProperty = DependencyPropertyRegistrator.Register<NativeImage, INativeImageRenderer>(System.Linq.Expressions.Expression.Lambda<Func<NativeImage, INativeImageRenderer>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NativeImage.get_Renderer)), parameters), null, (control, oldValue, newValue) => control.RendererChanged(oldValue, newValue));
            Control.BackgroundProperty.OverrideMetadata(typeof(NativeImage), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White), FrameworkPropertyMetadataOptions.None, (d, args) => ((NativeImage) d).BackgroundChanged((System.Windows.Media.Brush) args.NewValue)));
        }

        public NativeImage()
        {
            this.nativeRenderer = new NativeRenderer2(delegate {
                Func<SolidColorBrush, System.Drawing.Color> evaluator = <>c.<>9__15_1;
                if (<>c.<>9__15_1 == null)
                {
                    Func<SolidColorBrush, System.Drawing.Color> local1 = <>c.<>9__15_1;
                    evaluator = <>c.<>9__15_1 = x => x.Color.ToWinFormsColor();
                }
                return this.cachedBackground.Return<SolidColorBrush, System.Drawing.Color>(evaluator, <>c.<>9__15_2 ??= () => System.Drawing.Color.White);
            });
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
        }

        private void BackgroundChanged(System.Windows.Media.Brush newValue)
        {
            this.cachedBackground = newValue as SolidColorBrush;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            base.InvalidateVisual();
            long elapsedMilliseconds = this.stopwatch.ElapsedMilliseconds;
            if ((elapsedMilliseconds - this.last) <= 0x3e8L)
            {
                this.count++;
            }
            else
            {
                this.FPS = (int) (((double) this.count) / (((double) (elapsedMilliseconds - this.last)) / 1000.0));
                this.count = 0;
                this.last = elapsedMilliseconds;
            }
        }

        private void Initialize(System.Windows.Size size)
        {
            this.nativeRenderer.Resize(size);
            this.isInitialized = true;
            this.Invalidate();
        }

        public void Invalidate()
        {
            if (base.IsLoaded)
            {
                Rect region = new Rect(0.0, 0.0, base.RenderSize.Width, base.RenderSize.Height);
                double scaleX = ScreenHelper.GetScaleX(this);
                region.Scale(scaleX, scaleX);
                this.InvalidateInternal(region, true);
            }
        }

        public void Invalidate(Rect region)
        {
            this.InvalidateInternal(region, false);
        }

        private void InvalidateInternal(Rect region, bool whole)
        {
            if (this.isInitialized && (this.renderer != null))
            {
                this.invalidateRect.Union(region);
                this.requiresUpdate = true;
                this.whole = whole;
                base.InvalidateVisual();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            double scaleX = ScreenHelper.GetScaleX(this);
            System.Windows.Size size = base.RenderSize.IsEmpty ? base.RenderSize : new System.Windows.Size(base.RenderSize.Width * scaleX, base.RenderSize.Height * scaleX);
            this.Initialize(size);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (this.isInitialized)
            {
                if (this.requiresUpdate)
                {
                    this.nativeRenderer.StartRender();
                    try
                    {
                        this.nativeRenderer.Render(this.renderer, this.invalidateRect);
                    }
                    finally
                    {
                        this.nativeRenderer.EndRender(this.invalidateRect, this.whole);
                        this.whole = false;
                        this.requiresUpdate = true;
                        this.invalidateRect = Rect.Empty;
                    }
                    this.requiresUpdate = false;
                }
                this.RenderCore(drawingContext);
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double scaleX = ScreenHelper.GetScaleX(this);
            System.Windows.Size size = e.NewSize.IsEmpty ? e.NewSize : new System.Windows.Size(e.NewSize.Width * scaleX, e.NewSize.Height * scaleX);
            this.Initialize(size);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.nativeRenderer.Resize(System.Windows.Size.Empty);
            this.isInitialized = false;
        }

        private void RaisePropertyChanged(string property)
        {
            this.PropertyChanged.Do<PropertyChangedEventHandler>(x => x(this, new PropertyChangedEventArgs(property)));
        }

        private void RenderCore(DrawingContext drawingContext)
        {
            drawingContext.PushClip(new RectangleGeometry(new Rect(base.RenderSize)));
            double scaleX = ScreenHelper.GetScaleX(this);
            System.Windows.Size size = (this.nativeRenderer.Source != null) ? new System.Windows.Size(this.nativeRenderer.Source.Width / scaleX, this.nativeRenderer.Source.Height / scaleX) : base.RenderSize;
            this.nativeRenderer.InvalidateSource();
            drawingContext.DrawImage(this.nativeRenderer.Source, new Rect(size));
        }

        protected virtual void RendererChanged(INativeImageRenderer oldValue, INativeImageRenderer newValue)
        {
            Action<INativeImageRenderer> action = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Action<INativeImageRenderer> local1 = <>c.<>9__6_0;
                action = <>c.<>9__6_0 = x => x.ReleaseCallback();
            }
            oldValue.Do<INativeImageRenderer>(action);
            newValue.Do<INativeImageRenderer>(x => x.RegisterCallback(this));
            this.renderer = newValue;
            this.Invalidate();
        }

        public void SetRenderMask(DrawingBrush drawing)
        {
            base.OpacityMask = drawing;
        }

        public INativeImageRenderer Renderer
        {
            get => 
                this.renderer;
            set => 
                base.SetValue(RendererProperty, value);
        }

        public int FPS
        {
            get => 
                this.fps;
            set
            {
                if (this.fps != value)
                {
                    this.fps = value;
                    this.RaisePropertyChanged("FPS");
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NativeImage.<>c <>9 = new NativeImage.<>c();
            public static Action<INativeImageRenderer> <>9__6_0;
            public static Func<SolidColorBrush, System.Drawing.Color> <>9__15_1;
            public static Func<System.Drawing.Color> <>9__15_2;

            internal void <.cctor>b__3_0(NativeImage control, INativeImageRenderer oldValue, INativeImageRenderer newValue)
            {
                control.RendererChanged(oldValue, newValue);
            }

            internal void <.cctor>b__3_1(DependencyObject d, DependencyPropertyChangedEventArgs args)
            {
                ((NativeImage) d).BackgroundChanged((System.Windows.Media.Brush) args.NewValue);
            }

            internal System.Drawing.Color <.ctor>b__15_1(SolidColorBrush x) => 
                x.Color.ToWinFormsColor();

            internal System.Drawing.Color <.ctor>b__15_2() => 
                System.Drawing.Color.White;

            internal void <RendererChanged>b__6_0(INativeImageRenderer x)
            {
                x.ReleaseCallback();
            }
        }
    }
}

