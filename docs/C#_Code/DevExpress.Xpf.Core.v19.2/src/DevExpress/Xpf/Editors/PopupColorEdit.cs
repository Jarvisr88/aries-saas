namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class PopupColorEdit : PopupBaseEdit, IColorEdit
    {
        public static readonly DependencyProperty ColorProperty;
        public static readonly DependencyProperty DefaultColorProperty;
        public static readonly DependencyProperty ColumnCountProperty;
        public static readonly DependencyProperty ShowMoreColorsButtonProperty;
        public static readonly DependencyProperty ShowNoColorButtonProperty;
        public static readonly DependencyProperty ShowDefaultColorButtonProperty;
        public static readonly DependencyProperty DefaultColorButtonContentProperty;
        public static readonly DependencyProperty MoreColorsButtonContentProperty;
        public static readonly DependencyProperty NoColorButtonContentProperty;
        public static readonly DependencyProperty ChipSizeProperty;
        public static readonly DependencyProperty ChipMarginProperty;
        public static readonly DependencyProperty ChipBorderBrushProperty;
        public static readonly DependencyProperty PalettesProperty;
        public static readonly DependencyProperty ColorDisplayFormatProperty;
        public static readonly DependencyProperty DisplayModeProperty;
        public static readonly DependencyProperty ShowAlphaChannelProperty;
        public static readonly DependencyProperty OwnerPopupEditProperty;
        private static readonly DependencyPropertyKey OwnerPopupEditPropertyKey;
        public static readonly RoutedEvent ColorChangedEvent;
        public static readonly RoutedEvent GetColorNameEvent;
        private ColorEdit colorEdit;

        public event RoutedEventHandler ColorChanged
        {
            add
            {
                base.AddHandler(ColorChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ColorChangedEvent, value);
            }
        }

        public event GetColorNameEventHandler GetColorName
        {
            add
            {
                base.AddHandler(GetColorNameEvent, value);
            }
            remove
            {
                base.RemoveHandler(GetColorNameEvent, value);
            }
        }

        static PopupColorEdit()
        {
            Type ownerType = typeof(PopupColorEdit);
            DisplayModeProperty = DependencyProperty.Register("DisplayMode", typeof(PopupColorEditDisplayMode), ownerType, new FrameworkPropertyMetadata(PopupColorEditDisplayMode.Default, FrameworkPropertyMetadataOptions.None, (o, args) => ((PopupColorEdit) o).DisplayModeChanged((PopupColorEditDisplayMode) args.OldValue, (PopupColorEditDisplayMode) args.NewValue)));
            System.Windows.Media.Color defaultValue = new System.Windows.Media.Color();
            ColorProperty = DependencyPropertyManager.Register("Color", typeof(System.Windows.Media.Color), ownerType, new PropertyMetadata(defaultValue, new PropertyChangedCallback(PopupColorEdit.OnColorChanged), new CoerceValueCallback(PopupColorEdit.CoerceColor)));
            ColumnCountProperty = DependencyPropertyManager.Register("ColumnCount", typeof(int), ownerType, new PropertyMetadata((int) 10, new PropertyChangedCallback(PopupColorEdit.OnColumnCountChanged)));
            DefaultColorProperty = ColorEdit.DefaultColorProperty.AddOwner(ownerType);
            ShowMoreColorsButtonProperty = ColorEdit.ShowMoreColorsButtonProperty.AddOwner(ownerType);
            ShowNoColorButtonProperty = ColorEdit.ShowNoColorButtonProperty.AddOwner(ownerType);
            ShowDefaultColorButtonProperty = ColorEdit.ShowDefaultColorButtonProperty.AddOwner(ownerType);
            DefaultColorButtonContentProperty = ColorEdit.DefaultColorButtonContentProperty.AddOwner(ownerType);
            MoreColorsButtonContentProperty = ColorEdit.MoreColorsButtonContentProperty.AddOwner(ownerType);
            NoColorButtonContentProperty = ColorEdit.NoColorButtonContentProperty.AddOwner(ownerType);
            ChipSizeProperty = ColorEdit.ChipSizeProperty.AddOwner(ownerType);
            ChipMarginProperty = ColorEdit.ChipMarginProperty.AddOwner(ownerType);
            ChipBorderBrushProperty = ColorEdit.ChipBorderBrushProperty.AddOwner(ownerType);
            PalettesProperty = ColorEdit.PalettesProperty.AddOwner(ownerType);
            ColorDisplayFormatProperty = DependencyPropertyManager.Register("ColorDisplayFormat", typeof(DevExpress.Xpf.Editors.ColorDisplayFormat), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.ColorDisplayFormat.Default, new PropertyChangedCallback(PopupColorEdit.OnColorDisplayFormatChanged)));
            ShowAlphaChannelProperty = DependencyPropertyManager.Register("ShowAlphaChannel", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(PopupColorEdit.OnShowAlphaChannelChanged)));
            OwnerPopupEditPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("OwnerPopupEdit", ownerType, ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            OwnerPopupEditProperty = OwnerPopupEditPropertyKey.DependencyProperty;
            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChangedEvent", RoutingStrategy.Direct, typeof(RoutedEventArgs), ownerType);
            GetColorNameEvent = EventManager.RegisterRoutedEvent("GetColorName", RoutingStrategy.Direct, typeof(GetColorNameEventHandler), ownerType);
        }

        public PopupColorEdit()
        {
            this.SetDefaultStyleKey(typeof(PopupColorEdit));
        }

        protected override void AcceptPopupValue()
        {
            base.AcceptPopupValue();
            this.EditStrategy.AcceptPopupValue();
        }

        protected virtual object CoerceColor(System.Windows.Media.Color value) => 
            this.EditStrategy.CoerceColor(value);

        private static object CoerceColor(DependencyObject d, object value) => 
            ((PopupColorEdit) d).CoerceColor((System.Windows.Media.Color) value);

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new PopupColorEditPropertyProvider(this);

        protected internal override BaseEditSettings CreateEditorSettings() => 
            new PopupColorEditSettings();

        protected override EditStrategyBase CreateEditStrategy() => 
            new PopupColorEditStrategy(this);

        void IColorEdit.AddCustomColor(System.Windows.Media.Color color)
        {
            this.EditStrategy.OnAddCustomColor(color);
        }

        protected virtual void DisplayModeChanged(PopupColorEditDisplayMode oldValue, PopupColorEditDisplayMode newValue)
        {
            this.EditStrategy.DisplayModeChanged(oldValue, newValue);
        }

        protected internal virtual string GetColorNameCore(System.Windows.Media.Color color)
        {
            string colorName = ColorEditHelper.GetColorName(color, this.ColorDisplayFormat, this.ShowAlphaChannel);
            GetColorNameEventArgs args1 = new GetColorNameEventArgs(color, colorName);
            args1.RoutedEvent = GetColorNameEvent;
            GetColorNameEventArgs e = args1;
            base.RaiseEvent(e);
            return e.ColorName;
        }

        public static PopupColorEdit GetOwnerPopupEdit(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (PopupColorEdit) DependencyObjectHelper.GetValueWithInheritance(element, OwnerPopupEditProperty);
        }

        protected virtual void InitializePaletteCollection()
        {
            if (this.Palettes == null)
            {
                base.SetCurrentValue(PalettesProperty, PredefinedPaletteCollections.Office);
            }
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PopupColorEdit) d).OnColorChanged((System.Windows.Media.Color) e.OldValue, (System.Windows.Media.Color) e.NewValue);
        }

        protected virtual void OnColorChanged(System.Windows.Media.Color oldValue, System.Windows.Media.Color newValue)
        {
            this.EditStrategy.OnColorChanged(oldValue, newValue);
        }

        protected void OnColorDisplayFormatChanged()
        {
            this.EditStrategy.UpdateDisplayText();
        }

        private static void OnColorDisplayFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PopupColorEdit) d).OnColorDisplayFormatChanged();
        }

        protected virtual void OnColumnCountChanged(int columnCount)
        {
            this.RecentColors.SetSize(columnCount);
        }

        private static void OnColumnCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PopupColorEdit) d).OnColumnCountChanged((int) e.NewValue);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.InitializePaletteCollection();
        }

        protected override void OnIsReadOnlyChanged()
        {
            base.OnIsReadOnlyChanged();
            if (this.ColorEditControl != null)
            {
                this.ColorEditControl.IsReadOnly = base.IsReadOnly;
            }
        }

        protected virtual void OnShowAlphaChannelChanged()
        {
            this.EditStrategy.UpdateDisplayText();
        }

        private static void OnShowAlphaChannelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PopupColorEdit) d).OnShowAlphaChannelChanged();
        }

        protected internal virtual void RaiseColorChanged()
        {
            RoutedEventArgs e = new RoutedEventArgs();
            e.RoutedEvent = ColorChangedEvent;
            base.RaiseEvent(e);
        }

        internal void SetInnerColorEdit(ColorEdit colorEdit)
        {
            this.ColorEditControl = colorEdit;
        }

        internal static void SetOwnerPopupEdit(DependencyObject element, PopupColorEdit value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(OwnerPopupEditPropertyKey, value);
        }

        [Description("Gets or sets the display mode of the current PopupColorEdit edit box.This is a dependency property.")]
        public PopupColorEditDisplayMode DisplayMode
        {
            get => 
                (PopupColorEditDisplayMode) base.GetValue(DisplayModeProperty);
            set => 
                base.SetValue(DisplayModeProperty, value);
        }

        [Description("Gets or sets the currently selected color. This is a dependency property.")]
        public System.Windows.Media.Color Color
        {
            get => 
                (System.Windows.Media.Color) base.GetValue(ColorProperty);
            set => 
                base.SetValue(ColorProperty, value);
        }

        [Description("Gets or sets the default color. This is a dependency property.")]
        public System.Windows.Media.Color DefaultColor
        {
            get => 
                (System.Windows.Media.Color) base.GetValue(DefaultColorProperty);
            set => 
                base.SetValue(DefaultColorProperty, value);
        }

        [Description("Gets the recent colors.")]
        public CircularList<System.Windows.Media.Color> RecentColors =>
            this.Settings.RecentColors;

        [Description("Gets or sets the number of color columns. This is a dependency property.")]
        public int ColumnCount
        {
            get => 
                (int) base.GetValue(ColumnCountProperty);
            set => 
                base.SetValue(ColumnCountProperty, value);
        }

        [Description("Gets or sets whether the Default Color Button is visible. This is a dependency property.")]
        public bool ShowDefaultColorButton
        {
            get => 
                (bool) base.GetValue(ShowDefaultColorButtonProperty);
            set => 
                base.SetValue(ShowDefaultColorButtonProperty, value);
        }

        [Description("Gets or sets whether the More Colors button is visible. This is a dependency property.")]
        public bool ShowMoreColorsButton
        {
            get => 
                (bool) base.GetValue(ShowMoreColorsButtonProperty);
            set => 
                base.SetValue(ShowMoreColorsButtonProperty, value);
        }

        [Description("Gets or sets whether the No Color button is visible. This is a dependency property.")]
        public bool ShowNoColorButton
        {
            get => 
                (bool) base.GetValue(ShowNoColorButtonProperty);
            set => 
                base.SetValue(ShowNoColorButtonProperty, value);
        }

        [Description("Gets or sets the content of the Default Color Button. This is a dependency property."), TypeConverter(typeof(ObjectConverter))]
        public object DefaultColorButtonContent
        {
            get => 
                base.GetValue(DefaultColorButtonContentProperty);
            set => 
                base.SetValue(DefaultColorButtonContentProperty, value);
        }

        [Description("Gets or sets the content of the More Colors button. This is a dependency property."), TypeConverter(typeof(ObjectConverter))]
        public object MoreColorsButtonContent
        {
            get => 
                base.GetValue(MoreColorsButtonContentProperty);
            set => 
                base.SetValue(MoreColorsButtonContentProperty, value);
        }

        [TypeConverter(typeof(ObjectConverter)), Description("Gets or sets the content of the No Color button. This is a dependency property.")]
        public object NoColorButtonContent
        {
            get => 
                base.GetValue(NoColorButtonContentProperty);
            set => 
                base.SetValue(NoColorButtonContentProperty, value);
        }

        [Description("Gets or sets the size of color chips. This is a dependency property.")]
        public DevExpress.Xpf.Editors.ChipSize ChipSize
        {
            get => 
                (DevExpress.Xpf.Editors.ChipSize) base.GetValue(ChipSizeProperty);
            set => 
                base.SetValue(ChipSizeProperty, value);
        }

        [Description("Gets or sets the outer margin of a color chip. This is a dependency property.")]
        public Thickness ChipMargin
        {
            get => 
                (Thickness) base.GetValue(ChipMarginProperty);
            set => 
                base.SetValue(ChipMarginProperty, value);
        }

        [Description("Gets or sets the Brush that draws the outer border of a color chip. This is a dependency property.")]
        public Brush ChipBorderBrush
        {
            get => 
                (Brush) base.GetValue(ChipBorderBrushProperty);
            set => 
                base.SetValue(ChipBorderBrushProperty, value);
        }

        [Description("Gets or sets the collection of palettes. This is a dependency property.")]
        public PaletteCollection Palettes
        {
            get => 
                (PaletteCollection) base.GetValue(PalettesProperty);
            set => 
                base.SetValue(PalettesProperty, value);
        }

        [Description("Gets or sets a value that specifies in which format the selected color is displayed. This is a dependency property.")]
        public DevExpress.Xpf.Editors.ColorDisplayFormat ColorDisplayFormat
        {
            get => 
                (DevExpress.Xpf.Editors.ColorDisplayFormat) base.GetValue(ColorDisplayFormatProperty);
            set => 
                base.SetValue(ColorDisplayFormatProperty, value);
        }

        public bool ShowAlphaChannel
        {
            get => 
                (bool) base.GetValue(ShowAlphaChannelProperty);
            set => 
                base.SetValue(ShowAlphaChannelProperty, value);
        }

        protected internal PopupColorEditSettings Settings =>
            (PopupColorEditSettings) base.Settings;

        protected internal ColorEdit ColorEditControl
        {
            get => 
                this.colorEdit;
            private set
            {
                if (!ReferenceEquals(this.ColorEditControl, value))
                {
                    this.colorEdit = value;
                    this.EditStrategy.SyncProperties();
                }
            }
        }

        protected PopupColorEditStrategy EditStrategy
        {
            get => 
                base.EditStrategy as PopupColorEditStrategy;
            set => 
                base.EditStrategy = value;
        }

        protected internal override Type StyleSettingsType =>
            typeof(PopupColorEditStyleSettings);

        protected internal override bool ShouldApplyPopupSize =>
            false;

        ColorPickerColorMode IColorEdit.ColorMode { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupColorEdit.<>c <>9 = new PopupColorEdit.<>c();

            internal void <.cctor>b__20_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((PopupColorEdit) o).DisplayModeChanged((PopupColorEditDisplayMode) args.OldValue, (PopupColorEditDisplayMode) args.NewValue);
            }
        }
    }
}

