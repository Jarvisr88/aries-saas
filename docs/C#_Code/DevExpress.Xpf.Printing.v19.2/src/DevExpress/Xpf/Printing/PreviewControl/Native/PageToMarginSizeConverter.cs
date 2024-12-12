namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Windows;

    public class PageToMarginSizeConverter : PageToMarginSizeConverterBase
    {
        protected override object GetResult(PageViewModel page, double actualZoom) => 
            new GridLength(((float) base.GetMarginSize(page, false)).DocToDip() * actualZoom);
    }
}

