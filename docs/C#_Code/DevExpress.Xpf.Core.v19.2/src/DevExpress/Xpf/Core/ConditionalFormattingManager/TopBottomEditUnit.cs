namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Text;

    public class TopBottomEditUnit : ExpressionEditUnit
    {
        private TopBottomRule rule;
        private double threshold;

        public override IModelItem BuildCondition(IConditionModelItemsBuilder builder, IModelItem source) => 
            builder.BuildTopBottomCondition(this, source);

        public override string GetDescription(IDialogContext context)
        {
            if (this.IsAboveBelowCondition())
            {
                return ConditionalFormattingLocalizer.GetString((this.Rule == TopBottomRule.AboveAverage) ? ConditionalFormattingStringId.ConditionalFormatting_Manager_Above : ConditionalFormattingStringId.ConditionalFormatting_Manager_Below);
            }
            if (!this.IsTopBottomCondition())
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(ConditionalFormattingLocalizer.GetString(((this.Rule == TopBottomRule.TopItems) || (this.Rule == TopBottomRule.TopPercent)) ? ConditionalFormattingStringId.ConditionalFormatting_Manager_Top : ConditionalFormattingStringId.ConditionalFormatting_Manager_Bottom));
            builder.Append(' ');
            builder.Append(this.Threshold);
            if ((this.Rule == TopBottomRule.TopPercent) || (this.Rule == TopBottomRule.BottomPercent))
            {
                builder.Append("%");
            }
            return builder.ToString();
        }

        public bool IsAboveBelowCondition() => 
            (this.Rule == TopBottomRule.AboveAverage) || (this.Rule == TopBottomRule.BelowAverage);

        public bool IsTopBottomCondition() => 
            (this.Rule == TopBottomRule.BottomItems) || ((this.Rule == TopBottomRule.BottomPercent) || ((this.Rule == TopBottomRule.TopItems) || (this.Rule == TopBottomRule.TopPercent)));

        public TopBottomRule Rule
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

        public double Threshold
        {
            get => 
                this.threshold;
            set
            {
                if (this.threshold != value)
                {
                    this.threshold = value;
                }
                base.RegisterPropertyModification("Threshold");
            }
        }
    }
}

