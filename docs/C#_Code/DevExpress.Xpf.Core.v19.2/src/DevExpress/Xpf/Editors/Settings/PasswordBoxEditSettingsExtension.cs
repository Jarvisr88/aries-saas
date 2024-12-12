namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings.Extension;
    using System;
    using System.Runtime.CompilerServices;

    public class PasswordBoxEditSettingsExtension : BaseSettingsExtension
    {
        public PasswordBoxEditSettingsExtension()
        {
            this.MaxLength = (int) TextEditBase.MaxLengthProperty.DefaultMetadata.DefaultValue;
            this.PasswordChar = (char) PasswordBoxEdit.PasswordCharProperty.DefaultMetadata.DefaultValue;
        }

        protected override BaseEditSettings CreateEditSettings()
        {
            PasswordBoxEditSettings settings1 = new PasswordBoxEditSettings();
            settings1.MaxLength = this.MaxLength;
            settings1.PasswordChar = this.PasswordChar;
            return settings1;
        }

        public int MaxLength { get; set; }

        public char PasswordChar { get; set; }
    }
}

