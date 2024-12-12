namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class RenderFontSettingsExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider);

        public double? FontSize { get; set; }

        public System.Windows.Media.FontFamily FontFamily { get; set; }

        public System.Windows.FontStretch? FontStretch { get; set; }

        public System.Windows.FontStyle? FontStyle { get; set; }

        public System.Windows.FontWeight? FontWeight { get; set; }
    }
}

