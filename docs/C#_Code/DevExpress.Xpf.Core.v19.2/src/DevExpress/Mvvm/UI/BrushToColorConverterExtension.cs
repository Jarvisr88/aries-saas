namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Windows.Markup;

    public class BrushToColorConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new BrushToColorConverter();
    }
}

