namespace DevExpress.Xpf.Office.UI
{
    using System;

    public class MarginsStringToBottomCaptionConverter : StringDelimitedWithCRLFAndTabsToPartConverter
    {
        protected override int X =>
            2;

        protected override int Y =>
            1;
    }
}

