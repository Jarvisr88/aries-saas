namespace DevExpress.Xpf.Office.UI
{
    using System;

    public class MarginsStringToLeftCaptionConverter : StringDelimitedWithCRLFAndTabsToPartConverter
    {
        protected override int X =>
            0;

        protected override int Y =>
            2;
    }
}

