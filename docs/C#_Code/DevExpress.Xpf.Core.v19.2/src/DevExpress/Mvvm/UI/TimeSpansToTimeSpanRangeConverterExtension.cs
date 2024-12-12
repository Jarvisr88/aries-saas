namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Windows.Markup;

    public class TimeSpansToTimeSpanRangeConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new TimeSpansToTimeSpanRangeConverter();
    }
}

