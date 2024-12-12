namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class ColorEditSettingsExtension : BaseSettingsExtension
    {
        public ColorEditSettingsExtension()
        {
            this.ColumnCount = (int) ColorEditSettings.ColumnCountProperty.DefaultMetadata.DefaultValue;
            this.DefaultColor = (Color) ColorEditSettings.DefaultColorProperty.DefaultMetadata.DefaultValue;
            this.ShowMoreColorsButton = (bool) ColorEditSettings.ShowMoreColorsButtonProperty.DefaultMetadata.DefaultValue;
            this.ShowNoColorButton = (bool) ColorEditSettings.ShowNoColorButtonProperty.DefaultMetadata.DefaultValue;
            this.ShowDefaultColorButton = (bool) ColorEditSettings.ShowDefaultColorButtonProperty.DefaultMetadata.DefaultValue;
            this.ChipSize = (DevExpress.Xpf.Editors.ChipSize) ColorEditSettings.ChipSizeProperty.DefaultMetadata.DefaultValue;
            this.ChipMargin = (Thickness) ColorEditSettings.ChipMarginProperty.DefaultMetadata.DefaultValue;
        }

        protected virtual void Assign(ColorEditSettings settings)
        {
            settings.ColumnCount = this.ColumnCount;
            settings.DefaultColor = this.DefaultColor;
            settings.ShowMoreColorsButton = this.ShowMoreColorsButton;
            settings.ShowNoColorButton = this.ShowNoColorButton;
            settings.ShowDefaultColorButton = this.ShowDefaultColorButton;
            settings.ChipSize = this.ChipSize;
            settings.ChipMargin = this.ChipMargin;
            settings.ChipBorderBrush = this.ChipBorderBrush;
        }

        protected override BaseEditSettings CreateEditSettings()
        {
            ColorEditSettings settings = new ColorEditSettings();
            this.Assign(settings);
            return settings;
        }

        public int ColumnCount { get; set; }

        public Color DefaultColor { get; set; }

        public bool ShowMoreColorsButton { get; set; }

        public bool ShowNoColorButton { get; set; }

        public bool ShowDefaultColorButton { get; set; }

        public DevExpress.Xpf.Editors.ChipSize ChipSize { get; set; }

        public Thickness ChipMargin { get; set; }

        public Brush ChipBorderBrush { get; set; }
    }
}

