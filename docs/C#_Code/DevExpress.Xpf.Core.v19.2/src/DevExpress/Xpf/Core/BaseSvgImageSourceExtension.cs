namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Utils.Svg;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public abstract class BaseSvgImageSourceExtension : MarkupExtension
    {
        private IServiceProvider serviceProvider;
        private IProvideValueTarget targetProvider;

        protected BaseSvgImageSourceExtension()
        {
            this.AutoSize = true;
        }

        protected virtual bool CheckServiceProvider(IServiceProvider provider, out bool returnSelf, out bool returnBinding, out bool returnExpression) => 
            UriQualifierHelper.CheckServiceProvider(provider, out returnSelf, out returnBinding, out returnExpression);

        protected virtual MultiBinding CreateBinding(Uri svgKey, SvgImage image, System.Windows.Size size)
        {
            // Unresolved stack state at '0000009E'
        }

        protected virtual IMultiValueConverter CreateConverter(SvgImage image, System.Windows.Size size, bool autoSize) => 
            this.CreateConverter(null, image, size, autoSize);

        protected virtual IMultiValueConverter CreateConverter(Uri cacheKey, SvgImage image, System.Windows.Size size, bool autoSize) => 
            new TreeWalkerToSvgImageConverter(cacheKey, image, size, autoSize);

        protected virtual SvgImage CreateSvgImage(Uri uri) => 
            SvgImageHelper.GetOrCreate(uri, new Func<Uri, SvgImage>(SvgImageHelper.CreateImage));

        protected abstract Uri CreateSvgUri(IServiceProvider provider);
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object obj2;
            try
            {
                this.serviceProvider = serviceProvider;
                obj2 = this.ProvideValueCore();
            }
            finally
            {
                this.serviceProvider = null;
                this.targetProvider = null;
            }
            return obj2;
        }

        private object ProvideValueCore()
        {
            bool flag;
            bool flag2;
            bool flag3;
            Uri uri = this.CreateSvgUri(this.serviceProvider);
            if (uri == null)
            {
                return null;
            }
            SvgImage image = this.CreateSvgImage(uri);
            System.Windows.Size? nullable = this.Size;
            System.Windows.Size size = (nullable != null) ? nullable.GetValueOrDefault() : new System.Windows.Size(image.Width, image.Height);
            if (this.CheckServiceProvider(this.serviceProvider, out flag, out flag2, out flag3))
            {
                if (flag)
                {
                    return this;
                }
                MultiBinding binding = this.CreateBinding(uri, image, size);
                if (flag2)
                {
                    return binding;
                }
                if (flag3)
                {
                    return binding.ProvideValue(this.serviceProvider);
                }
            }
            return WpfSvgRenderer.CreateImageSourceCore(image, new System.Windows.Size?(size), null, null, null, null, new bool?(this.AutoSize), this.ActionBeforeFreeze, null, null);
        }

        public System.Windows.Size? Size { get; set; }

        public bool AutoSize { get; set; }

        internal Action<DrawingImage> ActionBeforeFreeze { get; set; }

        private IProvideValueTarget TargetProvider
        {
            get
            {
                IProvideValueTarget target;
                if (this.targetProvider != null)
                {
                    return this.targetProvider;
                }
                if (this.serviceProvider == null)
                {
                    return null;
                }
                this.targetProvider = target = (IProvideValueTarget) this.serviceProvider.GetService(typeof(IProvideValueTarget));
                return target;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseSvgImageSourceExtension.<>c <>9 = new BaseSvgImageSourceExtension.<>c();
            public static Func<IProvideValueTarget, object> <>9__22_0;
            public static Func<IAttachableObject, IAttachableObject> <>9__22_1;
            public static Func<IAttachableObject, bool> <>9__22_2;
            public static Func<int, string> <>9__22_3;

            internal object <CreateBinding>b__22_0(IProvideValueTarget x) => 
                x.TargetObject;

            internal IAttachableObject <CreateBinding>b__22_1(IAttachableObject x) => 
                x.AssociatedObject as IAttachableObject;

            internal bool <CreateBinding>b__22_2(IAttachableObject x) => 
                ReferenceEquals(x, null);

            internal string <CreateBinding>b__22_3(int _) => 
                "AssociatedObject.";
        }

        private class TreeWalkerToSvgImageConverter : IMultiValueConverter
        {
            private readonly Uri uri;
            private SvgImage image;
            private readonly Size size;
            private readonly bool autoSize;

            public TreeWalkerToSvgImageConverter(Uri uri, SvgImage image, Size size, bool autoSize)
            {
                this.image = image;
                this.size = size;
                this.autoSize = autoSize;
                this.uri = uri;
            }

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if ((this.image == null) && (this.uri == null))
                {
                    return null;
                }
                DependencyObject element = values[0] as DependencyObject;
                WpfSvgPalette palette = values[3] as WpfSvgPalette;
                ThemeTreeWalker walker = values[1] as ThemeTreeWalker;
                WpfSvgPalette svgPalette = ((walker != null) ? walker.InplaceResourceProvider : ThemeHelper.Instance).GetSvgPalette(element);
                string state = values[2] as string;
                if (this.image != null)
                {
                    return WpfSvgRenderer.CreateImageSource(this.image, this.size, palette, svgPalette, state, this.autoSize);
                }
                if (this.uri == null)
                {
                    return null;
                }
                this.image = SvgImageHelper.GetOrCreate(this.uri, null);
                return ((this.image != null) ? WpfSvgRenderer.CreateImageSource(this.image, this.size, palette, svgPalette, state, this.autoSize) : null);
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}

