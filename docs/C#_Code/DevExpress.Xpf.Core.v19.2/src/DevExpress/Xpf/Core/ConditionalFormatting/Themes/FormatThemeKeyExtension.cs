namespace DevExpress.Xpf.Core.ConditionalFormatting.Themes
{
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Utils.Themes;
    using System;

    public class FormatThemeKeyExtension : ThemeKeyExtensionBase<string>
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            string str = DynamicConditionalFormattingResourceExtension.CorrectThemeName(serviceProvider);
            if (!string.IsNullOrEmpty(str))
            {
                base.ThemeName = str;
            }
            return this;
        }
    }
}

