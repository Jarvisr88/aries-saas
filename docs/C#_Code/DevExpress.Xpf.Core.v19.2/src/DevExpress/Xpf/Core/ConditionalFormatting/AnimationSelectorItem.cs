namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using System;
    using System.Runtime.CompilerServices;

    public class AnimationSelectorItem
    {
        public AnimationSelectorItem(ConditionalFormattingStringId stringId, DataUpdateRule rule)
        {
            this.StringId = stringId;
            this.Rule = rule;
        }

        public override string ToString() => 
            ConditionalFormattingLocalizer.GetString(this.StringId);

        public ConditionalFormattingStringId StringId { get; private set; }

        public DataUpdateRule Rule { get; private set; }
    }
}

