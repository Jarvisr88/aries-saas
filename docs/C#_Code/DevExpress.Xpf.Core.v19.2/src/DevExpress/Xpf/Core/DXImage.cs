namespace DevExpress.Xpf.Core
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Svg;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    public class DXImage : Image
    {
        [ThreadStatic]
        private static DispatcherTimer updateDXImageOffsetTimer;
        public static readonly DependencyProperty LockUpdatesProperty;
        public static BitmapScalingMode ImageQuality;
        private readonly WeakEventHandler<DXImage, EventArgs, EventHandler> weakUpdateHandler;
        private bool updateRequested;
        private bool lockUpdates;
        private PresentationSource presentationSource;
        private WpfSvgRenderer renderer = null;
        private WpfSvgPalette currentPalette;
        private string currentSvgState = "Normal";
        private ThemeTreeWalker treeWalker = null;
        private Point currentOffset;
        private Lazy<bool> useDrawWithDrawingContext;
        private bool hasSubscription;

        static DXImage()
        {
            EnablePixelCorrection = true;
            LockUpdatesProperty = DependencyPropertyManager.RegisterAttached("LockUpdates", typeof(bool), typeof(DXImage), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
            Image.SourceProperty.OverrideMetadata(typeof(DXImage), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DXImage.OnSourcePropertyChanged)));
            ThemeManager.TreeWalkerProperty.OverrideMetadata(typeof(DXImage), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DXImage.OnThemeTreeWalkerPropertyChanged)));
            WpfSvgPalette.PaletteProperty.OverrideMetadata(typeof(DXImage), new FrameworkPropertyMetadata(null, (d, e) => ((DXImage) d).OnWpfSvgPaletteChanged((WpfSvgPalette) e.OldValue, (WpfSvgPalette) e.NewValue)));
            SvgImageHelper.StateProperty.OverrideMetadata(typeof(DXImage), new FrameworkPropertyMetadata("Normal", FrameworkPropertyMetadataOptions.Inherits, (d, e) => ((DXImage) d).OnSvgStateChanged((string) e.OldValue, (string) e.NewValue)));
        }

        public DXImage()
        {
            base.SnapsToDevicePixels = false;
            base.UseLayoutRounding = true;
            base.VisualBitmapScalingMode = ImageQuality;
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
            base.Unloaded += new RoutedEventHandler(this.DXImage_Unloaded);
            base.Loaded += new RoutedEventHandler(this.DXImage_Loaded);
            this.IgnoreUseLayoutRoundingCheck = false;
            Action<DXImage, object, EventArgs> onEventAction = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                Action<DXImage, object, EventArgs> local1 = <>c.<>9__27_0;
                onEventAction = <>c.<>9__27_0 = (image, timer, args) => image.OnTimerTick();
            }
            this.weakUpdateHandler = new WeakEventHandler<DXImage, EventArgs, EventHandler>(this, onEventAction, <>c.<>9__27_1 ??= delegate (WeakEventHandler<DXImage, EventArgs, EventHandler> wHandler, object sender) {
                ((DispatcherTimer) sender).Tick -= wHandler.Handler;
            }, <>c.<>9__27_2 ??= wHandler => new EventHandler(wHandler.OnEvent));
            this.InvalidateUseDrawWithDrawingContext();
        }

        private static DispatcherTimer CreateOffsetTimer()
        {
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(500.0);
            DispatcherTimer timer = timer1;
            if (EnablePixelCorrection)
            {
                timer.Start();
            }
            return timer;
        }

        private void DXImage_Loaded(object sender, RoutedEventArgs e)
        {
            if (EnablePixelCorrection)
            {
                PresentationSource.AddSourceChangedHandler(this, new SourceChangedEventHandler(this.OnPresentationSourceChanged));
                this.presentationSource = PresentationSource.FromVisual(this);
                this.lockUpdates = false;
                UpdateDXImageOffsetTimer.Tick += this.weakUpdateHandler.Handler;
                this.hasSubscription = true;
            }
        }

        private void DXImage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.hasSubscription)
            {
                this.lockUpdates = true;
                PresentationSource.RemoveSourceChangedHandler(this, new SourceChangedEventHandler(this.OnPresentationSourceChanged));
                this.presentationSource = null;
                UpdateDXImageOffsetTimer.Tick -= this.weakUpdateHandler.Handler;
            }
        }

        public static bool GetLockUpdates(DependencyObject obj) => 
            (bool) obj.GetValue(LockUpdatesProperty);

        private Point GetOffset()
        {
            Point offset = new Point();
            if (!this.lockUpdates && EnablePixelCorrection)
            {
                if (this == null)
                {
                    return offset;
                }
                if (this.presentationSource != null)
                {
                    Visual rootVisual = this.presentationSource.RootVisual;
                    if (rootVisual == null)
                    {
                        return offset;
                    }
                    if (!this.IgnoreUseLayoutRoundingCheck && ((rootVisual is FrameworkElement) && ((FrameworkElement) rootVisual).UseLayoutRounding))
                    {
                        return offset;
                    }
                    offset = base.TransformToAncestor(rootVisual).Transform(offset);
                    offset = this.presentationSource.CompositionTarget.TransformToDevice.Transform(offset);
                    offset.X = Math.Round(offset.X);
                    offset.Y = Math.Round(offset.Y);
                    offset = this.presentationSource.CompositionTarget.TransformFromDevice.Transform(offset);
                    offset = rootVisual.TransformToDescendant(this).Return<GeneralTransform, Point>(x => x.Transform(offset), () => offset);
                }
            }
            return offset;
        }

        protected virtual bool GetUseDrawWithDrawingContext()
        {
            if (base.Source == null)
            {
                return false;
            }
            bool autoSize = SvgImageHelper.GetAutoSize(base.Source);
            SvgImage svgImage = SvgImageHelper.GetSvgImage(base.Source);
            WpfSvgPalette wpfSvgPalette = SvgImageHelper.GetWpfSvgPalette(base.Source);
            string currentSvgState = this.currentSvgState;
            WpfSvgPalette basePalette = (this.treeWalker != null) ? this.treeWalker.InplaceResourceProvider.GetSvgPalette(this) : new InplaceResourceProvider("DeepBlue").GetSvgPalette(this);
            if (!autoSize || (svgImage == null))
            {
                return false;
            }
            this.renderer = new WpfSvgRenderer(svgImage, wpfSvgPalette, this.currentPalette, basePalette, currentSvgState);
            return true;
        }

        protected void InvalidateUseDrawWithDrawingContext()
        {
            this.useDrawWithDrawingContext = new Lazy<bool>(new Func<bool>(this.GetUseDrawWithDrawingContext));
            base.InvalidateVisual();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            if (EnablePixelCorrection && ((base.Source != null) && ((base.ActualHeight != 0.0) && ((base.ActualWidth != 0.0) && !GetLockUpdates(this)))))
            {
                if (!this.hasSubscription && base.IsLoaded)
                {
                    this.DXImage_Loaded(this, null);
                }
                if (!UpdateDXImageOffsetTimer.IsEnabled)
                {
                    UpdateDXImageOffsetTimer.Start();
                }
                this.updateRequested = true;
            }
        }

        private void OnPresentationSourceChanged(object sender, SourceChangedEventArgs e)
        {
            this.presentationSource = e.NewSource;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (base.Source == null)
            {
                base.OnRender(dc);
            }
            else if ((base.Source is DrawingImage) && this.UseDrawWithDrawingContext)
            {
                this.renderer.DrawWithDrawingContext(this.currentOffset, base.RenderSize, dc);
            }
            else
            {
                dc.DrawImage(base.Source, new Rect(this.currentOffset, base.RenderSize));
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (sizeInfo.WidthChanged && sizeInfo.HeightChanged)
            {
                this.InvalidateUseDrawWithDrawingContext();
            }
        }

        protected virtual void OnSourceChanged(ImageSource newValue)
        {
            this.InvalidateUseDrawWithDrawingContext();
        }

        private static void OnSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DXImage) d).OnSourceChanged((ImageSource) e.NewValue);
        }

        private void OnSvgStateChanged(string oldValue, string newValue)
        {
            this.currentSvgState = newValue;
            this.InvalidateUseDrawWithDrawingContext();
        }

        protected virtual void OnThemeTreeWalkerChanged(ThemeTreeWalker newWalker)
        {
            this.treeWalker = newWalker;
            this.InvalidateUseDrawWithDrawingContext();
        }

        private static void OnThemeTreeWalkerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DXImage) d).OnThemeTreeWalkerChanged((ThemeTreeWalker) e.NewValue);
        }

        private void OnTimerTick()
        {
            if (this.updateRequested)
            {
                if (!EnablePixelCorrection)
                {
                    UpdateDXImageOffsetTimer.Stop();
                }
                else
                {
                    this.updateRequested = false;
                    if ((base.Source != null) && ((base.ActualHeight != 0.0) && ((base.ActualWidth != 0.0) && !GetLockUpdates(this))))
                    {
                        Point offset = this.GetOffset();
                        if (!LayoutDoubleHelper.AreClose(offset, this.currentOffset))
                        {
                            this.currentOffset = offset;
                            base.InvalidateVisual();
                        }
                    }
                }
            }
        }

        private void OnWpfSvgPaletteChanged(WpfSvgPalette oldValue, WpfSvgPalette newValue)
        {
            this.currentPalette = newValue;
            this.InvalidateUseDrawWithDrawingContext();
        }

        public static void SetLockUpdates(DependencyObject obj, bool value)
        {
            obj.SetValue(LockUpdatesProperty, value);
        }

        private static DispatcherTimer UpdateDXImageOffsetTimer =>
            updateDXImageOffsetTimer ??= CreateOffsetTimer();

        public static bool EnablePixelCorrection { get; set; }

        public bool IgnoreUseLayoutRoundingCheck { get; set; }

        private Size SourceSize
        {
            get
            {
                if (base.Source == null)
                {
                    return new Size();
                }
                if (!(base.Source is BitmapSource))
                {
                    return new Size(base.Source.Width, base.Source.Height);
                }
                BitmapSource source = (BitmapSource) base.Source;
                return new Size((double) source.PixelWidth, (double) source.PixelHeight);
            }
        }

        protected bool UseDrawWithDrawingContext =>
            this.useDrawWithDrawingContext.Value;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXImage.<>c <>9 = new DXImage.<>c();
            public static Action<DXImage, object, EventArgs> <>9__27_0;
            public static Action<WeakEventHandler<DXImage, EventArgs, EventHandler>, object> <>9__27_1;
            public static Func<WeakEventHandler<DXImage, EventArgs, EventHandler>, EventHandler> <>9__27_2;

            internal void <.cctor>b__10_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXImage) d).OnWpfSvgPaletteChanged((WpfSvgPalette) e.OldValue, (WpfSvgPalette) e.NewValue);
            }

            internal void <.cctor>b__10_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXImage) d).OnSvgStateChanged((string) e.OldValue, (string) e.NewValue);
            }

            internal void <.ctor>b__27_0(DXImage image, object timer, EventArgs args)
            {
                image.OnTimerTick();
            }

            internal void <.ctor>b__27_1(WeakEventHandler<DXImage, EventArgs, EventHandler> wHandler, object sender)
            {
                ((DispatcherTimer) sender).Tick -= wHandler.Handler;
            }

            internal EventHandler <.ctor>b__27_2(WeakEventHandler<DXImage, EventArgs, EventHandler> wHandler) => 
                new EventHandler(wHandler.OnEvent);
        }
    }
}

