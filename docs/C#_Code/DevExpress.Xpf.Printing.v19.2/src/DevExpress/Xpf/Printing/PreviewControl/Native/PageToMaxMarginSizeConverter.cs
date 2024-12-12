namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.XtraPrinting.Native;
    using System;

    public class PageToMaxMarginSizeConverter : PageToMarginSizeConverterBase
    {
        protected override object GetResult(PageViewModel page, double actualZoom)
        {
            double marginSize = base.GetMarginSize(page, true);
            double num3 = ((base.IsVertical ? ((double) page.Page.PageSize.Height) : ((double) page.Page.PageSize.Width)) - marginSize) - 25.0;
            if (num3 < 0.0)
            {
                throw new InvalidOperationException();
            }
            return (((float) num3).DocToDip() * actualZoom);
        }
    }
}

