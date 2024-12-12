namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public interface IBarItemLinkControl : IFrameworkElementAPISupport, IUIElementAPI, IAnimatable, IFrameworkInputElement, IInputElement, ISupportInitialize, IQueryAmbient
    {
        event RoutedEventHandler Click;

        event RoutedEventHandler IsHighlightedChanged;

        event RoutedEventHandler IsSelectedChanged;

        void DestroyDragDropHelper();
        void ForceSetIsSelected(bool value);
        INavigationOwner GetBoundOwner();
        bool GetCanShowKeyTip();
        double GetHorizontalKeyTipOffset();
        BarItemLinkControlToolTipHorizontalPlacement GetHorizontalKeyTipPlacement();
        bool GetIsSelectable();
        double GetVerticalKeyTipOffset();
        BarItemLinkControlToolTipVerticalPlacement GetVerticalKeyTipPlacement();
        void OnActualAllowGlyphThemingChanged(bool oldValue, bool newValue);
        void OnActualBarItemDisplayModeChanged(BarItemDisplayMode oldValue, BarItemDisplayMode newValue);
        void OnActualContentChanged(object oldValue, object newValue);
        void OnActualContentTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        void OnActualContentTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue);
        void OnActualDescriptionChanged(object oldValue, object newValue);
        void OnActualDescriptionTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        void OnActualGlyphAlignmentChanged(Dock oldValue, Dock newValue);
        void OnActualGlyphChanged(ImageSource oldValue, ImageSource newValue);
        void OnActualGlyphSizeChanged(Size oldValue, Size newValue);
        void OnActualGlyphTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        void OnActualIsArrowEnabledChanged(bool oldValue, bool newValue);
        void OnActualIsCheckedChanged(bool? oldValue, bool? newValue);
        void OnActualIsContentEnabledChanged(bool oldValue, bool newValue);
        void OnActualIsHoverEnabledChanged(bool oldValue, bool newValue);
        void OnActualKeyGestureTextChanged(string oldValue, string newValue);
        void OnActualShowContentChanged(bool oldValue, bool newValue);
        void OnActualShowGlyphChanged(bool oldValue, bool newValue);
        void OnBadgeChanged(DevExpress.Xpf.Core.Badge oldValue, DevExpress.Xpf.Core.Badge newValue);
        void OnCalculatedRibbonStyleChanged(RibbonItemStyles oldValue, RibbonItemStyles newValue);
        void OnClear();
        void OnClick();
        void OnContainerTypeChanged(LinkContainerType oldValue, LinkContainerType newValue);
        void OnCurrentRibbonStyleChanged(RibbonItemStyles oldValue, RibbonItemStyles newValue);
        void OnCustomizationModeChanged();
        void OnDesiredRibbonStyleChanged(RibbonItemStyles? oldValue, RibbonItemStyles? newValue);
        void OnGlyphChanged(ImageSource oldValue, ImageSource newValue);
        void OnHasGlyphChanged(bool oldValue, bool newValue);
        void OnIsHighlightedChanged(bool oldValue, bool newValue);
        void OnIsInSimplifiedRibbonChanged(bool oldValue, bool newValue);
        void OnIsPressedChanged(bool oldValue, bool newValue);
        bool OnIsPressedCoerce(bool baseValue);
        void OnIsRibbonStyleLargeChanged(bool oldValue, bool newValue);
        void OnIsSelectedChanged(bool oldValue, bool newValue);
        void OnLinkBaseChanged(BarItemLinkBase oldValue, BarItemLinkBase newValue);
        void OnLinkInfoChanged(BarItemLinkInfo oldValue, BarItemLinkInfo newValue);
        void OnMaxGlyphSizeChanged(Size oldValue, Size newValue);
        void OnOrientationChanged(System.Windows.Controls.Orientation oldValue, System.Windows.Controls.Orientation newValue);
        void OnRecycled();
        void OnRotateWhenVerticalChanged(bool oldValue, bool newValue);
        void OnShowCustomizationBorderChanged(bool oldValue, bool newValue);
        void OnShowDescriptionChanged(bool oldValue, bool newValue);
        void OnShowGlyphInPopupMenuChanged(bool? oldValue, bool? newValue);
        void OnShowHotBorderChanged(bool oldValue, bool newValue);
        void OnShowKeyGestureChanged(bool oldValue, bool newValue);
        void OnShowPressedBorderChanged(bool oldValue, bool newValue);
        void OnSpacingModeChanged(DevExpress.Xpf.Bars.SpacingMode oldValue, DevExpress.Xpf.Bars.SpacingMode newValue);
        void OnToolTipGlyphChanged(ImageSource oldValue, ImageSource newValue);
        bool ProcessKeyDown(KeyEventArgs e);
        bool SelectOnKeyTip();
        Point TranslatePointWithoutTransform(Point point);
        void UpdateActualAllowGlyphTheming();
        void UpdateActualBarItemDisplayMode();
        void UpdateActualContent();
        void UpdateActualContentTemplate();
        void UpdateActualContentTemplateSelector();
        void UpdateActualDescriptionTemplate();
        void UpdateActualGlyph();
        void UpdateActualGlyphTemplate();
        void UpdateActualIsChecked();
        void UpdateActualKeyGesture();
        void UpdateActualProperties();
        void UpdateActualPropertiesOverride();
        void UpdateActualVisibility();
        void UpdateBadge();
        void UpdateCurrentRibbonStyle();
        void UpdateDataContext();
        void UpdateDescription();
        void UpdateGlyphAlignment();
        void UpdateIsEnabled();
        void UpdateOrientation();
        void UpdateShowGlyphInPopupMenu();
        void UpdateShowKeyGesture();
        void UpdateToolTip();
        void UpdateVerticalAlignment();

        BarItemLinkControlStrategy Strategy { get; }

        DevExpress.Xpf.Bars.LinksControl LinksControl { get; }

        DevExpress.Xpf.Bars.SpacingMode SpacingMode { get; set; }

        BarItemLinkInfo LinkInfo { get; set; }

        BarItemLinkBase LinkBase { get; set; }

        LinkContainerType ContainerType { get; set; }

        bool CloseSubMenuOnClick { get; }

        bool IsLinkInBar { get; }

        bool IsLinkInCustomizationMode { get; }

        bool IsLinkInRibbon { get; }

        bool IsLinkInHorizontalHeader { get; }

        bool IsLinkInApplicationMenu { get; }

        bool IsLinkInMenu { get; }

        bool IsLinkInMainMenu { get; }

        bool IsLinkInRadialMenu { get; }

        bool IsLinkInStatusBar { get; }

        bool IsInSimplifiedRibbon { get; set; }

        BarItemLink Link { get; }

        RibbonItemStyles CurrentRibbonStyle { get; set; }

        RibbonItemStyles? DesiredRibbonStyle { get; set; }

        RibbonItemStyles CalculatedRibbonStyle { get; set; }

        BarItemDisplayMode ActualBarItemDisplayMode { get; set; }

        DataTemplate ActualGlyphTemplate { get; set; }

        DataTemplate ActualContentTemplate { get; set; }

        DataTemplate ActualDescriptionTemplate { get; set; }

        bool ActualShowGlyph { get; set; }

        ImageSource Glyph { get; set; }

        ImageSource MediumGlyph { get; set; }

        object ActualDescription { get; set; }

        string ActualKeyGestureText { get; set; }

        bool ActualShowContent { get; set; }

        Size ActualGlyphSize { get; set; }

        bool? ActualIsChecked { get; set; }

        Dock ActualGlyphAlignment { get; set; }

        bool ActualAllowGlyphTheming { get; set; }

        bool ActualIsArrowEnabled { get; set; }

        bool ActualIsHoverEnabled { get; set; }

        DataTemplateSelector ActualContentTemplateSelector { get; set; }

        bool ActualIsContentEnabled { get; set; }

        ImageSource ActualGlyph { get; set; }

        ImageSource ToolTipGlyph { get; set; }

        object ActualContent { get; set; }

        bool ShowKeyGesture { get; set; }

        bool ShowHotBorder { get; set; }

        bool ShowDescription { get; set; }

        bool ShowCustomizationBorder { get; set; }

        bool RotateWhenVertical { get; set; }

        bool ShowPressedBorder { get; set; }

        System.Windows.Controls.Orientation Orientation { get; set; }

        bool IsRibbonStyleLarge { get; set; }

        Size MaxGlyphSize { get; set; }

        bool? ShowGlyphInPopupMenu { get; set; }

        bool HasGlyph { get; set; }

        bool IsSelected { get; set; }

        bool IsHighlighted { get; set; }

        bool IsPressed { get; set; }

        DependencyObject VisualParent { get; }

        DevExpress.Xpf.Core.Badge Badge { get; set; }
    }
}

