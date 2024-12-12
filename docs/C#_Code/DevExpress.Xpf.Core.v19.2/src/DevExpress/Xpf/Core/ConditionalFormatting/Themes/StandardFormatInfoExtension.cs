namespace DevExpress.Xpf.Core.ConditionalFormatting.Themes
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class StandardFormatInfoExtension : MarkupExtension
    {
        protected virtual string GetDescription() => 
            null;

        protected virtual string GetGroupName() => 
            null;

        protected virtual ImageSource GetIcon() => 
            null;

        protected virtual string GetStringIdPrefix() => 
            "ConditionalFormatting_PredefinedFormat_";

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            FormatThemeKeyExtension extension1 = new FormatThemeKeyExtension();
            extension1.ResourceKey = this.FormatName;
            object resourceKey = extension1.ProvideValue(serviceProvider);
            FormatInfo info1 = new FormatInfo();
            info1.FormatName = this.FormatName;
            info1.Format = new StaticResourceExtension(resourceKey).ProvideValue(serviceProvider);
            info1.DisplayName = ConditionalFormattingLocalizer.GetString(this.GetStringIdPrefix() + this.FormatName);
            info1.Icon = this.GetIcon();
            info1.Description = this.GetDescription();
            info1.GroupName = this.GetGroupName();
            FormatInfo info = info1;
            info.Freeze();
            return info;
        }

        public string FormatName { get; set; }
    }
}

