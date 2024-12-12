namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Svg;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class SvgImageHelper
    {
        private static readonly WeakCache<Uri, SvgImage> ImageCache = new WeakCache<Uri, SvgImage>();
        private static readonly IDictionary<Uri, Func<Uri, SvgImage>> CreateImageCache = new ConcurrentDictionary<Uri, Func<Uri, SvgImage>>();
        private static readonly Func<Uri, Stream> CreateRequestAndGetResponseStream;
        public static readonly DependencyProperty AutoSizeProperty = DependencyPropertyManager.RegisterAttached("AutoSize", typeof(bool), typeof(SvgImageHelper), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty StateProperty = DependencyPropertyManager.RegisterAttached("State", typeof(string), typeof(SvgImageHelper), new FrameworkPropertyMetadata("Normal", FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(SvgImageHelper.OnStatePropertyChanged)));
        public static readonly DependencyProperty TreeWalkerProperty;
        public static readonly DependencyProperty WidthProperty = DependencyPropertyManager.RegisterAttached("Width", typeof(double), typeof(SvgImageHelper), new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, new PropertyChangedCallback(SvgImageHelper.OnWidthPropertyChanged)));
        public static readonly DependencyProperty HeightProperty = DependencyPropertyManager.RegisterAttached("Height", typeof(double), typeof(SvgImageHelper), new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, new PropertyChangedCallback(SvgImageHelper.OnHeightPropertyChanged)));
        public static readonly DependencyProperty TargetProperty = DependencyPropertyManager.RegisterAttached("Target", typeof(DependencyObject), typeof(SvgImageHelper), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty SvgImageProperty = DependencyPropertyManager.RegisterAttached("SvgImage", typeof(SvgImage), typeof(SvgImageHelper), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty WpfSvgPaletteProperty;
        public static readonly DependencyProperty OutputSizeProperty;

        static SvgImageHelper()
        {
            Size defaultValue = new Size();
            OutputSizeProperty = DependencyPropertyManager.RegisterAttached("OutputSize", typeof(Size), typeof(SvgImageHelper), new FrameworkPropertyMetadata(defaultValue));
            TreeWalkerProperty = DependencyPropertyManager.RegisterAttached("TreeWalker", typeof(ThemeTreeWalker), typeof(SvgImageHelper), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(SvgImageHelper.OnTreeWalkerPropertyChanged)));
            Type instanceType = typeof(BitmapImage).Assembly.GetType("MS.Internal.WpfWebRequestHelper");
            CreateRequestAndGetResponseStream = ReflectionHelper.CreateInstanceMethodHandler<Func<Uri, Stream>>(null, "CreateRequestAndGetResponseStream", BindingFlags.NonPublic | BindingFlags.Static, instanceType, 1, null, true);
            WpfSvgPaletteProperty = DependencyPropertyManager.RegisterAttached("WpfSvgPalette", typeof(WpfSvgPalette), typeof(SvgImageHelper), new FrameworkPropertyMetadata(null));
        }

        public static SvgImage CreateImage(Stream stream) => 
            SvgLoader.LoadFromStream(stream);

        public static SvgImage CreateImage(Uri uri)
        {
            Stream stream = CreateStream(uri);
            return ((stream == null) ? null : SvgLoader.LoadFromStream(stream));
        }

        public static Stream CreateStream(Uri uri) => 
            CreateRequestAndGetResponseStream(uri);

        public static bool GetAutoSize(DependencyObject obj) => 
            (bool) obj.GetValue(AutoSizeProperty);

        public static double GetHeight(DependencyObject obj) => 
            (double) obj.GetValue(HeightProperty);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static SvgImage GetOrCreate(Uri uri, Func<Uri, SvgImage> createHandler = null)
        {
            if (uri == null)
            {
                return null;
            }
            SvgImage image = ImageCache.Get(uri);
            if (image == null)
            {
                createHandler ??= CreateImageCache.GetValueOrDefault<Uri, Func<Uri, SvgImage>>(uri);
                if (createHandler == null)
                {
                    return null;
                }
                image = createHandler(uri);
                if (image == null)
                {
                    return null;
                }
                ImageCache.Set(uri, image);
                CreateImageCache[uri] = createHandler;
            }
            return image;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static SvgImage GetOrCreateSvgImage(Stream stream, ref object cacheKey)
        {
            cacheKey = null;
            return CreateImage(stream);
        }

        public static Size GetOutputSize(DependencyObject obj) => 
            (Size) obj.GetValue(OutputSizeProperty);

        public static string GetState(DependencyObject obj) => 
            (string) obj.GetValue(StateProperty);

        public static SvgImage GetSvgImage(DependencyObject obj) => 
            (SvgImage) obj.GetValue(SvgImageProperty);

        public static DependencyObject GetTarget(DependencyObject obj) => 
            (DependencyObject) obj.GetValue(TargetProperty);

        public static ThemeTreeWalker GetTreeWalker(DependencyObject obj) => 
            (ThemeTreeWalker) obj.GetValue(TreeWalkerProperty);

        public static double GetWidth(DependencyObject obj) => 
            (double) obj.GetValue(WidthProperty);

        public static WpfSvgPalette GetWpfSvgPalette(DependencyObject obj) => 
            (WpfSvgPalette) obj.GetValue(WpfSvgPaletteProperty);

        protected static void OnHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DrawingImage)
            {
                UpdateImage((DrawingImage) d);
            }
        }

        protected static void OnStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DrawingImage)
            {
                UpdateImage((DrawingImage) d);
            }
        }

        protected static void OnTreeWalkerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DrawingImage)
            {
                UpdateImage((DrawingImage) d);
            }
        }

        protected static void OnWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DrawingImage)
            {
                UpdateImage((DrawingImage) d);
            }
        }

        public static void SetAutoSize(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoSizeProperty, value);
        }

        public static void SetHeight(DependencyObject obj, double value)
        {
            obj.SetValue(HeightProperty, value);
        }

        public static void SetOutputSize(DependencyObject obj, Size value)
        {
            obj.SetValue(OutputSizeProperty, value);
        }

        public static void SetState(DependencyObject obj, string value)
        {
            obj.SetValue(StateProperty, value);
        }

        public static void SetSvgImage(DependencyObject obj, SvgImage value)
        {
            obj.SetValue(SvgImageProperty, value);
        }

        public static void SetTarget(DependencyObject obj, DependencyObject value)
        {
            obj.SetValue(TargetProperty, value);
        }

        public static void SetTreeWalker(DependencyObject obj, ThemeTreeWalker value)
        {
            obj.SetValue(TreeWalkerProperty, value);
        }

        public static void SetWidth(DependencyObject obj, double value)
        {
            obj.SetValue(WidthProperty, value);
        }

        public static void SetWpfSvgPalette(DependencyObject obj, WpfSvgPalette value)
        {
            obj.SetValue(WpfSvgPaletteProperty, value);
        }

        private static void UpdateImage(DrawingImage image)
        {
            SvgImage svgImage = GetSvgImage(image);
            WpfSvgPalette svgPalette = null;
            if (svgImage != null)
            {
                double width;
                double height = GetHeight(image);
                if (double.IsNaN(GetWidth(image)))
                {
                    width = svgImage.Width;
                }
                if (double.IsNaN(height))
                {
                    height = svgImage.Height;
                }
                ThemeTreeWalker treeWalker = GetTreeWalker(image);
                DependencyObject target = GetTarget(image);
                if ((treeWalker != null) && (target != null))
                {
                    svgPalette = treeWalker.InplaceResourceProvider.GetSvgPalette(target);
                }
                string state = "";
                if (target != null)
                {
                    state = GetState(target);
                }
                image.Drawing = null;
                image.Drawing = WpfSvgRenderer.CreateDrawing(svgImage, new Size(width, height), svgPalette, state);
            }
        }
    }
}

