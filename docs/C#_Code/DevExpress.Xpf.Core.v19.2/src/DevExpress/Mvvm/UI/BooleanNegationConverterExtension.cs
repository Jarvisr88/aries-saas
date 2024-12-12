namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Windows.Markup;

    public class BooleanNegationConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new BooleanNegationConverter();
    }
}

