namespace DevExpress.Xpf.Core.ConditionalFormatting.Themes
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;

    public class StandardIconSetFormatInfoExtension : StandardIndicatorFormatInfoExtension
    {
        protected override string GetDescription() => 
            ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_PredefinedIconSetFormat_Description);

        protected override string GetGroupName() => 
            ConditionalFormattingLocalizer.GetString(this.GetStringIdPrefix() + this.Group.ToString());

        protected override string GetStringIdPrefix() => 
            "ConditionalFormatting_PredefinedIconSetFormat_";

        public StandardIconSetFormatGroup Group { get; set; }
    }
}

