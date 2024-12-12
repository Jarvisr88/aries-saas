namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;

    public class MenuItemSeparatorAdjuster : SeparatorAdjuster
    {
        protected override IList GetItems(object item);
        protected override bool IsSeparator(object item);
        protected override bool IsVisible(object item);
        protected override void SetVisibility(object item, bool visible);
    }
}

