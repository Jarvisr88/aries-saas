namespace DevExpress.Xpf.Core.ConditionalFormatting.Themes
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;

    public class StandardDataBarFormatInfoExtension : StandardIndicatorFormatInfoExtension
    {
        protected override string GetDescription() => 
            ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_PredefinedDataBarFormat_Description);

        protected override string GetGroupName() => 
            ConditionalFormattingLocalizer.GetString(this.GetStringIdPrefix() + this.Group.ToString());

        protected override string GetStringIdPrefix() => 
            "ConditionalFormatting_PredefinedDataBarFormat_";

        public StandardDataBarFormatGroup Group { get; set; }
    }
}

