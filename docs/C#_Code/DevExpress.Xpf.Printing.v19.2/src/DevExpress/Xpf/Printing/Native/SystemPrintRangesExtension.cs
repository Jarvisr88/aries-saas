namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Drawing.Printing;
    using System.Windows.Markup;

    public class SystemPrintRangesExtension : MarkupExtension
    {
        private static readonly PrintRange[] ranges;

        static SystemPrintRangesExtension()
        {
            PrintRange[] rangeArray1 = new PrintRange[3];
            rangeArray1[1] = PrintRange.CurrentPage;
            rangeArray1[2] = PrintRange.SomePages;
            ranges = rangeArray1;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            ranges;
    }
}

