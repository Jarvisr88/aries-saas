namespace DevExpress.Xpf.Editors
{
    using System;

    public class ComboBoxStyleSettingsExtension : BaseComboBoxStyleSettingsExtension
    {
        public ComboBoxStyleSettingsExtension()
        {
        }

        public ComboBoxStyleSettingsExtension(bool? scrollToSelectionOnPopup) : base(scrollToSelectionOnPopup)
        {
        }

        protected override BaseComboBoxStyleSettings ProvideValueCore(IServiceProvider serviceProvider) => 
            new ComboBoxStyleSettings();
    }
}

