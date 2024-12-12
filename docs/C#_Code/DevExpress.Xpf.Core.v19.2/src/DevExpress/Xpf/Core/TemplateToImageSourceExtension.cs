namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class TemplateToImageSourceExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            ButtonsThemeKeyExtension extension1 = new ButtonsThemeKeyExtension();
            extension1.ResourceKey = this.ResourceKey;
            ButtonsThemeKeyExtension resourceKey = extension1;
            Func<IProvideValueTarget, object> evaluator = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<IProvideValueTarget, object> local1 = <>c.<>9__4_0;
                evaluator = <>c.<>9__4_0 = x => x.TargetObject;
            }
            object obj2 = (serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget).With<IProvideValueTarget, object>(evaluator);
            DataTemplate template = null;
            if (obj2 != null)
            {
                FrameworkElement element = obj2 as FrameworkElement;
                if (element != null)
                {
                    template = element.TryFindResource(resourceKey) as DataTemplate;
                }
                if (template == null)
                {
                    FrameworkContentElement element2 = obj2 as FrameworkContentElement;
                    if (element2 != null)
                    {
                        template = element2.TryFindResource(resourceKey) as DataTemplate;
                    }
                }
            }
            template ??= (Application.Current.With<Application, object>(x => x.TryFindResource(resourceKey)) as DataTemplate);
            if (template == null)
            {
                return null;
            }
            ContentControl visual = new ContentControl {
                Content = "",
                ContentTemplate = template
            };
            visual.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            visual.Arrange(new Rect(new Point(0.0, 0.0), visual.DesiredSize));
            visual.UpdateLayout();
            VisualBrush brush1 = new VisualBrush(visual);
            brush1.Stretch = Stretch.None;
            brush1.ViewboxUnits = BrushMappingMode.Absolute;
            brush1.ViewportUnits = BrushMappingMode.Absolute;
            VisualBrush brush = brush1;
            brush.Viewbox = new Rect(visual.RenderSize);
            brush.Viewport = new Rect(visual.RenderSize);
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int) (visual.RenderSize.Width * ScreenHelper.ScaleX), (int) (visual.RenderSize.Height * ScreenHelper.ScaleX), ScreenHelper.CurrentDpi, ScreenHelper.CurrentDpi, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            return bitmap;
        }

        public ButtonsThemeKeys ResourceKey { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TemplateToImageSourceExtension.<>c <>9 = new TemplateToImageSourceExtension.<>c();
            public static Func<IProvideValueTarget, object> <>9__4_0;

            internal object <ProvideValue>b__4_0(IProvideValueTarget x) => 
                x.TargetObject;
        }
    }
}

