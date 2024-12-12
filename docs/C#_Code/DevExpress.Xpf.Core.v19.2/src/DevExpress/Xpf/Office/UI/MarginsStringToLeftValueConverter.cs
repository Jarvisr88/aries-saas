namespace DevExpress.Xpf.Office.UI
{
    using System;

    public class MarginsStringToLeftValueConverter : StringDelimitedWithCRLFAndTabsToPartConverter
    {
        protected override int X =>
            1;

        protected override int Y =>
            2;
    }
}

