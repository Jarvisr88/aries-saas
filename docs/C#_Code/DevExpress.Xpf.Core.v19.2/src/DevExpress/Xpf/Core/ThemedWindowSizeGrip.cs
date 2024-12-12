namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls.Primitives;

    public class ThemedWindowSizeGrip : Thumb
    {
        private Thickness windowPadding;
        private const double correctionFactor = 0.6;
        private const double offset = 1.0;

        static ThemedWindowSizeGrip()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedWindowSizeGrip), new FrameworkPropertyMetadata(typeof(ThemedWindowSizeGrip)));
            Thickness defaultValue = new Thickness();
            FrameworkElement.MarginProperty.OverrideMetadata(typeof(ThemedWindowSizeGrip), new FrameworkPropertyMetadata(defaultValue, null, new CoerceValueCallback(ThemedWindowSizeGrip.CoerceMarginProperty)));
        }

        private object CoerceMargin(Thickness padding)
        {
            this.windowPadding = padding;
            return (((padding.Right < (base.RenderSize.Width * 0.6)) || (padding.Bottom < (base.RenderSize.Height * 0.6))) ? new Thickness(0.0, 0.0, padding.Right + 1.0, padding.Bottom + 1.0) : new Thickness(0.0, 0.0, 1.0, 1.0));
        }

        private static object CoerceMarginProperty(DependencyObject d, object baseValue) => 
            ((ThemedWindowSizeGrip) d).CoerceMargin((Thickness) baseValue);

        protected override AutomationPeer OnCreateAutomationPeer() => 
            null;

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.CoerceMargin(this.windowPadding);
        }
    }
}

