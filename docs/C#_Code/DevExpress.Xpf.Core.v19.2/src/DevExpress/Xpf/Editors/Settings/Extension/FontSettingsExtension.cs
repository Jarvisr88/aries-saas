namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;

    public class FontSettingsExtension : ComboBoxSettingsExtension
    {
        public FontSettingsExtension()
        {
            base.AutoComplete = true;
            base.ValidateOnTextInput = false;
            this.AllowConfirmFontUseDialog = (bool) FontEdit.AllowConfirmFontUseDialogProperty.DefaultMetadata.DefaultValue;
        }

        protected override PopupBaseEditSettings CreatePopupBaseEditSettings()
        {
            FontEditSettings settings1 = new FontEditSettings();
            settings1.AllowConfirmFontUseDialog = this.AllowConfirmFontUseDialog;
            return settings1;
        }

        public bool AllowConfirmFontUseDialog { get; set; }
    }
}

