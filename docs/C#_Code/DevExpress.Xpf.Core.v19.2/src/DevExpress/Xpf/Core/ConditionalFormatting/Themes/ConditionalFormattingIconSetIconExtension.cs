namespace DevExpress.Xpf.Core.ConditionalFormatting.Themes
{
    using DevExpress.Xpf.Core;
    using System;
    using System.IO;

    public class ConditionalFormattingIconSetIconExtension : ConditionalFormattingIconExtensionBase
    {
        public ConditionalFormattingIconSetIconExtension() : base(string.Empty)
        {
        }

        public ConditionalFormattingIconSetIconExtension(string iconName) : base(iconName)
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IconSetExtension extension1 = new IconSetExtension();
            extension1.Name = Path.GetFileNameWithoutExtension(base.IconName);
            return extension1.ProvideValue(null);
        }

        protected override string IconPrefix =>
            "IconSets/";

        protected override string SvgIconPrefix =>
            "IconSets/SVG/";
    }
}

