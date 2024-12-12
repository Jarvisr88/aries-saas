namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.XtraPrinting;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    public abstract class PageToMarginSizeConverterBase : MarkupExtension, IMultiValueConverter
    {
        protected PageToMarginSizeConverterBase()
        {
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            PageViewModel page = Guard.ArgumentMatchType<PageViewModel>(values[0], "values[0]");
            double num = Guard.ArgumentMatchType<double>(values[1], "values[1]");
            return this.GetResult(page, ScreenHelper.ScaleX * num);
        }

        protected double GetMarginSize(PageViewModel page, bool reverse)
        {
            MarginSide side = this.Side;
            switch (side)
            {
                case MarginSide.Left:
                    return (reverse ? page.PageMargins.Right : page.PageMargins.Left);

                case MarginSide.Top:
                    return (reverse ? page.PageMargins.Bottom : page.PageMargins.Top);

                case (MarginSide.Top | MarginSide.Left):
                    break;

                case MarginSide.Right:
                    return (reverse ? page.PageMargins.Left : page.PageMargins.Right);

                default:
                    if (side != MarginSide.Bottom)
                    {
                        break;
                    }
                    return (reverse ? page.PageMargins.Top : page.PageMargins.Bottom);
            }
            throw new InvalidOperationException();
        }

        protected abstract object GetResult(PageViewModel page, double actualZoom);
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public MarginSide Side { get; set; }

        protected bool IsVertical =>
            (this.Side == MarginSide.Top) || (this.Side == MarginSide.Bottom);
    }
}

