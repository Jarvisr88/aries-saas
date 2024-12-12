namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class TimeSpanRangeExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new TimeSpanRange(this.Start, this.End);

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }
    }
}

