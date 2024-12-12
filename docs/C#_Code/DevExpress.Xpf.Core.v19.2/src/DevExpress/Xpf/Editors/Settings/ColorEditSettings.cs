namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class ColorEditSettings : BaseEditSettings
    {
        public static readonly DependencyProperty ColumnCountProperty;
        public static readonly DependencyProperty DefaultColorProperty;
        public static readonly DependencyProperty ShowMoreColorsButtonProperty;
        public static readonly DependencyProperty ShowNoColorButtonProperty;
        public static readonly DependencyProperty ShowDefaultColorButtonProperty;
        public static readonly DependencyProperty ShowAlphaChannelProperty;
        public static readonly DependencyProperty ChipSizeProperty;
        public static readonly DependencyProperty ChipMarginProperty;
        public static readonly DependencyProperty ChipBorderBrushProperty;
        public static readonly DependencyProperty PalettesProperty;
        private readonly int defaultColumnCount = ((int) ColumnCountProperty.GetMetadata(typeof(PopupColorEditSettings)).DefaultValue);
        private CircularList<Color> recentColors;

        static ColorEditSettings()
        {
            Type ownerType = typeof(ColorEditSettings);
            ColumnCountProperty = DependencyPropertyManager.Register("ColumnCount", typeof(int), ownerType, new PropertyMetadata((int) 10, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            DefaultColorProperty = DependencyPropertyManager.Register("DefaultColor", typeof(Color), ownerType, new PropertyMetadata(Colors.Black, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ShowMoreColorsButtonProperty = DependencyPropertyManager.Register("ShowMoreColorsButton", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ShowNoColorButtonProperty = DependencyPropertyManager.Register("ShowNoColorButton", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ShowDefaultColorButtonProperty = DependencyPropertyManager.Register("ShowDefaultColorButton", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ChipSizeProperty = DependencyPropertyManager.Register("ChipSize", typeof(DevExpress.Xpf.Editors.ChipSize), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.ChipSize.Default, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ChipMarginProperty = DependencyPropertyManager.Register("ChipMargin", typeof(Thickness), ownerType, new PropertyMetadata(new Thickness(2.0, 0.0, 2.0, 0.0), new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ChipBorderBrushProperty = DependencyPropertyManager.Register("ChipBorderBrush", typeof(Brush), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            PalettesProperty = DependencyPropertyManager.Register("Palettes", typeof(PaletteCollection), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ShowAlphaChannelProperty = DependencyPropertyManager.Register("ShowAlphaChannel", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            ColorEdit editor = edit as ColorEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(ColumnCountProperty, () => editor.ColumnCount = this.ColumnCount);
                base.SetValueFromSettings(DefaultColorProperty, () => editor.DefaultColor = this.DefaultColor);
                base.SetValueFromSettings(ShowDefaultColorButtonProperty, () => editor.ShowDefaultColorButton = this.ShowDefaultColorButton);
                base.SetValueFromSettings(ShowMoreColorsButtonProperty, () => editor.ShowMoreColorsButton = this.ShowMoreColorsButton);
                base.SetValueFromSettings(ShowNoColorButtonProperty, () => editor.ShowNoColorButton = this.ShowNoColorButton);
                base.SetValueFromSettings(ChipSizeProperty, () => editor.ChipSize = this.ChipSize);
                base.SetValueFromSettings(ChipMarginProperty, () => editor.ChipMargin = this.ChipMargin);
                base.SetValueFromSettings(ShowAlphaChannelProperty, () => editor.ShowAlphaChannel = this.ShowAlphaChannel);
                if (this.Palettes != null)
                {
                    base.SetValueFromSettings(PalettesProperty, () => editor.Palettes = this.Palettes);
                }
                if (this.ChipBorderBrush != null)
                {
                    base.SetValueFromSettings(ChipBorderBrushProperty, () => editor.ChipBorderBrush = this.ChipBorderBrush);
                }
                if (!ReferenceEquals(editor.Settings, this))
                {
                    editor.RecentColors.Assign(this.RecentColors);
                }
            }
        }

        public CircularList<Color> RecentColors
        {
            get
            {
                this.recentColors ??= new CircularList<Color>(this.defaultColumnCount);
                return this.recentColors;
            }
        }

        public int ColumnCount
        {
            get => 
                (int) base.GetValue(ColumnCountProperty);
            set => 
                base.SetValue(ColumnCountProperty, value);
        }

        public Color DefaultColor
        {
            get => 
                (Color) base.GetValue(DefaultColorProperty);
            set => 
                base.SetValue(DefaultColorProperty, value);
        }

        public bool ShowDefaultColorButton
        {
            get => 
                (bool) base.GetValue(ShowDefaultColorButtonProperty);
            set => 
                base.SetValue(ShowDefaultColorButtonProperty, value);
        }

        public bool ShowMoreColorsButton
        {
            get => 
                (bool) base.GetValue(ShowMoreColorsButtonProperty);
            set => 
                base.SetValue(ShowMoreColorsButtonProperty, value);
        }

        public bool ShowNoColorButton
        {
            get => 
                (bool) base.GetValue(ShowNoColorButtonProperty);
            set => 
                base.SetValue(ShowNoColorButtonProperty, value);
        }

        public bool ShowAlphaChannel
        {
            get => 
                (bool) base.GetValue(ShowAlphaChannelProperty);
            set => 
                base.SetValue(ShowAlphaChannelProperty, value);
        }

        public DevExpress.Xpf.Editors.ChipSize ChipSize
        {
            get => 
                (DevExpress.Xpf.Editors.ChipSize) base.GetValue(ChipSizeProperty);
            set => 
                base.SetValue(ChipSizeProperty, value);
        }

        public Thickness ChipMargin
        {
            get => 
                (Thickness) base.GetValue(ChipMarginProperty);
            set => 
                base.SetValue(ChipMarginProperty, value);
        }

        public Brush ChipBorderBrush
        {
            get => 
                (Brush) base.GetValue(ChipBorderBrushProperty);
            set => 
                base.SetValue(ChipBorderBrushProperty, value);
        }

        public PaletteCollection Palettes
        {
            get => 
                (PaletteCollection) base.GetValue(PalettesProperty);
            set => 
                base.SetValue(PalettesProperty, value);
        }
    }
}

