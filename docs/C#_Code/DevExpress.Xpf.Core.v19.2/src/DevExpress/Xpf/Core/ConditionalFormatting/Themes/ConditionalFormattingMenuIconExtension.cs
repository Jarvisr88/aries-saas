namespace DevExpress.Xpf.Core.ConditionalFormatting.Themes
{
    using System;

    public class ConditionalFormattingMenuIconExtension : ConditionalFormattingIconExtensionBase
    {
        public ConditionalFormattingMenuIconExtension(string iconName) : base(iconName)
        {
        }

        protected override string IconPrefix =>
            "Menu/ConditionalFormatting";

        protected override string SvgIconPrefix =>
            "Menu/SVG/";
    }
}

