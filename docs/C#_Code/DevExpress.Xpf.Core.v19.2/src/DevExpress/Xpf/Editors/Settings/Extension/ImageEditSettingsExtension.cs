namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public class ImageEditSettingsExtension : BaseSettingsExtension
    {
        public ImageEditSettingsExtension()
        {
            this.ShowMenu = (bool) ImageEdit.ShowMenuProperty.DefaultMetadata.DefaultValue;
            this.ShowMenuMode = (DevExpress.Xpf.Editors.ShowMenuMode) ImageEdit.ShowMenuModeProperty.DefaultMetadata.DefaultValue;
            this.Stretch = (System.Windows.Media.Stretch) ImageEdit.StretchProperty.DefaultMetadata.DefaultValue;
            this.ShowLoadDialogOnClickMode = (DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode) ImageEdit.ShowLoadDialogOnClickModeProperty.DefaultMetadata.DefaultValue;
            this.ImageEffect = (Effect) ImageEdit.ImageEffectProperty.DefaultMetadata.DefaultValue;
        }

        protected virtual void Assign(ImageEditSettings settings)
        {
            settings.ShowMenu = this.ShowMenu;
            settings.ShowMenuMode = this.ShowMenuMode;
            settings.Stretch = this.Stretch;
            settings.ShowLoadDialogOnClickMode = this.ShowLoadDialogOnClickMode;
            settings.ImageEffect = this.ImageEffect;
        }

        protected override BaseEditSettings CreateEditSettings()
        {
            ImageEditSettings settings = new ImageEditSettings();
            this.Assign(settings);
            return settings;
        }

        public bool ShowMenu { get; set; }

        public DevExpress.Xpf.Editors.ShowMenuMode ShowMenuMode { get; set; }

        public System.Windows.Media.Stretch Stretch { get; set; }

        public DevExpress.Xpf.Editors.ShowLoadDialogOnClickMode ShowLoadDialogOnClickMode { get; set; }

        public Effect ImageEffect { get; set; }
    }
}

