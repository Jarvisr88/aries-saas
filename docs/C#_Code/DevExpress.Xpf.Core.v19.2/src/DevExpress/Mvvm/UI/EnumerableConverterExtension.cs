namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class EnumerableConverterExtension : MarkupExtension
    {
        public EnumerableConverterExtension()
        {
        }

        public EnumerableConverterExtension(IValueConverter itemConverter)
        {
            this.ItemConverter = itemConverter;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            EnumerableConverter converter1 = new EnumerableConverter();
            converter1.ItemConverter = this.ItemConverter;
            converter1.TargetItemType = this.TargetItemType;
            return converter1;
        }

        public IValueConverter ItemConverter { get; set; }

        public Type TargetItemType { get; set; }
    }
}

