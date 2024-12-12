namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class WatermarkPictureVerticalAlignmentExtension : WatermarkEnumValueSource<VerticalAlignment>
    {
        protected override string GetDisplayName(VerticalAlignment value) => 
            WatermarkLocalizers.LocalizeVerticalAlignment(value);

        protected override IEnumerable<VerticalAlignment> ExcludedItems =>
            new VerticalAlignment[] { VerticalAlignment.Stretch };
    }
}

