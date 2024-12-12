namespace DevExpress.Xpf.Office.UI
{
    using System;

    public class MarginsStringToRightCaptionConverter : StringDelimitedWithCRLFAndTabsToPartConverter
    {
        protected override int X =>
            2;

        protected override int Y =>
            2;
    }
}

