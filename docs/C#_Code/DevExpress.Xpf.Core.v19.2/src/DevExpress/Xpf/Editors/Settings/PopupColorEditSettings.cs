namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class PopupColorEditSettings : PopupBaseEditSettings
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
        public static readonly DependencyProperty DisplayModeProperty;
        private CircularList<Color> recentColors;
        private readonly int defaultColumnCount = ((int) ColumnCountProperty.GetMetadata(typeof(PopupColorEditSettings)).DefaultValue);

        static PopupColorEditSettings()
        {
            Type ownerType = typeof(PopupColorEditSettings);
            ColumnCountProperty = ColorEditSettings.ColumnCountProperty.AddOwner(ownerType, new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            DefaultColorProperty = ColorEditSettings.DefaultColorProperty.AddOwner(ownerType, new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ShowMoreColorsButtonProperty = ColorEditSettings.ShowMoreColorsButtonProperty.AddOwner(ownerType, new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ShowNoColorButtonProperty = ColorEditSettings.ShowNoColorButtonProperty.AddOwner(ownerType, new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ShowDefaultColorButtonProperty = ColorEditSettings.ShowDefaultColorButtonProperty.AddOwner(ownerType, new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ChipSizeProperty = ColorEditSettings.ChipSizeProperty.AddOwner(ownerType, new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ChipMarginProperty = ColorEditSettings.ChipMarginProperty.AddOwner(ownerType, new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ChipBorderBrushProperty = ColorEditSettings.ChipBorderBrushProperty.AddOwner(ownerType, new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            PalettesProperty = ColorEditSettings.PalettesProperty.AddOwner(ownerType, new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            DisplayModeProperty = DependencyProperty.Register("DisplayMode", typeof(PopupColorEditDisplayMode), ownerType, new PropertyMetadata(PopupColorEditDisplayMode.Default));
            ButtonEditSettings.IsTextEditableProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(null));
            ShowAlphaChannelProperty = ColorEditSettings.ShowAlphaChannelProperty.AddOwner(ownerType, new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            PopupColorEdit editor = edit as PopupColorEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(DisplayModeProperty, () => editor.DisplayMode = this.DisplayMode);
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

        public PopupColorEditDisplayMode DisplayMode
        {
            get => 
                (PopupColorEditDisplayMode) base.GetValue(DisplayModeProperty);
            set => 
                base.SetValue(DisplayModeProperty, value);
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

