namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class BaseBarItemLinkControlStrategy
    {
        public virtual void OnActualAllowGlyphThemingChanged(bool oldValue, bool newValue);
        public virtual void OnActualBarItemDisplayModeChanged(BarItemDisplayMode oldValue, BarItemDisplayMode newValue);
        public virtual void OnActualContentChanged(object oldValue, object newValue);
        public virtual void OnActualContentTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        public virtual void OnActualContentTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue);
        public virtual void OnActualDescriptionChanged(object oldValue, object newValue);
        public virtual void OnActualDescriptionTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        public virtual void OnActualGlyphAlignmentChanged(Dock oldValue, Dock newValue);
        public virtual void OnActualGlyphChanged(ImageSource oldValue, ImageSource newValue);
        public virtual void OnActualGlyphSizeChanged(Size oldValue, Size newValue);
        public virtual void OnActualGlyphTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        public virtual void OnActualIsArrowEnabledChanged(bool oldValue, bool newValue);
        public virtual void OnActualIsCheckedChanged(bool? oldValue, bool? newValue);
        public virtual void OnActualIsContentEnabledChanged(bool oldValue, bool newValue);
        public virtual void OnActualIsHoverEnabledChanged(bool oldValue, bool newValue);
        public virtual void OnActualKeyGestureTextChanged(string oldValue, string newValue);
        public virtual void OnActualSectorIndexChanged(int oldValue, int newValue);
        public virtual void OnActualShowArrowChanged(bool oldValue, bool newValue);
        public virtual void OnActualShowContentChanged(bool oldValue, bool newValue);
        public virtual void OnActualShowDescriptionChanged(bool oldValue, bool newValue);
        public virtual void OnCalculatedRibbonStyleChanged(RibbonItemStyles oldValue, RibbonItemStyles newValue);
        public virtual void OnContainerTypeChanged(LinkContainerType oldValue, LinkContainerType newValue);
        public virtual void OnCurrentRibbonStyleChanged(RibbonItemStyles oldValue, RibbonItemStyles newValue);
        public virtual void OnDesiredRibbonStyleChanged(RibbonItemStyles? oldValue, RibbonItemStyles? newValue);
        public virtual void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e);
        public virtual void OnHasGlyphChanged(bool oldValue, bool newValue);
        public virtual void OnIsEnabledChanged(bool oldValue, bool newValue);
        public virtual void OnIsHighlightedChanged(bool oldValue, bool newValue);
        public virtual void OnIsPressedChanged(bool oldValue, bool newValue);
        public virtual void OnIsRibbonStyleLargeChanged(bool oldValue, bool newValue);
        public virtual void OnIsSelectedChanged(bool oldValue, bool newValue);
        public virtual void OnLinkBaseChanged(BarItemLinkBase oldValue, BarItemLinkBase newValue);
        public virtual void OnLinkInfoChanged(BarItemLinkInfo oldValue, BarItemLinkInfo newValue);
        public virtual void OnLoaded();
        public virtual void OnMaxGlyphSizeChanged(Size oldValue, Size newValue);
        public virtual void OnOrientationChanged(Orientation oldValue, Orientation newValue);
        public virtual void OnRecycled();
        public virtual void OnRotateWhenVerticalChanged(bool oldValue, bool newValue);
        public virtual void OnShowCustomizationBorderChanged(bool oldValue, bool newValue);
        public virtual void OnShowDescriptionChanged(bool oldValue, bool newValue);
        public virtual void OnShowGlyphInPopupMenuChanged(bool? oldValue, bool? newValue);
        public virtual void OnShowHotBorderChanged(bool oldValue, bool newValue);
        public virtual void OnShowKeyGestureChanged(bool oldValue, bool newValue);
        public virtual void OnShowPressedBorderChanged(bool oldValue, bool newValue);
        public virtual void OnSpacingModeChanged(SpacingMode oldValue, SpacingMode newValue);
        public virtual void OnToolTipGlyphChanged(ImageSource oldValue, ImageSource newValue);
        public virtual void OnUnloaded();
        public virtual bool ReceiveEvent(object sender, EventArgs args);
    }
}

