namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Resources;

    public class ConditionalFormattingResXLocalizer : DXResXLocalizer<ConditionalFormattingStringId>
    {
        public ConditionalFormattingResXLocalizer() : base(new ConditionalFormattingLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Xpf.Core.Core.ConditionalFormatting.LocalizationRes", typeof(ConditionalFormattingResXLocalizer).Assembly);
    }
}

