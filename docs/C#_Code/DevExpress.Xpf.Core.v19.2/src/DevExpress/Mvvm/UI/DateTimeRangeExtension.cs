namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DateTimeRangeExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new DateTimeRange(this.Start, this.End);

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}

