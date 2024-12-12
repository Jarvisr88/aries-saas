namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;

    public class AnimationEditUnit : ExpressionEditUnit
    {
        private DataUpdateRule rule;
        private DataUpdateAnimationSettings animationSettings;

        public override IModelItem BuildCondition(IConditionModelItemsBuilder builder, IModelItem source) => 
            builder.BuildDataUpdateFormatCondition(this, source);

        public override string GetDescription(IDialogContext context) => 
            ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_Manager_AnimationRule);

        public DataUpdateRule Rule
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

        public DataUpdateAnimationSettings AnimationSettings
        {
            get => 
                this.animationSettings;
            set
            {
                if (!ReferenceEquals(this.animationSettings, value))
                {
                    this.animationSettings = value;
                }
                base.RegisterPropertyModification("AnimationSettings");
            }
        }
    }
}

