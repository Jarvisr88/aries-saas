namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;

    public class UniqueDuplicateEditUnit : ExpressionEditUnit
    {
        private UniqueDuplicateRule rule;

        public override IModelItem BuildCondition(IConditionModelItemsBuilder builder, IModelItem source) => 
            builder.BuildUniqueDuplicateCondition(this, source);

        public override string GetDescription(IDialogContext context) => 
            (this.Rule != UniqueDuplicateRule.Duplicate) ? ((this.Rule != UniqueDuplicateRule.Unique) ? string.Empty : ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_UniqueDuplicateDialog_Unique)) : ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_UniqueDuplicateDialog_Duplicate);

        public UniqueDuplicateRule Rule
        {
            get => 
                this.rule;
            set
            {
                if (this.rule != value)
                {
                    this.rule = value;
                }
                base.RegisterPropertyModification("Rule");
            }
        }
    }
}

