namespace DevExpress.Xpf.Core.ConditionalFormatting.Themes
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;
    using System.Windows.Media.Imaging;

    public abstract class ConditionalFormattingIconExtensionBase : MarkupExtension
    {
        public ConditionalFormattingIconExtensionBase(string iconName)
        {
            this.IconName = iconName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!this.UseSvg)
            {
                return new BitmapImage(new Uri(ConditionalFormatResourceHelper.BasePathCore + this.IconPrefix + this.IconName, UriKind.Absolute));
            }
            SvgImageSourceExtension extension = new SvgImageSourceExtension {
                Uri = new Uri(ConditionalFormatResourceHelper.BasePathCore + this.SvgIconPrefix + this.SvgIconName, UriKind.Absolute)
            };
            return extension.ProvideValue(null);
        }

        protected virtual string IconPrefix =>
            string.Empty;

        protected virtual string SvgIconPrefix =>
            string.Empty;

        public string IconName { get; set; }

        public string SvgIconName { get; set; }

        protected bool UseSvg =>
            ApplicationThemeHelper.UseDefaultSvgImages && !string.IsNullOrEmpty(this.SvgIconName);
    }
}

