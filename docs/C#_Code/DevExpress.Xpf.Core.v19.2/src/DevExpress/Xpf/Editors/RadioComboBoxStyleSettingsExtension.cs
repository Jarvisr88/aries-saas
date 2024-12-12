namespace DevExpress.Xpf.Editors
{
    using System;

    public class RadioComboBoxStyleSettingsExtension : BaseComboBoxStyleSettingsExtension
    {
        public RadioComboBoxStyleSettingsExtension()
        {
        }

        public RadioComboBoxStyleSettingsExtension(bool? scrollToSelectionOnPopup) : base(scrollToSelectionOnPopup)
        {
        }

        protected override BaseComboBoxStyleSettings ProvideValueCore(IServiceProvider serviceProvider) => 
            new RadioComboBoxStyleSettings();
    }
}

