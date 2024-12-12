﻿namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class BarItemLinkControlTemplateProvider : DependencyObject
    {
        public static readonly DependencyProperty BorderStyleInBarProperty;
        public static readonly DependencyProperty BorderStyleInMainMenuProperty;
        public static readonly DependencyProperty BorderStyleInMenuProperty;
        public static readonly DependencyProperty BorderStyleInMenuHorizontalProperty;
        public static readonly DependencyProperty BorderStyleInApplicationMenuProperty;
        public static readonly DependencyProperty BorderStyleInStatusBarProperty;
        public static readonly DependencyProperty BorderStyleInRibbonPageGroupProperty;
        public static readonly DependencyProperty BorderStyleInButtonGroupProperty;
        public static readonly DependencyProperty BorderStyleInQuickAccessToolbarProperty;
        public static readonly DependencyProperty BorderStyleInQuickAccessToolbarFooterProperty;
        public static readonly DependencyProperty BorderStyleInRibbonPageHeaderProperty;
        public static readonly DependencyProperty BorderStyleInRibbonStatusBarLeftProperty;
        public static readonly DependencyProperty BorderStyleInRibbonStatusBarRightProperty;
        public static readonly DependencyProperty NormalContentStyleInBarProperty;
        public static readonly DependencyProperty HotContentStyleInBarProperty;
        public static readonly DependencyProperty PressedContentStyleInBarProperty;
        public static readonly DependencyProperty DisabledContentStyleInBarProperty;
        public static readonly DependencyProperty NormalContentStyleInMainMenuProperty;
        public static readonly DependencyProperty HotContentStyleInMainMenuProperty;
        public static readonly DependencyProperty PressedContentStyleInMainMenuProperty;
        public static readonly DependencyProperty DisabledContentStyleInMainMenuProperty;
        public static readonly DependencyProperty NormalContentStyleInMenuProperty;
        public static readonly DependencyProperty HotContentStyleInMenuProperty;
        public static readonly DependencyProperty PressedContentStyleInMenuProperty;
        public static readonly DependencyProperty DisabledContentStyleInMenuProperty;
        public static readonly DependencyProperty NormalContentStyleInApplicationMenuProperty;
        public static readonly DependencyProperty HotContentStyleInApplicationMenuProperty;
        public static readonly DependencyProperty PressedContentStyleInApplicationMenuProperty;
        public static readonly DependencyProperty DisabledContentStyleInApplicationMenuProperty;
        public static readonly DependencyProperty NormalDescriptionStyleInApplicationMenuProperty;
        public static readonly DependencyProperty HotDescriptionStyleInApplicationMenuProperty;
        public static readonly DependencyProperty PressedDescriptionStyleInApplicationMenuProperty;
        public static readonly DependencyProperty DisabledDescriptionStyleInApplicationMenuProperty;
        public static readonly DependencyProperty NormalContentStyleInStatusBarProperty;
        public static readonly DependencyProperty HotContentStyleInStatusBarProperty;
        public static readonly DependencyProperty PressedContentStyleInStatusBarProperty;
        public static readonly DependencyProperty DisabledContentStyleInStatusBarProperty;
        public static readonly DependencyProperty NormalContentStyleInRibbonPageGroupProperty;
        public static readonly DependencyProperty HotContentStyleInRibbonPageGroupProperty;
        public static readonly DependencyProperty PressedContentStyleInRibbonPageGroupProperty;
        public static readonly DependencyProperty DisabledContentStyleInRibbonPageGroupProperty;
        public static readonly DependencyProperty NormalContentStyleInButtonGroupProperty;
        public static readonly DependencyProperty HotContentStyleInButtonGroupProperty;
        public static readonly DependencyProperty PressedContentStyleInButtonGroupProperty;
        public static readonly DependencyProperty DisabledContentStyleInButtonGroupProperty;
        public static readonly DependencyProperty NormalContentStyleInQuickAccessToolbarProperty;
        public static readonly DependencyProperty HotContentStyleInQuickAccessToolbarProperty;
        public static readonly DependencyProperty PressedContentStyleInQuickAccessToolbarProperty;
        public static readonly DependencyProperty DisabledContentStyleInQuickAccessToolbarProperty;
        public static readonly DependencyProperty NormalContentStyleInQuickAccessToolbarFooterProperty;
        public static readonly DependencyProperty HotContentStyleInQuickAccessToolbarFooterProperty;
        public static readonly DependencyProperty PressedContentStyleInQuickAccessToolbarFooterProperty;
        public static readonly DependencyProperty DisabledContentStyleInQuickAccessToolbarFooterProperty;
        public static readonly DependencyProperty NormalContentStyleInRibbonPageHeaderProperty;
        public static readonly DependencyProperty HotContentStyleInRibbonPageHeaderProperty;
        public static readonly DependencyProperty PressedContentStyleInRibbonPageHeaderProperty;
        public static readonly DependencyProperty DisabledContentStyleInRibbonPageHeaderProperty;
        public static readonly DependencyProperty NormalContentStyleInRibbonStatusBarLeftProperty;
        public static readonly DependencyProperty HotContentStyleInRibbonStatusBarLeftProperty;
        public static readonly DependencyProperty PressedContentStyleInRibbonStatusBarLeftProperty;
        public static readonly DependencyProperty DisabledContentStyleInRibbonStatusBarLeftProperty;
        public static readonly DependencyProperty NormalContentStyleInRibbonStatusBarRightProperty;
        public static readonly DependencyProperty HotContentStyleInRibbonStatusBarRightProperty;
        public static readonly DependencyProperty PressedContentStyleInRibbonStatusBarRightProperty;
        public static readonly DependencyProperty DisabledContentStyleInRibbonStatusBarRightProperty;
        public static readonly DependencyProperty LayoutPanelStyleInBarProperty;
        public static readonly DependencyProperty LayoutPanelStyleInMainMenuProperty;
        public static readonly DependencyProperty LayoutPanelStyleInStatusBarProperty;
        public static readonly DependencyProperty LayoutPanelStyleInRibbonPageGroupProperty;
        public static readonly DependencyProperty LayoutPanelStyleInButtonGroupProperty;
        public static readonly DependencyProperty LayoutPanelStyleInQuickAccessToolbarProperty;
        public static readonly DependencyProperty LayoutPanelStyleInQuickAccessToolbarFooterProperty;
        public static readonly DependencyProperty LayoutPanelStyleInRibbonPageHeaderProperty;
        public static readonly DependencyProperty LayoutPanelStyleInRibbonStatusBarLeftProperty;
        public static readonly DependencyProperty LayoutPanelStyleInRibbonStatusBarRightProperty;
        public static readonly DependencyProperty DisableStateOpacityProperty;
        public static readonly DependencyProperty TemplateProperty;
        public static readonly DependencyProperty TemplateInMenuProperty;
        public static readonly DependencyProperty TemplateInMenuHorizontalProperty;
        public static readonly DependencyProperty TemplateInRibbonPageGroupProperty;
        public static readonly DependencyProperty TemplateInRibbonStatusBarLeftPartProperty;
        public static readonly DependencyProperty TemplateInRibbonStatusBarRightPartProperty;
        public static readonly DependencyProperty TemplateInStatusBarProperty;
        public static readonly DependencyProperty TemplateInRibbonQuickAccessToolbarProperty;
        public static readonly DependencyProperty TemplateInRibbonQuickAccessToolbarFooterProperty;
        public static readonly DependencyProperty TemplateInRibbonPageHeaderProperty;

        static BarItemLinkControlTemplateProvider();
        public static Style GetBorderStyleInApplicationMenu(DependencyObject target);
        public static Style GetBorderStyleInBar(DependencyObject target);
        public static Style GetBorderStyleInButtonGroup(DependencyObject target);
        public static Style GetBorderStyleInMainMenu(DependencyObject target);
        public static Style GetBorderStyleInMenu(DependencyObject target);
        public static Style GetBorderStyleInMenuHorizontal(DependencyObject target);
        public static Style GetBorderStyleInQuickAccessToolbar(DependencyObject target);
        public static Style GetBorderStyleInQuickAccessToolbarFooter(DependencyObject target);
        public static Style GetBorderStyleInRibbonPageGroup(DependencyObject target);
        public static Style GetBorderStyleInRibbonPageHeader(DependencyObject target);
        public static Style GetBorderStyleInRibbonStatusBarLeft(DependencyObject target);
        public static Style GetBorderStyleInRibbonStatusBarRight(DependencyObject target);
        public static Style GetBorderStyleInStatusBar(DependencyObject target);
        public static Style GetDisabledContentStyleInApplicationMenu(DependencyObject target);
        public static Style GetDisabledContentStyleInBar(DependencyObject target);
        public static Style GetDisabledContentStyleInButtonGroup(DependencyObject target);
        public static Style GetDisabledContentStyleInMainMenu(DependencyObject target);
        public static Style GetDisabledContentStyleInMenu(DependencyObject target);
        public static Style GetDisabledContentStyleInQuickAccessToolbar(DependencyObject target);
        public static Style GetDisabledContentStyleInQuickAccessToolbarFooter(DependencyObject target);
        public static Style GetDisabledContentStyleInRibbonPageGroup(DependencyObject target);
        public static Style GetDisabledContentStyleInRibbonPageHeader(DependencyObject target);
        public static Style GetDisabledContentStyleInRibbonStatusBarLeft(DependencyObject target);
        public static Style GetDisabledContentStyleInRibbonStatusBarRight(DependencyObject target);
        public static Style GetDisabledContentStyleInStatusBar(DependencyObject target);
        public static Style GetDisabledDescriptionStyleInApplicationMenu(DependencyObject target);
        public static double GetDisableStateOpacity(DependencyObject target);
        public static Style GetHotContentStyleInApplicationMenu(DependencyObject target);
        public static Style GetHotContentStyleInBar(DependencyObject target);
        public static Style GetHotContentStyleInButtonGroup(DependencyObject target);
        public static Style GetHotContentStyleInMainMenu(DependencyObject target);
        public static Style GetHotContentStyleInMenu(DependencyObject target);
        public static Style GetHotContentStyleInQuickAccessToolbar(DependencyObject target);
        public static Style GetHotContentStyleInQuickAccessToolbarFooter(DependencyObject target);
        public static Style GetHotContentStyleInRibbonPageGroup(DependencyObject target);
        public static Style GetHotContentStyleInRibbonPageHeader(DependencyObject target);
        public static Style GetHotContentStyleInRibbonStatusBarLeft(DependencyObject target);
        public static Style GetHotContentStyleInRibbonStatusBarRight(DependencyObject target);
        public static Style GetHotContentStyleInStatusBar(DependencyObject target);
        public static Style GetHotDescriptionStyleInApplicationMenu(DependencyObject target);
        public static Style GetLayoutPanelStyleInBar(DependencyObject target);
        public static Style GetLayoutPanelStyleInButtonGroup(DependencyObject target);
        public static Style GetLayoutPanelStyleInMainMenu(DependencyObject target);
        public static Style GetLayoutPanelStyleInQuickAccessToolbar(DependencyObject target);
        public static Style GetLayoutPanelStyleInQuickAccessToolbarFooter(DependencyObject target);
        public static Style GetLayoutPanelStyleInRibbonPageGroup(DependencyObject target);
        public static Style GetLayoutPanelStyleInRibbonPageHeader(DependencyObject target);
        public static Style GetLayoutPanelStyleInRibbonStatusBarLeft(DependencyObject target);
        public static Style GetLayoutPanelStyleInRibbonStatusBarRight(DependencyObject target);
        public static Style GetLayoutPanelStyleInStatusBar(DependencyObject target);
        public static Style GetNormalContentStyleInApplicationMenu(DependencyObject target);
        public static Style GetNormalContentStyleInBar(DependencyObject target);
        public static Style GetNormalContentStyleInButtonGroup(DependencyObject target);
        public static Style GetNormalContentStyleInMainMenu(DependencyObject target);
        public static Style GetNormalContentStyleInMenu(DependencyObject target);
        public static Style GetNormalContentStyleInQuickAccessToolbar(DependencyObject target);
        public static Style GetNormalContentStyleInQuickAccessToolbarFooter(DependencyObject target);
        public static Style GetNormalContentStyleInRibbonPageGroup(DependencyObject target);
        public static Style GetNormalContentStyleInRibbonPageHeader(DependencyObject target);
        public static Style GetNormalContentStyleInRibbonStatusBarLeft(DependencyObject target);
        public static Style GetNormalContentStyleInRibbonStatusBarRight(DependencyObject target);
        public static Style GetNormalContentStyleInStatusBar(DependencyObject target);
        public static Style GetNormalDescriptionStyleInApplicationMenu(DependencyObject target);
        public static Style GetPressedContentStyleInApplicationMenu(DependencyObject target);
        public static Style GetPressedContentStyleInBar(DependencyObject target);
        public static Style GetPressedContentStyleInButtonGroup(DependencyObject target);
        public static Style GetPressedContentStyleInMainMenu(DependencyObject target);
        public static Style GetPressedContentStyleInMenu(DependencyObject target);
        public static Style GetPressedContentStyleInQuickAccessToolbar(DependencyObject target);
        public static Style GetPressedContentStyleInQuickAccessToolbarFooter(DependencyObject target);
        public static Style GetPressedContentStyleInRibbonPageGroup(DependencyObject target);
        public static Style GetPressedContentStyleInRibbonPageHeader(DependencyObject target);
        public static Style GetPressedContentStyleInRibbonStatusBarLeft(DependencyObject target);
        public static Style GetPressedContentStyleInRibbonStatusBarRight(DependencyObject target);
        public static Style GetPressedContentStyleInStatusBar(DependencyObject target);
        public static Style GetPressedDescriptionStyleInApplicationMenu(DependencyObject target);
        public static ControlTemplate GetTemplate(DependencyObject target);
        public static ControlTemplate GetTemplateInMenu(DependencyObject target);
        public static ControlTemplate GetTemplateInMenuHorizontal(DependencyObject target);
        public static ControlTemplate GetTemplateInRibbonPageGroup(DependencyObject target);
        public static ControlTemplate GetTemplateInRibbonPageHeader(DependencyObject target);
        public static ControlTemplate GetTemplateInRibbonQuickAccessToolbar(DependencyObject target);
        public static ControlTemplate GetTemplateInRibbonQuickAccessToolbarFooter(DependencyObject target);
        public static ControlTemplate GetTemplateInRibbonStatusBarLeftPart(DependencyObject target);
        public static ControlTemplate GetTemplateInRibbonStatusBarRightPart(DependencyObject target);
        public static ControlTemplate GetTemplateInStatusBar(DependencyObject target);
        private static void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SetBorderStyleInApplicationMenu(DependencyObject target, Style value);
        public static void SetBorderStyleInBar(DependencyObject target, Style value);
        public static void SetBorderStyleInButtonGroup(DependencyObject target, Style value);
        public static void SetBorderStyleInMainMenu(DependencyObject target, Style value);
        public static void SetBorderStyleInMenu(DependencyObject target, Style value);
        public static void SetBorderStyleInMenuHorizontal(DependencyObject target, Style value);
        public static void SetBorderStyleInQuickAccessToolbar(DependencyObject target, Style value);
        public static void SetBorderStyleInQuickAccessToolbarFooter(DependencyObject target, Style value);
        public static void SetBorderStyleInRibbonPageGroup(DependencyObject target, Style value);
        public static void SetBorderStyleInRibbonPageHeader(DependencyObject target, Style value);
        public static void SetBorderStyleInRibbonStatusBarLeft(DependencyObject target, Style value);
        public static void SetBorderStyleInRibbonStatusBarRight(DependencyObject target, Style value);
        public static void SetBorderStyleInStatusBar(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInApplicationMenu(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInBar(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInButtonGroup(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInMainMenu(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInMenu(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInQuickAccessToolbar(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInQuickAccessToolbarFooter(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInRibbonPageGroup(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInRibbonPageHeader(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInRibbonStatusBarLeft(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInRibbonStatusBarRight(DependencyObject target, Style value);
        public static void SetDisabledContentStyleInStatusBar(DependencyObject target, Style value);
        public static void SetDisabledDescriptionStyleInApplicationMenu(DependencyObject target, Style value);
        public static void SetDisableStateOpacity(DependencyObject target, double value);
        public static void SetHotContentStyleInApplicationMenu(DependencyObject target, Style value);
        public static void SetHotContentStyleInBar(DependencyObject target, Style value);
        public static void SetHotContentStyleInButtonGroup(DependencyObject target, Style value);
        public static void SetHotContentStyleInMainMenu(DependencyObject target, Style value);
        public static void SetHotContentStyleInMenu(DependencyObject target, Style value);
        public static void SetHotContentStyleInQuickAccessToolbar(DependencyObject target, Style value);
        public static void SetHotContentStyleInQuickAccessToolbarFooter(DependencyObject target, Style value);
        public static void SetHotContentStyleInRibbonPageGroup(DependencyObject target, Style value);
        public static void SetHotContentStyleInRibbonPageHeader(DependencyObject target, Style value);
        public static void SetHotContentStyleInRibbonStatusBarLeft(DependencyObject target, Style value);
        public static void SetHotContentStyleInRibbonStatusBarRight(DependencyObject target, Style value);
        public static void SetHotContentStyleInStatusBar(DependencyObject target, Style value);
        public static void SetHotDescriptionStyleInApplicationMenu(DependencyObject target, Style value);
        public static void SetLayoutPanelStyleInBar(DependencyObject target, Style value);
        public static void SetLayoutPanelStyleInButtonGroup(DependencyObject target, Style value);
        public static void SetLayoutPanelStyleInMainMenu(DependencyObject target, Style value);
        public static void SetLayoutPanelStyleInQuickAccessToolbar(DependencyObject target, Style value);
        public static void SetLayoutPanelStyleInQuickAccessToolbarFooter(DependencyObject target, Style value);
        public static void SetLayoutPanelStyleInRibbonPageGroup(DependencyObject target, Style value);
        public static void SetLayoutPanelStyleInRibbonPageHeader(DependencyObject target, Style value);
        public static void SetLayoutPanelStyleInRibbonStatusBarLeft(DependencyObject target, Style value);
        public static void SetLayoutPanelStyleInRibbonStatusBarRight(DependencyObject target, Style value);
        public static void SetLayoutPanelStyleInStatusBar(DependencyObject target, Style value);
        public static void SetNormalContentStyleInApplicationMenu(DependencyObject target, Style value);
        public static void SetNormalContentStyleInBar(DependencyObject target, Style value);
        public static void SetNormalContentStyleInButtonGroup(DependencyObject target, Style value);
        public static void SetNormalContentStyleInMainMenu(DependencyObject target, Style value);
        public static void SetNormalContentStyleInMenu(DependencyObject target, Style value);
        public static void SetNormalContentStyleInQuickAccessToolbar(DependencyObject target, Style value);
        public static void SetNormalContentStyleInQuickAccessToolbarFooter(DependencyObject target, Style value);
        public static void SetNormalContentStyleInRibbonPageGroup(DependencyObject target, Style value);
        public static void SetNormalContentStyleInRibbonPageHeader(DependencyObject target, Style value);
        public static void SetNormalContentStyleInRibbonStatusBarLeft(DependencyObject target, Style value);
        public static void SetNormalContentStyleInRibbonStatusBarRight(DependencyObject target, Style value);
        public static void SetNormalContentStyleInStatusBar(DependencyObject target, Style value);
        public static void SetNormalDescriptionStyleInApplicationMenu(DependencyObject target, Style value);
        public static void SetPressedContentStyleInApplicationMenu(DependencyObject target, Style value);
        public static void SetPressedContentStyleInBar(DependencyObject target, Style value);
        public static void SetPressedContentStyleInButtonGroup(DependencyObject target, Style value);
        public static void SetPressedContentStyleInMainMenu(DependencyObject target, Style value);
        public static void SetPressedContentStyleInMenu(DependencyObject target, Style value);
        public static void SetPressedContentStyleInQuickAccessToolbar(DependencyObject target, Style value);
        public static void SetPressedContentStyleInQuickAccessToolbarFooter(DependencyObject target, Style value);
        public static void SetPressedContentStyleInRibbonPageGroup(DependencyObject target, Style value);
        public static void SetPressedContentStyleInRibbonPageHeader(DependencyObject target, Style value);
        public static void SetPressedContentStyleInRibbonStatusBarLeft(DependencyObject target, Style value);
        public static void SetPressedContentStyleInRibbonStatusBarRight(DependencyObject target, Style value);
        public static void SetPressedContentStyleInStatusBar(DependencyObject target, Style value);
        public static void SetPressedDescriptionStyleInApplicationMenu(DependencyObject target, Style value);
        public static void SetTemplate(DependencyObject target, ControlTemplate value);
        public static void SetTemplateInMenu(DependencyObject target, ControlTemplate value);
        public static void SetTemplateInMenuHorizontal(DependencyObject target, ControlTemplate value);
        public static void SetTemplateInRibbonPageGroup(DependencyObject target, ControlTemplate value);
        public static void SetTemplateInRibbonPageHeader(DependencyObject target, ControlTemplate value);
        public static void SetTemplateInRibbonQuickAccessToolbar(DependencyObject target, ControlTemplate value);
        public static void SetTemplateInRibbonQuickAccessToolbarFooter(DependencyObject target, ControlTemplate value);
        public static void SetTemplateInRibbonStatusBarLeftPart(DependencyObject target, ControlTemplate value);
        public static void SetTemplateInRibbonStatusBarRightPart(DependencyObject target, ControlTemplate value);
        public static void SetTemplateInStatusBar(DependencyObject target, ControlTemplate value);
    }
}

