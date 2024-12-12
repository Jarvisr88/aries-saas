namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows.Markup;

    public class TrackBarEditRangeMultiValueConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new TrackBarEditRangeMultiValueConverter();
    }
}

