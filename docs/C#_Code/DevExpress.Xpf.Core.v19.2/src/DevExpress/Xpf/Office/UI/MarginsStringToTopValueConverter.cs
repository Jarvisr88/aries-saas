namespace DevExpress.Xpf.Office.UI
{
    using System;

    public class MarginsStringToTopValueConverter : StringDelimitedWithCRLFAndTabsToPartConverter
    {
        protected override int X =>
            1;

        protected override int Y =>
            1;
    }
}

