namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class ToolbarSeparatorAdjuster : SeparatorAdjuster
    {
        protected override bool IsSeparator(object item);
        protected override bool IsVisible(object item);
        protected override void SetVisibility(object item, bool visible);
    }
}

