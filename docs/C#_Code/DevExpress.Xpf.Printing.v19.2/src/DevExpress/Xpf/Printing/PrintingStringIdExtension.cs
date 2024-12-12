namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class PrintingStringIdExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            PrintingLocalizer.GetString(this.StringId);

        public PrintingStringId StringId { get; set; }
    }
}

