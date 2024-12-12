namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    public class ThicknessExtension : MarkupExtension
    {
        public ThicknessExtension();
        public ThicknessExtension(Thickness origin);
        public override object ProvideValue(IServiceProvider serviceProvider);

        public Thickness OriginValue { get; set; }
    }
}

