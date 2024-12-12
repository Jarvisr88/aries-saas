namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class PopupColorEditSettingsExtension : PopupBaseSettingsExtension
    {
        public PopupColorEditSettingsExtension()
        {
            this.ColumnCount = (int) PopupColorEditSettings.ColumnCountProperty.DefaultMetadata.DefaultValue;
            this.DefaultColor = (Color) PopupColorEditSettings.DefaultColorProperty.DefaultMetadata.DefaultValue;
            this.ShowMoreColorsButton = (bool) PopupColorEditSettings.ShowMoreColorsButtonProperty.DefaultMetadata.DefaultValue;
            this.ShowNoColorButton = (bool) PopupColorEditSettings.ShowNoColorButtonProperty.DefaultMetadata.DefaultValue;
            this.ShowDefaultColorButton = (bool) PopupColorEditSettings.ShowDefaultColorButtonProperty.DefaultMetadata.DefaultValue;
            this.ChipSize = (DevExpress.Xpf.Editors.ChipSize) PopupColorEditSettings.ChipSizeProperty.DefaultMetadata.DefaultValue;
            this.ChipMargin = (Thickness) PopupColorEditSettings.ChipMarginProperty.DefaultMetadata.DefaultValue;
            this.DisplayMode = (PopupColorEditDisplayMode) PopupColorEditSettings.DisplayModeProperty.DefaultMetadata.DefaultValue;
        }

        protected override void Assign(PopupBaseEditSettings settings)
        {
            base.Assign(settings);
            PopupColorEditSettings settings2 = (PopupColorEditSettings) settings;
            settings2.ColumnCount = this.ColumnCount;
            settings2.DefaultColor = this.DefaultColor;
            settings2.ShowMoreColorsButton = this.ShowMoreColorsButton;
            settings2.ShowNoColorButton = this.ShowNoColorButton;
            settings2.ShowDefaultColorButton = this.ShowDefaultColorButton;
            settings2.ChipSize = this.ChipSize;
            settings2.ChipMargin = this.ChipMargin;
            settings2.ChipBorderBrush = this.ChipBorderBrush;
            settings2.DisplayMode = this.DisplayMode;
        }

        protected override PopupBaseEditSettings CreatePopupBaseEditSettings() => 
            new PopupColorEditSettings();

        public int ColumnCount { get; set; }

        public Color DefaultColor { get; set; }

        public bool ShowMoreColorsButton { get; set; }

        public bool ShowNoColorButton { get; set; }

        public bool ShowDefaultColorButton { get; set; }

        public DevExpress.Xpf.Editors.ChipSize ChipSize { get; set; }

        public Thickness ChipMargin { get; set; }

        public Brush ChipBorderBrush { get; set; }

        public PopupColorEditDisplayMode DisplayMode { get; set; }
    }
}

