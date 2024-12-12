namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class SystemPrintRangeExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this.Range;

        public PrintRange Range { get; set; }
    }
}

