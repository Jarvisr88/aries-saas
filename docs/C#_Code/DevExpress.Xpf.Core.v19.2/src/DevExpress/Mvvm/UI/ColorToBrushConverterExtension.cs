namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class ColorToBrushConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            ColorToBrushConverter converter1 = new ColorToBrushConverter();
            converter1.CustomA = this.CustomA;
            return converter1;
        }

        public byte? CustomA { get; set; }
    }
}

