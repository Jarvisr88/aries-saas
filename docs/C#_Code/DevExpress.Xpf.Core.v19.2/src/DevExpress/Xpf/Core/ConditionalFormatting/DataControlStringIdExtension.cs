namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DataControlStringIdExtension : MarkupExtension
    {
        public DataControlStringIdExtension(ConditionalFormattingStringId stringId)
        {
            this.StringId = stringId;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            ConditionalFormattingLocalizer.GetString(this.StringId);

        public ConditionalFormattingStringId StringId { get; set; }
    }
}

