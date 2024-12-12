namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Ribbon;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public static class BarManagerHelper
    {
        public static readonly DependencyProperty PopupProperty;
        public static readonly DependencyProperty LinksHolderProperty;
        internal static readonly DependencyPropertyKey PopupPropertyKey;
        public static readonly DependencyProperty RemovableProperty;
        public static readonly DependencyProperty ShowPopupWithDelayProperty;
        public static readonly DependencyProperty PopupExpandModeProperty;
        public static readonly DependencyProperty MDIChildHostProperty;

        static BarManagerHelper();
        public static void AddLogicalChild(BarManager manager, UIElement child);
        public static void CheckCloseAllPopups(BarManager manager, MouseEventArgs e);
        public static bool CompareItemsCreatedFromItemsSource(BarItem first, BarItem second);
        public static BarControl FindBarControl(Bar bar);
        public static BarControl FindBarControl(DependencyObject d);
        public static BarManager FindBarManager(DependencyObject d);
        [Obsolete("Use the GetOrFindBarManager instead")]
        public static BarManagerInfo FindBarManagerInfo(DependencyObject d);
        public static BarContainerControl FindContainerControl(DependencyObject d);
        public static object GetChildRibbonControl(BarManager manager);
        public static object GetChildRibbonStatusBar(BarManager manager);
        public static ImageSource GetDefaultBarItemGlyph(GlyphSize glyphSize);
        public static bool GetIsPrivate(Bar bar);
        public static FrameworkElement GetItemLinkControlArrow(BarItemLinkControl control);
        public static Image GetItemLinkControlGlyph(BarItemLinkControl control);
        public static FrameworkElement GetItemLinkControlGlyphBorder(BarItemLinkControl control);
        public static Popup GetItemLinkControlPopup(IBarItemLinkControl control);
        internal static BarItemLinkBase GetLinkByItemName(string itemName, BarItemLinkCollection links);
        internal static Orientation GetLinkContainerOrientation(IBarItemLinkControl linkControl);
        public static ILinksHolder GetLinksHolder(DependencyObject obj);
        public static IMDIChildHost GetMDIChildHost(DependencyObject element);
        public static BarManager GetOrFindBarManager(DependencyObject d);
        [Obsolete("Use the GetOrFindBarManager instead")]
        public static BarManagerInfo GetOrFindBarManagerInfo(DependencyObject d);
        private static DependencyObject GetParent(DependencyObject d);
        public static BarPopupBase GetPopup(DependencyObject element);
        public static BarPopupExpandMode GetPopupExpandMode(DependencyObject element);
        internal static PlacementMode GetPopupPlacement(IBarItemLinkControl linkControl);
        public static IEnumerable<BarItem> GetPrivateItems(BarManager manager);
        public static bool GetRemovable(DependencyObject obj);
        public static IRibbonControl GetRibbonControl(BarManager manager);
        public static IRibbonStatusBarControl GetRibbonStatusBar(BarManager manager);
        public static bool GetShowPopupWithDelay(DependencyObject obj);
        public static string GetThemeName(DependencyObject obj);
        public static void HideFloatingBars(DependencyObject treeNode);
        public static void HideFloatingBars(DependencyObject treeNode, bool includeChildren);
        public static void HideFloatingBars(DependencyObject treeNode, bool includeChildren, bool hideForm);
        public static void InitializeItemLinks(BarManager manager, BarItemLinkCollection links);
        public static bool IsMergedChild(BarManager childManager);
        public static bool IsMergedParent(BarManager parentManager);
        public static bool Merge(BarManager parentManager, BarManager childManager, ILinksHolder extraItems);
        private static void OnPopupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void RemoveLogicalChild(BarManager manager, UIElement child);
        public static void SetBarIsPrivate(Bar bar, bool isPrivate);
        public static void SetChildRibbonControl(BarManager manager, object ribbonControl);
        public static void SetChildRibbonStatusBar(BarManager manager, object ribbonStatusBar);
        public static void SetLinksHolder(DependencyObject obj, ILinksHolder value);
        public static void SetMDIChildHost(DependencyObject element, IMDIChildHost value);
        internal static void SetPopup(DependencyObject element, BarPopupBase value);
        public static void SetPopupExpandMode(DependencyObject element, BarPopupExpandMode value);
        public static void SetRemovable(DependencyObject obj, bool value);
        public static void SetShowPopupWithDelay(DependencyObject obj, bool value);
        public static void ShowFloatingBars(DependencyObject treeNode);
        public static void ShowFloatingBars(DependencyObject treeNode, bool includeChildren);
        public static void UnMerge(BarManager parentManager, BarManager childManager, ILinksHolder extraItems);
        public static void UpdateSeparatorsVisibility(ILinksHolder links, bool force = false);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarManagerHelper.<>c <>9;
            public static Func<DependencyObject, BarManager> <>9__37_1;
            public static Func<IPopupControl, BarPopupBase> <>9__40_0;
            public static Action<Bar> <>9__51_0;
            public static Action<Bar> <>9__53_0;
            public static Func<BarItem, bool> <>9__61_0;

            static <>c();
            internal BarPopupBase <GetItemLinkControlPopup>b__40_0(IPopupControl x);
            internal BarManager <GetOrFindBarManager>b__37_1(DependencyObject x);
            internal bool <GetPrivateItems>b__61_0(BarItem item);
            internal void <HideFloatingBars>b__53_0(Bar x);
            internal void <ShowFloatingBars>b__51_0(Bar x);
        }
    }
}

