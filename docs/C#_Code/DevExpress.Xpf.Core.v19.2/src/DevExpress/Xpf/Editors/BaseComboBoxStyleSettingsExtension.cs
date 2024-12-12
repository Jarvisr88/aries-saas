namespace DevExpress.Xpf.Editors
{
    using System;
    using System.ComponentModel;
    using System.Windows.Markup;

    public abstract class BaseComboBoxStyleSettingsExtension : MarkupExtension
    {
        private bool? scrollToSelectionOnPopup;

        protected BaseComboBoxStyleSettingsExtension()
        {
        }

        protected BaseComboBoxStyleSettingsExtension(bool? scrollToSelectionOnPopup)
        {
            this.scrollToSelectionOnPopup = scrollToSelectionOnPopup;
        }

        public sealed override object ProvideValue(IServiceProvider serviceProvider)
        {
            BaseComboBoxStyleSettings settings = this.ProvideValueCore(serviceProvider);
            settings.ScrollToSelectionOnPopup = this.ScrollToSelectionOnPopup;
            return settings;
        }

        protected abstract BaseComboBoxStyleSettings ProvideValueCore(IServiceProvider serviceProvider);

        [ConstructorArgument("scrollToSelectionOnPopup"), DefaultValue((string) null)]
        public bool? ScrollToSelectionOnPopup
        {
            get => 
                this.scrollToSelectionOnPopup;
            set => 
                this.scrollToSelectionOnPopup = value;
        }
    }
}

