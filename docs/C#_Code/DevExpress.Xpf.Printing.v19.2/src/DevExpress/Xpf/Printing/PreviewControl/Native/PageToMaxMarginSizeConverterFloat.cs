namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.XtraPrinting;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class PageToMaxMarginSizeConverterFloat : MarkupExtension, IValueConverter
    {
        protected double GetMarginSize(PageViewModel page)
        {
            MarginSide side = this.Side;
            switch (side)
            {
                case MarginSide.Left:
                    return page.PageMargins.Right;

                case MarginSide.Top:
                    return page.PageMargins.Bottom;

                case (MarginSide.Top | MarginSide.Left):
                    break;

                case MarginSide.Right:
                    return page.PageMargins.Left;

                default:
                    if (side != MarginSide.Bottom)
                    {
                        break;
                    }
                    return page.PageMargins.Top;
            }
            throw new InvalidOperationException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PageViewModel page = Guard.ArgumentMatchType<PageViewModel>(value, "value");
            return (((this.IsVertical ? page.PageSize.Height : page.PageSize.Width) - this.GetMarginSize(page)) - 25.0);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public MarginSide Side { get; set; }

        private bool IsVertical =>
            (this.Side == MarginSide.Top) || (this.Side == MarginSide.Bottom);
    }
}

