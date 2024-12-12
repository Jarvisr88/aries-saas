namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;

    public class ButtonSettingsExtension : TextSettingsExtension
    {
        public ButtonSettingsExtension()
        {
            this.ShowText = (bool) ButtonEditSettings.ShowTextProperty.DefaultMetadata.DefaultValue;
            this.IsTextEditable = (bool?) ButtonEditSettings.IsTextEditableProperty.DefaultMetadata.DefaultValue;
            this.AllowDefaultButton = (bool?) ButtonEditSettings.AllowDefaultButtonProperty.DefaultMetadata.DefaultValue;
            this.NullValueButtonPlacement = (EditorPlacement?) ButtonEditSettings.NullValueButtonPlacementProperty.DefaultMetadata.DefaultValue;
        }

        protected virtual ButtonEditSettings CreateButtonEditSettings() => 
            new ButtonEditSettings();

        protected sealed override TextEditSettings CreateTextEditSettings()
        {
            ButtonEditSettings settings = this.CreateButtonEditSettings();
            settings.IsTextEditable = this.GetIsTextEditable();
            settings.AllowDefaultButton = this.AllowDefaultButton;
            settings.NullValueButtonPlacement = this.NullValueButtonPlacement;
            settings.ShowText = this.ShowText;
            return settings;
        }

        protected virtual bool? GetIsTextEditable() => 
            this.IsTextEditable;

        public bool ShowText { get; set; }

        public bool? IsTextEditable { get; set; }

        public bool? AllowDefaultButton { get; set; }

        public EditorPlacement? NullValueButtonPlacement { get; set; }
    }
}

