namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class RotateTransformExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider);

        public double Angle { get; set; }
    }
}

