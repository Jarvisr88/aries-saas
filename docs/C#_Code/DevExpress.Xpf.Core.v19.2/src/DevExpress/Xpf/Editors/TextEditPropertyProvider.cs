namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class TextEditPropertyProvider : ActualPropertyProvider
    {
        private bool alwaysShowDecimalSeparator;
        private TextInputSettingsBase textInputSettings;

        public TextEditPropertyProvider(TextEditBase editor) : base(editor)
        {
            this.alwaysShowDecimalSeparator = true;
            TextEdit edit = editor as TextEdit;
            if (edit != null)
            {
                this.SetMaskType(edit.MaskType);
            }
        }

        public void SetAlwaysShowDecimalSeparator(bool value)
        {
            this.alwaysShowDecimalSeparator = value;
        }

        public void SetDateTimeKind(System.DateTimeKind kind)
        {
            this.DateTimeKind = kind;
        }

        public void SetDateTimeKindAssigned(bool value)
        {
            this.IsDateTimeKindAssigned = value;
        }

        public void SetMaskType(DevExpress.Xpf.Editors.MaskType maskType)
        {
            this.MaskType = maskType;
        }

        public void SetShowNullTextIfFocused(bool value)
        {
            this.ShowNullTextIfFocused = value;
        }

        public void SetTextInputSettings(TextInputSettingsBase settings)
        {
            this.textInputSettings = settings;
        }

        public DevExpress.Xpf.Editors.MaskType MaskType { get; private set; }

        private TextEditBase Editor =>
            base.Editor as TextEditBase;

        public TextInputSettingsBase TextInputSettings
        {
            get
            {
                TextInputSettingsBase textInputSettings = this.textInputSettings;
                if (this.textInputSettings == null)
                {
                    TextInputSettingsBase local1 = this.textInputSettings;
                    textInputSettings = this.textInputSettings = this.Editor.CreateTextInputSettings();
                }
                return textInputSettings;
            }
        }

        public bool AlwaysShowDecimalSeparator =>
            this.alwaysShowDecimalSeparator;

        public System.DateTimeKind DateTimeKind { get; private set; }

        public bool IsDateTimeKindAssigned { get; private set; }

        public bool ShowNullTextIfFocused { get; private set; }
    }
}

