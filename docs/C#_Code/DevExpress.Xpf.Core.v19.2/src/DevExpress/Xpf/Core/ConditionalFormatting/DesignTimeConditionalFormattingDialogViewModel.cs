namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DesignTimeConditionalFormattingDialogViewModel : IConditionalFormattingDialogViewModel
    {
        public string Description { get; set; }

        public string ConnectorText { get; set; }

        public IEnumerable<FormatInfo> Formats =>
            null;

        public FormatInfo SelectedFormatInfo { get; set; }

        public object Value { get; set; }

        public DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType { get; set; }
    }
}

