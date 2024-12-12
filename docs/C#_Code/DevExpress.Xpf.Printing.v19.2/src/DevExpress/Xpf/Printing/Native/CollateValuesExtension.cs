namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Windows.Markup;

    public class CollateValuesExtension : MarkupExtension
    {
        private static readonly PrintingStringId[] values = new PrintingStringId[] { PrintingStringId.Collated, PrintingStringId.Uncollated };

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            values;
    }
}

