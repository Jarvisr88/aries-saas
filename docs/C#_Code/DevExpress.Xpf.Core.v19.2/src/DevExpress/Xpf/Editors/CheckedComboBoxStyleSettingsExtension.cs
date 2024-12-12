namespace DevExpress.Xpf.Editors
{
    using System;

    public class CheckedComboBoxStyleSettingsExtension : BaseComboBoxStyleSettingsExtension
    {
        public CheckedComboBoxStyleSettingsExtension()
        {
        }

        public CheckedComboBoxStyleSettingsExtension(bool? scrollToSelectionOnPopup) : base(scrollToSelectionOnPopup)
        {
        }

        protected override BaseComboBoxStyleSettings ProvideValueCore(IServiceProvider serviceProvider) => 
            new CheckedComboBoxStyleSettings();
    }
}

