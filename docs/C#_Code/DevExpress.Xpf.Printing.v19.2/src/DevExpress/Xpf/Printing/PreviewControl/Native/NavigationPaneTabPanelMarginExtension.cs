namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Markup;

    public class NavigationPaneTabPanelMarginExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Thickness thickness = new Thickness {
                Left = -7.0,
                Top = 0.0,
                Right = 3.0,
                Bottom = 1.0
            };
            return thickness.Multiply((1.0 / ScreenHelper.DpiThicknessCorrection));
        }
    }
}

