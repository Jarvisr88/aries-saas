namespace DevExpress.Xpf.Editors
{
    using System;

    public class RadioListBoxEditStyleSettingsExtension : ListBoxEditStyleSettingsExtension
    {
        protected override BaseListBoxEditStyleSettings ProvideValueCore(IServiceProvider serviceProvider)
        {
            RadioListBoxEditStyleSettings settings1 = new RadioListBoxEditStyleSettings();
            settings1.SelectionEventMode = base.SelectionEventMode;
            return settings1;
        }
    }
}

