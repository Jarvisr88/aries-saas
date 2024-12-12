namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public class PopupImageEditSettingsExtension : PopupBaseSettingsExtension
    {
        public PopupImageEditSettingsExtension()
        {
            base.IsTextEditable = false;
            base.ShowSizeGrip = true;
            base.PopupMinHeight = 200.0;
            this.ShowMenu = (bool) PopupImageEdit.ShowMenuProperty.DefaultMetadata.DefaultValue;
            this.ShowMenuMode = (DevExpress.Xpf.Editors.ShowMenuMode) ImageEdit.ShowMenuModeProperty.DefaultMetadata.DefaultValue;
            this.Stretch = (System.Windows.Media.Stretch) PopupImageEdit.StretchProperty.DefaultMetadata.DefaultValue;
            this.ShowLoadDialogOnClickMode = (DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode) PopupImageEdit.ShowLoadDialogOnClickModeProperty.DefaultMetadata.DefaultValue;
            this.ImageEffect = (Effect) PopupImageEdit.ImageEffectProperty.DefaultMetadata.DefaultValue;
        }

        protected virtual PopupImageEditSettings CreateImageEditSettings() => 
            new PopupImageEditSettings();

        protected sealed override PopupBaseEditSettings CreatePopupBaseEditSettings()
        {
            PopupImageEditSettings settings = this.CreateImageEditSettings();
            settings.ShowMenu = this.ShowMenu;
            settings.ShowMenuMode = this.ShowMenuMode;
            settings.Stretch = this.Stretch;
            settings.ShowLoadDialogOnClickMode = this.ShowLoadDialogOnClickMode;
            settings.ImageEffect = this.ImageEffect;
            return settings;
        }

        public bool ShowMenu { get; set; }

        public DevExpress.Xpf.Editors.ShowMenuMode ShowMenuMode { get; set; }

        public System.Windows.Media.Stretch Stretch { get; set; }

        public DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode ShowLoadDialogOnClickMode { get; set; }

        public Effect ImageEffect { get; set; }
    }
}

