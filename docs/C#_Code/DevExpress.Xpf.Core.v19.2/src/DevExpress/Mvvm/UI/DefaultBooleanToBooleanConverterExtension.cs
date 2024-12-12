namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Windows.Markup;

    public class DefaultBooleanToBooleanConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new DefaultBooleanToBooleanConverter();
    }
}

