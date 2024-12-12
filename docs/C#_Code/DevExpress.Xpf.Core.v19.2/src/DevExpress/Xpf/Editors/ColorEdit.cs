namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Media;

    [TemplatePart(Name="PART_NoColorButton", Type=typeof(BarButtonItem)), TemplatePart(Name="PART_MoreColorsButton", Type=typeof(BarButtonItem)), TemplatePart(Name="PART_ResetButton", Type=typeof(BarButtonItem)), TemplatePart(Name="PART_Gallery", Type=typeof(DevExpress.Xpf.Bars.Gallery)), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class ColorEdit : BaseEdit, IColorEdit
    {
        private const string ElementGalleryName = "PART_Gallery";
        private const string ElementResetButtonName = "PART_ResetButton";
        private const string ElementMoreColorsButtonName = "PART_MoreColorsButton";
        private const string ElementNoColorButtonName = "PART_NoColorButton";
        public static readonly System.Windows.Media.Color EmptyColor;
        public static readonly DependencyProperty ColorProperty;
        public static readonly DependencyProperty DefaultColorProperty;
        public static readonly DependencyProperty ShowMoreColorsButtonProperty;
        public static readonly DependencyProperty ShowNoColorButtonProperty;
        public static readonly DependencyProperty ShowDefaultColorButtonProperty;
        public static readonly DependencyProperty DefaultColorButtonContentProperty;
        public static readonly DependencyProperty MoreColorsButtonContentProperty;
        public static readonly DependencyProperty NoColorButtonContentProperty;
        public static readonly DependencyProperty RecentColorsCaptionProperty;
        public static readonly DependencyProperty ColumnCountProperty;
        public static readonly DependencyProperty ChipSizeProperty;
        public static readonly DependencyProperty ChipMarginProperty;
        public static readonly DependencyProperty ChipBorderBrushProperty;
        public static readonly DependencyProperty PalettesProperty;
        public static readonly DependencyProperty ToolTipColorDisplayFormatProperty;
        public static readonly DependencyProperty CloseOwnerPopupOnClickProperty;
        public static readonly DependencyProperty ShowAlphaChannelProperty;
        public static readonly RoutedEvent ColorChangedEvent;
        public static readonly RoutedEvent GetColorNameEvent;
        private Locker updateLocker = new Locker();

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

        static ColorEdit()
        {
            Type ownerType = typeof(ColorEdit);
            System.Windows.Media.Color defaultValue = new System.Windows.Media.Color();
            ColorProperty = DependencyPropertyManager.Register("Color", typeof(System.Windows.Media.Color), ownerType, new PropertyMetadata(defaultValue, new PropertyChangedCallback(ColorEdit.OnColorChanged), new CoerceValueCallback(ColorEdit.CoerceColor)));
            DefaultColorProperty = DependencyPropertyManager.Register("DefaultColor", typeof(System.Windows.Media.Color), ownerType, new PropertyMetadata(Colors.Black, new PropertyChangedCallback(ColorEdit.OnDefaultColorChanged)));
            ShowMoreColorsButtonProperty = DependencyPropertyManager.Register("ShowMoreColorsButton", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(ColorEdit.OnShowMoreColorsButtonChanged)));
            ShowNoColorButtonProperty = DependencyPropertyManager.Register("ShowNoColorButton", typeof(bool), ownerType, new PropertyMetadata(false));
            ShowDefaultColorButtonProperty = DependencyPropertyManager.Register("ShowDefaultColorButton", typeof(bool), ownerType, new PropertyMetadata(true));
            DefaultColorButtonContentProperty = DependencyPropertyManager.Register("DefaultColorButtonContent", typeof(object), ownerType);
            MoreColorsButtonContentProperty = DependencyPropertyManager.Register("MoreColorsButtonContent", typeof(object), ownerType);
            NoColorButtonContentProperty = DependencyPropertyManager.Register("NoColorButtonContent", typeof(object), ownerType);
            RecentColorsCaptionProperty = DependencyPropertyManager.Register("RecentColorsCaption", typeof(string), ownerType, new PropertyMetadata(new PropertyChangedCallback(ColorEdit.OnRecentColorsCaptionChanged)));
            ColumnCountProperty = DependencyPropertyManager.Register("ColumnCount", typeof(int), ownerType, new PropertyMetadata((int) 10, new PropertyChangedCallback(ColorEdit.OnColumnCountChanged)));
            ChipSizeProperty = DependencyPropertyManager.Register("ChipSize", typeof(DevExpress.Xpf.Editors.ChipSize), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.ChipSize.Default));
            ChipMarginProperty = DependencyPropertyManager.Register("ChipMargin", typeof(Thickness), ownerType, new PropertyMetadata(new Thickness(2.0, 0.0, 2.0, 0.0), new PropertyChangedCallback(ColorEdit.OnChipMarginChanged)));
            ChipBorderBrushProperty = DependencyPropertyManager.Register("ChipBorderBrush", typeof(Brush), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColorEdit.OnChipBorderBrushChanged)));
            PalettesProperty = DependencyPropertyManager.Register("Palettes", typeof(PaletteCollection), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColorEdit.OnPalettesChanged)));
            ToolTipColorDisplayFormatProperty = DependencyPropertyManager.Register("ToolTipColorDisplayFormat", typeof(ColorDisplayFormat), ownerType, new PropertyMetadata(ColorDisplayFormat.Default, new PropertyChangedCallback(ColorEdit.OnToolTipColorDisplayFormatChanged)));
            CloseOwnerPopupOnClickProperty = DependencyPropertyManager.Register("CloseOwnerPopupOnClick", typeof(bool), ownerType, new PropertyMetadata(false));
            ShowAlphaChannelProperty = DependencyPropertyManager.Register("ShowAlphaChannel", typeof(bool), ownerType, new PropertyMetadata(true));
            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChangedEvent", RoutingStrategy.Direct, typeof(RoutedEventArgs), ownerType);
            GetColorNameEvent = EventManager.RegisterRoutedEvent("GetColorName", RoutingStrategy.Direct, typeof(GetColorNameEventHandler), ownerType);
            BaseEdit.EditValueProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, null, true, UpdateSourceTrigger.PropertyChanged));
        }

        public ColorEdit()
        {
            this.SetDefaultStyleKey(typeof(ColorEdit));
            this.RecentColors = new CircularList<System.Windows.Media.Color>((int) ColumnCountProperty.GetMetadata(typeof(ColorEdit)).DefaultValue);
            this.RecentColors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnRecentColorsCollectionChanged);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
        }

        private void AddGroups()
        {
            if ((this.Gallery != null) && (this.Palettes != null))
            {
                this.Gallery.Groups.Clear();
                foreach (ColorPalette palette in this.Palettes)
                {
                    this.Gallery.Groups.Add(this.CreateGalleryItemGroup(palette.Name, palette.Colors, palette.CalcBorder && !this.IsTopBottomChipMarginSet()));
                }
                this.UpdateActualShowRecentColors();
            }
        }

        protected virtual void ApplyCustomColor(System.Windows.Media.Color color)
        {
            this.Color = color;
        }

        protected internal void CloseOwnedPopup(bool accept)
        {
            if (this.OwnerPopupEdit == null)
            {
                if (this.CloseOwnerPopupOnClick & accept)
                {
                    this.FindOwnedPopupAndClose();
                }
            }
            else
            {
                this.OwnerPopupEdit.Focus();
                if (accept)
                {
                    this.OwnerPopupEdit.ClosePopup();
                }
                else
                {
                    this.OwnerPopupEdit.CancelPopup();
                }
            }
        }

        protected virtual object CoerceColor(System.Windows.Media.Color color) => 
            this.EditStrategy.CoerceColor(color);

        private static object CoerceColor(DependencyObject d, object value) => 
            ((ColorEdit) d).CoerceColor((System.Windows.Media.Color) value);

        protected virtual ColorGalleryItem CreateColorItem(System.Windows.Media.Color color)
        {
            ColorGalleryItem item1 = new ColorGalleryItem();
            item1.Color = color;
            item1.Hint = this.GetColorNameCore(color);
            item1.HideBorderSide = HideBorderSide.None;
            return item1;
        }

        protected internal override BaseEditSettings CreateEditorSettings() => 
            new ColorEditSettings();

        protected override EditStrategyBase CreateEditStrategy() => 
            new ColorEditStrategy(this);

        protected virtual GalleryItemGroup CreateGalleryItemGroup(string caption, IList<System.Windows.Media.Color> colors, bool calcBorder)
        {
            GalleryItemGroup group = new GalleryItemGroup();
            if (caption != null)
            {
                group.Caption = caption;
            }
            for (int i = 0; i < colors.Count; i++)
            {
                ColorGalleryItem item = this.CreateColorItem(colors[i]);
                group.Items.Add(item);
                if (calcBorder)
                {
                    this.UpdateItemBorderVisibility(item, i, colors.Count);
                }
            }
            Binding binding = new Binding("Caption");
            binding.Source = group;
            binding.Converter = new CaptionToVisibilityConverter();
            group.SetBinding(GalleryItemGroup.IsCaptionVisibleProperty, binding);
            return group;
        }

        void IColorEdit.AddCustomColor(System.Windows.Media.Color color)
        {
            this.RecentColors.Add(color);
            this.ApplyCustomColor(color);
            this.CloseOwnedPopup(true);
        }

        private void FindOwnedPopupAndClose()
        {
            for (DependencyObject obj2 = this; obj2 != null; obj2 = this.GetParent(obj2))
            {
                Popup popup = obj2 as Popup;
                if (popup != null)
                {
                    if (popup is BarPopupBase)
                    {
                        PopupMenuManager.ClosePopupBranch((BarPopupBase) popup);
                        return;
                    }
                    popup.IsOpen = false;
                    return;
                }
            }
        }

        protected override bool FocusEditCore() => 
            (this.OwnerPopupEdit == null) ? base.FocusEditCore() : false;

        protected internal virtual string GetColorNameCore(System.Windows.Media.Color color)
        {
            if (this.OwnerPopupEdit != null)
            {
                return this.OwnerPopupEdit.GetColorNameCore(color);
            }
            string colorName = ColorEditHelper.GetColorName(color, this.ToolTipColorDisplayFormat, this.ShowAlphaChannel);
            GetColorNameEventArgs args1 = new GetColorNameEventArgs(color, colorName);
            args1.RoutedEvent = GetColorNameEvent;
            GetColorNameEventArgs e = args1;
            base.RaiseEvent(e);
            return e.ColorName;
        }

        private DependencyObject GetParent(DependencyObject d) => 
            LayoutHelper.GetParent(d, true);

        protected virtual void InitGallery()
        {
            if (this.Gallery != null)
            {
                SetOwnerEdit(this.Gallery, this);
                this.AddGroups();
            }
        }

        protected virtual void InitializePaletteCollection()
        {
            if (this.Palettes == null)
            {
                base.SetCurrentValue(PalettesProperty, PredefinedPaletteCollections.Office);
            }
        }

        private bool IsTopBottomChipMarginSet() => 
            (this.ChipMargin.Top > 0.0) || (this.ChipMargin.Bottom > 0.0);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.OwnerPopupEdit != null)
            {
                this.OwnerPopupEdit.SetInnerColorEdit(this);
            }
        }

        private static void OnChipBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorEdit) d).UpdateResetButtonGlyph();
            ((ColorEdit) d).UpdateNoColorButtonGlyph();
        }

        protected virtual void OnChipMarginChanged()
        {
            this.AddGroups();
        }

        private static void OnChipMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorEdit) d).OnChipMarginChanged();
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorEdit) d).OnColorChanged((System.Windows.Media.Color) e.OldValue, (System.Windows.Media.Color) e.NewValue);
        }

        protected virtual void OnColorChanged(System.Windows.Media.Color oldValue, System.Windows.Media.Color newValue)
        {
            this.EditStrategy.OnColorChanged(oldValue, newValue);
        }

        protected virtual void OnColumnCountChanged(int columnCount)
        {
            this.RecentColors.SetSize(columnCount);
            this.AddGroups();
        }

        private static void OnColumnCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorEdit) d).OnColumnCountChanged((int) e.NewValue);
        }

        protected virtual void OnDefaultColorChanged(System.Windows.Media.Color newValue)
        {
            this.UpdateResetButtonGlyph();
        }

        private static void OnDefaultColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorEdit) d).OnDefaultColorChanged((System.Windows.Media.Color) e.NewValue);
        }

        private void OnGalleryItemClick(object sender, GalleryItemEventArgs e)
        {
            this.EditStrategy.OnGalleryColorChanged(((ColorGalleryItem) e.Item).Color);
            this.CloseOwnedPopup(true);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.InitializePaletteCollection();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this.OwnerPopupEdit != null)
            {
                this.OwnerPopupEdit.SetInnerColorEdit(this);
            }
            PopupBorderControl control = LayoutHelper.FindParentObject<PopupBorderControl>(this);
            if ((control != null) && (control.Popup != null))
            {
                this.ParentPopup = control.Popup;
            }
        }

        private void OnMoreColorsButtonClick(object sender, ItemClickEventArgs e)
        {
            IColorEdit owner = (this.OwnerPopupEdit != null) ? ((IColorEdit) this.OwnerPopupEdit) : ((IColorEdit) this);
            this.CloseOwnedPopup(false);
            ColorEditHelper.ShowColorChooserDialog(owner);
        }

        private void OnNoColorButtonClick(object sender, ItemClickEventArgs e)
        {
            this.EditStrategy.OnNoColorButtonClick();
            this.CloseOwnedPopup(true);
        }

        private static void OnPalettesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorEdit) d).AddGroups();
        }

        protected void OnRecentColorsCaptionChanged()
        {
            if (this.RecentColorsItemGroup != null)
            {
                string recentColorsCaption = this.RecentColorsCaption;
                string text2 = recentColorsCaption;
                if (recentColorsCaption == null)
                {
                    string local1 = recentColorsCaption;
                    text2 = EditorLocalizer.GetString(EditorStringId.ColorEdit_RecentColorsCaption);
                }
                this.RecentColorsItemGroup.Caption = text2;
            }
        }

        private static void OnRecentColorsCaptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorEdit) d).OnRecentColorsCaptionChanged();
        }

        protected virtual void OnRecentColorsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateActualShowRecentColors();
        }

        private void OnResetButtonClick(object sender, ItemClickEventArgs e)
        {
            this.EditStrategy.OnResetButtonClick();
            this.CloseOwnedPopup(true);
        }

        private static void OnShowMoreColorsButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorEdit) d).UpdateActualShowRecentColors();
        }

        protected virtual void OnToolTipColorDisplayFormatChanged()
        {
            this.EditStrategy.UpdateItemsState();
        }

        private static void OnToolTipColorDisplayFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorEdit) d).OnToolTipColorDisplayFormatChanged();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (this.ParentPopup != null)
            {
                this.ParentPopup = null;
            }
        }

        protected internal virtual void RaiseColorChanged()
        {
            RoutedEventArgs e = new RoutedEventArgs();
            e.RoutedEvent = ColorChangedEvent;
            base.RaiseEvent(e);
        }

        protected override void SubscribeEditEventsCore()
        {
            base.SubscribeEditEventsCore();
            this.ResetButton = base.EditCore.FindName("PART_ResetButton") as BarButtonItem;
            this.MoreColorsButton = base.EditCore.FindName("PART_MoreColorsButton") as BarButtonItem;
            this.NoColorButton = base.EditCore.FindName("PART_NoColorButton") as BarButtonItem;
            this.Gallery = base.EditCore.FindName("PART_Gallery") as DevExpress.Xpf.Bars.Gallery;
            this.InitGallery();
            this.SubscribeElementsEvents();
            this.UpdateResetButtonGlyph();
            this.UpdateNoColorButtonGlyph();
        }

        protected virtual void SubscribeElementsEvents()
        {
            if (this.Gallery != null)
            {
                this.Gallery.ItemClick += new GalleryItemEventHandler(this.OnGalleryItemClick);
            }
            if (this.ResetButton != null)
            {
                this.ResetButton.ItemClick += new ItemClickEventHandler(this.OnResetButtonClick);
            }
            if (this.MoreColorsButton != null)
            {
                this.MoreColorsButton.ItemClick += new ItemClickEventHandler(this.OnMoreColorsButtonClick);
            }
            if (this.NoColorButton != null)
            {
                this.NoColorButton.ItemClick += new ItemClickEventHandler(this.OnNoColorButtonClick);
            }
        }

        protected override void UnsubscribeEditEventsCore()
        {
            base.UnsubscribeEditEventsCore();
            this.UnsubscribeElementsEvents();
        }

        protected virtual void UnsubscribeElementsEvents()
        {
            if (this.NoColorButton != null)
            {
                this.NoColorButton.ItemClick -= new ItemClickEventHandler(this.OnNoColorButtonClick);
            }
            if (this.ResetButton != null)
            {
                this.ResetButton.ItemClick -= new ItemClickEventHandler(this.OnResetButtonClick);
            }
            if (this.MoreColorsButton != null)
            {
                this.MoreColorsButton.ItemClick -= new ItemClickEventHandler(this.OnMoreColorsButtonClick);
            }
            if (this.Gallery != null)
            {
                this.Gallery.ItemClick -= new GalleryItemEventHandler(this.OnGalleryItemClick);
            }
        }

        private void UpdateActualShowRecentColors()
        {
            if (this.Gallery != null)
            {
                bool flag = this.ShowMoreColorsButton && (this.RecentColors.Count > 0);
                if (this.RecentColorsItemGroup != null)
                {
                    this.Gallery.Groups.Remove(this.RecentColorsItemGroup);
                }
                if (flag)
                {
                    string recentColorsCaption = this.RecentColorsCaption;
                    string caption = recentColorsCaption;
                    if (recentColorsCaption == null)
                    {
                        string local1 = recentColorsCaption;
                        caption = EditorLocalizer.GetString(EditorStringId.ColorEdit_RecentColorsCaption);
                    }
                    this.RecentColorsItemGroup = this.CreateGalleryItemGroup(caption, this.RecentColors.ToList<System.Windows.Media.Color>(), false);
                    this.Gallery.Groups.Add(this.RecentColorsItemGroup);
                }
            }
        }

        protected virtual void UpdateItemBorderVisibility(ColorGalleryItem item, int index, int count)
        {
            int columnCount = this.ColumnCount;
            if (columnCount != 0)
            {
                int num2 = index;
                if (num2 < columnCount)
                {
                    item.HideBorderSide = HideBorderSide.Bottom;
                }
                else if (num2 > ((count - columnCount) - 1))
                {
                    item.HideBorderSide = HideBorderSide.Top;
                }
                else
                {
                    item.HideBorderSide = HideBorderSide.All | HideBorderSide.Right;
                }
            }
        }

        protected void UpdateNoColorButtonGlyph()
        {
            if (this.NoColorButton != null)
            {
                this.NoColorButton.Glyph = ColorEditHelper.CreateGlyph(EmptyColor, this.ChipBorderBrush, new Size(16.0, 16.0));
            }
        }

        protected void UpdateResetButtonGlyph()
        {
            if (this.ResetButton != null)
            {
                this.ResetButton.Glyph = ColorEditHelper.CreateGlyph(this.DefaultColor, this.ChipBorderBrush, new Size(16.0, 16.0));
            }
        }

        private Popup ParentPopup { get; set; }

        protected internal DevExpress.Xpf.Bars.Gallery Gallery { get; private set; }

        protected internal BarButtonItem ResetButton { get; private set; }

        protected internal BarButtonItem MoreColorsButton { get; private set; }

        protected internal BarButtonItem NoColorButton { get; private set; }

        protected PopupColorEdit OwnerPopupEdit
        {
            get
            {
                Predicate<DependencyObject> predicate = <>c.<>9__62_0;
                if (<>c.<>9__62_0 == null)
                {
                    Predicate<DependencyObject> local1 = <>c.<>9__62_0;
                    predicate = <>c.<>9__62_0 = element => GetOwnerEdit(element) is PopupColorEdit;
                }
                DependencyObject obj2 = LayoutHelper.FindLayoutOrVisualParentObject(this, predicate, false, null);
                return ((obj2 != null) ? (GetOwnerEdit(obj2) as PopupColorEdit) : null);
            }
        }

        [Description("Gets the recent colors.")]
        public CircularList<System.Windows.Media.Color> RecentColors { get; private set; }

        protected GalleryItemGroup RecentColorsItemGroup { get; private set; }

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

        [Description("Gets or sets whether the Default Color button is visible. This is a dependency property.")]
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

        [Description("Gets or sets the content of the Default Color button. This is a dependency property."), TypeConverter(typeof(ObjectConverter))]
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

        [Description("Gets or sets the content of the No Color button. This is a dependency property."), TypeConverter(typeof(ObjectConverter))]
        public object NoColorButtonContent
        {
            get => 
                base.GetValue(NoColorButtonContentProperty);
            set => 
                base.SetValue(NoColorButtonContentProperty, value);
        }

        [Description("Gets or sets the caption of the \"Recent Colors\" palette.")]
        public string RecentColorsCaption
        {
            get => 
                (string) base.GetValue(RecentColorsCaptionProperty);
            set => 
                base.SetValue(RecentColorsCaptionProperty, value);
        }

        [Description("Gets or sets the number of color columns. This is a dependency property.")]
        public int ColumnCount
        {
            get => 
                (int) base.GetValue(ColumnCountProperty);
            set => 
                base.SetValue(ColumnCountProperty, value);
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

        [Description("Gets or sets the brush that draws the outer border of a color chip. This is a dependency property.")]
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

        [Description("Gets or sets a value that specifies in which format the selected color is displayed within a tooltip. This is a dependency property.")]
        public ColorDisplayFormat ToolTipColorDisplayFormat
        {
            get => 
                (ColorDisplayFormat) base.GetValue(ToolTipColorDisplayFormatProperty);
            set => 
                base.SetValue(ToolTipColorDisplayFormatProperty, value);
        }

        [Description("Gets or sets whether the popup containing the ColorEdit, is automatically closed, after the ColorEdit has been clicked.")]
        public bool CloseOwnerPopupOnClick
        {
            get => 
                (bool) base.GetValue(CloseOwnerPopupOnClickProperty);
            set => 
                base.SetValue(CloseOwnerPopupOnClickProperty, value);
        }

        public bool ShowAlphaChannel
        {
            get => 
                (bool) base.GetValue(ShowAlphaChannelProperty);
            set => 
                base.SetValue(ShowAlphaChannelProperty, value);
        }

        protected ColorEditStrategy EditStrategy =>
            base.EditStrategy as ColorEditStrategy;

        protected internal ColorEditSettings Settings =>
            (ColorEditSettings) base.Settings;

        ColorPickerColorMode IColorEdit.ColorMode { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColorEdit.<>c <>9 = new ColorEdit.<>c();
            public static Predicate<DependencyObject> <>9__62_0;

            internal bool <get_OwnerPopupEdit>b__62_0(DependencyObject element) => 
                BaseEdit.GetOwnerEdit(element) is PopupColorEdit;
        }
    }
}

