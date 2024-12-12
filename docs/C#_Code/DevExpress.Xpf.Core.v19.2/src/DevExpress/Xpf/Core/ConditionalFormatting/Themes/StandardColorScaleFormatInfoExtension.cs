namespace DevExpress.Xpf.Core.ConditionalFormatting.Themes
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;

    public class StandardColorScaleFormatInfoExtension : StandardIndicatorFormatInfoExtension
    {
        protected override string GetDescription() => 
            ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_PredefinedColorScaleFormat_Description);

        protected override string GetStringIdPrefix() => 
            "ConditionalFormatting_PredefinedColorScaleFormat_";
    }
}

