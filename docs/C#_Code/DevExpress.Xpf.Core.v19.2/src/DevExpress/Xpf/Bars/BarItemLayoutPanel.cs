namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Themes;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class BarItemLayoutPanel : Panel, IBadgeHost, IInputElement
    {
        public static readonly DependencyProperty SplitTextModeProperty;
        public static readonly DependencyProperty ApplyDisabledOpacityProperty;
        public static readonly DependencyProperty GlyphRenderTransformProperty;
        public static readonly DependencyProperty ArrowTransformProperty;
        public static readonly DependencyProperty TouchSplitterThemeKeyProperty;
        public static readonly DependencyProperty TextSplitterStyleKeyProperty;
        public static readonly DependencyProperty SecondBorderUseNormalStateOnlyProperty;
        public static readonly DependencyProperty FirstBorderUseNormalStateOnlyProperty;
        public static readonly DependencyProperty SecondBorderNormalStateIsEmptyProperty;
        public static readonly DependencyProperty FirstBorderNormalStateIsEmptyProperty;
        public static readonly DependencyProperty DescriptionFontSettingsProperty;
        public static readonly DependencyProperty KeyGestureFontSettingsProperty;
        public static readonly DependencyProperty Content2FontSettingsProperty;
        public static readonly DependencyProperty ContentFontSettingsProperty;
        public static readonly DependencyProperty ImageColorizerSettingsProperty;
        public static readonly DependencyProperty GlyphBackgroundTemplateProperty;
        public static readonly DependencyProperty GlyphBackgroundThicknessProperty;
        public static readonly DependencyProperty CommonContentMarginProperty;
        public static readonly DependencyProperty RightContentAndGlyphMarginProperty;
        public static readonly DependencyProperty LeftContentAndGlyphMarginProperty;
        public static readonly DependencyProperty BottomContentAndGlyphMarginProperty;
        public static readonly DependencyProperty TopContentAndGlyphMarginProperty;
        public static readonly DependencyProperty ContentAndGlyphMarginProperty;
        public static readonly DependencyProperty RightArrowMarginProperty;
        public static readonly DependencyProperty LeftArrowMarginProperty;
        public static readonly DependencyProperty BottomArrowMarginProperty;
        public static readonly DependencyProperty TopArrowMarginProperty;
        public static readonly DependencyProperty RightCommonContentMarginProperty;
        public static readonly DependencyProperty LeftCommonContentMarginProperty;
        public static readonly DependencyProperty BottomCommonContentMarginProperty;
        public static readonly DependencyProperty TopCommonContentMarginProperty;
        public static readonly DependencyProperty RightGlyphMarginProperty;
        public static readonly DependencyProperty LeftGlyphMarginProperty;
        public static readonly DependencyProperty LeftGlyphMarginInSimplifiedModeProperty;
        public static readonly DependencyProperty BottomGlyphMarginProperty;
        public static readonly DependencyProperty TopGlyphMarginProperty;
        public static readonly DependencyProperty DisabledOpacityProperty;
        public static readonly DependencyProperty ArrowTemplateProperty;
        public static readonly DependencyProperty PressedBorderTemplateProperty;
        public static readonly DependencyProperty CustomizationBorderTemplateProperty;
        public static readonly DependencyProperty HoverBorderTemplateProperty;
        public static readonly DependencyProperty NormalBorderTemplateProperty;
        public static readonly DependencyProperty ArrowThemeKeyProperty;
        public static readonly DependencyProperty BorderThemeKeyProperty;
        public static readonly DependencyProperty CommonMarginProperty;
        public static readonly DependencyProperty CommonMarginInSimplifiedModeProperty;
        public static readonly DependencyProperty AdditionalContentMarginProperty;
        public static readonly DependencyProperty AdditionalContentProperty;
        public static readonly DependencyProperty ArrowMarginProperty;
        public static readonly DependencyProperty KeyGestureMarginProperty;
        public static readonly DependencyProperty DescriptionMarginProperty;
        public static readonly DependencyProperty Content2MarginProperty;
        public static readonly DependencyProperty ContentMarginProperty;
        public static readonly DependencyProperty GlyphMarginProperty;
        public static readonly DependencyProperty IsInSimplifiedRibbonProperty;
        public static readonly DependencyProperty ContainerTypeProperty;
        private bool showGlyph;
        private ImageSource actualGlyph;
        private Size glyphSize;
        private Size maxGlyphSize;
        private DevExpress.Xpf.Core.Badge badge;
        private DataTemplate glyphTemplate;
        private Dock glyphToContentAlignment;
        private object content;
        private object content2;
        private object description;
        private object keyGesture;
        private bool splitContent;
        private DataTemplate contentTemplate;
        private DataTemplate descriptionTemplate;
        private DataTemplateSelector contentTemplateSelector;
        private bool showFirstBorder;
        private bool showSecondBorder;
        private DevExpress.Xpf.Bars.BorderState borderState;
        private bool showArrow;
        private bool isFirstBorderActive;
        private bool isSecondBorderActive;
        private bool showContent;
        private bool showContent2;
        private bool showDescription;
        private bool showKeyGesture;
        private HorizontalItemPositionType itemPosition;
        private CodedPanelOwnerKind ownerKind;
        private bool showAdditionalContent;
        private DevExpress.Xpf.Bars.SecondBorderPlacement secondBorderPlacement;
        private Dock contentAndGlyphToArrowAlignment;
        private bool showGlyphBackground;
        private DevExpress.Xpf.Bars.BorderState firstBorderState;
        private DevExpress.Xpf.Bars.BorderState secondBorderState;
        private DataTemplate additionalContentTemplate;
        private DataTemplate content2Template;
        private SizeSettings additionalContentSizeSettings;
        private HorizontalAlignment contentHorizontalAlignment;
        private HorizontalAlignment additionalContentHorizontalAlignment;
        private HorizontalItemPositionType firstBorderItemPosition;
        private HorizontalItemPositionType secondBorderItemPosition;
        private bool stretchAdditionalContentVertically;
        private bool colorizeGlyph;
        private DevExpress.Xpf.Bars.SpacingMode spacingMode;
        private bool actAsDropDown;
        private bool recognizesAccessKey;
        [ThreadStatic]
        private static ControlTemplate accessTextControlTemplate;
        private readonly List<UIElement> elementsToArrage;
        private Thickness actualGlyphMargin;
        private Thickness actualCommonContentMargin;
        private bool stretchTemplatedGlyph;

        static BarItemLayoutPanel();
        public BarItemLayoutPanel();
        protected virtual Size ArrangeButton(Size finalSize);
        protected virtual void ArrangeContents(Point startPoint, Size availableSize);
        protected void ArrangeElement(UIElement element, Rect rect);
        protected override Size ArrangeOverride(Size finalSize);
        protected void BeginArrange();
        public void BeginUpdate();
        protected void CalculateActualMargins();
        public virtual void Clear();
        protected virtual void ClearArrowControl();
        protected virtual void ClearBorderControl(ItemBorderControl borderControl);
        protected virtual void ClearBorderControls();
        protected virtual void ClearFontSettings(Control element);
        protected virtual void ClearTextSplitterControl();
        private static object CoerceThickness(DependencyObject d, object baseValue);
        protected virtual void CreateElementAdditionalContentHost();
        protected virtual void CreateElementArrowControl();
        protected virtual void CreateElementContent();
        protected virtual void CreateElementContent2();
        protected virtual void CreateElementDescription();
        protected virtual void CreateElementFirstBorderControl();
        protected virtual void CreateElementGlyph();
        protected virtual void CreateElementGlyphBackground();
        protected virtual void CreateElementKeyGesture();
        protected virtual void CreateElementSecondBorderControl();
        protected virtual void CreateElementTemplatedGlyph();
        protected virtual void CreateElementTextSplitter();
        protected virtual void CreateElementTouchSplitter();
        protected static Size CreateNewSize();
        protected static Size CreateNewSize(double width, double height);
        bool IBadgeHost.CalculateBounds(Size badgeSize, HorizontalAlignment horizontalAlignment, HorizontalAlignment horizontalAnchor, VerticalAlignment verticalAlignment, VerticalAlignment verticalAnchor, ref Rect targetBounds, ref Rect badgeBounds);
        protected void EndArrange();
        public virtual void EndUpdate();
        [IteratorStateMachine(typeof(BarItemLayoutPanel.<EnumerateElements>d__627))]
        private IEnumerable<FrameworkElement> EnumerateElements();
        protected double GetLeftRight(Thickness t);
        protected double GetTopBottom(Thickness t);
        protected virtual void InvalidateArrowControl();
        protected virtual void InvalidateBorderControl();
        protected void InvalidateParentMeasure();
        protected virtual Size MeasureButton(Size availableSize);
        protected virtual Size MeasureContents(Size measureSize);
        protected override Size MeasureOverride(Size availableSize);
        protected virtual void ModifyThemeKey(ref BarItemBorderThemeKeyExtension themeKeyExtension, BarItemBorderThemeKeys key);
        protected virtual void OnActAsDropDownChanged(bool oldValue);
        protected virtual void OnActualGlyphChanged(ImageSource oldValue);
        protected virtual void OnAdditionalContentChanged(UIElement oldContent);
        protected virtual void OnAdditionalContentHorizontalAlignmentChanged(HorizontalAlignment oldValue);
        protected virtual void OnAdditionalContentSizeSettingsChanged(SizeSettings oldValue);
        private void OnAdditionalContentSizeSettingsChangedEvent(object sender, EventArgs e);
        protected virtual void OnAdditionalContentTemplateChanged(DataTemplate oldValue);
        protected virtual void OnApplyDisabledOpacityChanged(bool oldValue, bool newValue);
        protected virtual void OnBadgeChanged(DevExpress.Xpf.Core.Badge oldValue);
        protected virtual void OnBorderStateChanged(DevExpress.Xpf.Bars.BorderState oldValue);
        protected virtual void OnColorizeGlyphChanged(bool oldValue);
        protected virtual void OnContent2Changed(object oldValue);
        protected virtual void OnContent2TemplateChanged(DataTemplate oldValue);
        protected virtual void OnContentAndGlyphToArrowAlignmentChanged(Dock oldValue);
        protected virtual void OnContentChanged(object oldValue);
        protected virtual void OnContentHorizontalAlignmentChanged(HorizontalAlignment oldValue);
        protected virtual void OnContentTemplateChanged(DataTemplate oldValue);
        protected virtual void OnContentTemplateSelectorChanged(DataTemplateSelector oldValue);
        protected virtual void OnDescriptionChanged(object oldValue);
        protected virtual void OnDescriptionTemplateChanged(DataTemplate oldValue);
        private void OnDisabledOpacityChanged(double oldValue, double newValue);
        private void OnElementCreated(UIElement uiElement);
        protected virtual void OnFirstBorderItemPositionChanged(HorizontalItemPositionType oldValue);
        protected virtual void OnFirstBorderNormalStateIsEmptyChanged(bool oldValue);
        protected virtual void OnFirstBorderStateChanged(DevExpress.Xpf.Bars.BorderState oldValue);
        protected virtual void OnGlyphBackgroundTemplateChanged(ControlTemplate oldValue);
        protected virtual void OnGlyphRenderTransformChanged(Transform oldValue);
        protected virtual void OnGlyphSizeChanged(Size oldValue);
        protected virtual void OnGlyphTemplateChanged(DataTemplate oldValue);
        protected virtual void OnGlyphToContentAlignmentChanged(Dock oldValue);
        protected virtual void OnIsFirstBorderActiveChanged(bool oldValue);
        protected virtual void OnIsInSimplifiedRibbonChanged(bool oldValue);
        protected virtual void OnIsSecondBorderActiveChanged(bool oldValue);
        protected virtual void OnItemPositionChanged(HorizontalItemPositionType oldValue);
        protected virtual void OnKeyGestureChanged(object oldValue);
        protected virtual void OnMaxGlyphSizeChanged(Size oldValue);
        protected override void OnMouseMove(MouseEventArgs e);
        protected virtual void OnOwnerKindChanged(CodedPanelOwnerKind oldValue);
        protected virtual void OnRecognizesAccessKeyChanged(bool oldValue);
        protected virtual void OnSecondBorderItemPositionChanged(HorizontalItemPositionType oldValue);
        protected virtual void OnSecondBorderNormalStateIsEmptyChanged(bool oldValue);
        protected virtual void OnSecondBorderPlacementChanged(DevExpress.Xpf.Bars.SecondBorderPlacement oldValue);
        protected virtual void OnSecondBorderStateChanged(DevExpress.Xpf.Bars.BorderState oldValue);
        protected virtual void OnShowAdditionalContentChanged(bool oldValue);
        protected virtual void OnShowArrowChanged(bool oldValue);
        protected virtual void OnShowContent2Changed(bool oldValue);
        protected virtual void OnShowContentChanged(bool oldValue);
        protected virtual void OnShowDescriptionChanged(bool oldValue);
        protected virtual void OnShowFirstBorderChanged(bool oldValue);
        protected virtual void OnShowGlyphBackgroundChanged(bool oldValue);
        protected virtual void OnShowGlyphChanged(bool oldValue);
        protected virtual void OnShowKeyGestureChanged(bool oldValue);
        protected virtual void OnShowSecondBorderChanged(bool oldValue);
        protected virtual void OnSpacingModeChanged(DevExpress.Xpf.Bars.SpacingMode oldValue);
        protected virtual void OnSplitContentChanged(bool oldValue);
        protected virtual void OnSplitTextModeChanged();
        protected virtual void OnStretchAdditionalContentVerticallyChanged(bool oldValue);
        protected virtual void OnStretchTemplatedGlyphChanged(bool oldValue);
        protected virtual void OnTextSplitterStyleKeyChanged(ThemeKeyExtensionGeneric oldValue);
        protected internal virtual void OnThemeChanged();
        protected virtual void UpdateAdditionalContentFontSettings();
        protected virtual void UpdateAdditionalContentHost();
        protected virtual void UpdateArrowControl();
        protected virtual void UpdateArrowControlFontSettings();
        protected virtual void UpdateBorderControl();
        protected virtual void UpdateBorderControlsState();
        protected virtual void UpdateBorderControlTemplate(ItemBorderControl borderControl, BarItemBorderThemeKeyExtension themeKeyExtension, DevExpress.Xpf.Bars.BorderState state, bool useNormalState);
        protected virtual void UpdateContent();
        protected virtual void UpdateContent2();
        protected virtual void UpdateContent2FontSettings();
        protected virtual void UpdateContentFontSettings();
        protected virtual void UpdateCustomizationTemplate(ItemBorderControl borderControl, BarItemBorderThemeKeyExtension themeKeyExtension);
        protected virtual void UpdateDescription();
        protected virtual void UpdateDescriptionFontSettings();
        protected virtual void UpdateElementOpacity(UIElement element);
        protected virtual void UpdateElementTextSplitterIsArrowVisible();
        protected virtual void UpdateElementTextSplitterStyle();
        protected virtual void UpdateElementTouchSplitterTemplate();
        protected virtual void UpdateFontSettings(bool clear = false);
        protected virtual void UpdateFontSettings(Control element, FontSettings settings);
        protected virtual void UpdateGlyph();
        protected virtual void UpdateGlyphBackground();
        protected virtual void UpdateHoverTemplate(ItemBorderControl borderControl, BarItemBorderThemeKeyExtension themeKeyExtension);
        protected virtual void UpdateImageColorizerSettings();
        protected virtual void UpdateKeyGesture();
        protected virtual void UpdateKeyGestureFontSettings();
        protected virtual void UpdateNormalTemplate(ItemBorderControl borderControl, BarItemBorderThemeKeyExtension themeKeyExtension);
        protected virtual void UpdatePressedTemplate(ItemBorderControl borderControl, BarItemBorderThemeKeyExtension themeKeyExtension);
        protected virtual void UpdateSvgPaletteState();
        protected virtual void UpdateTemplatedGlyphFontSettings();
        protected virtual void UpdateTextSplitterFontSettings();
        protected virtual void UpdateTouchSplitter();
        protected virtual void UpdateTouchSplitterOrientation();

        public LinkContainerType ContainerType { get; set; }

        public Thickness GlyphMargin { get; set; }

        public bool IsInSimplifiedRibbon { get; set; }

        public Thickness ContentMargin { get; set; }

        public Thickness Content2Margin { get; set; }

        public Thickness DescriptionMargin { get; set; }

        public Thickness KeyGestureMargin { get; set; }

        public Thickness ArrowMargin { get; set; }

        public FrameworkElement AdditionalContent { get; set; }

        public Thickness AdditionalContentMargin { get; set; }

        public Thickness CommonMargin { get; set; }

        public Thickness? CommonMarginInSimplifiedMode { get; set; }

        public BarItemBorderThemeKeyExtension BorderThemeKey { get; set; }

        public ThemeKeyExtensionGeneric ArrowThemeKey { get; set; }

        public ControlTemplate NormalBorderTemplate { get; set; }

        public ControlTemplate HoverBorderTemplate { get; set; }

        public ControlTemplate CustomizationBorderTemplate { get; set; }

        public ControlTemplate PressedBorderTemplate { get; set; }

        public ControlTemplate ArrowTemplate { get; set; }

        public double DisabledOpacity { get; set; }

        public Thickness? TopGlyphMargin { get; set; }

        public Thickness? BottomGlyphMargin { get; set; }

        public Thickness? LeftGlyphMargin { get; set; }

        public Thickness? LeftGlyphMarginInSimplifiedMode { get; set; }

        public Thickness? RightGlyphMargin { get; set; }

        public Thickness? TopCommonContentMargin { get; set; }

        public Thickness? BottomCommonContentMargin { get; set; }

        public Thickness? LeftCommonContentMargin { get; set; }

        public Thickness? RightCommonContentMargin { get; set; }

        public Thickness? TopArrowMargin { get; set; }

        public Thickness? BottomArrowMargin { get; set; }

        public Thickness? LeftArrowMargin { get; set; }

        public Thickness? RightArrowMargin { get; set; }

        public Thickness ContentAndGlyphMargin { get; set; }

        public Thickness? TopContentAndGlyphMargin { get; set; }

        public Thickness? BottomContentAndGlyphMargin { get; set; }

        public Thickness? LeftContentAndGlyphMargin { get; set; }

        public Thickness? RightContentAndGlyphMargin { get; set; }

        public Thickness CommonContentMargin { get; set; }

        public Thickness GlyphBackgroundThickness { get; set; }

        public ControlTemplate GlyphBackgroundTemplate { get; set; }

        public FontSettings ContentFontSettings { get; set; }

        public BarItemImageColorizerSettings ImageColorizerSettings { get; set; }

        public FontSettings Content2FontSettings { get; set; }

        public FontSettings KeyGestureFontSettings { get; set; }

        public FontSettings DescriptionFontSettings { get; set; }

        public bool FirstBorderNormalStateIsEmpty { get; set; }

        public bool SecondBorderNormalStateIsEmpty { get; set; }

        public bool FirstBorderUseNormalStateOnly { get; set; }

        public bool SecondBorderUseNormalStateOnly { get; set; }

        public ThemeKeyExtensionGeneric TextSplitterStyleKey { get; set; }

        public ThemeKeyExtensionGeneric TouchSplitterThemeKey { get; set; }

        public Transform ArrowTransform { get; set; }

        public Transform GlyphRenderTransform { get; set; }

        public bool ApplyDisabledOpacity { get; set; }

        public DevExpress.Xpf.Bars.SplitTextMode? SplitTextMode { get; set; }

        public bool RecognizesAccessKey { get; set; }

        public DevExpress.Xpf.Bars.SpacingMode SpacingMode { get; set; }

        public bool StretchAdditionalContentVertically { get; set; }

        public HorizontalItemPositionType SecondBorderItemPosition { get; set; }

        public HorizontalItemPositionType FirstBorderItemPosition { get; set; }

        public HorizontalAlignment AdditionalContentHorizontalAlignment { get; set; }

        public HorizontalAlignment ContentHorizontalAlignment { get; set; }

        public SizeSettings AdditionalContentSizeSettings { get; set; }

        public DataTemplate Content2Template { get; set; }

        public DataTemplate AdditionalContentTemplate { get; set; }

        public DevExpress.Xpf.Bars.BorderState SecondBorderState { get; set; }

        public DevExpress.Xpf.Bars.BorderState FirstBorderState { get; set; }

        public bool ShowGlyphBackground { get; set; }

        public DevExpress.Xpf.Bars.SecondBorderPlacement SecondBorderPlacement { get; set; }

        public bool ShowAdditionalContent { get; set; }

        public CodedPanelOwnerKind OwnerKind { get; set; }

        public HorizontalItemPositionType ItemPosition { get; set; }

        public bool ShowKeyGesture { get; set; }

        public bool ShowDescription { get; set; }

        public bool ShowContent2 { get; set; }

        public bool ShowContent { get; set; }

        public bool IsSecondBorderActive { get; set; }

        public bool IsFirstBorderActive { get; set; }

        public bool ShowArrow { get; set; }

        public bool ActualShowArrow { get; }

        public bool ShouldShowTouchSplitter { get; }

        public bool ActualShowTouchSplitter { get; }

        public bool ShowArrowInTextSplitter { get; }

        public DevExpress.Xpf.Bars.BorderState BorderState { get; set; }

        public bool SecondBorderShowing { get; }

        public Thickness ActualCommonMargin { get; }

        public Thickness? ActualLeftGlyphMargin { get; }

        public bool ShowSecondBorder { get; set; }

        public bool FirstBorderShowing { get; }

        public bool ShowFirstBorder { get; set; }

        public DataTemplateSelector ContentTemplateSelector { get; set; }

        public DataTemplate ContentTemplate { get; set; }

        public DataTemplate DescriptionTemplate { get; set; }

        public bool SplitContent { get; set; }

        public object KeyGesture { get; set; }

        public object Description { get; set; }

        public object Content2 { get; set; }

        public object Content { get; set; }

        public Dock GlyphToContentAlignment { get; set; }

        public DataTemplate GlyphTemplate { get; set; }

        public Size MaxGlyphSize { get; set; }

        public DevExpress.Xpf.Core.Badge Badge { get; set; }

        public bool StretchTemplatedGlyph { get; set; }

        public Size GlyphSize { get; set; }

        public ImageSource ActualGlyph { get; set; }

        public bool ShowGlyph { get; set; }

        public Dock ContentAndGlyphToArrowAlignment { get; set; }

        public bool ColorizeGlyph { get; set; }

        public bool ActAsDropDown { get; set; }

        protected string ThemeName { get; }

        protected internal FrameworkElement ElementTargetGlyph { get; }

        protected internal Image ElementGlyph { get; private set; }

        protected internal MeasurePixelSnapperContentControl ElementTemplatedGlyph { get; private set; }

        protected internal ContentControl ElementContent { get; private set; }

        protected internal ContentControl ElementContent2 { get; private set; }

        protected internal ContentControl ElementDescription { get; private set; }

        protected internal ContentControl ElementKeyGesture { get; private set; }

        protected internal TextSplitterControl ElementTextSplitter { get; private set; }

        protected internal ItemBorderControl ElementFirstBorderControl { get; private set; }

        protected internal ItemBorderControl ElementSecondBorderControl { get; private set; }

        protected internal ArrowControl ElementArrowControl { get; private set; }

        protected internal ContentControl ElementGlyphBackground { get; private set; }

        protected internal ContentControl ElementAdditionalContentHost { get; private set; }

        protected internal LayoutTransformPanel ElementTouchSplitterTransformPanel { get; set; }

        public bool IsUpdating { get; private set; }

        private static ControlTemplate AccessTextControlTemplate { get; }

        protected Size ContentDesiredSize { get; set; }

        protected Size ContentAndGlyphDesiredSize { get; set; }

        protected bool ActualShowAdditionalContent { get; }

        protected bool ShowAnyContent { get; }

        protected internal Thickness ActualCommonContentMargin { get; }

        protected internal Thickness ActualArrowMargin { get; set; }

        protected internal Thickness ActualGlyphMargin { get; }

        public bool IsMouseOverFirstBorder { get; set; }

        public bool IsMouseOverSecondBorder { get; set; }

        public bool CalculateIsMouseOver { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLayoutPanel.<>c <>9;

            static <>c();
            internal void <.cctor>b__229_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_10(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_11(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_12(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_13(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_14(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_15(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_16(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_17(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_18(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_19(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_20(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_21(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_22(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_23(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_5(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_6(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_7(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_8(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__229_9(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }

    }
}

