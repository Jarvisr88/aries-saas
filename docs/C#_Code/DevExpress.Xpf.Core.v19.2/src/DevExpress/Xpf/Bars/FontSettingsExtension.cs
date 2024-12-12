namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class FontSettingsExtension : MarkupExtension
    {
        public FontSettingsExtension();
        public override object ProvideValue(IServiceProvider serviceProvider);

        public Brush Normal { get; set; }

        public Brush Hover { get; set; }

        public Brush Pressed { get; set; }

        public Brush Disabled { get; set; }

        public FontFamily Family { get; set; }

        public FontWeight? Weight { get; set; }

        public double? Size { get; set; }
    }
}

