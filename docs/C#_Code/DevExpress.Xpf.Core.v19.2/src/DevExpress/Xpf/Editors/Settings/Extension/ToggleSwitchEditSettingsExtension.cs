namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ToggleSwitchEditSettingsExtension : BaseSettingsExtension
    {
        public ToggleSwitchEditSettingsExtension()
        {
            this.ClickMode = (System.Windows.Controls.ClickMode) ToggleSwitchEditSettings.ClickModeProperty.GetMetadata(typeof(ToggleSwitchEditSettings)).DefaultValue;
            this.IsThreeState = (bool) ToggleSwitchEditSettings.IsThreeStateProperty.GetMetadata(typeof(ToggleSwitchEditSettings)).DefaultValue;
            this.CheckedStateContentTemplate = (DataTemplate) ToggleSwitchEditSettings.CheckedStateContentTemplateProperty.GetMetadata(typeof(ToggleSwitchEditSettings)).DefaultValue;
            this.UncheckedStateContentTemplate = (DataTemplate) ToggleSwitchEditSettings.UncheckedStateContentTemplateProperty.GetMetadata(typeof(ToggleSwitchEditSettings)).DefaultValue;
            this.ContentPlacement = (ToggleSwitchContentPlacement) ToggleSwitchEditSettings.ContentPlacementProperty.GetMetadata(typeof(ToggleSwitchEditSettings)).DefaultValue;
            this.CheckedStateContent = ToggleSwitchEditSettings.CheckedStateContentProperty.GetMetadata(typeof(ToggleSwitchEditSettings)).DefaultValue;
            this.UncheckedStateContent = ToggleSwitchEditSettings.UncheckedStateContentProperty.GetMetadata(typeof(ToggleSwitchEditSettings)).DefaultValue;
            this.EnableAnimation = (bool) ToggleSwitchEditSettings.EnableAnimationProperty.GetMetadata(typeof(ToggleSwitchEditSettings)).DefaultValue;
        }

        protected override BaseEditSettings CreateEditSettings()
        {
            ToggleSwitchEditSettings settings1 = new ToggleSwitchEditSettings();
            settings1.ClickMode = this.ClickMode;
            settings1.IsThreeState = this.IsThreeState;
            settings1.CheckedStateContentTemplate = this.CheckedStateContentTemplate;
            settings1.UncheckedStateContentTemplate = this.UncheckedStateContentTemplate;
            settings1.ContentPlacement = this.ContentPlacement;
            settings1.CheckedStateContent = this.CheckedStateContent;
            settings1.UncheckedStateContent = this.UncheckedStateContent;
            settings1.EnableAnimation = this.EnableAnimation;
            return settings1;
        }

        public System.Windows.Controls.ClickMode ClickMode { get; set; }

        public bool IsThreeState { get; set; }

        public DataTemplate CheckedStateContentTemplate { get; set; }

        public DataTemplate UncheckedStateContentTemplate { get; set; }

        public ToggleSwitchContentPlacement ContentPlacement { get; set; }

        public object CheckedStateContent { get; set; }

        public object UncheckedStateContent { get; set; }

        public bool EnableAnimation { get; set; }
    }
}

