namespace DevExpress.Xpf.Editors
{
    using System;

    public class CheckedListBoxStyleSettingsExtension : ListBoxEditStyleSettingsExtension
    {
        protected override BaseListBoxEditStyleSettings ProvideValueCore(IServiceProvider serviceProvider)
        {
            CheckedListBoxEditStyleSettings settings1 = new CheckedListBoxEditStyleSettings();
            settings1.SelectionEventMode = base.SelectionEventMode;
            return settings1;
        }
    }
}

