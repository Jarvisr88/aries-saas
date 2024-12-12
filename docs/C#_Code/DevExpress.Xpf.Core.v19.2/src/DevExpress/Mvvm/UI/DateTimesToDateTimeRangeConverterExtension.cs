namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Windows.Markup;

    public class DateTimesToDateTimeRangeConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new DateTimesToDateTimeRangeConverter();
    }
}

