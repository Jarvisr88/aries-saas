namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class WatermarkPictureHorizontalAlignmentExtension : WatermarkEnumValueSource<HorizontalAlignment>
    {
        protected override string GetDisplayName(HorizontalAlignment value) => 
            WatermarkLocalizers.LocalizeHorizontalAlignment(value);

        protected override IEnumerable<HorizontalAlignment> ExcludedItems =>
            new HorizontalAlignment[] { HorizontalAlignment.Stretch };
    }
}

